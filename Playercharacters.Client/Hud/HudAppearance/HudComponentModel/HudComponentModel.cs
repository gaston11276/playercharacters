using System.Threading.Tasks;
using System.Collections.Generic;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Gaston11276.SimpleUi;

namespace Gaston11276.Playercharacters.Client
{
	class ModelData
	{
		public PedHash pedHash;
		public ModelData(PedHash pedHash)
		{
			this.pedHash = pedHash;
		}
	}

	public class HudComponentModel : HudComponent
	{
		//List<ModelData> modelData;

		// Columns
		UiPanel uiColumnLabels = new UiPanel();
		UiPanel uiColumnNames = new UiPanel();
		UiPanel uiColumnModelId = new UiPanel();
		UiPanel uiColumnDecrease = new UiPanel();
		UiPanel uiColumnIncrease = new UiPanel();

		public MenuEntryModel uiModel;
		private int currentModelIndex = 0;
		private List<ModelData> modelData = new List<ModelData>();


		public HudComponentModel()
		{
			cameraMode = CameraMode.Front;
			CreateModelData(modelData);
		}

		protected override void CreateColumns()
		{
			UiPanel uiCenterPanel = new UiPanel();
			uiCenterPanel.SetPadding(new UiRectangle(defaultPadding));
			uiAppearanceMain.AddElement(uiCenterPanel);

			CreateColumn(uiCenterPanel, HGravity.Left, uiColumnLabels, "");
			CreateColumn(uiCenterPanel, HGravity.Left, uiColumnNames, "");
			CreateColumn(uiCenterPanel, HGravity.Right, uiColumnModelId, "");
			CreateColumn(uiCenterPanel, HGravity.Center, uiColumnDecrease, "");
			CreateColumn(uiCenterPanel, HGravity.Center, uiColumnIncrease, "");
		}

		private MenuEntryModel CreateEntry(int type, string label)
		{
			MenuEntryModel entry = new MenuEntryModel();
			entry.SetDelay(Delay);
			entry.SetLogger(Logger);
			
			entry.uiLabel.SetText("Model");
			entry.uiLabel.SetPadding(new UiRectangle(defaultPadding));
			uiColumnLabels.AddElement(entry.uiLabel);

			entry.uiName.SetText($"name");
			entry.uiName.SetPadding(new UiRectangle(defaultPadding));
			uiColumnNames.AddElement(entry.uiName);

			entry.uiIndex.SetText($"{0}/{397}");
			entry.uiIndex.SetPadding(new UiRectangle(defaultPadding));
			uiColumnModelId.AddElement(entry.uiIndex);

			entry.btnIndexDecrease.SetText("-");
			entry.btnIndexDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnIndexDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnIndexDecrease.RegisterOnLMBRelease(entry.DecreaseIndex);
			inputsOnMouseMove.Add(entry.btnIndexDecrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnIndexDecrease.OnMouseButton);
			uiColumnDecrease.AddElement(entry.btnIndexDecrease);

			entry.btnIndexIncrease.SetText("+");
			entry.btnIndexIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnIndexIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnIndexIncrease.RegisterOnLMBRelease(entry.IncreaseIndex);
			inputsOnMouseMove.Add(entry.btnIndexIncrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnIndexIncrease.OnMouseButton);
			uiColumnIncrease.AddElement(entry.btnIndexIncrease);

			entry.GetIndexMax = GetModelIndexMax;
			entry.GetIndex = GetModelIndex;
			entry.SetIndex = SetModelIndex;
			entry.GetName = GetModelName;

			return entry;
		}

		protected override void CreateContent()
		{
			uiHeader.SetText("Model");
			uiAppearanceMain.SetAlignment(HAlignment.Right);  // Make buttons (right-aligned) appear at the same location regardless of position or size
			uiAppearanceMain.SetAlignment(VAlignment.Bottom);   // Make buttons (bottom-aligned) appear at the same location regardless of position or size

			uiModel = CreateEntry(0, "Model");

			CreateApplyCancelButtons();
		}

		public override async void SetUi()
		{
			currentModelIndex = FindIndex();
			await uiModel.SetUi();
		}

		protected override void OnOpen()
		{
			SetUi();
			base.OnOpen();
		}

		private int GetModelIndexMax()
		{
			return modelData.Count;
		}

		private int FindIndex()
		{
			int cnt = 0;
			foreach (ModelData modelData in modelData)
			{
				if (modelData.pedHash == character.ModelHash)
				{
					return cnt;
				}
				cnt++;
			}
			return 0;
		}

		private int GetModelIndex()
		{
			return currentModelIndex;
		}
		private async void SetModelIndex(int index)
		{
			currentModelIndex = index;
			if (index < modelData.Count)
			{
				ResetPedComponents();
				character.Model = ((uint)modelData[currentModelIndex].pedHash).ToString();
				await ApplyToPed();
			}
		}

		private string GetModelName()
		{
			return character.ModelHash.ToString();
		}

		public async Task ApplyToPed()
		{
			await SetModel();
		}

		public async Task SetDefaults()
		{
			if (character.Gender == (short)Gender.Male)
			{
				character.Model = ((uint)PedHash.FreemodeMale01).ToString();
			}
			else if (character.Gender == (short)Gender.Female)
			{
				character.Model = ((uint)PedHash.FreemodeFemale01).ToString();
			}


			await Delay(10);
		}

		async Task SetModel()
		{
			API.RequestModel((uint)character.ModelHash);

			Vehicle current_vehicle = Game.PlayerPed.CurrentVehicle;
			bool in_vehicle = false;
			if (Game.PlayerPed.IsInVehicle())
			{
				in_vehicle = true;
			}

			await Game.Player.ChangeModel(new Model(character.ModelHash));
			API.SetPedDefaultComponentVariation(Game.PlayerPed.Handle);

			if (in_vehicle)
			{
				Game.PlayerPed.SetIntoVehicle(current_vehicle, VehicleSeat.Driver);
			}
		}

		private void ResetPedComponents()
		{
			character.PedComponents.Face.Index = 0;
			character.PedComponents.Face.Texture = 0;
			character.PedComponents.Mask.Index = 0;
			character.PedComponents.Mask.Texture = 0;
			character.PedComponents.Hair.Index = 0;
			character.PedComponents.Hair.Texture = 0;
			character.PedComponents.Torso.Index = 0;
			character.PedComponents.Torso.Texture = 0;
			character.PedComponents.Legs.Index = 0;
			character.PedComponents.Legs.Texture = 0;
			character.PedComponents.Back.Index = 0;
			character.PedComponents.Back.Texture = 0;
			character.PedComponents.Shoes.Index = 0;
			character.PedComponents.Shoes.Texture = 0;
			character.PedComponents.Accessory.Index = 0;
			character.PedComponents.Accessory.Texture = 0;
			character.PedComponents.Undershirt.Index = 0;
			character.PedComponents.Undershirt.Texture = 0;
			character.PedComponents.Kevlar.Index = 0;
			character.PedComponents.Kevlar.Texture = 0;
			character.PedComponents.Badge.Index = 0;
			character.PedComponents.Badge.Texture = 0;
			character.PedComponents.Torso2.Index = 0;
			character.PedComponents.Torso2.Texture = 0;
			character.PedProps.Hat.Index = 0;
			character.PedProps.Hat.Texture = 0;
			character.PedProps.Glasses.Index = 0;
			character.PedProps.Glasses.Texture = 0;
			character.PedProps.Ear.Index = 0;
			character.PedProps.Ear.Texture = 0;
			character.PedProps.Watch.Index = 0;
			character.PedProps.Watch.Texture = 0;
			character.PedProps.Bracelet.Index = 0;
			character.PedProps.Bracelet.Texture = 0;
		}

		static void CreateModelData(List<ModelData> ModelData)
		{
			foreach (PedHash thisEnum in PedHash.GetValues(typeof(PedHash)))
			{
				ModelData.Add(new ModelData(thisEnum));
			}
		}
	}
}
