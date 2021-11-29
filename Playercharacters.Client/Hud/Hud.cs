using System.Collections.Generic;
using System.Threading.Tasks;
using NFive.SDK.Core.Diagnostics;
using Gaston11276.SimpleUi;

namespace Gaston11276.Playercharacters.Client
{
	public delegate void OnMouseMove(float x, float y);
	public delegate void OnMouseButton(int state, int button, float x, float y);
	public delegate void OnKey(int state, int keycode);

	public delegate Task fpDelay(int ms);
	
	public abstract class Hud
	{
		protected ILogger Logger;
		protected fpDelay Delay;

		public delegate void fpVoid();

		protected List<OnMouseMove> inputsOnMouseMove = new List<OnMouseMove>();
		protected List<OnMouseButton> inputsOnMouseButton = new List<OnMouseButton>();
		protected List<OnKey> inputsOnKey = new List<OnKey>();
		protected List<fpVoid> onHudOpenCallbacks = new List<fpVoid>();
		protected List<fpVoid> onHudCloseCallbacks = new List<fpVoid>();

		protected UiPanel uiMain = null;
		protected float defaultPadding = 0.0025f;
		
		protected int hotkey;
		public bool isRefreshingUi = false;

		protected List<HudInput.KeyData> keys;

		public Hud()
		{
			uiMain = new UiPanel();
			HudInput.InitInput(ref keys);
		}

		public virtual void SetDelay(fpDelay Delay)
		{
			this.Delay = Delay;
		}

		public virtual void SetLogger(ILogger Logger)
		{
			this.Logger = Logger;
		}

		public void RegisterOnOpenCallback(fpVoid OnOpen)
		{
			onHudOpenCallbacks.Add(OnOpen);
		}

		public void RegisterOnCloseCallback(fpVoid OnClose)
		{
			onHudCloseCallbacks.Add(OnClose);
		}

		protected virtual void OnOpen()
		{
			foreach (fpVoid onOpen in onHudOpenCallbacks)
			{
				onOpen();
			}
		}

		protected virtual void OnClose()
		{
			foreach (fpVoid onClose in onHudCloseCallbacks)
			{
				onClose();
			}
		}

		public void SetHotkey(int keycode)
		{
			hotkey = keycode;
		}

		public virtual void OnInputKey(int state, int keycode)
		{
			foreach (OnKey OnKey in inputsOnKey)
			{
				OnKey(state, keycode);
			}
		}

		public virtual void OnMouseMove(float cursorX, float cursorY)
		{
			foreach (OnMouseMove OnMouseMove in inputsOnMouseMove)
			{
				OnMouseMove(cursorX, cursorY);
			}
		}

		public virtual void OnMouseButton(int state, int button, float CursorX, float CursorY)
		{
			if (state == 1)
			{
				foreach (OnMouseButton OnMouseButton in inputsOnMouseButton)
				{
					OnMouseButton(state, button, CursorX, CursorY);
				}
			}
			if (state == 3)
			{
				foreach (OnMouseButton OnMouseButton in inputsOnMouseButton)
				{
					OnMouseButton(state, button, CursorX, CursorY);
				}
			}
		}

		public void Refresh()
		{
			uiMain.Refresh();
		}

		public bool IsOpen()
		{
			return ((uiMain.GetFlags() & UiElement.HIDDEN) == 0);
		}

		public virtual void Open()
		{
			uiMain.ClearFlags(UiElement.HIDDEN);
			OnOpen();
		}

		public virtual void Close()
		{
			uiMain.SetFlags(UiElement.HIDDEN);
			OnClose();
		}

		public virtual void Toggle()
		{
			if (IsOpen())
			{
				Close();
			}
			else
			{
				Open();
			}
		}

		public void Draw()
		{
			uiMain.Draw();			
		}

		public void SetResolution(int width, int height)
		{
			UiPanel.screenResolutionX = width;
			UiPanel.screenResolutionY = height;
		}

		public virtual void CreateUi()
		{
			uiMain.SetFlags(UiElement.HIDDEN);

			uiMain.SetLogger(this.Logger);
			uiMain.name = "Main";

			UiPanel.screenBoundaries = new UiRectangle(0f, 0f, 1f, 1f);

			uiMain.SetFlags(UiElement.TRANSPARENT);
			uiMain.SetPadding(new UiRectangle(defaultPadding));
			uiMain.SetHDimension(Dimension.Max);
			uiMain.SetVDimension(Dimension.Max);
			inputsOnMouseMove.Add(uiMain.OnCursorMove);
			inputsOnMouseButton.Add(uiMain.OnMouseButton);
		}

		public virtual async Task RefreshUi()
		{
			await Delay(1);
		}
	}
}
