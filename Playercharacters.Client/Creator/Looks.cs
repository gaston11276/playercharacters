using CitizenFX.Core;
using NFive.SDK.Core.Diagnostics;
using Gaston11276.SimpleUi;
using Gaston11276.Playercharacters.Client.Models;

namespace Gaston11276.Playercharacters.Client
{
	public class Looks : Hud
	{
		static private UiCamera camera = new UiCamera();

		UiModel uiModel;
		UiHeadBlendData uiHeadBlendData;
		UiFaceFeatures uiFaceFeatures;
		UiHeadOverlays uiHeadOverlays;
		UiDecorations uiDecorations;
		UiComponents uiComponents;
		UiAnimations uiAnimations;


		public delegate void OnSaveCharacter();
		OnSaveCharacter SaveCharacterCallback;
		public delegate void OnRevertCharacter();
		OnRevertCharacter RevertCharacterCallback;

		public Looks()
		{
			uiModel = new UiModel();
			uiModel.RegisterSaveCharacter(SaveCharacter);
			uiModel.RegisterRevertCharacter(RevertCharacter);
			uiModel.SetInput(inputsOnMouseMove, inputsOnLeftMouseButton, inputsOnKey);

			uiHeadBlendData = new UiHeadBlendData();
			uiHeadBlendData.RegisterSaveCharacter(SaveCharacter);
			uiHeadBlendData.RegisterRevertCharacter(RevertCharacter);
			uiHeadBlendData.SetInput(inputsOnMouseMove, inputsOnLeftMouseButton, inputsOnKey);

			uiFaceFeatures = new UiFaceFeatures();
			uiFaceFeatures.RegisterSaveCharacter(SaveCharacter);
			uiFaceFeatures.RegisterRevertCharacter(RevertCharacter);
			uiFaceFeatures.SetInput(inputsOnMouseMove, inputsOnLeftMouseButton, inputsOnKey);

			uiHeadOverlays = new UiHeadOverlays();
			uiHeadOverlays.RegisterSaveCharacter(SaveCharacter);
			uiHeadOverlays.RegisterRevertCharacter(RevertCharacter);
			uiHeadOverlays.SetInput(inputsOnMouseMove, inputsOnLeftMouseButton, inputsOnKey);

			uiDecorations = new UiDecorations();
			uiDecorations.RegisterSaveCharacter(SaveCharacter);
			uiDecorations.RegisterRevertCharacter(RevertCharacter);
			uiDecorations.SetInput(inputsOnMouseMove, inputsOnLeftMouseButton, inputsOnKey);

			uiComponents = new UiComponents();
			uiComponents.RegisterSaveCharacter(SaveCharacter);
			uiComponents.RegisterRevertCharacter(RevertCharacter);
			uiComponents.SetInput(inputsOnMouseMove, inputsOnLeftMouseButton, inputsOnKey);

			uiAnimations = new UiAnimations();
			uiAnimations.RegisterSaveCharacter(SaveCharacter);
			uiAnimations.RegisterRevertCharacter(RevertCharacter);
			uiAnimations.SetInput(inputsOnMouseMove, inputsOnLeftMouseButton, inputsOnKey);
		}

		public override void SetLogger(ILogger Logger)
		{
			base.SetLogger(Logger);
			camera.SetLogger(Logger);
		}

		public void RegisterSaveCallback(OnSaveCharacter SaveCharacterCallback)
		{
			this.SaveCharacterCallback = SaveCharacterCallback;
		}

		public void RegisterRevertCallback(OnRevertCharacter RevertCharacterCallback)
		{
			this.RevertCharacterCallback = RevertCharacterCallback;
		}

		public void SetCharacter(Character character)
		{
			uiModel.SetCharacter(character);
			uiHeadBlendData.SetCharacter(character);
			uiFaceFeatures.SetCharacter(character);
			uiHeadOverlays.SetCharacter(character);
			uiDecorations.SetCharacter(character);
			uiComponents.SetCharacter(character);
			uiAnimations.SetCharacter(character);
		}

		public override void Open()
		{
			uiMain.ClearFlags(UiElement.DISABLED);
			camera.SetMode(CameraMode.Front);

			Game.Player.CanControlCharacter = false;
			Game.Player.Character.IsPositionFrozen = true;
			Game.Player.Character.IsInvincible = true;
			Game.Player.Character.Task.ClearAllImmediately();
		}

		public override void Close ()
		{
			uiMain.SetFlags(UiElement.DISABLED);
			camera.SetMode(CameraMode.Game);

			Game.Player.CanControlCharacter = true;
			Game.Player.Character.IsPositionFrozen = false;
			Game.Player.Character.IsInvincible = false;
			Game.Player.Character.Task.ClearAllImmediately();
		}

		public override void OnInputRMBMouseMoveAxis(int state, float AxisX, float AxisY)
		{
			if (state == 2)
			{
				camera.UpdateCamera(AxisX, AxisY);
			}
		}

		public override void CreateUi()
		{
			base.CreateUi();
			uiModel.SetLogger(Logger);
			uiHeadBlendData.SetLogger(Logger);
			uiFaceFeatures.SetLogger(Logger);
			uiHeadOverlays.SetLogger(Logger);
			uiDecorations.SetLogger(Logger);
			uiComponents.SetLogger(Logger);
			uiAnimations.SetLogger(Logger);

			uiModel.SetCamera(camera);
			uiHeadBlendData.SetCamera(camera);
			uiFaceFeatures.SetCamera(camera);
			uiHeadOverlays.SetCamera(camera);
			uiDecorations.SetCamera(camera);
			uiComponents.SetCamera(camera);
			uiAnimations.SetCamera(camera);

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
			inputsOnLeftMouseButton.Add(uiLooksPanel.OnLeftMouseButton);
			uiMain.AddElement(uiLooksPanel);

			Textbox buttonModel= new Textbox();
			buttonModel.SetText("Model");
			buttonModel.SetPadding(new UiRectangle(defaultPadding));
			buttonModel.SetHDimension(Dimension.Max);
			buttonModel.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			buttonModel.RegisterOnSelectCallback(CloseAll);
			buttonModel.RegisterOnSelectCallback(uiModel.Open);
			buttonModel.RegisterOffSelectCallback(uiModel.Close);
			inputsOnMouseMove.Add(buttonModel.OnCursorMove);
			inputsOnLeftMouseButton.Add(buttonModel.OnLeftMouseButton);
			uiLooksPanel.AddElement(buttonModel);

			Textbox buttonHeadBlendData= new Textbox();
			buttonHeadBlendData.SetText("Head Blend Data");
			buttonHeadBlendData.SetPadding(new UiRectangle(defaultPadding));
			buttonHeadBlendData.SetMargin(new UiRectangle(0f, -0.004f, 0f, 0f));
			buttonHeadBlendData.SetHDimension(Dimension.Max);
			buttonHeadBlendData.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			buttonHeadBlendData.RegisterOnSelectCallback(CloseAll);
			buttonHeadBlendData.RegisterOnSelectCallback(uiHeadBlendData.Open);
			buttonHeadBlendData.RegisterOffSelectCallback(uiHeadBlendData.Close);
			inputsOnMouseMove.Add(buttonHeadBlendData.OnCursorMove);
			inputsOnLeftMouseButton.Add(buttonHeadBlendData.OnLeftMouseButton);
			uiLooksPanel.AddElement(buttonHeadBlendData);

			Textbox buttonFaceFeatures = new Textbox();
			buttonFaceFeatures.SetText("Face Features");
			buttonFaceFeatures.SetPadding(new UiRectangle(defaultPadding));
			buttonFaceFeatures.SetMargin(new UiRectangle(0f, -0.004f, 0f, 0f));
			buttonFaceFeatures.SetHDimension(Dimension.Max);
			buttonFaceFeatures.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			buttonFaceFeatures.RegisterOnSelectCallback(CloseAll);
			buttonFaceFeatures.RegisterOnSelectCallback(uiFaceFeatures.Open);
			buttonFaceFeatures.RegisterOffSelectCallback(uiFaceFeatures.Close);
			inputsOnMouseMove.Add(buttonFaceFeatures.OnCursorMove);
			inputsOnLeftMouseButton.Add(buttonFaceFeatures.OnLeftMouseButton);
			uiLooksPanel.AddElement(buttonFaceFeatures);

			Textbox buttonHeadOverlays = new Textbox();
			buttonHeadOverlays.SetText("Head Overlays");
			buttonHeadOverlays.SetPadding(new UiRectangle(defaultPadding));
			buttonHeadOverlays.SetMargin(new UiRectangle(0f, -0.004f, 0f, 0f));
			buttonHeadOverlays.SetHDimension(Dimension.Max);
			buttonHeadOverlays.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			buttonHeadOverlays.RegisterOnSelectCallback(CloseAll);
			buttonHeadOverlays.RegisterOnSelectCallback(uiHeadOverlays.Open);
			buttonHeadOverlays.RegisterOffSelectCallback(uiHeadOverlays.Close);
			inputsOnMouseMove.Add(buttonHeadOverlays.OnCursorMove);
			inputsOnLeftMouseButton.Add(buttonHeadOverlays.OnLeftMouseButton);
			uiLooksPanel.AddElement(buttonHeadOverlays);

			Textbox buttonDecorations = new Textbox();
			buttonDecorations.SetText("Decorations");
			buttonDecorations.SetPadding(new UiRectangle(defaultPadding));
			buttonDecorations.SetMargin(new UiRectangle(0f, -0.004f, 0f, 0f));
			buttonDecorations.SetHDimension(Dimension.Max);
			buttonDecorations.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			buttonDecorations.RegisterOnSelectCallback(CloseAll);
			buttonDecorations.RegisterOnSelectCallback(uiDecorations.Open);
			buttonDecorations.RegisterOffSelectCallback(uiDecorations.Close);
			inputsOnMouseMove.Add(buttonDecorations.OnCursorMove);
			inputsOnLeftMouseButton.Add(buttonDecorations.OnLeftMouseButton);
			uiLooksPanel.AddElement(buttonDecorations);

			Textbox buttonComponents= new Textbox();
			buttonComponents.SetText("Components");
			buttonComponents.SetPadding(new UiRectangle(defaultPadding));
			buttonComponents.SetMargin(new UiRectangle(0f, -0.004f, 0f, 0f));
			buttonComponents.SetHDimension(Dimension.Max);
			buttonComponents.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			buttonComponents.RegisterOnSelectCallback(CloseAll);
			buttonComponents.RegisterOnSelectCallback(uiComponents.Open);
			buttonComponents.RegisterOffSelectCallback(uiComponents.Close);
			inputsOnMouseMove.Add(buttonComponents.OnCursorMove);
			inputsOnLeftMouseButton.Add(buttonComponents.OnLeftMouseButton);
			uiLooksPanel.AddElement(buttonComponents);

			Textbox buttonAnimations = new Textbox();
			buttonAnimations.SetText("Animations");
			buttonAnimations.SetPadding(new UiRectangle(defaultPadding));
			buttonAnimations.SetMargin(new UiRectangle(0f, -0.004f, 0f, 0f));
			buttonAnimations.SetHDimension(Dimension.Max);
			buttonAnimations.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			buttonAnimations.RegisterOnSelectCallback(CloseAll);
			buttonAnimations.RegisterOnSelectCallback(uiAnimations.Open);
			buttonAnimations.RegisterOffSelectCallback(uiAnimations.Close);
			inputsOnMouseMove.Add(buttonAnimations.OnCursorMove);
			inputsOnLeftMouseButton.Add(buttonAnimations.OnLeftMouseButton);
			uiLooksPanel.AddElement(buttonAnimations);

			uiModel.CreateUi(uiMain);
			uiHeadBlendData.CreateUi(uiMain);
			uiFaceFeatures.CreateUi(uiMain);
			uiHeadOverlays.CreateUi(uiMain);
			uiDecorations.CreateUi(uiMain);
			uiComponents.CreateUi(uiMain);
			uiAnimations.CreateUi(uiMain);

		}

		private void SaveCharacter()
		{
			SaveCharacterCallback();
		}
		private void RevertCharacter()
		{
			RevertCharacterCallback();
		}

		private void CloseAll()
		{
			uiModel.Close();
			uiHeadBlendData.Close();
			uiFaceFeatures.Close();
			uiHeadOverlays.Close();
			uiDecorations.Close();
			uiComponents.Close();
			uiAnimations.Close();
		}

		public void ApplyToPed()
		{
			//uiModel.ApplyToPed();
			uiHeadBlendData.ApplyToPed();
			uiFaceFeatures.ApplyToPed();
			uiHeadOverlays.ApplyToPed();
			uiDecorations.ApplyToPed();
			uiComponents.ApplyToPed();
			uiAnimations.ApplyToPed();
		}
	}
}
