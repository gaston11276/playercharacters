using System;
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
		
		private Hotkey HotkeyCharacterList;
		private Hotkey HotkeyAppearanceMenu;

		private bool holdFocus = true;

		public PlayercharactersService(ILogger logger, ITickManager ticks, ICommunicationManager comms, ICommandManager commands, IOverlayManager overlay, User user) : base(logger, ticks, comms, commands, overlay, user)
		{}

		public override async Task Started()
		{
			hudCharacters.SetLogger(this.Logger);
			hudCharacters.SetDelay(Delay);
			hudCharacters.RegisterOnCreateCallback(OnCreate);
			hudCharacters.RegisterOnDeleteCallback(OnDelete);
			hudCharacters.RegisterOnCharacterListOpenCallback(OnCharacterListOpen);
			hudCharacters.RegisterOnCharacterListCloseCallback(OnCharacterListClose);
			hudCharacters.RegisterOnPlayCallback(OnPlay);
			hudCharacters.CreateUi();
			hudCharacters.InitUi();

			hudAppearance.SetLogger(this.Logger);
			hudAppearance.SetDelay(Delay);
			hudAppearance.RegisterSaveCallback(SaveCharacter);
			hudAppearance.RegisterRevertCallback(RevertCharacter);
			hudAppearance.RegisterOnOpenCallback(OnAppearanceMenuOpen);
			hudAppearance.RegisterOnCloseCallback(OnAppearanceMenuClose);
			hudAppearance.CreateUi();
			hudAppearance.InitUi();

			OnResolutionChanged(); // Should be called if resolution (or screen size) is changed.

			this.config = await this.Comms.Event(PlayercharactersEvents.Configuration).ToServer().Request<Configuration>();
			this.Comms.Event(PlayercharactersEvents.Configuration).FromServer().On<Configuration>((e, c) => this.config = c);

			this.HotkeyCharacterList = new Hotkey(this.config.SelectionScreen.HotkeyCharacterList);
			this.HotkeyAppearanceMenu = new Hotkey(this.config.SelectionScreen.HotkeyAppearanceMenu);
			
			hudCharacters.SetHotkey((int)HudInput.ConvertKeycode(HotkeyCharacterList.UserKeyboardKey));
			hudAppearance.SetHotkey((int)HudInput.ConvertKeycode(HotkeyAppearanceMenu.UserKeyboardKey));

			this.Ticks.On(new Action(OnInput));
			this.Ticks.On(new Action(OnDraw));
		}

		public override async Task HoldFocus()
		{
			API.SetManualShutdownLoadingScreenNui(true);

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
		}

		void OnCharacterListOpen()
		{
			this.Ticks.Off(OnSaveCharacter);
			this.Ticks.Off(OnSavePosition);
			Screen.Hud.IsVisible = false;
			LoadCharacters();
			OpenNui();
		}

		void OnCharacterListClose(Guid characterId)
		{
			CloseNui();
			Screen.Hud.IsVisible = true;

			if (activeCharacter != null)
			{
				this.Ticks.On(OnSaveCharacter);
				this.Ticks.On(OnSavePosition);
			}
		}

		async void OnPlay(Guid characterId)
		{
			Screen.Fading.FadeOut(200);
			await SetNewCharacter(characterId);
			
			this.Ticks.On(OnSaveCharacter);
			this.Ticks.On(OnSavePosition);
			Screen.Fading.FadeIn(200);
		}

		void OnAppearanceMenuOpen()
		{
			this.Ticks.Off(OnSaveCharacter);
			this.Ticks.Off(OnSavePosition);
			Screen.Hud.IsVisible = false;
			OpenNui();
		}

		void OnAppearanceMenuClose()
		{
			CloseNui();
			Screen.Hud.IsVisible = true;

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
			while (holdFocus)
			{
				await Delay(20);
			}
			this.overlay.Blur();
		}

		private void CloseNui()
		{
			holdFocus = false;			
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

		private async Task SetNewCharacter(Guid selectedCharacterId)
		{
			Character selectedCharacter = characters.First(c => c.Id == selectedCharacterId);
			await this.Comms.Event(PlayercharactersEvents.Select).ToServer().Request<CharacterSession>(selectedCharacter.Id);
			activeCharacter = selectedCharacter;

			await hudAppearance.SetCharacter(activeCharacter);
			await hudAppearance.ApplyToPed();
			SwitchCharacter(selectedCharacter);
			await hudAppearance.RefreshUi();
		}

		private void SwitchCharacter(Character character)
		{
			Game.Player.Character.IsPositionFrozen = false;
			API.SwitchInPlayer(API.PlayerPedId());
			Game.Player.Character.Position = character.Position.ToVector3().ToCitVector3();
			Game.Player.Character.Armor = character.Armor;
			this.activeCharacter = character;
		}

		private async void OnCreate()
		{
			Character character = new Character();
			hudCharacters.SetCharacterInfo(character);
			
			character = await this.Comms.Event(PlayercharactersEvents.CreateCharacter).ToServer().Request<Character>(character);

			await hudAppearance.SetCharacter(character);
			await hudAppearance.SetDefaults();

			await this.Comms.Event(PlayercharactersEvents.SaveCharacter).ToServer().Request<Character>(character);
			await RequestCharacters();

			hudCharacters.ClearCharacterListEdit();
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
			await hudAppearance.SetCharacter(activeCharacter);
			await hudAppearance.ApplyToPed();
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
		}

		private void OnInput()
		{
			// Non Nui Input
			if (HotkeyCharacterList.IsJustReleased()) // default F1
			{
				hudCharacters.Open();
			}

			if (HotkeyAppearanceMenu.IsJustReleased()) // default F2
			{
				hudAppearance.Open();
			}
		}

		private void OnNuiKey(object sender, OnKeyOverlayEventArgs args)
		{
			hudCharacters.OnInputKey(args.state, args.keycode);
			hudAppearance.OnInputKey(args.state, args.keycode);
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
		}	
	}
}
