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
using Gaston11276.SimpleUi;

using Font = CitizenFX.Core.UI.Font;

namespace Gaston11276.Playercharacters.Client
{


	[PublicAPI]
	public class PlayercharactersService : Service
	{
		private Configuration config;
		private PlayercharactersOverlay overlay;

		List<Character> characters;
		private Character activeCharacter;
		private CharacterSession session;

		Creator creator;
		Looks looks;

		private Hotkey toggleCreator;
		private Hotkey toggleLooks;
		private Hotkey LMB;
		private Hotkey RMB;

		private int lastCursorX;
		private int lastCursorY;

		bool holdFocus = true;


		//DEBUG
		int currentControl = 0;
		Hotkey nextControl = new Hotkey(InputControl.SelectNextWeapon);
		Hotkey prevControl = new Hotkey(InputControl.SelectPrevWeapon);
		UiPanel debugPanel = new UiPanel();
		Textbox debug01 = new Textbox();
		Textbox debug02 = new Textbox();
		Textbox debug03 = new Textbox();
		Textbox debug04 = new Textbox();
		Textbox debug05 = new Textbox();
		Textbox debug06 = new Textbox();
		Textbox debug07 = new Textbox();
		Textbox debug08 = new Textbox();
		Textbox debug09 = new Textbox();
		Textbox debug10 = new Textbox();

		public PlayercharactersService(ILogger logger, ITickManager ticks, ICommunicationManager comms, ICommandManager commands, IOverlayManager overlay, User user) : base(logger, ticks, comms, commands, overlay, user)
		{
			characters = new List<Character>();

			creator = new Creator();
			creator.SetLogger(this.Logger);
			creator.CreateUi();
			creator.RegisterLoadCharactersCallback(LoadCharacters);
			creator.RegisterOnCreateCallback(OnCreate);
			creator.RegisterOnSelectCallback(OnSelect);
			creator.RegisterOnDeleteCallback(OnDelete);
			creator.RegisterOnPlayCallback(OnPlay);
			creator.Build();

			looks = new Looks();
			looks.SetLogger(this.Logger);
			looks.RegisterSaveCallback(SaveCharacter);
			looks.RegisterRevertCallback(RevertCharacter);
			looks.CreateUi();
			looks.Build();

			//int screenWidth = new int();
			//int screenHeight = new int();
			//API.GetScreenResolution(ref screenWidth, ref screenHeight);
			//creator.SetResolution(screenWidth, screenHeight);
			//looks.SetResolution(screenWidth, screenHeight);
		}

		public override async Task Started()
		{
			// Request server configuration
			this.config = await this.Comms.Event(PlayercharactersEvents.Configuration).ToServer().Request<Configuration>();

			// Update local configuration on server configuration change
			this.Comms.Event(PlayercharactersEvents.Configuration).FromServer().On<Configuration>((e, c) => this.config = c);

			// Create overlay
			this.overlay = new PlayercharactersOverlay(this.OverlayManager);

			this.toggleCreator = new Hotkey(this.config.SelectionScreen.HotkeyCreator);
			this.toggleLooks = new Hotkey(this.config.SelectionScreen.HotkeyLooks);
			this.LMB = new Hotkey(InputControl.CursorAccept);
			this.RMB = new Hotkey(InputControl.CursorCancel);
			
			debug01.SetText("Debug1");
			debug02.SetText("Debug2");
			debug03.SetText("Debug3");
			debug04.SetText("Debug4");
			debug05.SetText("Debug5");
			debug06.SetText("Debug6");
			debug07.SetText("Debug7");
			debug08.SetText("Debug8");
			debug09.SetText("Debug9");
			debug10.SetText("Debug10");

			currentControl = 0;

			debugPanel.SetProperties(UiElement.MOVABLE);
			debugPanel.SetHDimension(Dimension.Absolute);
			debugPanel.SetVDimension(Dimension.Absolute);
			debugPanel.SetPosition(0.5f, 0.5f);
			debugPanel.SetWidth(0.3f);
			debugPanel.SetHeight(0.3f);
			//debugPanel.SetAlignment(HAlignment.Left);
			debugPanel.SetOrientation(Orientation.Vertical);
			debugPanel.AddElement(debug01);
			debugPanel.AddElement(debug02);
			debugPanel.AddElement(debug03);
			debugPanel.AddElement(debug04);
			debugPanel.AddElement(debug05);
			debugPanel.AddElement(debug06);
			debugPanel.AddElement(debug07);
			debugPanel.AddElement(debug08);
			debugPanel.AddElement(debug09);
			debugPanel.AddElement(debug10);
			debugPanel.Build();

			this.Ticks.On(OnToggleCreator);
			this.Ticks.On(OnToggleLooks);
			this.Ticks.On(OnInput);
			this.Ticks.On(OnDraw);


		}

		public override async Task HoldFocus()
		{
			Screen.Fading.FadeOut(100);
			while (Screen.Fading.IsFadingOut) await Delay(10);

			OpenCreator();
			Screen.Fading.FadeIn(1);

			while (holdFocus || creator.IsOpen())
			{
				await Delay(20);
			}
		}

		async void OpenCreator()
		{
			this.Ticks.Off(OnSaveCharacter);
			this.Ticks.Off(OnSavePosition);

			Screen.Hud.IsVisible = false;

			// Disable the loading screen from automatically being dismissed
			//Logger.Debug($"Setting manual shutdown of loading screen on");
			//API.SetManualShutdownLoadingScreenNui(true);

			// Position character, required for switching
			//Logger.Debug($"Positioning character");
			Game.Player.Character.Position = Vector3.Zero; // Gives a position for switching to zoom out from

			// Freeze
			//Logger.Debug($"Freezing player");
			//Game.Player.Freeze();

			// Switch out the player if it isn't already in a switch state
			//Logger.Debug($"Switch out player if not switched out already");
			//if (!API.IsPlayerSwitchInProgress()) API.SwitchOutPlayer(API.PlayerPedId(), 0, 1);
			if (!API.IsPlayerSwitchInProgress()) API.SwitchOutPlayer(API.PlayerPedId(), 1, 2);

			// Remove most clouds
			//API.SetCloudHatOpacity(0.01f);

			// Wait for switch
			//Logger.Debug($"Waiting for switch");
			while (API.GetPlayerSwitchState() != 5) await Delay(10);

			// Hide loading screen
			//Logger.Debug($"Hiding loading screen");
			API.ShutdownLoadingScreen(); // Otherwise stuck at mountain view

			// Fade out
			//Logger.Debug($"Fading Out");
			//Screen.Fading.FadeOut(0);
			//while (Screen.Fading.IsFadingOut) await Delay(10);
			//Logger.Debug($"Fading out finished");

			// Shut down the NUI loading screen
			//Logger.Debug($"Shutting down NUI loading screen");
			//API.ShutdownLoadingScreenNui();

			//Logger.Debug($"Opening creator");
			creator.Open();
			holdFocus = false;

			LoadCharacters();


			// Fade in
			//Logger.Debug($"Fading in");
			//Screen.Fading.FadeIn(500);
			//while (Screen.Fading.IsFadingIn) await Delay(10);
			//Logger.Debug($"Fading in finished");

		}

		async void CloseCreator()
		{
			creator.Close();
			await Delay(1);
		}

		private async void LoadCharacters()
		{
			await AsyncLoadCharacters();
		}

		private async Task AsyncLoadCharacters()
		{
			characters = await this.Comms.Event(PlayercharactersEvents.GetCharactersForUser).ToServer().Request<List<Character>>();
			creator.SetCharacters(characters);
			creator.UpdateCharacterList();
		}

		private async void OnPlay(System.Guid selectedCharacterId)
		{
			Character selectedCharacter = characters.First(c => c.Id == selectedCharacterId);
			this.session = await this.Comms.Event(PlayercharactersEvents.Select).ToServer().Request<CharacterSession>(selectedCharacter.Id);
			await Play(selectedCharacter);
		}


		private async void OnCreate()
		{
			Character character = new Character();
			creator.GetCharacterInfo(ref character);
			creator.ClearCreatorEdit();

			if (string.IsNullOrWhiteSpace(character.Middlename)) character.Middlename = null;

			// Send new character
			var character2 = this.Comms.Event(PlayercharactersEvents.CreateCharacter).ToServer().Request<Character>(character);

			LoadCharacters();

			await Delay(1);
		}

		private void OnDisconnect(object sender, OverlayEventArgs e)
		{
			this.Comms.Event(SessionEvents.DisconnectPlayer).ToServer().Emit("Thanks for playing");
		}

		private async void OnSelect(System.Guid selectedCharacterId)
		{
			//Character selectedCharacter = characters.First(c => c.Id == selectedCharacterId);
			//this.session = await this.Comms.Event(PlayercharactersEvents.Select).ToServer().Request<CharacterSession>(selectedCharacter.Id);
			//await Play(selectedCharacter);
		}

		private async void OnDelete(System.Guid selectedCharacterId)
		{
			this.Logger.Debug($"Delete {selectedCharacterId}");
			characters = await this.Comms.Event(PlayercharactersEvents.DeleteCharacter).ToServer().Request<List<Character>>(selectedCharacterId);
			creator.SetCharacters(characters);
		}

		private async Task Play(Character character)
		{
			Game.Player.Unfreeze();
			Screen.Hud.IsVisible = true;
			API.SwitchInPlayer(API.PlayerPedId());

			while (!await Game.Player.ChangeModel(new Model(character.ModelHash)))
			{
				await Delay(10);
			}

			//API.SetPedDefaultComponentVariation(Game.PlayerPed.Handle);







			Game.Player.Character.Position = character.Position.ToVector3().ToCitVector3();
			Game.Player.Character.Armor = character.Armor;

			//API.RequestClipSet(character.AnimationSet);
			//await BaseScript.Delay(100); // Required to load
			//Game.Player.Character.MovementAnimationSet = character.AnimationSet;


			CloseCreator();

			this.activeCharacter = character;
			// Set player health (Rare #OnSpawnDeath Fix)
			//this.activeCharacter.Health = character.Health;



			looks.SetCharacter(activeCharacter);
			looks.ApplyToPed();

			this.Ticks.On(OnSaveCharacter);
			this.Ticks.On(OnSavePosition);
		}

		void OpenLooks()
		{
			this.Ticks.Off(OnSaveCharacter);
			this.Ticks.Off(OnSavePosition);
			Screen.Hud.IsVisible = false;
			API.DisplayHud(false);

			looks.SetCharacter(activeCharacter);
			looks.Open();
		}

		void CloseLooks()
		{
			looks.Close();

			API.DisplayHud(true);
			Screen.Hud.IsVisible = true;
			this.Ticks.On(OnSaveCharacter);
			this.Ticks.On(OnSavePosition);
		}


		public async Task OnToggleCreator()
		{
			if (toggleCreator.IsJustPressed())
			{
				if (!creator.IsOpen())
				{
					OpenCreator();
				}
				else
				{
					CloseCreator();
				}
			}
			await Delay(1);
		}

		public async Task OnToggleLooks()
		{
			if (toggleLooks.IsJustPressed())
			{
				if (!creator.IsOpen())
				{
					if (!looks.IsOpen())
					{
						OpenLooks();
					}
					else
					{
						CloseLooks();
					}
				}
			}
			await Delay(1);
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
			//if (!this.isPlaying) return;
			this.activeCharacter.Position = Game.Player.Character.Position.ToVector3().ToPosition();
			this.Comms.Event(PlayercharactersEvents.SaveCharacter).ToServer().Emit(this.activeCharacter);
		}

		private void RevertCharacter()
		{
			System.Guid characterId = activeCharacter.Id;
			LoadCharacters();
			Character selectedCharacter = characters.First(c => c.Id == characterId);
			activeCharacter = selectedCharacter;
			looks.SetCharacter(activeCharacter);
			looks.ApplyToPed();
		}

		private void SavePosition()
		{
			//if (!this.isPlaying) return;

			this.Comms.Event(PlayercharactersEvents.SavePosition).ToServer().Emit(this.activeCharacter.Id, Game.Player.Character.Position.ToVector3().ToPosition());
		}



		private void OnInput()
		{
			if (creator.IsOpen() || looks.IsOpen())
			{

				Hotkey bajs = new Hotkey(InputControl.Attack);
				if (bajs.IsJustReleased())
				{
					Logger.Debug("Rebuilding looks");
					looks.Build();
				}


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

				//int AxisX = Game.GetControlValue(0, CitizenFX.Core.Control.FrontendAxisX);
				//int AxisY = Game.GetControlValue(0, CitizenFX.Core.Control.FrontendAxisX);
				//float AxisX = Game.GetControlNormal(0, Control.ScriptRightAxisX);
				//float AxisY = Game.GetControlNormal(0, Control.ScriptRightAxisY);

				float AxisX = ((float)(lastCursorX - cursorX)) / 1280f;
				float AxisY = ((float)(lastCursorY - cursorY)) / 1280f;

				if (creator.IsOpen())
				{
					creator.OnInputKey();
					if (leftMouseButtonState == 1 || leftMouseButtonState == 3)
					{
						creator.OnLMB(leftMouseButtonState, cursorX, cursorY);
					}
					if (mouseHasMoved)
					{
						creator.OnInputMouseMove(cursorX, cursorY);
					}
				}

				if (looks.IsOpen())
				{
					looks.OnInputKey();
					looks.OnInputRMBMouseMoveAxis(rightMouseButtonState, AxisX, AxisY);
					if (leftMouseButtonState == 1 || leftMouseButtonState == 3)
					{
						looks.OnLMB(leftMouseButtonState, cursorX, cursorY);
					}
					//if (rightMouseButtonState != 0)
					//{
					//looks.OnRMB(rightMouseButtonState, AxisX, AxisY);
					//}
					if (mouseHasMoved)
					{
						looks.OnInputMouseMove(cursorX, cursorY);
					}
				}
				lastCursorX = cursorX;
				lastCursorY = cursorY;
			}

			/*
			if (nextControl.IsJustReleased())
			{
				currentControl++;
				PrintControl();
			}
			if (prevControl.IsJustReleased())
			{
				currentControl--;
				PrintControl();
			}
			*/
		}

		public void OnDraw()
		{
			creator.Draw();
			looks.Draw();
			//debugPanel.Draw();
		}

		void PrintControl()
		{
			//for (int i = 0; i < (int)InputControl.VehicleFlyTransform; i++)
			{

				Hotkey hotkey = new Hotkey((InputControl)currentControl);
				
				/*
				Logger.Debug("------------------");
				Logger.Debug($"Display name: {hotkey.ControlDisplayName}");
				Logger.Debug($"Native name: {hotkey.ControlNativeName}");
				Logger.Debug($"Key: {hotkey.DefaultKeyboardKeyDisplayName}");
				Logger.Debug($"Keycode: {hotkey.DefaultKeyboardKey}");
				Logger.Debug($"User key: {hotkey.UserKeyboardKeyDisplayName}");
				Logger.Debug($"User keykode: {hotkey.UserKeyboardKey}");
				*/

				debug01.SetText($"Display name: {hotkey.ControlDisplayName}");
				debug02.SetText($"Native name: {hotkey.ControlNativeName}");
				debug03.SetText($"Key: {hotkey.DefaultKeyboardKeyDisplayName}");
				debug04.SetText($"Keycode: {hotkey.DefaultKeyboardKey}");
				debug05.SetText($"User key: {hotkey.UserKeyboardKeyDisplayName}");
				debug06.SetText($"User keykode: {hotkey.UserKeyboardKey}");
				debugPanel.Build();

			}
			
		}
	}
}
