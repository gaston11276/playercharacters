using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using JetBrains.Annotations;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using NFive.SDK.Core.Models.Player;
using NFive.SDK.Core.Extensions;
using NFive.SDK.Core.Diagnostics;
using NFive.SDK.Core.Input;
using NFive.SDK.Client.Commands;
using NFive.SDK.Client.Communications;
using NFive.SDK.Client.Events;
using NFive.SDK.Client.Interface;
using NFive.SDK.Client.Services;
using NFive.SDK.Client.Input;
using NFive.SDK.Client.Extensions;
using Gaston11276.Playercharacters.Client.Overlays;
using Gaston11276.Playercharacters.Shared;
using Gaston11276.Playercharacters.Client.Models;

namespace Gaston11276.Playercharacters.Client
{
	[PublicAPI]
	public class PlayercharactersService : Service
	{
		private Configuration config;
		private PlayercharactersOverlay overlay;

		List<Character> characters = new List<Character>();
		private Character activeCharacter;

		HudCharacters hudCharacters = new HudCharacters();
		HudAppearance hudAppearance = new HudAppearance();
		HudSpawnLocation hudSpawnLocation = new HudSpawnLocation();
		
		private Hotkey openCreator;
		private Hotkey openLooks;
		private Hotkey openSpawn;

		private bool holdFocus = true;

		private Hotkey LMB;
		private Hotkey RMB;
		private int lastCursorX;
		private int lastCursorY;

		public PlayercharactersService(ILogger logger, ITickManager ticks, ICommunicationManager comms, ICommandManager commands, IOverlayManager overlay, User user) : base(logger, ticks, comms, commands, overlay, user)
		{
			
		}

		public override async Task Started()
		{
			// After a resolution change hud must be updated and rebuilt.
			OnResolutionChanged(); // Should be called if resolution is changed.
			

			hudCharacters.SetLogger(this.Logger);
			hudCharacters.RegisterLoadCharactersCallback(LoadCharacters);
			hudCharacters.RegisterOnCreateCallback(OnCreate);
			hudCharacters.RegisterOnDeleteCallback(OnDelete);
			hudCharacters.RegisterOnCreatorOpenCallback(OnCreatorOpen);
			hudCharacters.RegisterOnCreatorCloseCallback(OnCreatorClose);
			hudCharacters.CreateUi();

			hudAppearance.SetLogger(this.Logger);
			hudAppearance.SetDelay(Delay);
			hudAppearance.RegisterSaveCallback(SaveCharacter);
			hudAppearance.RegisterRevertCallback(RevertCharacter);
			hudAppearance.RegisterOnOpenCallback(OnLooksOpen);
			hudAppearance.RegisterOnCloseCallback(OnLooksClose);
			hudAppearance.CreateUi();

			hudSpawnLocation.SetLogger(this.Logger);
			hudSpawnLocation.RegisterOnOpenCallback(OnSpawnLocationOpen);
			hudSpawnLocation.RegisterOnCloseCallback(OnSpawnLocationClose);
			hudSpawnLocation.CreateUi();

			this.config = await this.Comms.Event(PlayercharactersEvents.Configuration).ToServer().Request<Configuration>();
			this.Comms.Event(PlayercharactersEvents.Configuration).FromServer().On<Configuration>((e, c) => this.config = c);


			this.overlay = new PlayercharactersOverlay(this.OverlayManager);
			this.openCreator = new Hotkey(this.config.SelectionScreen.HotkeyCreator);
			this.openLooks = new Hotkey(this.config.SelectionScreen.HotkeyLooks);
			this.openSpawn = new Hotkey(this.config.SelectionScreen.HotkeySpawnLocation);
			this.LMB = new Hotkey(InputControl.CursorAccept);
			this.RMB = new Hotkey(InputControl.CursorCancel);
			
			hudCharacters.SetHotkey((int)HudInput.ConvertKeycode(openCreator.UserKeyboardKey));
			hudAppearance.SetHotkey((int)HudInput.ConvertKeycode(openLooks.UserKeyboardKey));
			hudSpawnLocation.SetHotkey((int)HudInput.ConvertKeycode(openSpawn.UserKeyboardKey));

			this.Ticks.On(OnInput);
			this.Ticks.On(OnDraw);

			this.overlay = new PlayercharactersOverlay(OverlayManager);
			this.overlay.OnKey += OnNuiKey;
			this.overlay.OnMouseButton += OnNuiMouseButton;
			this.overlay.OnMouseMove += OnNuiMouseMove;

			hudCharacters.Open();
			while (hudCharacters.IsOpen())
			{
				await Delay(20);
			}

			while (!Screen.Fading.IsFadedOut) await Delay(20);
			API.ShutdownLoadingScreen();
		}

		void OnResolutionChanged()
		{
			int screenWidth = new int();
			int screenHeight = new int();
			API.GetActiveScreenResolution(ref screenWidth, ref screenHeight);
			hudCharacters.SetResolution(screenWidth, screenHeight);
			hudCharacters.Refresh();
			hudAppearance.SetResolution(screenWidth, screenHeight);
			hudAppearance.Refresh();
			hudSpawnLocation.SetResolution(screenWidth, screenHeight);
			hudSpawnLocation.Refresh();
		}

		void OnCreatorOpen()
		{
			this.Ticks.Off(OnSaveCharacter);
			this.Ticks.Off(OnSavePosition);
			LoadCharacters();
			OpenNui();
		}

		async void OnCreatorClose(System.Guid characterId)
		{
			CloseNui();

			if (activeCharacter == null || activeCharacter.Id != characterId)
			{
				Screen.Fading.FadeOut(200);
				await SetNewCharacter(characterId);
			}

			this.Ticks.On(OnSaveCharacter);
			this.Ticks.On(OnSavePosition);
			Screen.Fading.FadeIn(200);
		}

		void OnLooksOpen()
		{
			this.Ticks.Off(OnSaveCharacter);
			this.Ticks.Off(OnSavePosition);				
			OpenNui();
		}

		void OnLooksClose()
		{
			CloseNui();
			this.Ticks.On(OnSaveCharacter);
			this.Ticks.On(OnSavePosition);
		}

		async void OnSpawnLocationOpen()
		{
			await hudSpawnLocation.RefreshUi();
			this.Ticks.Off(OnSaveCharacter);
			this.Ticks.Off(OnSavePosition);	
		}

		void OnSpawnLocationClose()
		{
			this.Ticks.On(OnSaveCharacter);
			this.Ticks.On(OnSavePosition);
		}

		async void OpenNui()
		{
			this.overlay.Show(true, true);
			holdFocus = true;
			await HoldNui();
		}

		public async Task HoldNui()
		{
			while (holdFocus)// || creator.IsOpen())
			{
				await Delay(20);
			}
			API.SetNuiFocus(false, false);
		}

		private void CloseNui()
		{
			holdFocus = false;
			this.overlay.Hide(false);
		}

		private async void LoadCharacters()
		{
			await RequestCharacters();
		}

		private async Task RequestCharacters()
		{
			characters = await this.Comms.Event(PlayercharactersEvents.GetCharactersForUser).ToServer().Request<List<Character>>();
			hudCharacters.SetCharacters(characters);
			hudCharacters.UpdateCharacterList();
		}

		private async Task SetNewCharacter(System.Guid selectedCharacterId)
		{
			Character selectedCharacter = characters.First(c => c.Id == selectedCharacterId);
			await this.Comms.Event(PlayercharactersEvents.Select).ToServer().Request<CharacterSession>(selectedCharacter.Id);
			await SwitchCharacter(selectedCharacter);
			hudAppearance.SetCharacter(activeCharacter);
			hudAppearance.ApplyToPed();
			await hudAppearance.RefreshUi();
		}

		private async Task SwitchCharacter(Character character)
		{
			Game.Player.Character.IsPositionFrozen = false;
			API.SwitchInPlayer(API.PlayerPedId());

			while (!await Game.Player.ChangeModel(new Model(character.ModelHash)))
			{
				await Delay(10);
			}

			Game.Player.Character.Position = character.Position.ToVector3().ToCitVector3();
			Game.Player.Character.Armor = character.Armor;
			this.activeCharacter = character;
		}

		private async void OnCreate()
		{
			Character character = new Character();
			hudCharacters.GetCharacterInfo(ref character);
			hudCharacters.ClearCreatorEdit();

			if (string.IsNullOrWhiteSpace(character.Middlename)) character.Middlename = null;

			var character2 = await this.Comms.Event(PlayercharactersEvents.CreateCharacter).ToServer().Request<Character>(character);
			await RequestCharacters();
		}

		private void OnDisconnect(object sender, OverlayEventArgs e)
		{
			//this.Comms.Event(SessionEvents.DisconnectPlayer).ToServer().Emit("Am I Disconnected?! ...Hello?");
		}

		private async void OnDelete(System.Guid selectedCharacterId)
		{
			characters = await this.Comms.Event(PlayercharactersEvents.DeleteCharacter).ToServer().Request<List<Character>>(selectedCharacterId);
			hudCharacters.SetCharacters(characters);
		}

		public async Task OnSaveCharacter()
		{
			SaveCharacter();
			await Delay(this.config.Autosave.CharacterInterval);
		}

		public async Task OnSavePosition()
		{
			SavePosition();
			await Delay(this.config.Autosave.PositionInterval);
		}

		private void SaveCharacter()
		{
			this.activeCharacter.Position = Game.Player.Character.Position.ToVector3().ToPosition();
			this.Comms.Event(PlayercharactersEvents.SaveCharacter).ToServer().Emit(this.activeCharacter);
		}

		private async void RevertCharacter()
		{
			await RequestCharacter();
			hudAppearance.SetCharacter(activeCharacter);
			hudAppearance.ApplyToPed();
		}

		private async Task RequestCharacter()
		{
			activeCharacter = await this.Comms.Event(PlayercharactersEvents.GetCharacterForUser).ToServer().Request<Character>(activeCharacter);
		}

		private void SavePosition()
		{
			this.Comms.Event(PlayercharactersEvents.SavePosition).ToServer().Emit(this.activeCharacter.Id, Game.Player.Character.Position.ToVector3().ToPosition());
		}

		public void OnDraw()
		{
			hudCharacters.Draw();
			hudAppearance.Draw();
			hudSpawnLocation.Draw();
		}

		private void OnInput()
		{
			// Non Nui Input

			if (openCreator.IsJustReleased()) // default F1
			{
				hudCharacters.Open();
			}

			if (openLooks.IsJustReleased()) // default F2
			{
				hudAppearance.Open();
			}

			if (openSpawn.IsJustReleased()) // default F5
			{
				hudSpawnLocation.Toggle();
			}

			if (hudSpawnLocation.IsOpen())
			{
				int cursorX = new int();
				int cursorY = new int();
				API.GetNuiCursorPosition(ref cursorX, ref cursorY);


				bool mouseHasMoved = false;
				if (lastCursorX != cursorX || lastCursorY != cursorY)
				{
					mouseHasMoved = true;
				}

				int leftMouseButtonState = 0;
				if (this.LMB.IsJustPressed())
				{
					leftMouseButtonState = 1;

				}
				if (this.LMB.IsJustReleased())
				{
					leftMouseButtonState = 3;
				}

				int rightMouseButtonState = 0;
				if (this.RMB.IsJustPressed())
				{
					rightMouseButtonState = 1;

				}
				else if (this.RMB.IsPressed())
				{
					rightMouseButtonState = 2;

				}
				else if (this.RMB.IsJustReleased())
				{
					rightMouseButtonState = 3;
				}

				if (rightMouseButtonState == 0)
				{
					API.SetMouseCursorActiveThisFrame();
				}

				if (hudSpawnLocation.IsOpen())
				{					
					if (leftMouseButtonState == 1 || leftMouseButtonState == 3)
					{
						hudSpawnLocation.OnMouseButton(leftMouseButtonState, 0, cursorX, cursorY);
					}
					if (rightMouseButtonState == 1 || rightMouseButtonState == 3)
					{
						hudSpawnLocation.OnMouseButton(rightMouseButtonState, 2, cursorX, cursorY);
					}
					if (mouseHasMoved)
					{
						hudSpawnLocation.OnMouseMove(cursorX, cursorY);
					}
				}			
				lastCursorX = cursorX;
				lastCursorY = cursorY;
			}
		}

		private void OnNuiKey(object sender, OnKeyOverlayEventArgs args)
		{
			hudCharacters.OnInputKey(args.state, args.keycode);
			hudAppearance.OnInputKey(args.state, args.keycode);
			hudSpawnLocation.OnInputKey(args.state, args.keycode);
		}
		private void OnNuiMouseButton(object sender, OnMouseButtonOverlayEventArgs args)
		{
			int x = 0; int y = 0;
			API.GetNuiCursorPosition(ref x, ref y);

			if (hudCharacters.IsOpen())
			{
				hudCharacters.OnMouseButton(args.state, args.button, x, y);
			}
			if (hudAppearance.IsOpen())
			{
				hudAppearance.OnMouseButton(args.state, args.button, x, y);
			}
			if (hudSpawnLocation.IsOpen())
			{
				hudSpawnLocation.OnMouseButton(args.state, args.button, x, y);
			}
		}

		private void OnNuiMouseMove(object sender, OverlayEventArgs args)
		{
			int x = 0; int y = 0;
			API.GetNuiCursorPosition(ref x, ref y);
			if (hudCharacters.IsOpen())
			{
				hudCharacters.OnMouseMove(x, y);
			}

			if (hudAppearance.IsOpen())
			{
				hudAppearance.OnMouseMove(x, y);
			}

			if (hudSpawnLocation.IsOpen())
			{
				hudSpawnLocation.OnMouseMove(x, y);
			}
		}
		
	}
}
