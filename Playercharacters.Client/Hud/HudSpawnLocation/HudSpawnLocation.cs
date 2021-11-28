using CitizenFX.Core;
using CitizenFX.Core.Native;

using NFive.SDK.Core.Diagnostics;
using Gaston11276.SimpleUi;
using Gaston11276.Playercharacters.Client.Models;

namespace Gaston11276.Playercharacters.Client
{
	public class HudSpawnLocation : Hud
	{
		UiSpawnPosition uiSpawnLocation;


		public HudSpawnLocation()
		{
			uiSpawnLocation = new UiSpawnPosition();
			uiSpawnLocation.SetInput(inputsOnMouseMove, inputsOnMouseButton, inputsOnKey);
			
		}

		protected override void OnOpen()
		{
			Game.Player.CanControlCharacter = false;
			Game.Player.Character.Task.ClearAllImmediately();
			API.FreezePedCameraRotation(Game.PlayerPed.Handle);
			
			uiSpawnLocation.Open();

			base.OnOpen();
		}

		protected override void OnClose()
		{
			Game.Player.CanControlCharacter = true;
			Game.Player.Character.Task.ClearAllImmediately();
			API.RenderScriptCams(false, true, 200, true, true);
			uiSpawnLocation.Close();

			base.OnClose();
		}

		public override void SetLogger(ILogger Logger)
		{
			base.SetLogger(Logger);
		}

		public override void OnInputKey(int state, int keycode)
		{
			if (IsOpen())
			{
				base.OnInputKey(state, keycode);

				if (state == 3 && keycode == 27)// Escape
				{
					Close();
				}

				if (state == 3 && keycode == hotkey)// Hotkey F5
				{
					Close();
				}
			}
			else
			{
				/*if (keycode == 113)// F2
				{
					Open();
				}*/
			}

		}

		public override void CreateUi()
		{
			base.CreateUi();
			uiSpawnLocation.SetLogger(Logger);
			uiSpawnLocation.CreateUi(uiMain);
		}

		public override void Open()
		{
			base.Open();
			
		}
	}
}
