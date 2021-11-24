using System.Collections.Generic;
using NFive.SDK.Core.Diagnostics;
using Gaston11276.SimpleUi;
using Gaston11276.Playercharacters.Client.Models;

namespace Gaston11276.Playercharacters.Client
{
	public class UiAppearance
	{
		protected ILogger Logger;

		private UiCamera camera;
		public CameraMode cameraMode = CameraMode.Front;

		protected float defaultPadding = 0.0025f;

		protected Character character;

		public delegate void OnSaveCharacter();
		protected OnSaveCharacter SaveCharacterCallback;
		public delegate void OnRevertCharacter();
		protected OnRevertCharacter RevertCharacterCallback;

		protected UiPanel uiAppearanceMain;
		protected Textbox uiHeader = new Textbox();
		protected UiPanel contentFrame = new UiPanel();

		protected List<OnMouseMove> inputsOnMouseMove;
		protected List<OnMouseButton> inputsOnMouseButton;
		protected List<OnKey> inputsOnKey;

		public UiAppearance()
		{
			uiAppearanceMain = new UiPanel();
			uiHeader = new Textbox();
		}

		public void SetLogger(ILogger Logger)
		{
			this.Logger = Logger;
		}

		public void SetInput(List<OnMouseMove> inputsOnMouseMove, List<OnMouseButton> inputsOnMouseButton, List<OnKey> inputsOnKey)
		{
			this.inputsOnMouseMove = inputsOnMouseMove;
			this.inputsOnMouseButton = inputsOnMouseButton;
			this.inputsOnKey = inputsOnKey;
		}

		public void SetCamera(UiCamera camera)
		{
			this.camera = camera;
		}

		public virtual void SetUi()
		{ }

		public void Open()
		{
			this.camera.SetMode(cameraMode);
			SetUi();
			uiAppearanceMain.ClearFlags(UiElement.DISABLED);
		}
		public void Close()
		{
			uiAppearanceMain.SetFlags(UiElement.DISABLED);
		}
		public bool IsOpen()
		{
			return ((uiAppearanceMain.GetFlags() & UiElement.DISABLED) == 0);
		}

		public void SetCharacter(Character character)
		{
			this.character = character;
		}

		public void RegisterSaveCharacter(OnSaveCharacter SaveCharacterCallback)
		{
			this.SaveCharacterCallback = SaveCharacterCallback;
		}
		public void RegisterRevertCharacter(OnRevertCharacter RevertCharacterCallback)
		{
			this.RevertCharacterCallback = RevertCharacterCallback;
		}

		public virtual void CreateContent()
		{
		}

		public virtual void CreateColumns()
		{
			
		}

		protected void CreateColumn(UiPanel uiPanel, HGravity gravity, UiPanel uiColumn, string label = null)
		{
			uiColumn.SetOrientation(Orientation.Vertical);
			uiColumn.SetPadding(new UiRectangle(defaultPadding));
			uiColumn.SetGravity(gravity);
			uiColumn.SetFlags(UiElement.NO_DRAW);
			uiPanel.AddElement(uiColumn);

			if (label != null)
			{
				Textbox header = new Textbox();
				header.SetPadding(new UiRectangle(defaultPadding));
				header.SetText(label);
				header.SetFlags(UiElement.NO_DRAW);
				if (label.Length == 0)
				{
					header.SetTextFlags(UiElement.NO_DRAW);
				}
				uiColumn.AddElement(header);
			}
		}

		public void CreateUi(UiPanel uiMain)
		{
			uiAppearanceMain.SetFlags(UiElement.DISABLED);

			uiAppearanceMain.SetOrientation(Orientation.Vertical);
			uiAppearanceMain.SetPadding(new UiRectangle(defaultPadding));
			uiAppearanceMain.SetProperties(UiElement.FLOATING | UiElement.COLLISION_PARENT | UiElement.MOVABLE | UiElement.RESIZEABLE);
			inputsOnMouseMove.Add(uiAppearanceMain.OnCursorMove);
			inputsOnMouseButton.Add(uiAppearanceMain.OnMouseButton);
			uiMain.AddElement(uiAppearanceMain);

			UiPanel uiHeaderPanel = new UiPanel();
			uiHeaderPanel.SetPadding(new UiRectangle(defaultPadding));
			uiHeaderPanel.SetFlags(UiElement.NO_DRAW);
			uiAppearanceMain.AddElement(uiHeaderPanel);

			uiHeader.SetText("Header");
			uiHeader.SetPadding(new UiRectangle(defaultPadding));
			uiHeader.SetFlags(UiElement.NO_DRAW);
			uiHeaderPanel.AddElement(uiHeader);

			contentFrame.SetOrientation(Orientation.Vertical);
			contentFrame.SetPadding(new UiRectangle(defaultPadding));
			contentFrame.SetGravity(HGravity.Left);
			contentFrame.SetFlags(UiElement.NO_DRAW);
			uiAppearanceMain.AddElement(contentFrame);
			
			CreateColumns();
			CreateContent();

			// Buttons
			UiPanel uiButtonsPanel = new UiPanel();
			uiButtonsPanel.SetPadding(new UiRectangle(defaultPadding));
			uiButtonsPanel.SetGravity(HGravity.Left);
			uiButtonsPanel.SetFlags(UiElement.NO_DRAW);
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
