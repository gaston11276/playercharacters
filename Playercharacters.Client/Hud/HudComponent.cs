using System.Collections.Generic;
using NFive.SDK.Core.Diagnostics;
using Gaston11276.SimpleUi;
using Gaston11276.Playercharacters.Client.Models;

namespace Gaston11276.Playercharacters.Client
{
	public abstract class HudComponent
	{
		protected ILogger Logger;
		protected fpDelay Delay;
		static public int delayMs = 100;

		protected UiCamera camera;
		public CameraMode cameraMode = CameraMode.Front;

		protected float defaultPadding = 0.0025f;

		protected Character character;

		protected fpVoid SaveCharacterCallback;
		protected fpVoid RevertCharacterCallback;

		protected UiPanel uiAppearanceMain;
		protected Textbox uiHeader = new Textbox();
		protected UiPanel contentFrame = new UiPanel();

		protected List<OnMouseMove> inputsOnMouseMove;
		protected List<OnMouseButton> inputsOnMouseButton;
		protected List<OnKey> inputsOnKey;

		protected List<fpVoid> onHudOpenCallbacks = new List<fpVoid>();
		protected List<fpVoid> onHudCloseCallbacks = new List<fpVoid>();

		public HudComponent()
		{
			uiAppearanceMain = new UiPanel();
			uiHeader = new Textbox();
		}

		public void SetDelay(fpDelay Delay)
		{
			this.Delay = Delay;
		}

		public virtual async void SetUi()
		{
			await Delay(delayMs);
		}

		public virtual void SetLogger(ILogger Logger)
		{
			this.Logger = Logger;
		}

		public void SetInput(List<OnMouseMove> inputsOnMouseMove, List<OnMouseButton> inputsOnMouseButton, List<OnKey> inputsOnKey)
		{
			this.inputsOnMouseMove = inputsOnMouseMove;
			this.inputsOnMouseButton = inputsOnMouseButton;
			this.inputsOnKey = inputsOnKey;
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

		public void SetCamera(UiCamera camera)
		{
			this.camera = camera;
		}

		public void Open()
		{
			this.camera?.SetMode(cameraMode);
			uiAppearanceMain.ClearFlags(UiElement.HIDDEN);
			OnOpen();
		}

		public void Close()
		{
			uiAppearanceMain.SetFlags(UiElement.HIDDEN);
			OnClose();
		}

		public void Toggle()
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

		public bool IsOpen()
		{
			return ((uiAppearanceMain.GetFlags() & UiElement.HIDDEN) == 0);
		}

		public void SetCharacter(Character character)
		{
			this.character = character;
		}

		public void RegisterSaveCharacter(fpVoid SaveCharacterCallback)
		{
			this.SaveCharacterCallback = SaveCharacterCallback;
		}

		public void RegisterRevertCharacter(fpVoid RevertCharacterCallback)
		{
			this.RevertCharacterCallback = RevertCharacterCallback;
		}

		protected virtual void CreateContent()
		{
		}

		protected virtual void CreateColumns()
		{
			
		}

		protected void CreateColumn(UiPanel uiPanel, HGravity gravity, UiPanel uiColumn, string label = null)
		{
			uiColumn.SetOrientation(Orientation.Vertical);
			uiColumn.SetPadding(new UiRectangle(defaultPadding));
			uiColumn.SetGravity(gravity);
			uiColumn.SetFlags(UiElement.TRANSPARENT);
			uiPanel.AddElement(uiColumn);

			if (label != null)
			{
				Textbox header = new Textbox();
				header.SetPadding(new UiRectangle(defaultPadding));
				header.SetText(label);
				header.SetFlags(UiElement.TRANSPARENT);
				if (label.Length == 0)
				{
					header.SetTextFlags(UiElement.TRANSPARENT);
				}
				uiColumn.AddElement(header);
			}
		}

		public void CreateUi(UiPanel uiMain)
		{
			uiAppearanceMain.SetFlags(UiElement.HIDDEN);
			uiAppearanceMain.SetOrientation(Orientation.Vertical);
			uiAppearanceMain.SetPadding(new UiRectangle(defaultPadding));
			uiAppearanceMain.SetProperties(UiElement.FLOATING | UiElement.COLLISION_PARENT | UiElement.MOVABLE | UiElement.RESIZEABLE);
			inputsOnMouseMove.Add(uiAppearanceMain.OnCursorMove);
			inputsOnMouseButton.Add(uiAppearanceMain.OnMouseButton);
			uiMain.AddElement(uiAppearanceMain);

			UiPanel uiHeaderPanel = new UiPanel();
			uiHeaderPanel.SetPadding(new UiRectangle(defaultPadding));
			uiHeaderPanel.SetFlags(UiElement.TRANSPARENT);
			uiAppearanceMain.AddElement(uiHeaderPanel);

			uiHeader.SetText("Header");
			uiHeader.SetPadding(new UiRectangle(defaultPadding));
			uiHeader.SetFlags(UiElement.TRANSPARENT);
			uiHeaderPanel.AddElement(uiHeader);

			contentFrame.SetOrientation(Orientation.Vertical);
			contentFrame.SetPadding(new UiRectangle(defaultPadding));
			contentFrame.SetFlags(UiElement.TRANSPARENT);
			uiAppearanceMain.AddElement(contentFrame);
			
			CreateColumns();
			CreateContent();
		}

		protected void CreateApplyCancelButtons()
		{
			// Buttons
			UiPanel uiButtonsPanel = new UiPanel();
			uiButtonsPanel.SetPadding(new UiRectangle(defaultPadding));
			uiButtonsPanel.SetGravity(HGravity.Right);
			uiButtonsPanel.SetFlags(UiElement.TRANSPARENT);
			contentFrame.AddElement(uiButtonsPanel);

			Textbox uiButtonApply = new Textbox();
			uiButtonApply.SetText("Apply");
			uiButtonApply.SetPadding(new UiRectangle(defaultPadding));
			uiButtonApply.SetProperties(UiElement.CANFOCUS);
			uiButtonApply.RegisterOnLMBRelease(SaveCharacter);
			inputsOnMouseMove.Add(uiButtonApply.OnCursorMove);
			inputsOnMouseButton.Add(uiButtonApply.OnMouseButton);
			uiButtonsPanel.AddElement(uiButtonApply);

			Textbox uiButtonCancel = new Textbox();
			uiButtonCancel.SetText("Cancel");
			uiButtonCancel.SetPadding(new UiRectangle(defaultPadding));
			uiButtonCancel.SetProperties(UiElement.CANFOCUS);
			uiButtonCancel.RegisterOnLMBRelease(RevertCharacter);
			inputsOnMouseMove.Add(uiButtonCancel.OnCursorMove);
			inputsOnMouseButton.Add(uiButtonCancel.OnMouseButton);
			uiButtonsPanel.AddElement(uiButtonCancel);
		}

		protected void SaveCharacter()
		{
			SaveCharacterCallback();
			Close();
		}
		protected void RevertCharacter()
		{
			RevertCharacterCallback();
			Close();
		}
	}
}
