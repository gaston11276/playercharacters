using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
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

		public override async Task Loaded()
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

			// Request server configuration
			this.config = await this.Comms.Event(PlayercharactersEvents.Configuration).ToServer().Request<Configuration>();

			// Update local configuration on server configuration change
			this.Comms.Event(PlayercharactersEvents.Configuration).FromServer().On<Configuration>((e, c) => this.config = c);

			// Create overlay
			this.overlay = new PlayercharactersOverlay(this.OverlayManager);

			this.openCreator = new Hotkey(this.config.SelectionScreen.HotkeyCreator);
			this.openLooks = new Hotkey(this.config.SelectionScreen.HotkeyLooks);
			this.openSpawn = new Hotkey(this.config.SelectionScreen.HotkeySpawnLocation);
			this.LMB = new Hotkey(InputControl.CursorAccept);
			this.RMB = new Hotkey(InputControl.CursorCancel);

			///////////////////////////////////
			// Debug
			hotkeyF1 = new Hotkey(InputControl.ReplayStartStopRecording);
			hotkeyF2 = new Hotkey(InputControl.ReplayStartStopRecordingSecondary);
			hotkeyF3 = new Hotkey(InputControl.ReplayTimelineSave);
			hotkeyF5 = new Hotkey(InputControl.SelectCharacterMichael);
			hotkeyF6 = new Hotkey(InputControl.SelectCharacterFranklin);
			hotkeyF7 = new Hotkey(InputControl.SelectCharacterTrevor);
			hotkeyF9 = new Hotkey(InputControl.DropWeapon);
			hotkeyF10 = new Hotkey(InputControl.DropAmmo);
			hotkeyF11 = new Hotkey(InputControl.SwitchVisor);
			///////////////////////////////////
			
			hudCharacters.SetHotkey((int)ConvertKeycodeNFiveToVK(openCreator.UserKeyboardKey));
			hudAppearance.SetHotkey((int)ConvertKeycodeNFiveToVK(openLooks.UserKeyboardKey));
			hudSpawnLocation.SetHotkey((int)ConvertKeycodeNFiveToVK(openSpawn.UserKeyboardKey));

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
			//Logger.Debug($"Active resolution: {screenWidth} {screenHeight}");
			//Logger.Debug($"Aspect: {API.GetAspectRatio(true)} {API.GetAspectRatio(false)} - {API.GetScreenAspectRatio(true)} {API.GetScreenAspectRatio(false)}");
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

		void OnSpawnLocationOpen()
		{
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

			//API.RequestClipSet(character.AnimationSet);
			//await BaseScript.Delay(100); // Required to load
			//Game.Player.Character.MovementAnimationSet = character.AnimationSet;

			this.activeCharacter = character;
			// Set player health (Rare #OnSpawnDeath Fix)
			//this.activeCharacter.Health = character.Health;
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
			//this.Comms.Event(SessionEvents.DisconnectPlayer).ToServer().Emit("Am I Disconnected?!");
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

				//int AxisX = Game.GetControlValue(0, CitizenFX.Core.Control.FrontendAxisX);
				//int AxisY = Game.GetControlValue(0, CitizenFX.Core.Control.FrontendAxisX);
				//float AxisX = Game.GetControlNormal(0, Control.ScriptRightAxisX);
				//float AxisY = Game.GetControlNormal(0, Control.ScriptRightAxisY);

				//float AxisX = ((float)(lastCursorX - cursorX)) / 1280f;
				//float AxisY = ((float)(lastCursorY - cursorY)) / 1280f;

				if (hudSpawnLocation.IsOpen())
				{
					// Todo
					//spawnLocation.OnInputKey(args.state, args.keycode);
					
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


			//////////////////////////////////////////////////
			// Debug
			DebugInput();

		}

		//DEBUG
		private Hotkey hotkeyF1;
		private Hotkey hotkeyF2;
		private Hotkey hotkeyF3;
		private Hotkey hotkeyF5;
		private Hotkey hotkeyF6;
		private Hotkey hotkeyF7;
		private Hotkey hotkeyF9;
		private Hotkey hotkeyF10;
		private Hotkey hotkeyF11;

		void DebugInput()
		{
			if (this.hotkeyF3.IsJustPressed())
			{
				

			}
			if (this.hotkeyF5.IsJustPressed())
			{

			}
			if (this.hotkeyF6.IsJustPressed())
			{

			}
			if (this.hotkeyF7.IsJustPressed())
			{

			}
			if (this.hotkeyF9.IsJustPressed())
			{

			}
			if (this.hotkeyF10.IsJustPressed())
			{

			}
			if (this.hotkeyF11.IsJustPressed())
			{

			}
			if (this.hotkeyF11.IsJustPressed())
			{

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
		public KeycodesVK ConvertKeycodeNFiveToVK(KeyCode nfiveKeycode)
		{
			switch (nfiveKeycode)
			{
				case KeyCode.Unknown:
					return 0;
				case KeyCode.None:
					return 0;
				case KeyCode.Backspace:
					return KeycodesVK.Backspace;
				case KeyCode.Tab:
					return KeycodesVK.Tab;
				case KeyCode.Pause:
					return KeycodesVK.Pause;
				case KeyCode.Escape:
					return KeycodesVK.Escape;
				case KeyCode.Space:
					return KeycodesVK.Space;
				case KeyCode.Digit1:
					return KeycodesVK.Key1;
				case KeyCode.Digit2:
					return KeycodesVK.Key2;
				case KeyCode.Digit3:
					return KeycodesVK.Key3;
				case KeyCode.Digit4:
					return KeycodesVK.Key4;
				case KeyCode.Digit5:
					return KeycodesVK.Key5;
				case KeyCode.Digit6:
					return KeycodesVK.Key6;
				case KeyCode.Digit7:
					return KeycodesVK.Key7;
				case KeyCode.Digit8:
					return KeycodesVK.Key8;
				case KeyCode.Digit9:
					return KeycodesVK.Key9;
				case KeyCode.Digit0:
					return KeycodesVK.Key0;
				case KeyCode.KeyA:
					return KeycodesVK.KeyA;
				case KeyCode.KeyB:
					return KeycodesVK.KeyB;
				case KeyCode.KeyC:
					return KeycodesVK.KeyC;
				case KeyCode.KeyD:
					return KeycodesVK.KeyD;
				case KeyCode.KeyE:
					return KeycodesVK.KeyE;
				case KeyCode.KeyF:
					return KeycodesVK.KeyF;
				case KeyCode.KeyG:
					return KeycodesVK.KeyG;
				case KeyCode.KeyH:
					return KeycodesVK.KeyH;
				case KeyCode.KeyI:
					return KeycodesVK.KeyI;
				case KeyCode.KeyJ:
					return KeycodesVK.KeyJ;
				case KeyCode.KeyK:
					return KeycodesVK.KeyK;
				case KeyCode.KeyL:
					return KeycodesVK.KeyL;
				case KeyCode.KeyM:
					return KeycodesVK.KeyM;
				case KeyCode.KeyN:
					return KeycodesVK.KeyN;
				case KeyCode.KeyO:
					return KeycodesVK.KeyO;
				case KeyCode.KeyP:
					return KeycodesVK.KeyP;
				case KeyCode.KeyQ:
					return KeycodesVK.KeyQ;
				case KeyCode.KeyR:
					return KeycodesVK.KeyR;
				case KeyCode.KeyS:
					return KeycodesVK.KeyS;
				case KeyCode.KeyT:
					return KeycodesVK.KeyT;
				case KeyCode.KeyU:
					return KeycodesVK.KeyU;
				case KeyCode.KeyV:
					return KeycodesVK.KeyV;
				case KeyCode.KeyW:
					return KeycodesVK.KeyW;
				case KeyCode.KeyX:
					return KeycodesVK.KeyX;
				case KeyCode.KeyY:
					return KeycodesVK.KeyY;
				case KeyCode.KeyZ:
					return KeycodesVK.KeyZ;
				case KeyCode.Backquote:
					return 0;
				case KeyCode.Minus:
					return KeycodesVK.OEMMinus;
				case KeyCode.Equal:
					return 0;
				case KeyCode.BracketLeft:
					return KeycodesVK.OEM4;
				case KeyCode.BracketRight:
					return KeycodesVK.OEM6;
				case KeyCode.Semicolon:
					return KeycodesVK.OEM1;
				case KeyCode.Quote:
					return KeycodesVK.OEM7;
				case KeyCode.Backslash:
					return KeycodesVK.OEM5;
				case KeyCode.Comma:
					return KeycodesVK.OEMComma;
				case KeyCode.Period:
					return KeycodesVK.OEMPeriod;
				case KeyCode.Slash:
					return KeycodesVK.OEM2;
				case KeyCode.IntlBackslash:
					return 0;
				case KeyCode.Numpad0:
					return KeycodesVK.Numpad0;
				case KeyCode.Numpad1:
					return KeycodesVK.Numpad1;
				case KeyCode.Numpad2:
					return KeycodesVK.Numpad2;
				case KeyCode.Numpad3:
					return KeycodesVK.Numpad3;
				case KeyCode.Numpad4:
					return KeycodesVK.Numpad4;
				case KeyCode.Numpad5:
					return KeycodesVK.Numpad5;
				case KeyCode.Numpad6:
					return KeycodesVK.Numpad6;
				case KeyCode.Numpad7:
					return KeycodesVK.Numpad7;
				case KeyCode.Numpad8:
					return KeycodesVK.Numpad8;
				case KeyCode.Numpad9:
					return KeycodesVK.Numpad9;
				case KeyCode.NumpadDecimal:
					return KeycodesVK.NumpadDecimal;
				case KeyCode.NumpadDivide:
					return KeycodesVK.NumpadDivide;
				case KeyCode.NumpadMultiply:
					return KeycodesVK.NumpadMultiply;
				case KeyCode.NumpadSubtract:
					return KeycodesVK.NumpadSubtract;
				case KeyCode.NumpadAdd:
					return KeycodesVK.NumpadAdd;
				case KeyCode.NumpadEnter:
					return KeycodesVK.Enter;
				case KeyCode.NumpadEqual:
					return 0;
				case KeyCode.NumLock:
					return KeycodesVK.NumLock;
				case KeyCode.ArrowUp:
					return KeycodesVK.ArrowUp;
				case KeyCode.ArrowDown:
					return KeycodesVK.ArrowDown;
				case KeyCode.ArrowRight:
					return KeycodesVK.ArrowRight;
				case KeyCode.ArrowLeft:
					return KeycodesVK.ArrowLeft;
				case KeyCode.Insert:
					return KeycodesVK.Insert;
				case KeyCode.Delete:
					return KeycodesVK.Delete;
				case KeyCode.Home:
					return KeycodesVK.Home;
				case KeyCode.End:
					return KeycodesVK.End;
				case KeyCode.PageUp:
					return KeycodesVK.PageUp;
				case KeyCode.PageDown:
					return KeycodesVK.PageDown;
				case KeyCode.F1:
					return KeycodesVK.F1;
				case KeyCode.F2:
					return KeycodesVK.F2;
				case KeyCode.F3:
					return KeycodesVK.F3;
				case KeyCode.F4:
					return KeycodesVK.F4;
				case KeyCode.F5:
					return KeycodesVK.F5;
				case KeyCode.F6:
					return KeycodesVK.F6;
				case KeyCode.F7:
					return KeycodesVK.F7;
				case KeyCode.F8:
					return KeycodesVK.F8;
				case KeyCode.F9:
					return KeycodesVK.F9;
				case KeyCode.F10:
					return KeycodesVK.F10;
				case KeyCode.F11:
					return KeycodesVK.F11;
				case KeyCode.F12:
					return KeycodesVK.F12;
				case KeyCode.F13:
					return KeycodesVK.F13;
				case KeyCode.F14:
					return KeycodesVK.F14;
				case KeyCode.F15:
					return KeycodesVK.F15;
				case KeyCode.F16:
					return KeycodesVK.F16;
				case KeyCode.F17:
					return KeycodesVK.F17;
				case KeyCode.F18:
					return KeycodesVK.F18;
				case KeyCode.F19:
					return KeycodesVK.F19;
				case KeyCode.F20:
					return KeycodesVK.F20;
				case KeyCode.F21:
					return KeycodesVK.F21;
				case KeyCode.F22:
					return KeycodesVK.F22;
				case KeyCode.F23:
					return KeycodesVK.F23;
				case KeyCode.F24:
					return KeycodesVK.F24;
				case KeyCode.CapsLock:
					return KeycodesVK.CapsLock;
				case KeyCode.ScrollLock:
					return KeycodesVK.ScrollLock;
				case KeyCode.PrintScreen:
					return KeycodesVK.PrintScreen;
				case KeyCode.ShiftLeft:
					return KeycodesVK.Shift;
				case KeyCode.ShiftRight:
					return KeycodesVK.Shift;
				case KeyCode.ControlLeft:
					return KeycodesVK.Control;
				case KeyCode.ControlRight:
					return KeycodesVK.Control;
				case KeyCode.AltLeft:
					return KeycodesVK.AltLeft;
				case KeyCode.AltRight:
					return KeycodesVK.AltRight;
				case KeyCode.MetaLeft:
					return KeycodesVK.MetaLeft;
				case KeyCode.MetaRight:
					return KeycodesVK.MetaRight;
				case KeyCode.ContextMenu:
					return KeycodesVK.Menu;
				case KeyCode.MouseLeftClick:
					return KeycodesVK.LeftMouseButton;
				case KeyCode.MouseRightClick:
					return KeycodesVK.RightMouseButton;
				case KeyCode.MouseMiddleClick:
					return KeycodesVK.MiddleMouseButton;
				case KeyCode.MouseWheelUp:
					return KeycodesVK.MouseButton4;
				case KeyCode.MouseWheelDown:
					return KeycodesVK.MouseButton5;
				case KeyCode.MouseWheelUpDown:
					return 0;
				case KeyCode.MouseMoveUp:
					return 0;
				case KeyCode.MouseMoveDown:
					return 0;
				case KeyCode.MouseMoveUpDown:
					return 0;
				case KeyCode.MouseMoveLeft:
					return 0;
				case KeyCode.MouseMoveRight:
					return 0;
				case KeyCode.MouseMoveLeftRight:
					return 0;
				case KeyCode.KeyWKeyS:
					return 0;
				case KeyCode.KeyAKeyD:
					return 0;
				case KeyCode.LeftShiftLeftControl:
					return 0;
				case KeyCode.Numpad4Numpad6:
					return 0;
				case KeyCode.Numpad8Numpad5:
					return 0;
				default:
					return 0;
			}

		
		}

		public enum KeycodesVK
		{
			LeftMouseButton = 0x01,//	Left mouse button
			RightMouseButton =	0x02,//	Right mouse button
			//Cancel = 0x03,//	Control-break processing
			MiddleMouseButton = 0x04,//	Middle mouse button (three-button mouse)
			MouseButton4 = 0x05,//	X1 mouse button
			MouseButton5 = 0x06,//	X2 mouse button
			Backspace = 0x08,//	BACKSPACE key
			Tab = 0x09,//	TAB key
			//Clear = 0x0C,//	CLEAR key
			Enter = 0x0D,//	ENTER key
			Shift = 0x10,//	SHIFT key
			Control = 0x11,//	CTRL key
			Menu = 0x12,//	ALT key
			Pause = 0x13,//	PAUSE key
			CapsLock= 0x14,//	CAPS LOCK key
			Escape = 0x1B,//	ESC key
			Space = 0x20,//	SPACEBAR
			PageUp = 0x21,//	PAGE UP key
			PageDown = 0x22,//	PAGE DOWN key
			End = 0x23,//	END key
			Home = 0x24,//	HOME key
			ArrowLeft = 0x25,//	LEFT ARROW key
			ArrowUp = 0x26,//	UP ARROW key
			ArrowRight = 0x27,//	RIGHT ARROW key
			ArrowDown = 0x28,//	DOWN ARROW key
			Select = 0x29,//	SELECT key
			//Print = 0x2A,//	PRINT key
			//Execute = 0x2B,//	EXECUTE key
			PrintScreen = 0x2C,//	PRINT SCREEN key
			Insert = 0x2D,//	INS key
			Delete= 0x2E,//	DEL key
			//Help = 0x2F,//	HELP key
			Key0 = 0x30,//	0 key
			Key1 = 0x31,//	1 key
			Key2 = 0x32,//	2 key
			Key3 = 0x33,//	3 key
			Key4 = 0x34,//	4 key
			Key5 = 0x35,//	5 key
			Key6 = 0x36,//	6 key
			Key7 = 0x37,//	7 key
			Key8 = 0x38,//	8 key
			Key9 = 0x39,//	9 key
			KeyA = 0x41,//	A key
			KeyB = 0x42,//	B key
			KeyC = 0x43,//	C key
			KeyD = 0x44,//	D key
			KeyE = 0x45,//	E key
			KeyF = 0x46,//	F key
			KeyG = 0x47,//	G key
			KeyH = 0x48,//	H key
			KeyI = 0x49,//	I key
			KeyJ = 0x4A,//	J key
			KeyK = 0x4B,//	K key
			KeyL = 0x4C,//	L key
			KeyM = 0x4D,//	M key
			KeyN = 0x4E,//	N key
			KeyO = 0x4F,//	O key
			KeyP = 0x50,//	P key
			KeyQ = 0x51,//	Q key
			KeyR = 0x52,//	R key
			KeyS = 0x53,//	S key
			KeyT = 0x54,//	T key
			KeyU = 0x55,//	U key
			KeyV = 0x56,//	V key
			KeyW = 0x57,//	W key
			KeyX = 0x58,//	X key
			KeyY = 0x59,//	Y key
			KeyZ = 0x5A,//	Z key
			MetaLeft = 0x5B,//	Left Windows key(Natural keyboard)
			MetaRight = 0x5C,//	Right Windows key(Natural keyboard)
			//Sleep = 0x5F,//	Computer Sleep key
			Numpad0 = 0x60,//	Numeric keypad 0 key
			Numpad1 = 0x61,//	Numeric keypad 1 key
			Numpad2 = 0x62,//	Numeric keypad 2 key
			Numpad3 = 0x63,//	Numeric keypad 3 key
			Numpad4 = 0x64,//	Numeric keypad 4 key
			Numpad5 = 0x65,//	Numeric keypad 5 key
			Numpad6 = 0x66,//	Numeric keypad 6 key
			Numpad7 = 0x67,//	Numeric keypad 7 key
			Numpad8 = 0x68,//	Numeric keypad 8 key
			Numpad9 = 0x69,//	Numeric keypad 9 key
			NumpadMultiply = 0x6A,//	Multiply key
			NumpadAdd = 0x6B,//	Add key
			VK_SEPARATOR = 0x6C,//	Separator key
			NumpadSubtract = 0x6D,//	Subtract key
			NumpadDecimal = 0x6E,//	Decimal key
			NumpadDivide = 0x6F,//	Divide key
			F1 = 0x70,//	F1 key
			F2 = 0x71,//	F2 key
			F3 = 0x72,//	F3 key
			F4 = 0x73,//	F4 key
			F5 = 0x74,//	F5 key
			F6 = 0x75,//	F6 key
			F7 = 0x76,//	F7 key
			F8 = 0x77,//	F8 key
			F9 = 0x78,//	F9 key
			F10 = 0x79,//	F10 key
			F11 = 0x7A,//	F11 key
			F12 = 0x7B,//	F12 key
			F13 = 0x7C,//	F13 key
			F14 = 0x7D,//	F14 key
			F15 = 0x7E,//	F15 key
			F16 = 0x7F,//	F16 key
			F17 = 0x80,//	F17 key
			F18 = 0x81,//	F18 key
			F19 = 0x82,//	F19 key
			F20 = 0x83,//	F20 key
			F21 = 0x84,//	F21 key
			F22 = 0x85,//	F22 key
			F23 = 0x86,//	F23 key
			F24 = 0x87,//	F24 key
						  //-	0x88-8F	Unassigned
			NumLock = 0x90,//	NUM LOCK key
			ScrollLock = 0x91,//	SCROLL LOCK key
							 //0x92-96	OEM specific
							 //-	0x97-9F	Unassigned
			ShiftLeft = 0xA0,//	Left SHIFT key
			ShiftRight = 0xA1,//	Right SHIFT key
			ControlLeft = 0xA2,//	Left CONTROL key
			ControlRight = 0xA3,//	Right CONTROL key
			AltLeft = 0xA4,//	Left MENU key
			AltRight = 0xA5,//	Right MENU key
		
			OEM1 = 0xBA,//	Used for miscellaneous characters; it can vary by keyboard.For the US standard keyboard, the ';:' key
			OEMPlus = 0xBB,//	For any country/region, the '+' key
			OEMComma = 0xBC,//	For any country/region, the ',' key
			OEMMinus = 0xBD,//	For any country/region, the '-' key
			OEMPeriod = 0xBE,//	For any country/region, the '.' key
			OEM2 = 0xBF,//	Used for miscellaneous characters; it can vary by keyboard.For the US standard keyboard, the '/?' key
			OEM3 = 0xC0,//	Used for miscellaneous characters; it can vary by keyboard.For the US standard keyboard, the '`~' key
			OEM4 = 0xDB,//	Used for miscellaneous characters; it can vary by keyboard.For the US standard keyboard, the '[{' key
			OEM5 = 0xDC,//	Used for miscellaneous characters; it can vary by keyboard.For the US standard keyboard, the '\|' key
			OEM6 = 0xDD,//	Used for miscellaneous characters; it can vary by keyboard.For the US standard keyboard, the ']}' key
			OEM7 = 0xDE,//	Used for miscellaneous characters; it can vary by keyboard.For the US standard keyboard, the 'single-quote/double-quote' key
			OEM8 = 0xDF,//	Used for miscellaneous characters; it can vary by keyboard.
				
			OEM102 = 0xE2,//	Either the angle bracket key or the backslash key on the RT 102-key keyboard
							  //0xE3-E4 OEM specific
//			VK_PROCESSKEY = 0xE5,//	IME PROCESS key
								 //0xE6	OEM specific
//			VK_PACKET = 0xE7,//	Used to pass Unicode characters as if they were keystrokes.The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods.For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP
							 //-	0xE8	Unassigned
							 //0xE9-F5 OEM specific
//			VK_ATTN = 0xF6,//	Attn key
//			VK_CRSEL = 0xF7,//	CrSel key
//			VK_EXSEL = 0xF8,//	ExSel key
//			VK_EREOF = 0xF9,//	Erase EOF key
//			VK_PLAY = 0xFA,//	Play key
//			VK_ZOOM = 0xFB,//	Zoom key
						   //VK_NONAME	0xFC	Reserved
//			VK_PA1 = 0xFD,//	PA1 key
//			VK_OEM_CLEAR = 0xFE//	Clear key
		}

		public enum KeycodesJS
		{
			Backspace = 8,
			Tab = 9,
			Enter = 13,
			Shift = 16,
			Control = 17,
			Alt = 18,
			Pause = 19,
			CapsLock = 20,
			Escape = 27,
			Space = 32,
			PageUp = 33,
			PageDown = 34,
			End = 35,
			Home = 36,
			ArrowLeft = 37,
			ArrowUp = 38,
			ArrowRigh = 39,
			ArrowDow = 40,
			Insert = 45,
			Delete = 46,
			Digit0 = 48,
			Digit1 = 49,
			Digit2 = 50,
			Digit3 = 51,
			Digit4 = 52,
			Digit5 = 53,
			Digit6 = 54,
			Digit7 = 55,
			Digit8 = 56,
			Digit9 = 57,
			KeyA = 65,
			KeyB = 66,
			KeyC = 67,
			KeyD = 68,
			KeyE = 69,
			KeyF = 70,
			KeyG = 71,
			KeyH = 72,
			KeyI = 73,
			KeyJ = 74,
			KeyK = 75,
			KeyL = 76,
			KeyM = 77,
			KeyN = 78,
			KeyO = 79,
			KeyP = 80,
			KeyQ = 81,
			KeyR = 82,
			KeyS = 83,
			KeyT = 84,
			KeyU = 85,
			KeyV = 86,
			KeyW = 87,
			KeyX = 88,
			KeyY = 89,
			KeyZ = 90,
			MetaLeft = 91,
			MetaRight = 92,
			Select = 93,
			Numpad0 = 96,
			Numpad1 = 97,
			Numpad2 = 98,
			Numpad3 = 99,
			Numpad4 = 100,
			Numpad5 = 101,
			Numpad6 = 102,
			Numpad7 = 103,
			Numpad8 = 104,
			Numpad9 = 105,
			NumpadMultiply = 106,
			NumpadAdd = 107,
			NumpadSubtract = 109,
			NumpadDecimal = 110,
			NumpadDivide = 111,
			F1 = 112,
			F2 = 113,
			F3 = 114,
			F4 = 115,
			F5 = 116,
			F6 = 117,
			F7 = 118,
			F8 = 119,
			F9 = 120,
			F10 = 121,
			F11 = 122,
			F12 = 123,
			NumLock = 144,
			ScrollLock = 145,
			Semicolon = 186,
			Equal = 187,
			Comma = 188,
			Dash = 189,
			Period = 190,
			Slash = 191,
			Graveaccent = 192,
			Quote = 222,
			CloseBracket = 221,
			OpenBracket = 219,
			Backslash = 220,
		}
	}
}
