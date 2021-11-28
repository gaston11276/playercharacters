using CitizenFX.Core;
using CitizenFX.Core.Native;
using Gaston11276.SimpleUi;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Client
{	
	public class HudComponentPedComponents : HudComponent
	{
		// Columns
		UiPanel uiColumnIndexLabels = new UiPanel();
		UiPanel uiColumnIndexValues = new UiPanel();
		UiPanel uiColumnIndexDecrease = new UiPanel();
		UiPanel uiColumnIndexIncrease = new UiPanel();
		UiPanel uiColumnTextureValues = new UiPanel();
		UiPanel uiColumnTextureDecrease = new UiPanel();
		UiPanel uiColumnTextureIncrease = new UiPanel();

		//UiEntryComponent uiFace;
		UiEntryComponent uiMask;
		//UiEntryComponent uiHair;
		UiEntryComponent uiTorso;
		UiEntryComponent uiLegs;
		UiEntryComponent uiBack;
		UiEntryComponent uiShoes;
		UiEntryComponent uiAccessory;
		UiEntryComponent uiUndershirt;
		UiEntryComponent uiKevlar;
		UiEntryComponent uiBadge;
		UiEntryComponent uiTorso2;

		UiEntryProp uiHat;
		UiEntryProp uiGlasses;
		UiEntryProp uiEar;
		UiEntryProp uiWatch;
		UiEntryProp uiBracelet;

		public HudComponentPedComponents()
		{
			cameraMode = CameraMode.Front;
		}

		public override async void SetUi()
		{
			//uiFace.SetUi();
			await uiMask.SetUi();
			//uiHair.SetUi();
			await uiTorso.SetUi();
			await uiLegs.SetUi();
			await uiBack.SetUi();
			await uiShoes.SetUi();
			await uiAccessory.SetUi();
			await uiUndershirt.SetUi();
			await uiKevlar.SetUi();
			await uiBadge.SetUi();
			await uiTorso2.SetUi();

			await uiHat.SetUi();
			await uiGlasses.SetUi();
			await uiEar.SetUi();
			await uiWatch.SetUi();
			await uiBracelet.SetUi();
		}
		
		public override void CreateColumns()
		{
			UiPanel uiCenterPanel = new UiPanel();
			uiCenterPanel.SetPadding(new UiRectangle(defaultPadding));
			contentFrame.AddElement(uiCenterPanel);

			CreateColumn(uiCenterPanel, HGravity.Left, uiColumnIndexLabels, "");
			CreateColumn(uiCenterPanel, HGravity.Right, uiColumnIndexValues, "");
			CreateColumn(uiCenterPanel, HGravity.Center, uiColumnIndexDecrease, "");
			CreateColumn(uiCenterPanel, HGravity.Center, uiColumnIndexIncrease, "");
			CreateColumn(uiCenterPanel, HGravity.Left, uiColumnTextureValues, "Texture");
			CreateColumn(uiCenterPanel, HGravity.Center, uiColumnTextureDecrease, "");
			CreateColumn(uiCenterPanel, HGravity.Center, uiColumnTextureIncrease, "");
		}

		private UiEntryComponent CreateComponent(PedComponentType type, string label)
		{
			float defaultPadding = 0.0025f;

			UiEntryComponent entry = new UiEntryComponent();
			entry.type = type;
			entry.SetLogger(Logger);
			entry.SetDelay(Delay);

			entry.uiComponentLabel.SetPadding(new UiRectangle(defaultPadding));
			entry.uiComponentLabel.SetText(label);
			entry.uiComponentLabel.SetFlags(UiElement.TRANSPARENT);
			uiColumnIndexLabels.AddElement(entry.uiComponentLabel);

			entry.uiComponentIndex.SetPadding(new UiRectangle(defaultPadding));
			entry.uiComponentIndex.SetFlags(UiElement.TRANSPARENT);
			uiColumnIndexValues.AddElement(entry.uiComponentIndex);

			entry.btnComponentDecrease.SetText("-");
			entry.btnComponentDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnComponentDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnComponentDecrease.RegisterOnLMBRelease(entry.DecreaseIndex);
			inputsOnMouseMove.Add(entry.btnComponentDecrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnComponentDecrease.OnMouseButton);
			uiColumnIndexDecrease.AddElement(entry.btnComponentDecrease);

			entry.btnComponentIncrease.SetText("+");
			entry.btnComponentIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnComponentIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnComponentIncrease.RegisterOnLMBRelease(entry.IncreaseIndex);
			inputsOnMouseMove.Add(entry.btnComponentIncrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnComponentIncrease.OnMouseButton);
			uiColumnIndexIncrease.AddElement(entry.btnComponentIncrease);

			entry.uiTextureId.SetPadding(new UiRectangle(defaultPadding));
			entry.uiTextureId.SetFlags(UiElement.TRANSPARENT);
			uiColumnTextureValues.AddElement(entry.uiTextureId);

			entry.btnTextureDecrease.SetText("-");
			entry.btnTextureDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnTextureDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnTextureDecrease.RegisterOnLMBRelease(entry.DecreaseTexture);
			inputsOnMouseMove.Add(entry.btnTextureDecrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnTextureDecrease.OnMouseButton);
			uiColumnTextureDecrease.AddElement(entry.btnTextureDecrease);

			entry.btnTextureIncrease.SetText("+");
			entry.btnTextureIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnTextureIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnTextureIncrease.RegisterOnLMBRelease(entry.IncreaseTexture);
			inputsOnMouseMove.Add(entry.btnTextureIncrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnTextureIncrease.OnMouseButton);
			uiColumnTextureIncrease.AddElement(entry.btnTextureIncrease);

			entry.GetIndexMax = GetComponentIndexMax;
			entry.GetIndex = GetComponentIndex;
			entry.SetIndex = SetComponentIndex;
			entry.GetTextureMax = GetComponentTextureMax;
			entry.GetTexture = GetComponentTexture;
			entry.SetTexture = SetComponentTexture;

			return entry;
		}

		private UiEntryProp CreateProp(PedPropType type, string label)
		{
			float defaultPadding = 0.0025f;

			UiEntryProp entry = new UiEntryProp();
			entry.type = type;
			entry.SetLogger(Logger);
			entry.SetDelay(Delay);

			entry.uiComponentLabel.SetPadding(new UiRectangle(defaultPadding));
			entry.uiComponentLabel.SetText(label);
			uiColumnIndexLabels.AddElement(entry.uiComponentLabel);

			entry.uiComponentIndex.SetPadding(new UiRectangle(defaultPadding));
			uiColumnIndexValues.AddElement(entry.uiComponentIndex);

			entry.btnComponentDecrease.SetText("-");
			entry.btnComponentDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnComponentDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnComponentDecrease.RegisterOnLMBRelease(entry.DecreaseIndex);
			inputsOnMouseMove.Add(entry.btnComponentDecrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnComponentDecrease.OnMouseButton);
			uiColumnIndexDecrease.AddElement(entry.btnComponentDecrease);

			entry.btnComponentIncrease.SetText("+");
			entry.btnComponentIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnComponentIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnComponentIncrease.RegisterOnLMBRelease(entry.IncreaseIndex);
			inputsOnMouseMove.Add(entry.btnComponentIncrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnComponentIncrease.OnMouseButton);
			uiColumnIndexIncrease.AddElement(entry.btnComponentIncrease);

			entry.uiTextureId.SetPadding(new UiRectangle(defaultPadding));
			uiColumnTextureValues.AddElement(entry.uiTextureId);

			entry.btnTextureDecrease.SetText("-");
			entry.btnTextureDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnTextureDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnTextureDecrease.RegisterOnLMBRelease(entry.DecreaseTexture);
			inputsOnMouseMove.Add(entry.btnTextureDecrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnTextureDecrease.OnMouseButton);
			uiColumnTextureDecrease.AddElement(entry.btnTextureDecrease);

			entry.btnTextureIncrease.SetText("+");
			entry.btnTextureIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnTextureIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnTextureIncrease.RegisterOnLMBRelease(entry.IncreaseTexture);
			inputsOnMouseMove.Add(entry.btnTextureIncrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnTextureIncrease.OnMouseButton);
			uiColumnTextureIncrease.AddElement(entry.btnTextureIncrease);

			entry.GetIndexMax = GetPropIndexMax;
			entry.GetIndex = GetPropIndex;
			entry.SetIndex = SetPropIndex;
			entry.GetTextureMax = GetPropTextureMax;
			entry.GetTexture = GetPropTexture;
			entry.SetTexture = SetPropTexture;
			entry.DettachProp = DettachProp;

			return entry;
		}

		public int GetComponentIndex(PedComponentType type)
		{
			switch (type)
			{
				case PedComponentType.Face:
					return character.PedComponents.Face.Index;
				case PedComponentType.Mask:
					return character.PedComponents.Mask.Index;
				case PedComponentType.Hair:
					return character.PedComponents.Hair.Index;
				case PedComponentType.Torso:
					return character.PedComponents.Torso.Index;
				case PedComponentType.Legs:
					return character.PedComponents.Legs.Index;
				case PedComponentType.Back:
					return character.PedComponents.Back.Index;
				case PedComponentType.Shoes:
					return character.PedComponents.Shoes.Index;
				case PedComponentType.Accessory:
					return character.PedComponents.Accessory.Index;
				case PedComponentType.Undershirt:
					return character.PedComponents.Undershirt.Index;
				case PedComponentType.Kevlar:
					return character.PedComponents.Kevlar.Index;
				case PedComponentType.Badge:
					return character.PedComponents.Badge.Index;
				case PedComponentType.Torso2:
					return character.PedComponents.Torso2.Index;
				default:
					return 0;
			}
		}

		public int GetComponentIndexMax(PedComponentType type)
		{
			return API.GetNumberOfPedDrawableVariations(Game.PlayerPed.Handle, (int)type);
		}

		public void SetComponentIndex(PedComponentType type, int index)
		{
			switch (type)
			{
				case PedComponentType.Face:
					character.PedComponents.Face.Index = index;
					break;
				case PedComponentType.Mask:
					character.PedComponents.Mask.Index = index;
					break;
				case PedComponentType.Hair:
					character.PedComponents.Hair.Index = index;
					break;
				case PedComponentType.Torso:
					character.PedComponents.Torso.Index = index;
					break;
				case PedComponentType.Legs:
					character.PedComponents.Legs.Index = index;
					break;
				case PedComponentType.Back:
					character.PedComponents.Back.Index = index;
					break;
				case PedComponentType.Shoes:
					character.PedComponents.Shoes.Index = index;
					break;
				case PedComponentType.Accessory:
					character.PedComponents.Accessory.Index = index;
					break;
				case PedComponentType.Undershirt:
					character.PedComponents.Undershirt.Index = index;
					break;
				case PedComponentType.Kevlar:
					character.PedComponents.Kevlar.Index = index;
					break;
				case PedComponentType.Badge:
					character.PedComponents.Badge.Index = index;
					break;
				case PedComponentType.Torso2:
					character.PedComponents.Torso2.Index = index;
					break;
				default:
					break;
			}
			ApplyComponentToPed(type);
		}

		public int GetComponentTexture(PedComponentType type)
		{
			switch (type)
			{
				case PedComponentType.Face:
					return character.PedComponents.Face.Texture;
				case PedComponentType.Mask:
					return character.PedComponents.Mask.Texture;
				case PedComponentType.Hair:
					return character.PedComponents.Hair.Texture;
				case PedComponentType.Torso:
					return character.PedComponents.Torso.Texture;
				case PedComponentType.Legs:
					return character.PedComponents.Legs.Texture;
				case PedComponentType.Back:
					return character.PedComponents.Back.Texture;
				case PedComponentType.Shoes:
					return character.PedComponents.Shoes.Texture;
				case PedComponentType.Accessory:
					return character.PedComponents.Accessory.Texture;
				case PedComponentType.Undershirt:
					return character.PedComponents.Undershirt.Texture;
				case PedComponentType.Kevlar:
					return character.PedComponents.Kevlar.Texture;
				case PedComponentType.Badge:
					return character.PedComponents.Badge.Texture;
				case PedComponentType.Torso2:
					return character.PedComponents.Torso2.Texture;
				default:
					return 0;
			}
		}

		public int GetComponentTextureMax(PedComponentType type, int drawableId)
		{
			return API.GetNumberOfPedTextureVariations(Game.PlayerPed.Handle, (int)type, drawableId);
		}

		public void SetComponentTexture(PedComponentType type, int texture)
		{
			switch (type)
			{
				case PedComponentType.Face:
					character.PedComponents.Face.Texture = texture;
					break;
				case PedComponentType.Mask:
					character.PedComponents.Mask.Texture = texture;
					break;
				case PedComponentType.Hair:
					character.PedComponents.Hair.Texture = texture;
					break;
				case PedComponentType.Torso:
					character.PedComponents.Torso.Texture = texture;
					break;
				case PedComponentType.Legs:
					character.PedComponents.Legs.Texture = texture;
					break;
				case PedComponentType.Back:
					character.PedComponents.Back.Texture = texture;
					break;
				case PedComponentType.Shoes:
					character.PedComponents.Shoes.Texture = texture;
					break;
				case PedComponentType.Accessory:
					character.PedComponents.Accessory.Texture = texture;
					break;
				case PedComponentType.Undershirt:
					character.PedComponents.Undershirt.Texture = texture;
					break;
				case PedComponentType.Kevlar:
					character.PedComponents.Kevlar.Texture = texture;
					break;
				case PedComponentType.Badge:
					character.PedComponents.Badge.Texture = texture;
					break;
				case PedComponentType.Torso2:
					character.PedComponents.Torso2.Texture = texture;
					break;
				default:
					break;
			}
			ApplyComponentToPed(type);
		}

		public int GetComponentColorMax(PedComponentType type, int index)
		{
			return API.GetNumHairColors();
		}
		public int GetComponentColor1(PedComponentType type)
		{
			return character.PedHairColor;
		}
		public void SetComponentColor1(PedComponentType type, int colorId)
		{
			character.PedHairColor = colorId;
		}
		public int GetComponentColor2(PedComponentType type)
		{
			return character.PedSecondHairColor;
		}
		public void SetComponentColor2(PedComponentType type, int colorId)
		{
			character.PedSecondHairColor = colorId;
		}

		public int GetPropIndex(PedPropType type)
		{
			switch (type)
			{
				case PedPropType.Hat:
					return character.PedProps.Hat.Index;
				case PedPropType.Glasses:
					return character.PedProps.Glasses.Index;
				case PedPropType.Ear:
					return character.PedProps.Ear.Index;
				case PedPropType.Watch:
					return character.PedProps.Watch.Index;
				case PedPropType.Bracelet:
					return character.PedProps.Bracelet.Index;
				default:
					return 0;
			}
		}

		public int GetPropIndexMax(PedPropType type)
		{
			return API.GetNumberOfPedPropDrawableVariations(Game.PlayerPed.Handle, (int)type);
		}

		public void SetPropIndex(PedPropType type, int index)
		{
			switch (type)
			{
				case PedPropType.Hat:
					character.PedProps.Hat.Index = index;
					break;
				case PedPropType.Glasses:
					character.PedProps.Glasses.Index = index;
					break;
				case PedPropType.Ear:
					character.PedProps.Ear.Index = index;
					break;
				case PedPropType.Watch:
					character.PedProps.Watch.Index = index;
					break;
				case PedPropType.Bracelet:
					character.PedProps.Bracelet.Index = index;
					break;
				default:
					break;
			}
			ApplyPropToPed(type);
		}

		public int GetPropTexture(PedPropType type)
		{
			switch (type)
			{
				case PedPropType.Hat:
					return character.PedProps.Hat.Texture;
				case PedPropType.Glasses:
					return character.PedProps.Glasses.Texture;
				case PedPropType.Ear:
					return character.PedProps.Ear.Texture;
				case PedPropType.Watch:
					return character.PedProps.Watch.Texture;
				case PedPropType.Bracelet:
					return character.PedProps.Bracelet.Texture;
				default:
					return 0;
			}
		}

		public int GetPropTextureMax(PedPropType type, int drawableId)
		{
			return API.GetNumberOfPedPropTextureVariations(Game.PlayerPed.Handle, (int)type, drawableId);
		}

		public void SetPropTexture(PedPropType type, int texture)
		{
			switch (type)
			{
				case PedPropType.Hat:
					character.PedProps.Hat.Texture = texture;
					break;
				case PedPropType.Glasses:
					character.PedProps.Glasses.Texture = texture;
					break;
				case PedPropType.Ear:
					character.PedProps.Ear.Texture = texture;
					break;
				case PedPropType.Watch:
					character.PedProps.Watch.Texture = texture;
					break;
				case PedPropType.Bracelet:
					character.PedProps.Bracelet.Texture = texture;
					break;

				default:
					break;
			}
			ApplyPropToPed(type);
		}

		public override void CreateContent()
		{
			uiHeader.SetText("Components");

			//uiFace = CreateComponent(PedComponentType.Face, "Face");
			uiMask = CreateComponent(PedComponentType.Mask, "Mask");
			//uiHair = CreateComponent(PedComponentType.Hair, "Hair");
			uiTorso = CreateComponent(PedComponentType.Torso, "Torso");
			uiLegs = CreateComponent(PedComponentType.Legs, "Legs");
			uiBack = CreateComponent(PedComponentType.Back, "Back");
			uiShoes = CreateComponent(PedComponentType.Shoes, "Shoes");
			uiAccessory = CreateComponent(PedComponentType.Accessory, "Accessory");
			uiUndershirt = CreateComponent(PedComponentType.Undershirt, "Undershirt");
			uiKevlar = CreateComponent(PedComponentType.Kevlar, "Kevlar");
			uiBadge = CreateComponent(PedComponentType.Badge, "Badge");
			uiTorso2 = CreateComponent(PedComponentType.Torso2, "Torso2");

			uiHat = CreateProp(PedPropType.Hat, "Hat");
			uiGlasses = CreateProp(PedPropType.Glasses, "Glasses");
			uiEar = CreateProp(PedPropType.Ear, "Ear");
			uiWatch = CreateProp(PedPropType.Watch, "Watch");
			uiBracelet = CreateProp(PedPropType.Bracelet, "Bracelet");

			CreateApplyCancelButtons();
		}

		public void ApplyComponentToPed(PedComponentType type)
		{
			switch (type)
			{
				case PedComponentType.Face:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Face.Index, character.PedComponents.Face.Texture, 0);
					break;
				case PedComponentType.Mask:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Mask.Index, character.PedComponents.Mask.Texture, 0);
					break;
				case PedComponentType.Hair:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Hair.Index, character.PedComponents.Hair.Texture, 0);
					break;
				case PedComponentType.Torso:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Torso.Index, character.PedComponents.Torso.Texture, 0);
					break;
				case PedComponentType.Legs:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Legs.Index, character.PedComponents.Legs.Texture, 0);
					break;
				case PedComponentType.Back:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Back.Index, character.PedComponents.Back.Texture, 0);
					break;
				case PedComponentType.Shoes:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Shoes.Index, character.PedComponents.Shoes.Texture, 0);
					break;
				case PedComponentType.Accessory:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Accessory.Index, character.PedComponents.Accessory.Texture, 0);
					break;
				case PedComponentType.Undershirt:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Undershirt.Index, character.PedComponents.Undershirt.Texture, 0);
					break;
				case PedComponentType.Kevlar:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Kevlar.Index, character.PedComponents.Kevlar.Texture, 0);
					break;
				case PedComponentType.Badge:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Badge.Index, character.PedComponents.Badge.Texture, 0);
					break;
				case PedComponentType.Torso2:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Torso2.Index, character.PedComponents.Torso2.Texture, 0);
					break;
				default:
					break;
			}
		}

		public void ApplyPropToPed(PedPropType type)
		{
			switch (type)
			{
				case PedPropType.Hat:
					API.SetPedPropIndex(Game.PlayerPed.Handle, (int)type, character.PedProps.Hat.Index, character.PedProps.Hat.Texture, true);
					break;
				case PedPropType.Glasses:
					API.SetPedPropIndex(Game.PlayerPed.Handle, (int)type, character.PedProps.Glasses.Index, character.PedProps.Glasses.Texture, true);
					break;
				case PedPropType.Ear:
					API.SetPedPropIndex(Game.PlayerPed.Handle, (int)type, character.PedProps.Ear.Index, character.PedProps.Ear.Texture, true);
					break;
				case PedPropType.Watch:
					API.SetPedPropIndex(Game.PlayerPed.Handle, (int)type, character.PedProps.Watch.Index, character.PedProps.Watch.Texture, true);
					break;
				case PedPropType.Bracelet:
					API.SetPedPropIndex(Game.PlayerPed.Handle, (int)type, character.PedProps.Bracelet.Index, character.PedProps.Bracelet.Texture, true);
					break;
				default:
					break;
			}
		}

		public void DettachProp(PedPropType type)
		{
			switch (type)
			{
				case PedPropType.Hat:
					API.SetPedPropIndex(Game.PlayerPed.Handle, (int)type, character.PedProps.Hat.Index, character.PedProps.Hat.Texture, false);
					break;
				case PedPropType.Glasses:
					API.SetPedPropIndex(Game.PlayerPed.Handle, (int)type, character.PedProps.Glasses.Index, character.PedProps.Glasses.Texture, false);
					break;
				case PedPropType.Ear:
					API.SetPedPropIndex(Game.PlayerPed.Handle, (int)type, character.PedProps.Ear.Index, character.PedProps.Ear.Texture, false);
					break;
				case PedPropType.Watch:
					API.SetPedPropIndex(Game.PlayerPed.Handle, (int)type, character.PedProps.Watch.Index, character.PedProps.Watch.Texture, false);
					break;
				case PedPropType.Bracelet:
					API.SetPedPropIndex(Game.PlayerPed.Handle, (int)type, character.PedProps.Bracelet.Index, character.PedProps.Bracelet.Texture, false);
					break;
				default:
					break;
			}
		}

		public void ApplyToPed()
		{
			//API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Face, character.PedComponents.Face.Index, character.PedComponents.Face.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Mask, character.PedComponents.Mask.Index, character.PedComponents.Mask.Texture, 0);
			//API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Hair, character.PedComponents.Hair.Index, character.PedComponents.Hair.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Torso, character.PedComponents.Torso.Index, character.PedComponents.Torso.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Legs, character.PedComponents.Legs.Index, character.PedComponents.Legs.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Back, character.PedComponents.Back.Index, character.PedComponents.Back.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Shoes, character.PedComponents.Shoes.Index, character.PedComponents.Shoes.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Accessory, character.PedComponents.Accessory.Index, character.PedComponents.Accessory.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Undershirt, character.PedComponents.Undershirt.Index, character.PedComponents.Undershirt.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Kevlar, character.PedComponents.Kevlar.Index, character.PedComponents.Kevlar.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Badge, character.PedComponents.Badge.Index, character.PedComponents.Badge.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Torso2, character.PedComponents.Torso2.Index, character.PedComponents.Torso2.Texture, 0);

			API.SetPedPropIndex(Game.PlayerPed.Handle, (int)PedPropType.Hat, character.PedProps.Hat.Index, character.PedProps.Hat.Texture, true);
			API.SetPedPropIndex(Game.PlayerPed.Handle, (int)PedPropType.Glasses, character.PedProps.Glasses.Index, character.PedProps.Glasses.Texture, true);
			API.SetPedPropIndex(Game.PlayerPed.Handle, (int)PedPropType.Ear, character.PedProps.Ear.Index, character.PedProps.Ear.Texture, true);
			API.SetPedPropIndex(Game.PlayerPed.Handle, (int)PedPropType.Watch, character.PedProps.Watch.Index, character.PedProps.Watch.Texture, true);
			API.SetPedPropIndex(Game.PlayerPed.Handle, (int)PedPropType.Bracelet, character.PedProps.Bracelet.Index, character.PedProps.Bracelet.Texture, true);
		}
	}
}
