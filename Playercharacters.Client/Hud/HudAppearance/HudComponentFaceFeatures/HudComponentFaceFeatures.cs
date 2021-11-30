using CitizenFX.Core;
using CitizenFX.Core.Native;
using Gaston11276.SimpleUi;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Client
{
	public class HudComponentFaceFeatures : HudComponent
	{
		// Columns
		UiPanel uiColumnLabels = new UiPanel();
		UiPanel uiColumnValues = new UiPanel();
		UiPanel uiColumnDecrease = new UiPanel();
		UiPanel uiColumnIncrease = new UiPanel();

		UiEntryFaceFeature NoseWidth;
		UiEntryFaceFeature NosePeakLength;
		UiEntryFaceFeature NosePeakHeight;
		UiEntryFaceFeature NoseBoneHeight;
		UiEntryFaceFeature NosePeakLowering;
		UiEntryFaceFeature NoseBoneTwist;
		UiEntryFaceFeature EyeBrowHeight;
		UiEntryFaceFeature EyeBrowForward;
		UiEntryFaceFeature EyesOpening;
		UiEntryFaceFeature LipThickness;
		UiEntryFaceFeature CheekWidth;
		UiEntryFaceFeature CheekBoneWidth;
		UiEntryFaceFeature CheekBoneHeight;
		UiEntryFaceFeature ChinBoneWidth;
		UiEntryFaceFeature ChinBoneLength;
		UiEntryFaceFeature ChinBoneLowering;
		UiEntryFaceFeature ChinDimple;
		UiEntryFaceFeature JawBoneLength;
		UiEntryFaceFeature JawBoneWidth;
		UiEntryFaceFeature NeckThickness;

		public HudComponentFaceFeatures()
		{
			cameraMode = CameraMode.Face;
		}

		public override async void SetUi()
		{
			await NoseWidth.SetUi();
			await NosePeakLength.SetUi();
			await NosePeakHeight.SetUi();
			await NoseBoneHeight.SetUi();
			await NosePeakLowering.SetUi();
			await NoseBoneTwist.SetUi();
			await EyeBrowHeight.SetUi();
			await EyeBrowForward.SetUi();
			await EyesOpening.SetUi();
			await LipThickness.SetUi();
			await CheekWidth.SetUi();
			await CheekBoneWidth.SetUi();
			await CheekBoneHeight.SetUi();
			await ChinBoneWidth.SetUi();
			await ChinBoneLength.SetUi();
			await ChinBoneLowering.SetUi();
			await ChinDimple.SetUi();
			await JawBoneLength.SetUi();
			await JawBoneWidth.SetUi();
			await NeckThickness.SetUi();
		}

		private UiEntryFaceFeature CreateFaceFeature(PedFaceFeatureType type, string label)
		{
			cameraMode = CameraMode.Face;

			float defaultPadding = 0.0025f;

			UiEntryFaceFeature entry = new UiEntryFaceFeature();
			entry.SetDelay(Delay);
			entry.type = type;

			entry.uiLabel.SetPadding(new UiRectangle(defaultPadding));
			entry.uiLabel.SetText(label);
			entry.uiLabel.SetFlags(UiElement.TRANSPARENT);
			uiColumnLabels.AddElement(entry.uiLabel);

			entry.uiValue.SetPadding(new UiRectangle(defaultPadding));
			entry.uiValue.SetFlags(UiElement.TRANSPARENT);
			uiColumnValues.AddElement(entry.uiValue);

			entry.btnDecrease.SetText("-");
			entry.btnDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnDecrease.RegisterOnLMBRelease(entry.Decrease);
			inputsOnMouseMove.Add(entry.btnDecrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnDecrease.OnMouseButton);
			uiColumnDecrease.AddElement(entry.btnDecrease);

			entry.btnIncrease.SetText("+");
			entry.btnIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnIncrease.RegisterOnLMBRelease(entry.Increase);
			inputsOnMouseMove.Add(entry.btnIncrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnIncrease.OnMouseButton);
			uiColumnIncrease.AddElement(entry.btnIncrease);

			entry.SetValue = SetFaceFeatureValue;
			entry.GetValue = GetFaceFeatureValue;

			return entry;
		}

		protected override void CreateColumns()
		{
			UiPanel uiCenterPanel = new UiPanel();
			uiCenterPanel.SetPadding(new UiRectangle(defaultPadding));
			contentFrame.AddElement(uiCenterPanel);

			CreateColumn(uiCenterPanel, HGravity.Left, uiColumnLabels);
			CreateColumn(uiCenterPanel, HGravity.Right, uiColumnValues);
			CreateColumn(uiCenterPanel, HGravity.Center, uiColumnDecrease);
			CreateColumn(uiCenterPanel, HGravity.Center, uiColumnIncrease);
		}

		protected override void CreateContent()
		{
			uiHeader.SetText("Face Features");
			uiAppearanceMain.SetAlignment(HAlignment.Right);  // Make buttons (right-aligned) appear at the same location regardless of position or size
			uiAppearanceMain.SetAlignment(VAlignment.Bottom);   // Make buttons (bottom-aligned) appear at the same location regardless of position or size

			NoseWidth = CreateFaceFeature(PedFaceFeatureType.NoseWidth, "NoseWidth");
			NosePeakLength = CreateFaceFeature(PedFaceFeatureType.NosePeakLength, "NosePeakLength");
			NosePeakHeight = CreateFaceFeature(PedFaceFeatureType.NosePeakHeight, "NosePeakHeight");
			NoseBoneHeight = CreateFaceFeature(PedFaceFeatureType.NoseBoneHeight, "NoseBoneHeight");
			NosePeakLowering = CreateFaceFeature(PedFaceFeatureType.NosePeakLowering, "NosePeakLowering");
			NoseBoneTwist = CreateFaceFeature(PedFaceFeatureType.NoseBoneTwist, "NoseBoneTwist");
			EyeBrowHeight = CreateFaceFeature(PedFaceFeatureType.EyeBrowHeight, "EyeBrowHeight");
			EyeBrowForward = CreateFaceFeature(PedFaceFeatureType.EyeBrowForward, "EyeBrowForward");
			EyesOpening = CreateFaceFeature(PedFaceFeatureType.EyesOpening, "EyesOpening");
			LipThickness = CreateFaceFeature(PedFaceFeatureType.LipThickness, "LipThickness");
			CheekWidth = CreateFaceFeature(PedFaceFeatureType.CheekWidth, "CheekWidth");
			CheekBoneWidth = CreateFaceFeature(PedFaceFeatureType.CheekBoneWidth, "CheekBoneWidth");
			CheekBoneHeight = CreateFaceFeature(PedFaceFeatureType.CheekBoneHeight, "CheekBoneHeight");
			ChinBoneWidth = CreateFaceFeature(PedFaceFeatureType.ChinBoneWidth, "ChinBoneWidth");
			ChinBoneLength = CreateFaceFeature(PedFaceFeatureType.ChinBoneLength, "ChinBoneLength");
			ChinBoneLowering = CreateFaceFeature(PedFaceFeatureType.ChinBoneLowering, "ChinBoneLowering");
			ChinDimple = CreateFaceFeature(PedFaceFeatureType.ChinDimple, "ChinDimple");
			JawBoneLength = CreateFaceFeature(PedFaceFeatureType.JawBoneLength, "JawBoneLength");
			JawBoneWidth = CreateFaceFeature(PedFaceFeatureType.JawBoneWidth, "JawBoneWidth");
			NeckThickness = CreateFaceFeature(PedFaceFeatureType.NeckThickness, "NeckThickness");

			CreateApplyCancelButtons();
		}

		public float GetFaceFeatureValue(PedFaceFeatureType type)
		{
			switch (type)
			{
				case PedFaceFeatureType.NoseWidth:
					return character.PedFaceFeatures.NoseWidth;
				case PedFaceFeatureType.NosePeakHeight:
					return character.PedFaceFeatures.NosePeakHeight;
				case PedFaceFeatureType.NosePeakLength:
					return character.PedFaceFeatures.NosePeakLength;
				case PedFaceFeatureType.NoseBoneHeight:
					return character.PedFaceFeatures.NoseBoneHeight;
				case PedFaceFeatureType.NosePeakLowering:
					return character.PedFaceFeatures.NosePeakLowering;
				case PedFaceFeatureType.NoseBoneTwist:
					return character.PedFaceFeatures.NoseBoneTwist;
				case PedFaceFeatureType.EyeBrowHeight:
					return character.PedFaceFeatures.EyeBrowHeight;
				case PedFaceFeatureType.EyeBrowForward:
					return character.PedFaceFeatures.EyeBrowForward;
				case PedFaceFeatureType.CheekBoneHeight:
					return character.PedFaceFeatures.CheekBoneHeight;
				case PedFaceFeatureType.CheekBoneWidth:
					return character.PedFaceFeatures.CheekBoneWidth;
				case PedFaceFeatureType.CheekWidth:
					return character.PedFaceFeatures.CheekWidth;
				case PedFaceFeatureType.EyesOpening:
					return character.PedFaceFeatures.EyesOpening;
				case PedFaceFeatureType.LipThickness:
					return character.PedFaceFeatures.LipThickness;
				case PedFaceFeatureType.JawBoneWidth:
					return character.PedFaceFeatures.JawBoneWidth;
				case PedFaceFeatureType.JawBoneLength:
					return character.PedFaceFeatures.JawBoneLength;
				case PedFaceFeatureType.ChinBoneLowering:
					return character.PedFaceFeatures.ChinBoneLowering;
				case PedFaceFeatureType.ChinBoneLength:
					return character.PedFaceFeatures.ChinBoneLength;
				case PedFaceFeatureType.ChinBoneWidth:
					return character.PedFaceFeatures.ChinBoneWidth;
				case PedFaceFeatureType.ChinDimple:
					return character.PedFaceFeatures.ChinDimple;
				case PedFaceFeatureType.NeckThickness:
					return character.PedFaceFeatures.NeckThickness;
				default:
					return 0f;
			}
		}

		public void SetFaceFeatureValue(PedFaceFeatureType type, float value)
		{
			switch (type)
			{
				case PedFaceFeatureType.NoseWidth:
					character.PedFaceFeatures.NoseWidth = value;
					break;
				case PedFaceFeatureType.NosePeakHeight:
					character.PedFaceFeatures.NosePeakHeight = value;
					break;
				case PedFaceFeatureType.NosePeakLength:
					character.PedFaceFeatures.NosePeakLength = value;
					break;
				case PedFaceFeatureType.NoseBoneHeight:
					character.PedFaceFeatures.NoseBoneHeight = value;
					break;
				case PedFaceFeatureType.NosePeakLowering:
					character.PedFaceFeatures.NosePeakLowering = value;
					break;
				case PedFaceFeatureType.NoseBoneTwist:
					character.PedFaceFeatures.NoseBoneTwist = value;
					break;
				case PedFaceFeatureType.EyeBrowHeight:
					character.PedFaceFeatures.EyeBrowHeight = value;
					break;
				case PedFaceFeatureType.EyeBrowForward:
					character.PedFaceFeatures.EyeBrowForward = value;
					break;
				case PedFaceFeatureType.CheekBoneHeight:
					character.PedFaceFeatures.CheekBoneHeight = value;
					break;
				case PedFaceFeatureType.CheekBoneWidth:
					character.PedFaceFeatures.CheekBoneWidth = value;
					break;
				case PedFaceFeatureType.CheekWidth:
					character.PedFaceFeatures.CheekWidth = value;
					break;
				case PedFaceFeatureType.EyesOpening:
					character.PedFaceFeatures.EyesOpening = value;
					break;
				case PedFaceFeatureType.LipThickness:
					character.PedFaceFeatures.LipThickness = value;
					break;
				case PedFaceFeatureType.JawBoneWidth:
					character.PedFaceFeatures.JawBoneWidth = value;
					break;
				case PedFaceFeatureType.JawBoneLength:
					character.PedFaceFeatures.JawBoneLength = value;
					break;
				case PedFaceFeatureType.ChinBoneLowering:
					character.PedFaceFeatures.ChinBoneLowering = value;
					break;
				case PedFaceFeatureType.ChinBoneLength:
					character.PedFaceFeatures.ChinBoneLength = value;
					break;
				case PedFaceFeatureType.ChinBoneWidth:
					character.PedFaceFeatures.ChinBoneWidth = value;
					break;
				case PedFaceFeatureType.ChinDimple:
					character.PedFaceFeatures.ChinDimple = value;
					break;
				case PedFaceFeatureType.NeckThickness:
					character.PedFaceFeatures.NeckThickness = value;
					break;
				default:
					break;
			}
			ApplyToPed(type, value);
		}

		public void ApplyToPed(PedFaceFeatureType type, float value)
		{
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)type, value);
		}

		public void ApplyToPed()
		{
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.NoseWidth, character.PedFaceFeatures.NoseWidth);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.NosePeakHeight, character.PedFaceFeatures.NosePeakHeight);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.NosePeakLength, character.PedFaceFeatures.NosePeakLength);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.NoseBoneHeight, character.PedFaceFeatures.NoseBoneHeight);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.NosePeakLowering, character.PedFaceFeatures.NosePeakLowering);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.NoseBoneTwist, character.PedFaceFeatures.NoseBoneTwist);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.EyeBrowHeight, character.PedFaceFeatures.EyeBrowHeight);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.EyeBrowForward, character.PedFaceFeatures.EyeBrowForward);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.CheekBoneHeight, character.PedFaceFeatures.CheekBoneHeight);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.CheekBoneWidth, character.PedFaceFeatures.CheekBoneWidth);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.CheekWidth, character.PedFaceFeatures.CheekWidth);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.EyesOpening, character.PedFaceFeatures.EyesOpening);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.LipThickness, character.PedFaceFeatures.JawBoneWidth);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.JawBoneWidth, character.PedFaceFeatures.JawBoneWidth);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.JawBoneLength, character.PedFaceFeatures.JawBoneLength);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.ChinBoneLowering, character.PedFaceFeatures.ChinBoneLowering);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.ChinBoneLength, character.PedFaceFeatures.ChinBoneLength);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.ChinBoneWidth, character.PedFaceFeatures.ChinBoneWidth);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.ChinDimple, character.PedFaceFeatures.ChinDimple);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.NeckThickness, character.PedFaceFeatures.NeckThickness);
		}
	}
}
