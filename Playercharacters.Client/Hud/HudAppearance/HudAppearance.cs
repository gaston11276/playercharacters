using CitizenFX.Core;
using NFive.SDK.Core.Diagnostics;
using Gaston11276.SimpleUi;
using Gaston11276.Playercharacters.Client.Models;

using System.Threading.Tasks;

namespace Gaston11276.Playercharacters.Client
{
	public class HudAppearance : Hud
	{
		static private UiCamera camera = new UiCamera();
		bool rotatingCamera = false;
		float lastCursorX = 0f;
		float lastCursorY = 0f;

		HudComponentModel hudModel = new HudComponentModel();
		HudComponentHeadBlendData hudHeadBlendData = new HudComponentHeadBlendData();
		HudComponentFaceFeatures hudFaceFeatures = new HudComponentFaceFeatures();
		HudComponentHeadOverlays hudHeadOverlays = new HudComponentHeadOverlays();
		HudComponentDecorations hudDecorations = new HudComponentDecorations();
		HudComponentPedComponents hudPedComponents = new HudComponentPedComponents();
		HudComponentAnimations hudAnimations = new HudComponentAnimations();

		fpVoid SaveCharacterCallback;
		fpVoid RevertCharacterCallback;

		public HudAppearance()
		{
			hudModel.RegisterSaveCharacter(SaveCharacter);
			hudModel.RegisterRevertCharacter(RevertCharacter);
			hudModel.SetInput(inputsOnMouseMove, inputsOnMouseButton, inputsOnKey);

			hudHeadBlendData.RegisterSaveCharacter(SaveCharacter);
			hudHeadBlendData.RegisterRevertCharacter(RevertCharacter);
			hudHeadBlendData.SetInput(inputsOnMouseMove, inputsOnMouseButton, inputsOnKey);

			hudFaceFeatures.RegisterSaveCharacter(SaveCharacter);
			hudFaceFeatures.RegisterRevertCharacter(RevertCharacter);
			hudFaceFeatures.SetInput(inputsOnMouseMove, inputsOnMouseButton, inputsOnKey);

			hudHeadOverlays.RegisterSaveCharacter(SaveCharacter);
			hudHeadOverlays.RegisterRevertCharacter(RevertCharacter);
			hudHeadOverlays.SetInput(inputsOnMouseMove, inputsOnMouseButton, inputsOnKey);

			hudDecorations.RegisterSaveCharacter(SaveCharacter);
			hudDecorations.RegisterRevertCharacter(RevertCharacter);
			hudDecorations.SetInput(inputsOnMouseMove, inputsOnMouseButton, inputsOnKey);

			hudPedComponents.RegisterSaveCharacter(SaveCharacter);
			hudPedComponents.RegisterRevertCharacter(RevertCharacter);
			hudPedComponents.SetInput(inputsOnMouseMove, inputsOnMouseButton, inputsOnKey);

			hudAnimations.RegisterSaveCharacter(SaveCharacter);
			hudAnimations.RegisterRevertCharacter(RevertCharacter);
			hudAnimations.SetInput(inputsOnMouseMove, inputsOnMouseButton, inputsOnKey);
		}

		protected override void OnOpen()
		{
			base.OnOpen();

			camera.SetMode(CameraMode.Front);
			Game.Player.CanControlCharacter = false;
			Game.Player.Character.IsPositionFrozen = true;
			Game.Player.Character.IsInvincible = true;
			Game.Player.Character.Task.ClearAllImmediately();
		}

		protected override void OnClose()
		{
			base.OnClose();

			camera.SetMode(CameraMode.Game);
			Game.Player.CanControlCharacter = true;
			Game.Player.Character.IsPositionFrozen = false;
			Game.Player.Character.IsInvincible = false;
			Game.Player.Character.Task.ClearAllImmediately();
		}

		public override void SetLogger(ILogger Logger)
		{
			base.SetLogger(Logger);
			camera.SetLogger(Logger);
		}

		public void RegisterSaveCallback(fpVoid SaveCharacterCallback)
		{
			this.SaveCharacterCallback = SaveCharacterCallback;
		}

		public void RegisterRevertCallback(fpVoid RevertCharacterCallback)
		{
			this.RevertCharacterCallback = RevertCharacterCallback;
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

				if (state == 3 && keycode == hotkey)//113)// Hotkey F2
				{
					Close();
				}
			}
		}

		public void SetCharacter(Character character)
		{
			hudModel.SetCharacter(character);
			hudHeadBlendData.SetCharacter(character);
			hudFaceFeatures.SetCharacter(character);
			hudHeadOverlays.SetCharacter(character);
			hudDecorations.SetCharacter(character);
			hudPedComponents.SetCharacter(character);
			hudAnimations.SetCharacter(character);
		}

		public override void OnMouseButton(int state, int button, float CursorX, float CursorY)
		{
			base.OnMouseButton(state, button, CursorX, CursorY);

			if (!rotatingCamera && state == 1 && button == 2)
			{
				rotatingCamera = true;
				lastCursorX = CursorX;
				lastCursorY = CursorY;
			}
			else if (rotatingCamera && state == 3 && button == 2)
			{
				rotatingCamera = false;
			}
		}

		public override void OnMouseMove(float CursorX, float CursorY)
		{
			base.OnMouseMove(CursorX, CursorY);
			if (rotatingCamera)
			{
				float AxisX = ((float)(lastCursorX - CursorX)) / 1280f;
				float AxisY = ((float)(lastCursorY - CursorY)) / 1280f;
				camera.UpdateCamera(AxisX, AxisY);
				lastCursorX = CursorX;
				lastCursorY = CursorY;
			}
		}

		public override void CreateUi()
		{
			base.CreateUi();
			hudModel.SetLogger(Logger);
			hudHeadBlendData.SetLogger(Logger);
			hudFaceFeatures.SetLogger(Logger);
			hudHeadOverlays.SetLogger(Logger);
			hudDecorations.SetLogger(Logger);
			hudPedComponents.SetLogger(Logger);
			hudAnimations.SetLogger(Logger);

			hudModel.SetCamera(camera);
			hudHeadBlendData.SetCamera(camera);
			hudFaceFeatures.SetCamera(camera);
			hudHeadOverlays.SetCamera(camera);
			hudDecorations.SetCamera(camera);
			hudPedComponents.SetCamera(camera);
			hudAnimations.SetCamera(camera);

			UiPanel uiLooksPanel = new UiPanel();
			uiLooksPanel.name = "MainPanel";
			uiLooksPanel.SetPadding(new UiRectangle(defaultPadding));
			uiLooksPanel.SetOrientation(Orientation.Vertical);
			uiLooksPanel.SetHDimension(Dimension.Absolute);
			uiLooksPanel.SetVDimension(Dimension.Wrap);
			uiLooksPanel.SetGravity(VGravity.Top);
			uiLooksPanel.SetWidth(.15f);
			uiLooksPanel.SetPosition(0.0f, 0.0f);
			uiLooksPanel.SetAlignment(HAlignment.Left);
			uiLooksPanel.SetAlignment(VAlignment.Top);
			uiLooksPanel.SetProperties(UiElement.FLOATING | UiElement.COLLISION_PARENT | UiElement.MOVABLE);
			inputsOnMouseMove.Add(uiLooksPanel.OnCursorMove);
			inputsOnMouseButton.Add(uiLooksPanel.OnMouseButton);
			uiMain.AddElement(uiLooksPanel);

			Textbox buttonModel= new Textbox();
			buttonModel.SetText("Model");
			buttonModel.SetPadding(new UiRectangle(defaultPadding));
			buttonModel.SetHDimension(Dimension.Max);
			buttonModel.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			buttonModel.RegisterOnSelectCallback(hudModel.Open);
			buttonModel.RegisterOffSelectCallback(hudModel.Close);
			inputsOnMouseMove.Add(buttonModel.OnCursorMove);
			inputsOnMouseButton.Add(buttonModel.OnMouseButton);
			uiLooksPanel.AddElement(buttonModel);
			hudModel.RegisterOnCloseCallback(buttonModel.Deselect);

			Textbox buttonHeadBlendData= new Textbox();
			buttonHeadBlendData.SetText("Head Blend Data");
			buttonHeadBlendData.SetPadding(new UiRectangle(defaultPadding));
			buttonHeadBlendData.SetMargin(new UiRectangle(0f, -0.004f, 0f, 0f));
			buttonHeadBlendData.SetHDimension(Dimension.Max);
			buttonHeadBlendData.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			buttonHeadBlendData.RegisterOnSelectCallback(hudHeadBlendData.Open);
			buttonHeadBlendData.RegisterOffSelectCallback(hudHeadBlendData.Close);
			inputsOnMouseMove.Add(buttonHeadBlendData.OnCursorMove);
			inputsOnMouseButton.Add(buttonHeadBlendData.OnMouseButton);
			uiLooksPanel.AddElement(buttonHeadBlendData);
			hudHeadBlendData.RegisterOnCloseCallback(buttonHeadBlendData.Deselect);

			Textbox buttonFaceFeatures = new Textbox();
			buttonFaceFeatures.SetText("Face Features");
			buttonFaceFeatures.SetPadding(new UiRectangle(defaultPadding));
			buttonFaceFeatures.SetMargin(new UiRectangle(0f, -0.004f, 0f, 0f));
			buttonFaceFeatures.SetHDimension(Dimension.Max);
			buttonFaceFeatures.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			buttonFaceFeatures.RegisterOnSelectCallback(hudFaceFeatures.Open);
			buttonFaceFeatures.RegisterOffSelectCallback(hudFaceFeatures.Close);
			inputsOnMouseMove.Add(buttonFaceFeatures.OnCursorMove);
			inputsOnMouseButton.Add(buttonFaceFeatures.OnMouseButton);
			uiLooksPanel.AddElement(buttonFaceFeatures);
			hudFaceFeatures.RegisterOnCloseCallback(buttonFaceFeatures.Deselect);

			Textbox buttonHeadOverlays = new Textbox();
			buttonHeadOverlays.SetText("Head Overlays");
			buttonHeadOverlays.SetPadding(new UiRectangle(defaultPadding));
			buttonHeadOverlays.SetMargin(new UiRectangle(0f, -0.004f, 0f, 0f));
			buttonHeadOverlays.SetHDimension(Dimension.Max);
			buttonHeadOverlays.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			buttonHeadOverlays.RegisterOnSelectCallback(hudHeadOverlays.Open);
			buttonHeadOverlays.RegisterOffSelectCallback(hudHeadOverlays.Close);
			inputsOnMouseMove.Add(buttonHeadOverlays.OnCursorMove);
			inputsOnMouseButton.Add(buttonHeadOverlays.OnMouseButton);
			uiLooksPanel.AddElement(buttonHeadOverlays);
			hudHeadOverlays.RegisterOnCloseCallback(buttonHeadOverlays.Deselect);

			Textbox buttonDecorations = new Textbox();
			buttonDecorations.SetText("Decorations");
			buttonDecorations.SetPadding(new UiRectangle(defaultPadding));
			buttonDecorations.SetMargin(new UiRectangle(0f, -0.004f, 0f, 0f));
			buttonDecorations.SetHDimension(Dimension.Max);
			buttonDecorations.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			buttonDecorations.RegisterOnSelectCallback(hudDecorations.Open);
			buttonDecorations.RegisterOffSelectCallback(hudDecorations.Close);
			inputsOnMouseMove.Add(buttonDecorations.OnCursorMove);
			inputsOnMouseButton.Add(buttonDecorations.OnMouseButton);
			uiLooksPanel.AddElement(buttonDecorations);
			hudDecorations.RegisterOnCloseCallback(buttonDecorations.Deselect);

			Textbox buttonComponents= new Textbox();
			buttonComponents.SetText("Components");
			buttonComponents.SetPadding(new UiRectangle(defaultPadding));
			buttonComponents.SetMargin(new UiRectangle(0f, -0.004f, 0f, 0f));
			buttonComponents.SetHDimension(Dimension.Max);
			buttonComponents.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			buttonComponents.RegisterOnSelectCallback(hudPedComponents.Open);
			buttonComponents.RegisterOffSelectCallback(hudPedComponents.Close);
			inputsOnMouseMove.Add(buttonComponents.OnCursorMove);
			inputsOnMouseButton.Add(buttonComponents.OnMouseButton);
			uiLooksPanel.AddElement(buttonComponents);
			hudPedComponents.RegisterOnCloseCallback(buttonComponents.Deselect);

			Textbox buttonAnimations = new Textbox();
			buttonAnimations.SetText("Animations");
			buttonAnimations.SetPadding(new UiRectangle(defaultPadding));
			buttonAnimations.SetMargin(new UiRectangle(0f, -0.004f, 0f, 0f));
			buttonAnimations.SetHDimension(Dimension.Max);
			buttonAnimations.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			buttonAnimations.RegisterOnSelectCallback(hudAnimations.Open);
			buttonAnimations.RegisterOffSelectCallback(hudAnimations.Close);
			inputsOnMouseMove.Add(buttonAnimations.OnCursorMove);
			inputsOnMouseButton.Add(buttonAnimations.OnMouseButton);
			uiLooksPanel.AddElement(buttonAnimations);
			hudAnimations.RegisterOnCloseCallback(buttonAnimations.Deselect);

			hudModel.CreateUi(uiMain);
			hudHeadBlendData.CreateUi(uiMain);
			hudFaceFeatures.CreateUi(uiMain);
			hudHeadOverlays.CreateUi(uiMain);
			hudDecorations.CreateUi(uiMain);
			hudPedComponents.CreateUi(uiMain);
			hudAnimations.CreateUi(uiMain);
		}

		private void SaveCharacter()
		{
			SaveCharacterCallback();
		}
		private void RevertCharacter()
		{
			RevertCharacterCallback();
		}

		public void ApplyToPed()
		{
			//uiModel.ApplyToPed();
			hudHeadBlendData.ApplyToPed();
			hudFaceFeatures.ApplyToPed();
			hudHeadOverlays.ApplyToPed();
			hudDecorations.ApplyToPed();
			hudPedComponents.ApplyToPed();
			hudAnimations.ApplyToPed();
		}
	
		public override void SetDelay(fpDelay Delay)
		{
			this.Delay = Delay;

			hudModel.SetDelay(Delay);
			hudHeadBlendData.SetDelay(Delay);
			hudFaceFeatures.SetDelay(Delay);
			hudHeadOverlays.SetDelay(Delay);
			hudDecorations.SetDelay(Delay);
			hudPedComponents.SetDelay(Delay);
			hudAnimations.SetDelay(Delay);
		}
		
		public override async Task RefreshUi()
		{
			hudModel.SetUi();
			hudHeadBlendData.SetUi();
			hudFaceFeatures.SetUi();
			hudHeadOverlays.SetUi();
			hudDecorations.SetUi();
			hudPedComponents.SetUi();
			hudAnimations.SetUi();
			await Delay(1);
		}
	}
}
