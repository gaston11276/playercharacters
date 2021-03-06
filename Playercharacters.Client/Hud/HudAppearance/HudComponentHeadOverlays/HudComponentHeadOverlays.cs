using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Gaston11276.SimpleUi;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Client
{
	public class HudComponentHeadOverlays : HudComponent
	{
		// Upper panel columns
		UiPanel uiColumnIndexLabels = new UiPanel();
		UiPanel uiColumnIndexValues = new UiPanel();
		UiPanel uiColumnIndexDecrease = new UiPanel();
		UiPanel uiColumnIndexIncrease = new UiPanel();
		UiPanel uiColumnColorValues = new UiPanel();
		UiPanel uiColumnColorDecrease = new UiPanel();
		UiPanel uiColumnColorIncrease = new UiPanel();
		UiPanel uiColumnOpacityValues = new UiPanel();
		UiPanel uiColumnOpacityDecrease = new UiPanel();
		UiPanel uiColumnOpacityIncrease = new UiPanel();

		// Lower panel columns
		UiPanel uiColumnHairIndexLabels = new UiPanel();
		UiPanel uiColumnHairIndexValues = new UiPanel();
		UiPanel uiColumnHairIndexDecrease = new UiPanel();
		UiPanel uiColumnHairIndexIncrease = new UiPanel();
		UiPanel uiColumnHairColorValues = new UiPanel();
		UiPanel uiColumnHairColorDecrease = new UiPanel();
		UiPanel uiColumnHairColorIncrease = new UiPanel();
		UiPanel uiColumnHairSecondaryColorValues = new UiPanel();
		UiPanel uiColumnHairSecondaryColorDecrease = new UiPanel();
		UiPanel uiColumnHairSecondaryColorIncrease = new UiPanel();

		UiEntryHeadOverlay uiBlemishes;
		UiEntryHeadOverlay uiFacialHair;
		UiEntryHeadOverlay uiEyebrows;
		UiEntryHeadOverlay uiAgeing;
		UiEntryHeadOverlay uiMakeup;
		UiEntryHeadOverlay uiBlush;
		UiEntryHeadOverlay uiComplexion;
		UiEntryHeadOverlay uiSunDamage;
		UiEntryHeadOverlay uiLipstick;
		UiEntryHeadOverlay uiMolesFreckles;
		UiEntryHeadOverlay uiChestHair;
		UiEntryHeadOverlay uiBodyBlemishes;

		UiEntryHair uiHair;
		UiEntryEyeColor uiEyeColor;

		public HudComponentHeadOverlays()
		{
			cameraMode = CameraMode.Face;
		}

		public override async void SetUi()
		{
			await uiBlemishes.SetUi();
			await uiFacialHair.SetUi();
			await uiEyebrows.SetUi();
			await uiAgeing.SetUi();
			await uiMakeup.SetUi();
			await uiBlush.SetUi();
			await uiComplexion.SetUi();
			await uiSunDamage.SetUi();
			await uiLipstick.SetUi();
			await uiMolesFreckles.SetUi();
			await uiChestHair.SetUi();
			await uiBodyBlemishes.SetUi();
			await uiHair.SetUi();
			await uiEyeColor.SetUi();
		}

		public async Task SetDefaults()
		{	
			character.PedHeadOverlays.Eyebrows.Opacity = 1f;

			if (character.Gender == (short)Gender.Female)
			{
				character.PedHeadOverlays.Eyebrows.Index = 10;
			}
				

			await Delay(10);
		}

		protected override void CreateColumns()
		{
			// Upper panel
			UiPanel uiCenterPanel = new UiPanel();
			uiCenterPanel.SetPadding(new UiRectangle(defaultPadding));
			contentFrame.AddElement(uiCenterPanel);

			CreateColumn(uiCenterPanel, HGravity.Left, uiColumnIndexLabels, "");
			CreateColumn(uiCenterPanel, HGravity.Right, uiColumnIndexValues, "");
			CreateColumn(uiCenterPanel, HGravity.Center, uiColumnIndexDecrease, "");
			CreateColumn(uiCenterPanel, HGravity.Center, uiColumnIndexIncrease, "");
			CreateColumn(uiCenterPanel, HGravity.Right, uiColumnColorValues, "Color");
			CreateColumn(uiCenterPanel, HGravity.Center, uiColumnColorDecrease, "");
			CreateColumn(uiCenterPanel, HGravity.Center, uiColumnColorIncrease, "");
			CreateColumn(uiCenterPanel, HGravity.Right, uiColumnOpacityValues, "Opacity");
			CreateColumn(uiCenterPanel, HGravity.Center, uiColumnOpacityDecrease, "");
			CreateColumn(uiCenterPanel, HGravity.Center, uiColumnOpacityIncrease, "");
			
			// Lower panel
			UiPanel uiLowerPanel = new UiPanel();
			uiLowerPanel.SetPadding(new UiRectangle(defaultPadding));
			contentFrame.AddElement(uiLowerPanel);

			CreateColumn(uiLowerPanel, HGravity.Left, uiColumnHairIndexLabels, "");
			CreateColumn(uiLowerPanel, HGravity.Right, uiColumnHairIndexValues, "");
			CreateColumn(uiLowerPanel, HGravity.Center, uiColumnHairIndexDecrease, "");
			CreateColumn(uiLowerPanel, HGravity.Center, uiColumnHairIndexIncrease, "");
			CreateColumn(uiLowerPanel, HGravity.Right, uiColumnHairColorValues, "Color");
			CreateColumn(uiLowerPanel, HGravity.Center, uiColumnHairColorDecrease, "");
			CreateColumn(uiLowerPanel, HGravity.Center, uiColumnHairColorIncrease, "");
			CreateColumn(uiLowerPanel, HGravity.Right, uiColumnHairSecondaryColorValues, "Highlight");
			CreateColumn(uiLowerPanel, HGravity.Center, uiColumnHairSecondaryColorDecrease, "");
			CreateColumn(uiLowerPanel, HGravity.Center, uiColumnHairSecondaryColorIncrease, "");
		}

		protected override void CreateContent()
		{
			uiHeader.SetText("Head Overlays");
			uiAppearanceMain.SetAlignment(HAlignment.Right);  // Make buttons (right-aligned) appear at the same location regardless of position or size
			uiAppearanceMain.SetAlignment(VAlignment.Bottom);   // Make buttons (bottom-aligned) appear at the same location regardless of position or size

			// Upper panel
			uiBlemishes = CreateHeadOverlay(PedHeadOverlayType.Blemishes, "Blemishes");
			uiFacialHair = CreateHeadOverlay(PedHeadOverlayType.FacialHair, "FacialHair");
			uiEyebrows = CreateHeadOverlay(PedHeadOverlayType.Eyebrows, "Eyebrows");
			uiAgeing = CreateHeadOverlay(PedHeadOverlayType.Ageing, "Ageing");
			uiMakeup = CreateHeadOverlay(PedHeadOverlayType.Makeup, "Makeup");
			uiBlush = CreateHeadOverlay(PedHeadOverlayType.Blush, "Blush");
			uiComplexion = CreateHeadOverlay(PedHeadOverlayType.Complexion, "Complexion");
			uiSunDamage = CreateHeadOverlay(PedHeadOverlayType.SunDamage, "SunDamage");
			uiLipstick = CreateHeadOverlay(PedHeadOverlayType.Lipstick, "Lipstick");
			uiMolesFreckles = CreateHeadOverlay(PedHeadOverlayType.MolesAndFreckles, "MolesFreckles");
			uiChestHair = CreateHeadOverlay(PedHeadOverlayType.ChestHair, "ChestHair");
			uiBodyBlemishes = CreateHeadOverlay(PedHeadOverlayType.BodyBlemishes, "BodyBlemishes");

			// Lower panel
			uiHair = CreateHair("Hair");
			uiEyeColor = CreateEyeColor("Eye Color");

			CreateApplyCancelButtons();
		}

		private UiEntryHeadOverlay CreateHeadOverlay(PedHeadOverlayType type, string label)
		{
			float defaultPadding = 0.0025f;


			UiEntryHeadOverlay entry = new UiEntryHeadOverlay();
			entry.type = type;
			entry.SetLogger(Logger);
			entry.SetDelay(Delay);

			entry.uiOverlayLabel.SetPadding(new UiRectangle(defaultPadding));
			entry.uiOverlayLabel.SetText(label);
			entry.uiOverlayLabel.SetFlags(UiElement.TRANSPARENT);
			uiColumnIndexLabels.AddElement(entry.uiOverlayLabel);

			entry.uiOverlayIndex.SetPadding(new UiRectangle(defaultPadding));
			entry.uiOverlayIndex.SetProperties(UiElement.CANFOCUS);
			entry.uiOverlayIndex.SetFlags(UiElement.TRANSPARENT);
			uiColumnIndexValues.AddElement(entry.uiOverlayIndex);

			entry.btnIndexDecrease.SetText("-");
			entry.btnIndexDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnIndexDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnIndexDecrease.RegisterOnLMBRelease(entry.DecreaseIndex);
			inputsOnMouseMove.Add(entry.btnIndexDecrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnIndexDecrease.OnMouseButton);
			uiColumnIndexDecrease.AddElement(entry.btnIndexDecrease);

			entry.btnIndexIncrease.SetText("+");
			entry.btnIndexIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnIndexIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnIndexIncrease.RegisterOnLMBRelease(entry.IncreaseIndex);
			inputsOnMouseMove.Add(entry.btnIndexIncrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnIndexIncrease.OnMouseButton);
			uiColumnIndexIncrease.AddElement(entry.btnIndexIncrease);

			entry.uiColorId.SetPadding(new UiRectangle(defaultPadding));
			entry.uiColorId.SetFlags(UiElement.TRANSPARENT);
			uiColumnColorValues.AddElement(entry.uiColorId);

			entry.btnColorIdDecrease.SetText("-");
			entry.btnColorIdDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnColorIdDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnColorIdDecrease.RegisterOnLMBRelease(entry.DecreaseColor);
			inputsOnMouseMove.Add(entry.btnColorIdDecrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnColorIdDecrease.OnMouseButton);
			uiColumnColorDecrease.AddElement(entry.btnColorIdDecrease);

			entry.btnColorIdIncrease.SetText("+");
			entry.btnColorIdIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnColorIdIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnColorIdIncrease.RegisterOnLMBRelease(entry.IncreaseColor);
			inputsOnMouseMove.Add(entry.btnColorIdIncrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnColorIdIncrease.OnMouseButton);
			uiColumnColorIncrease.AddElement(entry.btnColorIdIncrease);

			// Opacity
			entry.uiOpacity.SetPadding(new UiRectangle(defaultPadding));
			entry.uiOpacity.SetFlags(UiElement.TRANSPARENT);
			uiColumnOpacityValues.AddElement(entry.uiOpacity);

			entry.btnOpacityDecrease.SetText("-");
			entry.btnOpacityDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnOpacityDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnOpacityDecrease.RegisterOnLMBRelease(entry.DecreaseOpacity);
			inputsOnMouseMove.Add(entry.btnOpacityDecrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnOpacityDecrease.OnMouseButton);
			uiColumnOpacityDecrease.AddElement(entry.btnOpacityDecrease);

			entry.btnOpacityIncrease.SetText("+");
			entry.btnOpacityIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnOpacityIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnOpacityIncrease.RegisterOnLMBRelease(entry.IncreaseOpacity);
			inputsOnMouseMove.Add(entry.btnOpacityIncrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnOpacityIncrease.OnMouseButton);
			uiColumnOpacityIncrease.AddElement(entry.btnOpacityIncrease);

			entry.GetIndexMax = GetHeadOverlayIndexMax;
			entry.GetIndex = GetHeadOverlayIndex;
			entry.SetIndex = SetHeadOverlayIndex;
			entry.GetColorMax = GetHeadOverlayColorMax;
			entry.GetColor = GetHeadOverlayColor;
			entry.SetColor = SetHeadOverlayColor;
			
			entry.SetOpacity = SetHeadOverlayOpacity;
			entry.GetOpacity = GetHeadOverlayOpacity;

			return entry;
		}

		private UiEntryHair CreateHair(string label)
		{
			float defaultPadding = 0.0025f;

			UiEntryHair entry = new UiEntryHair();
			entry.SetLogger(Logger);
			entry.SetDelay(Delay);

			entry.uiHairLabel.SetPadding(new UiRectangle(defaultPadding));
			entry.uiHairLabel.SetText(label);
			entry.uiHairLabel.SetFlags(UiElement.TRANSPARENT);
			uiColumnHairIndexLabels.AddElement(entry.uiHairLabel);
			
			entry.uiHairIndex.SetPadding(new UiRectangle(defaultPadding));
			entry.uiHairIndex.SetFlags(UiElement.TRANSPARENT);
			uiColumnHairIndexValues.AddElement(entry.uiHairIndex);
			
			entry.btnIndexDecrease.SetText("-");
			entry.btnIndexDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnIndexDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnIndexDecrease.RegisterOnLMBRelease(entry.DecreaseIndex);
			inputsOnMouseMove.Add(entry.btnIndexDecrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnIndexDecrease.OnMouseButton);
			uiColumnHairIndexDecrease.AddElement(entry.btnIndexDecrease);

			entry.btnIndexIncrease.SetText("+");
			entry.btnIndexIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnIndexIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnIndexIncrease.RegisterOnLMBRelease(entry.IncreaseIndex);
			inputsOnMouseMove.Add(entry.btnIndexIncrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnIndexIncrease.OnMouseButton);
			uiColumnHairIndexIncrease.AddElement(entry.btnIndexIncrease);

			// Color 1
			entry.uiColorId.SetPadding(new UiRectangle(defaultPadding));
			entry.uiColorId.SetFlags(UiElement.TRANSPARENT);
			uiColumnHairColorValues.AddElement(entry.uiColorId);

			entry.btnColorIdDecrease.SetText("-");
			entry.btnColorIdDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnColorIdDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnColorIdDecrease.RegisterOnLMBRelease(entry.DecreaseColor);
			inputsOnMouseMove.Add(entry.btnColorIdDecrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnColorIdDecrease.OnMouseButton);
			uiColumnHairColorDecrease.AddElement(entry.btnColorIdDecrease);

			entry.btnColorIdIncrease.SetText("+");
			entry.btnColorIdIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnColorIdIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnColorIdIncrease.RegisterOnLMBRelease(entry.IncreaseColor);
			inputsOnMouseMove.Add(entry.btnColorIdIncrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnColorIdIncrease.OnMouseButton);
			uiColumnHairColorIncrease.AddElement(entry.btnColorIdIncrease);

			// Color 2
			entry.uiSecondaryColorId.SetPadding(new UiRectangle(defaultPadding));
			entry.uiSecondaryColorId.SetFlags(UiElement.TRANSPARENT);
			uiColumnHairSecondaryColorValues.AddElement(entry.uiSecondaryColorId);

			entry.btnSecondaryColorIdDecrease.SetText("-");
			entry.btnSecondaryColorIdDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnSecondaryColorIdDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnSecondaryColorIdDecrease.RegisterOnLMBRelease(entry.DecreaseSecondaryColor);
			inputsOnMouseMove.Add(entry.btnSecondaryColorIdDecrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnSecondaryColorIdDecrease.OnMouseButton);
			uiColumnHairSecondaryColorDecrease.AddElement(entry.btnSecondaryColorIdDecrease);

			entry.btnSecondaryColorIdIncrease.SetText("+");
			entry.btnSecondaryColorIdIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnSecondaryColorIdIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnSecondaryColorIdIncrease.RegisterOnLMBRelease(entry.IncreaseSecondaryColor);
			inputsOnMouseMove.Add(entry.btnSecondaryColorIdIncrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnSecondaryColorIdIncrease.OnMouseButton);
			uiColumnHairSecondaryColorIncrease.AddElement(entry.btnSecondaryColorIdIncrease);

			entry.GetNumberOfHairIndex = GetNumberOfPedHairVariations;
			entry.GetIndex = GetHairIndex;
			entry.SetIndex = SetHairIndex;
			entry.GetNumberOfHairColors = GetNumberOfPedHairColors;
			entry.GetColor = GetHairColor;
			entry.SetColor = SetHairColor;
			entry.GetSecondaryColor = GetSecondaryHairColor;
			entry.SetSecondaryColor = SetSecondaryHairColor;

			return entry;
		}

		private UiEntryEyeColor CreateEyeColor(string label)
		{
			float defaultPadding = 0.0025f;

			UiEntryEyeColor entry = new UiEntryEyeColor();
			entry.SetLogger(Logger);
			entry.SetDelay(Delay);

			entry.uiEyeColorLabel.SetPadding(new UiRectangle(defaultPadding));
			entry.uiEyeColorLabel.SetText(label);
			entry.uiEyeColorLabel.SetFlags(UiElement.TRANSPARENT);
			uiColumnHairIndexLabels.AddElement(entry.uiEyeColorLabel);

			entry.uiEyeColorIndex.SetPadding(new UiRectangle(defaultPadding));
			entry.uiEyeColorIndex.SetFlags(UiElement.TRANSPARENT);
			uiColumnHairIndexValues.AddElement(entry.uiEyeColorIndex);

			entry.btnIndexDecrease.SetText("-");
			entry.btnIndexDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnIndexDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnIndexDecrease.RegisterOnLMBRelease(entry.DecreaseIndex);
			inputsOnMouseMove.Add(entry.btnIndexDecrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnIndexDecrease.OnMouseButton);
			uiColumnHairIndexDecrease.AddElement(entry.btnIndexDecrease);

			entry.btnIndexIncrease.SetText("+");
			entry.btnIndexIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnIndexIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnIndexIncrease.RegisterOnLMBRelease(entry.IncreaseIndex);
			inputsOnMouseMove.Add(entry.btnIndexIncrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnIndexIncrease.OnMouseButton);
			uiColumnHairIndexIncrease.AddElement(entry.btnIndexIncrease);

			entry.GetNumberOfEyeColors = GetNumberOfPedEyeColors;
			entry.GetColor = GetEyeColor;
			entry.SetColor = SetEyeColor;
			
			return entry;
		}

		public int GetHeadOverlayIndex(PedHeadOverlayType type)
		{
			switch (type)
			{
				case PedHeadOverlayType.Blemishes:
					return character.PedHeadOverlays.Blemishes.Index;
				case PedHeadOverlayType.FacialHair:
					return character.PedHeadOverlays.FacialHair.Index;
				case PedHeadOverlayType.Eyebrows:
					return character.PedHeadOverlays.Eyebrows.Index;
				case PedHeadOverlayType.Ageing:
					return character.PedHeadOverlays.Ageing.Index;
				case PedHeadOverlayType.Makeup:
					return character.PedHeadOverlays.Makeup.Index;
				case PedHeadOverlayType.Blush:
					return character.PedHeadOverlays.Blush.Index;
				case PedHeadOverlayType.Complexion:
					return character.PedHeadOverlays.Complexion.Index;
				case PedHeadOverlayType.SunDamage:
					return character.PedHeadOverlays.SunDamage.Index;
				case PedHeadOverlayType.Lipstick:
					return character.PedHeadOverlays.Lipstick.Index;
				case PedHeadOverlayType.MolesAndFreckles:
					return character.PedHeadOverlays.MolesAndFreckles.Index;
				case PedHeadOverlayType.ChestHair:
					return character.PedHeadOverlays.ChestHair.Index;
				case PedHeadOverlayType.BodyBlemishes:
					return character.PedHeadOverlays.BodyBlemishes.Index;
				default:
					return 0;
			}
		}

		public int GetHeadOverlayIndexMax(PedHeadOverlayType type)
		{
			switch (type)
			{
				case PedHeadOverlayType.Blemishes:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.Blemishes);
				case PedHeadOverlayType.FacialHair:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.FacialHair);
				case PedHeadOverlayType.Eyebrows:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.Eyebrows);
				case PedHeadOverlayType.Ageing:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.Ageing);
				case PedHeadOverlayType.Makeup:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.Makeup);
				case PedHeadOverlayType.Blush:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.Blush);
				case PedHeadOverlayType.Complexion:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.Complexion);
				case PedHeadOverlayType.SunDamage:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.SunDamage);
				case PedHeadOverlayType.Lipstick:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.Lipstick);
				case PedHeadOverlayType.MolesAndFreckles:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.MolesAndFreckles);
				case PedHeadOverlayType.ChestHair:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.ChestHair);
				case PedHeadOverlayType.BodyBlemishes:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.BodyBlemishes);
				default:
					return 0;
			}
		}

		public void SetHeadOverlayIndex(PedHeadOverlayType type, int index)
		{
			switch (type)
			{
				case PedHeadOverlayType.Blemishes:
					character.PedHeadOverlays.Blemishes.Index = index;
					break;
				case PedHeadOverlayType.FacialHair:
					character.PedHeadOverlays.FacialHair.Index = index;
					break;
				case PedHeadOverlayType.Eyebrows:
					character.PedHeadOverlays.Eyebrows.Index = index;
					break;
				case PedHeadOverlayType.Ageing:
					character.PedHeadOverlays.Ageing.Index = index;
					break;
				case PedHeadOverlayType.Makeup:
					character.PedHeadOverlays.Makeup.Index = index;
					break;
				case PedHeadOverlayType.Blush:
					character.PedHeadOverlays.Blush.Index = index;
					break;
				case PedHeadOverlayType.Complexion:
					character.PedHeadOverlays.Complexion.Index = index;
					break;
				case PedHeadOverlayType.SunDamage:
					character.PedHeadOverlays.SunDamage.Index = index;
					break;
				case PedHeadOverlayType.Lipstick:
					character.PedHeadOverlays.Lipstick.Index = index;
					break;
				case PedHeadOverlayType.MolesAndFreckles:
					character.PedHeadOverlays.MolesAndFreckles.Index = index;
					break;
				case PedHeadOverlayType.ChestHair:
					character.PedHeadOverlays.ChestHair.Index = index;
					break;
				case PedHeadOverlayType.BodyBlemishes:
					character.PedHeadOverlays.BodyBlemishes.Index = index;
					break;
				default:
					break;
			}
			ApplyOverlayToPed(type);
		}

		public int GetHeadOverlayColor(PedHeadOverlayType type)
		{
			switch (type)
			{
				case PedHeadOverlayType.FacialHair:
					return character.PedHeadOverlays.FacialHair.ColorId;
				case PedHeadOverlayType.Eyebrows:
					return character.PedHeadOverlays.Eyebrows.ColorId;
				case PedHeadOverlayType.Makeup:
					return character.PedHeadOverlays.Makeup.ColorId;
				case PedHeadOverlayType.Blush:
					return character.PedHeadOverlays.Blush.ColorId;
				case PedHeadOverlayType.Lipstick:
					return character.PedHeadOverlays.Lipstick.ColorId;
				case PedHeadOverlayType.ChestHair:
					return character.PedHeadOverlays.ChestHair.ColorId;
				default:
					return 0;
			}
		}

		public int GetHeadOverlayColorMax(PedHeadOverlayType type)
		{
			switch (type)
			{
				case PedHeadOverlayType.FacialHair:
				case PedHeadOverlayType.Eyebrows:
				case PedHeadOverlayType.ChestHair:
					return API.GetNumHairColors();
				case PedHeadOverlayType.Makeup:
				case PedHeadOverlayType.Blush:
				case PedHeadOverlayType.Lipstick:
					return API.GetNumMakeupColors();
				default:
					return 0;
			}
		}

		public void SetHeadOverlayColor(PedHeadOverlayType type, int colorId)
		{
			switch (type)
			{
				case PedHeadOverlayType.FacialHair:
					character.PedHeadOverlays.FacialHair.ColorId = colorId;
					break;
				case PedHeadOverlayType.Eyebrows:
					character.PedHeadOverlays.Eyebrows.ColorId = colorId;
					break;
				case PedHeadOverlayType.Makeup:
					character.PedHeadOverlays.Makeup.ColorId = colorId;
					break;
				case PedHeadOverlayType.Blush:
					character.PedHeadOverlays.Blush.ColorId = colorId;
					break;
				case PedHeadOverlayType.Lipstick:
					character.PedHeadOverlays.Lipstick.ColorId = colorId;
					break;
				case PedHeadOverlayType.ChestHair:
					character.PedHeadOverlays.ChestHair.ColorId = colorId;
					break;
				default:
					return;
			}
			
			ApplyOverlayColorToPed(type);
		}

		public int GetHeadOverlaySecondaryColor(PedHeadOverlayType type)
		{
			switch (type)
			{
				case PedHeadOverlayType.FacialHair:
					return character.PedHeadOverlays.FacialHair.SecondColorId;
				case PedHeadOverlayType.Eyebrows:
					return character.PedHeadOverlays.Eyebrows.SecondColorId;
				case PedHeadOverlayType.Makeup:
					return character.PedHeadOverlays.Makeup.SecondColorId;
				case PedHeadOverlayType.Blush:
					return character.PedHeadOverlays.Blush.SecondColorId;
				case PedHeadOverlayType.Lipstick:
					return character.PedHeadOverlays.Lipstick.SecondColorId;
				case PedHeadOverlayType.ChestHair:
					return character.PedHeadOverlays.ChestHair.SecondColorId;
				default:
					return 0;
			}
		}

		public void SetHeadOverlaySecondaryColor(PedHeadOverlayType type, int colorId)
		{
			switch (type)
			{
				case PedHeadOverlayType.FacialHair:
					character.PedHeadOverlays.FacialHair.SecondColorId = colorId;
					break;
				case PedHeadOverlayType.Eyebrows:
					character.PedHeadOverlays.Eyebrows.SecondColorId = colorId;
					break;
				case PedHeadOverlayType.Makeup:
					character.PedHeadOverlays.Makeup.SecondColorId = colorId;
					break;
				case PedHeadOverlayType.Blush:
					character.PedHeadOverlays.Blush.SecondColorId = colorId;
					break;
				case PedHeadOverlayType.Lipstick:
					character.PedHeadOverlays.Lipstick.SecondColorId = colorId;
					break;
				case PedHeadOverlayType.ChestHair:
					character.PedHeadOverlays.ChestHair.SecondColorId = colorId;
					break;
				default:
					return;
			}
			ApplyOverlayColorToPed(type);
		}

		public float GetHeadOverlayOpacity(PedHeadOverlayType type)
		{
			switch (type)
			{
				case PedHeadOverlayType.Blemishes:
					return character.PedHeadOverlays.Blemishes.Opacity;
				case PedHeadOverlayType.FacialHair:
					return character.PedHeadOverlays.FacialHair.Opacity;
				case PedHeadOverlayType.Eyebrows:
					return character.PedHeadOverlays.Eyebrows.Opacity;
				case PedHeadOverlayType.Ageing:
					return character.PedHeadOverlays.Ageing.Opacity;
				case PedHeadOverlayType.Makeup:
					return character.PedHeadOverlays.Makeup.Opacity;
				case PedHeadOverlayType.Blush:
					return character.PedHeadOverlays.Blush.Opacity;
				case PedHeadOverlayType.Complexion:
					return character.PedHeadOverlays.Complexion.Opacity;
				case PedHeadOverlayType.SunDamage:
					return character.PedHeadOverlays.SunDamage.Opacity;
				case PedHeadOverlayType.Lipstick:
					return character.PedHeadOverlays.Lipstick.Opacity;
				case PedHeadOverlayType.MolesAndFreckles:
					return character.PedHeadOverlays.MolesAndFreckles.Opacity;
				case PedHeadOverlayType.ChestHair:
					return character.PedHeadOverlays.ChestHair.Opacity;
				case PedHeadOverlayType.BodyBlemishes:
					return character.PedHeadOverlays.BodyBlemishes.Opacity;
				default:
					return 0f;
			}
		}

		public void SetHeadOverlayOpacity(PedHeadOverlayType type,  float opacity)
		{
			switch (type)
			{
				case PedHeadOverlayType.Blemishes:
					character.PedHeadOverlays.Blemishes.Opacity = opacity;
					break;
				case PedHeadOverlayType.FacialHair:
					character.PedHeadOverlays.FacialHair.Opacity = opacity;
					break;
				case PedHeadOverlayType.Eyebrows:
					character.PedHeadOverlays.Eyebrows.Opacity = opacity;
					break;
				case PedHeadOverlayType.Ageing:
					character.PedHeadOverlays.Ageing.Opacity = opacity;
					break;
				case PedHeadOverlayType.Makeup:
					character.PedHeadOverlays.Makeup.Opacity = opacity;
					break;
				case PedHeadOverlayType.Blush:
					character.PedHeadOverlays.Blush.Opacity = opacity;
					break;
				case PedHeadOverlayType.Complexion:
					character.PedHeadOverlays.Complexion.Opacity = opacity;
					break;
				case PedHeadOverlayType.SunDamage:
					character.PedHeadOverlays.SunDamage.Opacity = opacity;
					break;
				case PedHeadOverlayType.Lipstick:
					character.PedHeadOverlays.Lipstick.Opacity = opacity;
					break;
				case PedHeadOverlayType.MolesAndFreckles:
					character.PedHeadOverlays.MolesAndFreckles.Opacity = opacity;
					break;
				case PedHeadOverlayType.ChestHair:
					character.PedHeadOverlays.ChestHair.Opacity = opacity;
					break;
				case PedHeadOverlayType.BodyBlemishes:
					character.PedHeadOverlays.BodyBlemishes.Opacity = opacity;
					break;
				default:
					break;
			}
			ApplyOverlayToPed(type);
		}

		public int GetNumberOfPedHairVariations()
		{
			return API.GetNumberOfPedDrawableVariations(Game.PlayerPed.Handle, (int)PedComponentType.Hair);
		}
		public int GetHairIndex()
		{
			return character.PedComponents.Hair.Index;
		}
		public void SetHairIndex(int index)
		{
			character.PedComponents.Hair.Index = index;
			ApplyHairIndexToPed();
		}
		public int GetNumberOfPedHairColors()
		{
			return API.GetNumHairColors();
		}
		public int GetHairColor()
		{
			return character.PedHairColor;
		}
		public void SetHairColor(int colorId)
		{
			character.PedHairColor = colorId;
			ApplyHairColorToPed();
		}
		public int GetSecondaryHairColor()
		{
			return character.PedSecondHairColor;
		}

		public void SetSecondaryHairColor(int colorId)
		{
			character.PedSecondHairColor = colorId;
			ApplyHairColorToPed();
		}

		public void ApplyOverlayToPed(PedHeadOverlayType type)
		{
			switch (type)
			{
				case PedHeadOverlayType.Blemishes:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Blemishes.Type, character.PedHeadOverlays.Blemishes.Index, character.PedHeadOverlays.Blemishes.Opacity);
					break;
				case PedHeadOverlayType.FacialHair:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.FacialHair.Type, character.PedHeadOverlays.FacialHair.Index, character.PedHeadOverlays.FacialHair.Opacity);
					break;
				case PedHeadOverlayType.Eyebrows:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Eyebrows.Type, character.PedHeadOverlays.Eyebrows.Index, character.PedHeadOverlays.Eyebrows.Opacity);
					break;
				case PedHeadOverlayType.Ageing:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Ageing.Type, character.PedHeadOverlays.Ageing.Index, character.PedHeadOverlays.Ageing.Opacity);
					break;
				case PedHeadOverlayType.Makeup:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Makeup.Type, character.PedHeadOverlays.Makeup.Index, character.PedHeadOverlays.Makeup.Opacity);
					break;
				case PedHeadOverlayType.Blush:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Blush.Type, character.PedHeadOverlays.Blush.Index, character.PedHeadOverlays.Blush.Opacity);
					break;
				case PedHeadOverlayType.Complexion:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Complexion.Type, character.PedHeadOverlays.Complexion.Index, character.PedHeadOverlays.Complexion.Opacity);
					break;
				case PedHeadOverlayType.SunDamage:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.SunDamage.Type, character.PedHeadOverlays.SunDamage.Index, character.PedHeadOverlays.SunDamage.Opacity);
					break;
				case PedHeadOverlayType.Lipstick:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Lipstick.Type, character.PedHeadOverlays.Lipstick.Index, character.PedHeadOverlays.Lipstick.Opacity);
					break;
				case PedHeadOverlayType.MolesAndFreckles:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.MolesAndFreckles.Type, character.PedHeadOverlays.MolesAndFreckles.Index, character.PedHeadOverlays.MolesAndFreckles.Opacity);
					break;
				case PedHeadOverlayType.ChestHair:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.ChestHair.Type, character.PedHeadOverlays.ChestHair.Index, character.PedHeadOverlays.ChestHair.Opacity);
					break;
				case PedHeadOverlayType.BodyBlemishes:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.BodyBlemishes.Type, character.PedHeadOverlays.BodyBlemishes.Index, character.PedHeadOverlays.BodyBlemishes.Opacity);
					break;
				default:
					break;
			}
		}

		public void ApplyOverlayColorToPed(PedHeadOverlayType type)
		{
			switch (type)
			{
				case PedHeadOverlayType.FacialHair:
					API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.FacialHair.ColorType, (int)character.PedHeadOverlays.FacialHair.ColorType, character.PedHeadOverlays.FacialHair.ColorId, character.PedHeadOverlays.FacialHair.SecondColorId);
					break;
				case PedHeadOverlayType.Eyebrows:
					API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Eyebrows.Type, (int)character.PedHeadOverlays.Eyebrows.ColorType, character.PedHeadOverlays.Eyebrows.ColorId, character.PedHeadOverlays.Eyebrows.SecondColorId);
					break;
				case PedHeadOverlayType.Makeup:
					API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Makeup.Type, (int)character.PedHeadOverlays.Makeup.ColorType, character.PedHeadOverlays.Makeup.ColorId, character.PedHeadOverlays.Makeup.SecondColorId);
					break;
				case PedHeadOverlayType.Blush:
					API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Blush.Type, (int)character.PedHeadOverlays.Blush.ColorType, character.PedHeadOverlays.Blush.ColorId, character.PedHeadOverlays.Blush.SecondColorId);
					break;
				case PedHeadOverlayType.Lipstick:
					API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Lipstick.Type, (int)character.PedHeadOverlays.Lipstick.ColorType, character.PedHeadOverlays.Lipstick.ColorId, character.PedHeadOverlays.Lipstick.SecondColorId);
					break;
				case PedHeadOverlayType.ChestHair:
					API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.ChestHair.Type, (int)character.PedHeadOverlays.ChestHair.ColorType, character.PedHeadOverlays.ChestHair.ColorId, character.PedHeadOverlays.ChestHair.SecondColorId);
					break;
				default:
					break;
			}
		}

		public void ApplyToPed()
		{
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Blemishes.Type, character.PedHeadOverlays.Blemishes.Index, character.PedHeadOverlays.Blemishes.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.FacialHair.Type, character.PedHeadOverlays.FacialHair.Index, character.PedHeadOverlays.FacialHair.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Eyebrows.Type, character.PedHeadOverlays.Eyebrows.Index, character.PedHeadOverlays.Eyebrows.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Ageing.Type, character.PedHeadOverlays.Ageing.Index, character.PedHeadOverlays.Ageing.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Makeup.Type, character.PedHeadOverlays.Makeup.Index, character.PedHeadOverlays.Makeup.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Blush.Type, character.PedHeadOverlays.Blush.Index, character.PedHeadOverlays.Blush.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Complexion.Type, character.PedHeadOverlays.Complexion.Index, character.PedHeadOverlays.Complexion.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.SunDamage.Type, character.PedHeadOverlays.SunDamage.Index, character.PedHeadOverlays.SunDamage.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Lipstick.Type, character.PedHeadOverlays.Lipstick.Index, character.PedHeadOverlays.Lipstick.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.MolesAndFreckles.Type, character.PedHeadOverlays.MolesAndFreckles.Index, character.PedHeadOverlays.MolesAndFreckles.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.ChestHair.Type, character.PedHeadOverlays.ChestHair.Index, character.PedHeadOverlays.ChestHair.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.BodyBlemishes.Type, character.PedHeadOverlays.BodyBlemishes.Index, character.PedHeadOverlays.BodyBlemishes.Opacity);
			API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.FacialHair.Type, (int)character.PedHeadOverlays.FacialHair.ColorType, character.PedHeadOverlays.FacialHair.ColorId, character.PedHeadOverlays.FacialHair.SecondColorId);
			API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Eyebrows.Type, (int)character.PedHeadOverlays.Eyebrows.ColorType, character.PedHeadOverlays.Eyebrows.ColorId, character.PedHeadOverlays.Eyebrows.SecondColorId);
			API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Makeup.Type, (int)character.PedHeadOverlays.Makeup.ColorType, character.PedHeadOverlays.Makeup.ColorId, character.PedHeadOverlays.Makeup.SecondColorId);
			API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Blush.Type, (int)character.PedHeadOverlays.Blush.ColorType, character.PedHeadOverlays.Blush.ColorId, character.PedHeadOverlays.Blush.SecondColorId);
			API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Lipstick.Type, (int)character.PedHeadOverlays.Lipstick.ColorType, character.PedHeadOverlays.Lipstick.ColorId, character.PedHeadOverlays.Lipstick.SecondColorId);
			API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.ChestHair.Type, (int)character.PedHeadOverlays.ChestHair.ColorType, character.PedHeadOverlays.ChestHair.ColorId, character.PedHeadOverlays.ChestHair.SecondColorId);

			ApplyHairIndexToPed();
			ApplyHairColorToPed();
			ApplyEyeColorToPed();
		}

		public void ApplyHairIndexToPed()
		{
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Hair, character.PedComponents.Hair.Index, character.PedComponents.Hair.Texture, 0);
		}

		public void ApplyHairColorToPed()
		{
			API.SetPedHairColor(Game.PlayerPed.Handle, character.PedHairColor, character.PedSecondHairColor);
		}

		private int GetNumberOfPedEyeColors()
		{
			return (int)PedEyeColor.NumberOfPedEyeColors;
		}
		private int GetEyeColor()
		{
			return (int)character.PedEyeColor;
		}
		private void SetEyeColor(int eyeColor)
		{
			character.PedEyeColor = (PedEyeColor)eyeColor;
			ApplyEyeColorToPed();
		}

		private void ApplyEyeColorToPed()
		{
			API.SetPedEyeColor(Game.PlayerPed.Handle, (int)character.PedEyeColor);
		}
	}
}
