using System.Collections.Generic;
using NFive.SDK.Core.Diagnostics;
using NFive.SDK.Core.Input;
using NFive.SDK.Client.Input;
using Gaston11276.SimpleUi;

namespace Gaston11276.Playercharacters.Client
{
	public delegate void OnMouseMove(float x, float y);
	public delegate void OnLeftMouseButton(int state, float x, float y);
	public delegate void OnKey(int e);

	public class KeyData
	{
		public int keycode;
		public Hotkey hotkey;
		public KeyData(int keycode, InputControl control)
		{
			this.keycode = keycode;
			hotkey = new Hotkey(control);
		}
	}
	
	public class Hud
	{
		protected ILogger Logger;

		
		protected List<OnMouseMove> inputsOnMouseMove = new List<OnMouseMove>();
		protected List<OnLeftMouseButton> inputsOnLeftMouseButton = new List<OnLeftMouseButton>();
		protected List<OnKey> inputsOnKey = new List<OnKey>();

		protected UiPanel uiMain = null;

		protected float defaultPadding = 0.0025f;

		protected List<KeyData> keys;
		protected float screenWidth;
		protected float screenHeight;

		public Hud()
		{
			uiMain = new UiPanel();
			InitInput();
		}

		public virtual void SetLogger(ILogger Logger)
		{
			this.Logger = Logger;
		}

		public void SetResolution(float screenWidth, float screenHeight)
		{
			this.screenWidth = screenWidth;
			this.screenHeight = screenHeight;
		}

		void AddKeyData(int keycode, InputControl control)
		{
			KeyData keydata = new KeyData(keycode, control);
			keydata.keycode = keycode;
			keys.Add(keydata);
		}

		private void InitInput()
		{
			keys = new List<KeyData>();
			AddKeyData(65, InputControl.MoveLeftOnly); // A
			AddKeyData(66, InputControl.SpecialAbilitySecondary); // B
			AddKeyData(67, InputControl.CreatorRT); // C
			AddKeyData(68, InputControl.MoveRightOnly); // D
			AddKeyData(69, InputControl.Context); // E
			AddKeyData(70, InputControl.Arrest); // F
			AddKeyData(71, InputControl.Detonate); // G
			AddKeyData(72, InputControl.VehicleRoof); // H
			AddKeyData(73, 0); // I
			AddKeyData(74, 0); // J
			AddKeyData(75, InputControl.ReplayShoHotkey); // K
			AddKeyData(76, InputControl.CinematicSlowMo);				// L
			AddKeyData(77, InputControl.InteractionMenu); // M
			AddKeyData(78, InputControl.PushToTalk); // N
			AddKeyData(79, 0); // O
			AddKeyData(80, InputControl.FrontendPause); // P
			AddKeyData(81, InputControl.Cover); // Q
			AddKeyData(82, InputControl.Reload); // R
			AddKeyData(83, InputControl.MoveDownOnly); // S
			AddKeyData(84, InputControl.MpTextChatAll); // T
			AddKeyData(85, InputControl.ReplayScreenshot); // U
			AddKeyData(86, InputControl.NextCamera); // V
			AddKeyData(87, InputControl.MoveUpOnly); // W
			AddKeyData(88, InputControl.VehicleDuck); // X
			AddKeyData(89, InputControl.WeaponSpecial); // Y
			AddKeyData(90, InputControl.MultiplayerInfo); // Z
			AddKeyData(8, InputControl.PhoneCancel); // Backspace
		}


		public void OnInputKey()
		{
			foreach (KeyData keydata in keys)
			{
				if (keydata.hotkey.IsJustReleased())
				{
					foreach (OnKey OnKey in inputsOnKey)
					{
						OnKey(keydata.keycode);
					}
				}
			}
		}

		public void OnInputMouseMove(float cursorX, float cursorY)
		{
			foreach (OnMouseMove OnMouseMove in inputsOnMouseMove)
			{
				OnMouseMove(cursorX, cursorY);
			}
		}

		public void OnLMB(int state, float CursorX, float CursorY)
		{
			if (state == 1)
			{
				foreach (OnLeftMouseButton OnLeftMouseButton in inputsOnLeftMouseButton)
				{
					OnLeftMouseButton(1, CursorX, CursorY);
				}
			}
			if (state == 3)
			{
				foreach (OnLeftMouseButton OnLeftMouseButton in inputsOnLeftMouseButton)
				{
					OnLeftMouseButton(3, CursorX, CursorY);
				}
			}
		}

		public void Build()
		{
			uiMain.FirstBuild();
			uiMain.Build();
		}

		public bool IsOpen()
		{
			return ((uiMain.GetFlags() & UiElement.DISABLED) == 0);
		}

		public virtual void Open()
		{
			if (uiMain != null)
			{
				uiMain.ClearFlags(UiElement.DISABLED);
			}
			else
			{
				Logger.Debug("Hud: Open(): ERROR: uiMain is null.");
			}
			
		}

		public virtual void Close()
		{
			uiMain.SetFlags(UiElement.DISABLED);
		}

		public void Draw()
		{
			if (uiMain != null)
			{
				uiMain.Draw();
			}
			else
			{
				Logger.Debug("Hud: Draw(): ERROR: uiMain is null.");
			}
			
		}

		public virtual void CreateUi()
		{
			uiMain.SetFlags(UiElement.DISABLED);

			uiMain.SetLogger(this.Logger);
			uiMain.name = "Main";

			UiPanel.screenResolutionX = 2560;
			UiPanel.screenResolutionY = 1440;
			UiPanel.screenBoundaries = new UiRectangle(0f, 0f, 1f, 1f);

			uiMain.SetFlags(UiElement.NO_DRAW);
			uiMain.SetPadding(new UiRectangle(defaultPadding));
			uiMain.SetHDimension(Dimension.Max);
			uiMain.SetVDimension(Dimension.Max);
			inputsOnMouseMove.Add(uiMain.OnCursorMove);
			inputsOnLeftMouseButton.Add(uiMain.OnLeftMouseButton);
		}

		public virtual void OnInputRMBMouseMoveAxis(int rmb, float axisX, float axisY)
		{

		}
	}
}
