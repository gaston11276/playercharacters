using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using NFive.SDK.Core.Diagnostics;
using Gaston11276.SimpleUi;

namespace Gaston11276.Playercharacters.Client
{
	class ModelData
	{
		public string name;
		public uint hash;

		public ModelData(string name, uint hash)
		{
			this.name = name;
			this.hash = hash;
		}
	}

	class UiEntryModel
	{
		ILogger Logger;
		public int type;
		public Textbox uiLabel = new Textbox();
		public Textbox uiName = new Textbox();
		public Textbox uiIndex = new Textbox();
		public Textbox btnIndexDecrease = new Textbox();
		public Textbox btnIndexIncrease = new Textbox();

		public delegate void SetModelIndex(int index);
		public SetModelIndex SetIndex;
		public delegate int GetModelIndex();
		public GetModelIndex GetIndex;
		public GetModelIndex GetIndexMax;

		//public delegate void SetModelName(string name);
		//public SetModelIndex SetName;
		public delegate string GetModelName(int index);
		public GetModelName GetName;

		int currentIndex;


		public UiEntryModel()
		{
			type = 0;
			currentIndex = 0;
		}

		public void SetLogger(ILogger Logger)
		{
			this.Logger = Logger;
		}

		public void SetUi()
		{
			int index = GetIndex();
			int indexMax = GetIndexMax();
			uiIndex.SetText($"{index}/{indexMax}");

			string name = GetName(index);
			uiName.SetText($"{name}");
		}

		public void IncreaseIndex()
		{
			

			//int index = GetIndex();
			Logger.Debug($"Increasing index {currentIndex}");
			int indexMax = GetIndexMax();
			currentIndex++;

			if (currentIndex > indexMax)
			{
				currentIndex = indexMax;
			}

			Logger.Debug($"Increasing index to {currentIndex}");
			uiIndex.SetText($"{currentIndex}/{indexMax}");
			SetIndex(currentIndex);
		}

		public void DecreaseIndex()
		{
			int index = GetIndex();
			int indexMax = GetIndexMax();
			index--;

			if (index < 0)
			{
				index = 0;
			}

			uiIndex.SetText($"{index}/{indexMax}");
			SetIndex(index);
		}
	}

	public class UiModel : UiAppearance
	{
		List<ModelData> modelData;

		// Columns
		UiPanel uiColumnLabels = new UiPanel();
		UiPanel uiColumnNames = new UiPanel();
		UiPanel uiColumnModelId = new UiPanel();
		UiPanel uiColumnDecrease = new UiPanel();
		UiPanel uiColumnIncrease = new UiPanel();

		UiEntryModel uiModel;


		public UiModel()
		{
			modelData = new List<ModelData>();
			CreateModelData(modelData);

			cameraMode = CameraMode.Front;
		}

		public override void CreateColumns()
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

		private UiEntryModel CreateEntry(int type, string label)
		{
			UiEntryModel entry = new UiEntryModel();
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
			entry.GetName= GetModelName;
			//entry.SetName= SetModelName;


			return entry;
		}

		public override void CreateContent()
		{
			uiHeader.SetText("Model");

			uiModel = CreateEntry(0, "Model");

			
		}


		public override void SetUi()
		{
			uiModel.SetUi();
		}

		private int GetModelIndexMax()
		{
			return 710;
		}
		private int GetModelIndex()
		{
			int modelId = 0;
			foreach (ModelData ModelData in modelData)
			{
				if (ModelData.hash == (uint)character.ModelHash)
				{
					Logger.Debug("-----------------------");
					Logger.Debug("Getting model index.");
					Logger.Debug($"Model: {character.Model}");
					Logger.Debug($"Model: {character.ModelHash}");
					Logger.Debug($"Model Id: {modelId }");
					return modelId;
				}
				modelId++;
			}
			
			return modelId;
		}
		private void SetModelIndex(int index)
		{
			GetHashFromIndex(index);
			int cnt = 0;
			foreach (ModelData ModelData in modelData)
			{
				if (cnt == index)
				{
					character.Model = ModelData.name;
					//character.ModelHash = (PedHash)ModelData.hash;

					Logger.Debug("-----------------------");
					Logger.Debug($"Setting model index {index}.");
					Logger.Debug($"Model: {character.Model}");
					return;
				}
				cnt++;
			}

			
			//character.Model = ((uint)(PedHash)API.GetHashKey("mp_m_freemode_01")).ToString();
			//character.Model = ((uint)(character.Gender == 0 ? PedHash.FreemodeMale01 : PedHash.FreemodeFemale01)).ToString();
			ApplyToPed();
		}
		private string GetModelName(int index)
		{
			return GetSkinFromIndex(index);
		}
		//private void SetModelName(int index, string name)
		//{ }

		public void ApplyToPed()
		{
			SetModel();
		}

		private async void SetModel()
		{
			Logger.Debug($"Applying model: {character.Model}");
			await SetModelFromName(character.Model);

			//while (!await Game.Player.ChangeModel(new Model(character.ModelHash)))
			//{
			//	await Task.Delay(10);
			//}
		}





		/// ///
		public uint GetHashFromIndex(int index)
		{
			int cnt = 0;
			foreach (ModelData ModelData in modelData)
			{
				if (cnt == index)
				{
					return ModelData.hash;
				}
				cnt++;
			}
			return 0;
		}
		private string GetModelNameFromIndex(int model_index)
		{
			return GetSkinFromIndex(model_index);
		}

		public async Task SetModelFromIndex(int index)
		{
			string model_name = GetSkinFromIndex(index);
			await SetModelFromName(model_name);
		}

		public async Task SetModelFromName(string name)
		{
			await SetModelFromHash(API.GetHashKey(name));
		}

		private async Task SetModelFromHash(int model_hash)
		{


			if (!API.IsModelInCdimage((uint)model_hash))
			{
				Logger.Debug($"Model not found.");
				return;
			}

			if (!API.IsModelAPed((uint)model_hash))
			{
				Logger.Debug($"Model not a ped.");
				return;
			}

			API.RequestModel((uint)model_hash);

			Vehicle current_vehicle = Game.PlayerPed.CurrentVehicle;
			bool in_vehicle = false;
			if (Game.PlayerPed.IsInVehicle())
			{
				in_vehicle = true;
			}

			await Game.Player.ChangeModel(model_hash);

			//API.SetPedDefaultComponentVariation(Game.PlayerPed.Handle);

			if (in_vehicle)
			{
				//Logger.Debug("Setting into vehicle");
				Game.PlayerPed.SetIntoVehicle(current_vehicle, VehicleSeat.Driver);
			}
		}

		static void CreateModelData(List<ModelData> ModelData)
		{
			ModelData.Add(new ModelData("mp_m_freemode_01", 0x705E61F2));
			ModelData.Add(new ModelData("mp_f_freemode_01", 0x9C9EFFD8));
			ModelData.Add(new ModelData("player_one", 0x9B22DBAF));
			ModelData.Add(new ModelData("player_two", 0x9B810FA2));
			ModelData.Add(new ModelData("player_zero", 0x0D7114C9));

			ModelData.Add(new ModelData("a_c_boar", 0xCE5FF074));
			ModelData.Add(new ModelData("a_c_cat_01", 0x573201B8));
			ModelData.Add(new ModelData("a_c_chickenhawk", 0xAAB71F62));
			ModelData.Add(new ModelData("a_c_chimp", 0xA8683715));
			ModelData.Add(new ModelData("a_c_chop", 0x14EC17EA));
			ModelData.Add(new ModelData("a_c_cormorant", 0x56E29962));
			ModelData.Add(new ModelData("a_c_cow", 0xFCFA9E1E));
			ModelData.Add(new ModelData("a_c_coyote", 0x644AC75E));
			ModelData.Add(new ModelData("a_c_crow", 0x18012A9F));
			ModelData.Add(new ModelData("a_c_deer", 0xD86B5A95));
			ModelData.Add(new ModelData("a_c_dolphin", 0x8BBAB455));
			ModelData.Add(new ModelData("a_c_fish", 0x2FD800B7));
			ModelData.Add(new ModelData("a_c_hen", 0x6AF51FAF));
			ModelData.Add(new ModelData("a_c_humpback", 0x471BE4B2));
			ModelData.Add(new ModelData("a_c_husky", 0x4E8F95A2));
			ModelData.Add(new ModelData("a_c_killerwhale", 0x8D8AC8B9));
			ModelData.Add(new ModelData("a_c_mtlion", 0x1250D7BA));
			ModelData.Add(new ModelData("a_c_pig", 0xB11BAB56));
			ModelData.Add(new ModelData("a_c_pigeon", 0x06A20728));
			ModelData.Add(new ModelData("a_c_poodle", 0x431D501C));
			ModelData.Add(new ModelData("a_c_pug", 0x6D362854));
			ModelData.Add(new ModelData("a_c_rabbit_01", 0xDFB55C81));
			ModelData.Add(new ModelData("a_c_rat", 0xC3B52966));
			ModelData.Add(new ModelData("a_c_retriever", 0x349F33E1));
			ModelData.Add(new ModelData("a_c_rhesus", 0xC2D06F53));
			ModelData.Add(new ModelData("a_c_rottweiler", 0x9563221D));
			ModelData.Add(new ModelData("a_c_seagull", 0xD3939DFD));
			ModelData.Add(new ModelData("a_c_sharkhammer", 0x3C831724));
			ModelData.Add(new ModelData("a_c_sharktiger", 0x06C3F072));
			ModelData.Add(new ModelData("a_c_shepherd", 0x431FC24C));
			ModelData.Add(new ModelData("a_c_stingray", 0xA148614D));
			ModelData.Add(new ModelData("a_c_westy", 0xAD7844BB));
			ModelData.Add(new ModelData("a_f_m_beach_01", 0x303638A7));
			ModelData.Add(new ModelData("a_f_m_bevhills_01", 0xBE086EFD));
			ModelData.Add(new ModelData("a_f_m_bevhills_02", 0xA039335F));
			ModelData.Add(new ModelData("a_f_m_bodybuild_01", 0x3BD99114));
			ModelData.Add(new ModelData("a_f_m_business_02", 0x1FC37DBC));
			ModelData.Add(new ModelData("a_f_m_downtown_01", 0x654AD86E));
			ModelData.Add(new ModelData("a_f_m_eastsa_01", 0x9D3DCB7A));
			ModelData.Add(new ModelData("a_f_m_eastsa_02", 0x63C8D891));
			ModelData.Add(new ModelData("a_f_m_fatbla_01", 0xFAB48BCB));
			ModelData.Add(new ModelData("a_f_m_fatcult_01", 0xB5CF80E4));
			ModelData.Add(new ModelData("a_f_m_fatwhite_01", 0x38BAD33B));
			ModelData.Add(new ModelData("a_f_m_ktown_01", 0x52C824DE));
			ModelData.Add(new ModelData("a_f_m_ktown_02", 0x41018151));
			ModelData.Add(new ModelData("a_f_m_prolhost_01", 0x169BD1E1));
			ModelData.Add(new ModelData("a_f_m_salton_01", 0xDE0E0969));
			ModelData.Add(new ModelData("a_f_m_skidrow_01", 0xB097523B));
			ModelData.Add(new ModelData("a_f_m_soucent_01", 0x745855A1));
			ModelData.Add(new ModelData("a_f_m_soucent_02", 0xF322D338));
			ModelData.Add(new ModelData("a_f_m_soucentmc_01", 0xCDE955D2));
			ModelData.Add(new ModelData("a_f_m_tourist_01", 0x505603B9));
			ModelData.Add(new ModelData("a_f_m_tramp_01", 0x48F96F5B));
			ModelData.Add(new ModelData("a_f_m_trampbeac_01", 0x8CA0C266));
			ModelData.Add(new ModelData("a_f_o_genstreet_01", 0x61C81C85));
			ModelData.Add(new ModelData("a_f_o_indian_01", 0xBAD7BB80));
			ModelData.Add(new ModelData("a_f_o_ktown_01", 0x47CF5E96));
			ModelData.Add(new ModelData("a_f_o_salton_01", 0xCCFF7D8A));
			ModelData.Add(new ModelData("a_f_o_soucent_01", 0x3DFA1830));
			ModelData.Add(new ModelData("a_f_o_soucent_02", 0xA56DE716));
			ModelData.Add(new ModelData("a_f_y_beach_01", 0xC79F6928));
			ModelData.Add(new ModelData("a_f_y_bevhills_01", 0x445AC854));
			ModelData.Add(new ModelData("a_f_y_bevhills_02", 0x5C2CF7F8));
			ModelData.Add(new ModelData("a_f_y_bevhills_03", 0x20C8012F));
			ModelData.Add(new ModelData("a_f_y_bevhills_04", 0x36DF2D5D));
			ModelData.Add(new ModelData("a_f_y_business_01", 0x2799EFD8));
			ModelData.Add(new ModelData("a_f_y_business_02", 0x31430342));
			ModelData.Add(new ModelData("a_f_y_business_03", 0xAE86FDB4));
			ModelData.Add(new ModelData("a_f_y_business_04", 0xB7C61032));
			ModelData.Add(new ModelData("a_f_y_eastsa_01", 0xF5B0079D));
			ModelData.Add(new ModelData("a_f_y_eastsa_02", 0x0438A4AE));
			ModelData.Add(new ModelData("a_f_y_eastsa_03", 0x51C03FA4));
			ModelData.Add(new ModelData("a_f_y_epsilon_01", 0x689C2A80));
			ModelData.Add(new ModelData("a_f_y_femaleagent", 0x50610C43));
			ModelData.Add(new ModelData("a_f_y_fitness_01", 0x457C64FB));
			ModelData.Add(new ModelData("a_f_y_fitness_02", 0x13C4818C));
			ModelData.Add(new ModelData("a_f_y_genhot_01", 0x2F4AEC3E));
			ModelData.Add(new ModelData("a_f_y_golfer_01", 0x7DD8FB58));
			ModelData.Add(new ModelData("a_f_y_hiker_01", 0x30830813));
			ModelData.Add(new ModelData("a_f_y_hippie_01", 0x1475B827));
			ModelData.Add(new ModelData("a_f_y_hipster_01", 0x8247D331));
			ModelData.Add(new ModelData("a_f_y_hipster_02", 0x97F5FE8D));
			ModelData.Add(new ModelData("a_f_y_hipster_03", 0xA5BA9A16));
			ModelData.Add(new ModelData("a_f_y_hipster_04", 0x199881DC));
			ModelData.Add(new ModelData("a_f_y_indian_01", 0x092D9CC1));
			ModelData.Add(new ModelData("a_f_y_juggalo_01", 0xDB134533));
			ModelData.Add(new ModelData("a_f_y_runner_01", 0xC7496729));
			ModelData.Add(new ModelData("a_f_y_rurmeth_01", 0x3F789426));
			ModelData.Add(new ModelData("a_f_y_scdressy_01", 0xDB5EC400));
			ModelData.Add(new ModelData("a_f_y_skater_01", 0x695FE666));
			ModelData.Add(new ModelData("a_f_y_soucent_01", 0x2C641D7A));
			ModelData.Add(new ModelData("a_f_y_soucent_02", 0x5A8EF9CF));
			ModelData.Add(new ModelData("a_f_y_soucent_03", 0x87B25415));
			ModelData.Add(new ModelData("a_f_y_tennis_01", 0x550C79C6));
			ModelData.Add(new ModelData("a_f_y_topless_01", 0x9CF26183));
			ModelData.Add(new ModelData("a_f_y_tourist_01", 0x563B8570));
			ModelData.Add(new ModelData("a_f_y_tourist_02", 0x9123FB40));
			ModelData.Add(new ModelData("a_f_y_vinewood_01", 0x19F41F65));
			ModelData.Add(new ModelData("a_f_y_vinewood_02", 0xDAB6A0EB));
			ModelData.Add(new ModelData("a_f_y_vinewood_03", 0x379DDAB8));
			ModelData.Add(new ModelData("a_f_y_vinewood_04", 0xFAE46146));
			ModelData.Add(new ModelData("a_f_y_yoga_01", 0xC41B062E));
			ModelData.Add(new ModelData("a_m_m_acult_01", 0x5442C66B));
			ModelData.Add(new ModelData("a_m_m_afriamer_01", 0xD172497E));
			ModelData.Add(new ModelData("a_m_m_beach_01", 0x403DB4FD));
			ModelData.Add(new ModelData("a_m_m_beach_02", 0x787FA588));
			ModelData.Add(new ModelData("a_m_m_bevhills_01", 0x54DBEE1F));
			ModelData.Add(new ModelData("a_m_m_bevhills_02", 0x3FB5C3D3));
			ModelData.Add(new ModelData("a_m_m_business_01", 0x7E6A64B7));
			ModelData.Add(new ModelData("a_m_m_eastsa_01", 0xF9A6F53F));
			ModelData.Add(new ModelData("a_m_m_eastsa_02", 0x07DD91AC));
			ModelData.Add(new ModelData("a_m_m_farmer_01", 0x94562DD7));
			ModelData.Add(new ModelData("a_m_m_fatlatin_01", 0x61D201B3));
			ModelData.Add(new ModelData("a_m_m_genfat_01", 0x06DD569F));
			ModelData.Add(new ModelData("a_m_m_genfat_02", 0x13AEF042));
			ModelData.Add(new ModelData("a_m_m_golfer_01", 0xA9EB0E42));
			ModelData.Add(new ModelData("a_m_m_hasjew_01", 0x6BD9B68C));
			ModelData.Add(new ModelData("a_m_m_hillbilly_01", 0x6C9B2849));
			ModelData.Add(new ModelData("a_m_m_hillbilly_02", 0x7B0E452F));
			ModelData.Add(new ModelData("a_m_m_indian_01", 0xDDCAAA2C));
			ModelData.Add(new ModelData("a_m_m_ktown_01", 0xD15D7E71));
			ModelData.Add(new ModelData("a_m_m_malibu_01", 0x2FDE6EB7));
			ModelData.Add(new ModelData("a_m_m_mexcntry_01", 0xDD817EAD));
			ModelData.Add(new ModelData("a_m_m_mexlabor_01", 0xB25D16B2));
			ModelData.Add(new ModelData("a_m_m_og_boss_01", 0x681BD012));
			ModelData.Add(new ModelData("a_m_m_paparazzi_01", 0xECCA8C15));
			ModelData.Add(new ModelData("a_m_m_polynesian_01", 0xA9D9B69E));
			ModelData.Add(new ModelData("a_m_m_prolhost_01", 0x9712C38F));
			ModelData.Add(new ModelData("a_m_m_rurmeth_01", 0x3BAD4184));
			ModelData.Add(new ModelData("a_m_m_salton_01", 0x4F2E038A));
			ModelData.Add(new ModelData("a_m_m_salton_02", 0x60F4A717));
			ModelData.Add(new ModelData("a_m_m_salton_03", 0xB28C4A45));
			ModelData.Add(new ModelData("a_m_m_salton_04", 0x964511B7));
			ModelData.Add(new ModelData("a_m_m_skater_01", 0xD9D7588C));
			ModelData.Add(new ModelData("a_m_m_skidrow_01", 0x01EEA6BD));
			ModelData.Add(new ModelData("a_m_m_socenlat_01", 0x0B8D69E3));
			ModelData.Add(new ModelData("a_m_m_soucent_01", 0x6857C9B7));
			ModelData.Add(new ModelData("a_m_m_soucent_02", 0x9F6D37E1));
			ModelData.Add(new ModelData("a_m_m_soucent_03", 0x8BD990BA));
			ModelData.Add(new ModelData("a_m_m_soucent_04", 0xC2FBFEFE));
			ModelData.Add(new ModelData("a_m_m_stlat_02", 0xC2A87702));
			ModelData.Add(new ModelData("a_m_m_tennis_01", 0x546A5344));
			ModelData.Add(new ModelData("a_m_m_tourist_01", 0xC89F0184));
			ModelData.Add(new ModelData("a_m_m_tramp_01", 0x1EC93FD0));
			ModelData.Add(new ModelData("a_m_m_trampbeac_01", 0x53B57EB0));
			ModelData.Add(new ModelData("a_m_m_tranvest_01", 0xE0E69974));
			ModelData.Add(new ModelData("a_m_m_tranvest_02", 0xF70EC5C4));
			ModelData.Add(new ModelData("a_m_o_acult_01", 0x55446010));
			ModelData.Add(new ModelData("a_m_o_acult_02", 0x4BA14CCA));
			ModelData.Add(new ModelData("a_m_o_beach_01", 0x8427D398));
			ModelData.Add(new ModelData("a_m_o_genstreet_01", 0xAD54E7A8));
			ModelData.Add(new ModelData("a_m_o_ktown_01", 0x1536D95A));
			ModelData.Add(new ModelData("a_m_o_salton_01", 0x20208E4D));
			ModelData.Add(new ModelData("a_m_o_soucent_01", 0x2AD8921B));
			ModelData.Add(new ModelData("a_m_o_soucent_02", 0x4086BD77));
			ModelData.Add(new ModelData("a_m_o_soucent_03", 0x0E32D8D0));
			ModelData.Add(new ModelData("a_m_o_tramp_01", 0x174D4245));
			ModelData.Add(new ModelData("a_m_y_acult_01", 0xB564882B));
			ModelData.Add(new ModelData("a_m_y_acult_02", 0x80E59F2E));
			ModelData.Add(new ModelData("a_m_y_beach_01", 0xD1FEB884));
			ModelData.Add(new ModelData("a_m_y_beach_02", 0x23C7DC11));
			ModelData.Add(new ModelData("a_m_y_beach_03", 0xE7A963D9));
			ModelData.Add(new ModelData("a_m_y_beachvesp_01", 0x7E0961B8));
			ModelData.Add(new ModelData("a_m_y_beachvesp_02", 0xCA56FA52));
			ModelData.Add(new ModelData("a_m_y_bevhills_01", 0x76284640));
			ModelData.Add(new ModelData("a_m_y_bevhills_02", 0x668BA707));
			ModelData.Add(new ModelData("a_m_y_breakdance_01", 0x379F9596));
			ModelData.Add(new ModelData("a_m_y_busicas_01", 0x9AD32FE9));
			ModelData.Add(new ModelData("a_m_y_business_01", 0xC99F21C4));
			ModelData.Add(new ModelData("a_m_y_business_02", 0xB3B3F5E6));
			ModelData.Add(new ModelData("a_m_y_business_03", 0xA1435105));
			ModelData.Add(new ModelData("a_m_y_cyclist_01", 0xFDC653C7));
			ModelData.Add(new ModelData("a_m_y_dhill_01", 0xFF3E88AB));
			ModelData.Add(new ModelData("a_m_y_downtown_01", 0x2DADF4AA));
			ModelData.Add(new ModelData("a_m_y_eastsa_01", 0xA4471173));
			ModelData.Add(new ModelData("a_m_y_eastsa_02", 0x168775F6));
			ModelData.Add(new ModelData("a_m_y_epsilon_01", 0x77D41A3E));
			ModelData.Add(new ModelData("a_m_y_epsilon_02", 0xAA82FF9B));
			ModelData.Add(new ModelData("a_m_y_gay_01", 0xD1CCE036));
			ModelData.Add(new ModelData("a_m_y_gay_02", 0xA5720781));
			ModelData.Add(new ModelData("a_m_y_genstreet_01", 0x9877EF71));
			ModelData.Add(new ModelData("a_m_y_genstreet_02", 0x3521A8D2));
			ModelData.Add(new ModelData("a_m_y_golfer_01", 0xD71FE131));
			ModelData.Add(new ModelData("a_m_y_hasjew_01", 0xE16D8F01));
			ModelData.Add(new ModelData("a_m_y_hiker_01", 0x50F73C0C));
			ModelData.Add(new ModelData("a_m_y_hippy_01", 0x7D03E617));
			ModelData.Add(new ModelData("a_m_y_hipster_01", 0x2307A353));
			ModelData.Add(new ModelData("a_m_y_hipster_02", 0x14D506EE));
			ModelData.Add(new ModelData("a_m_y_hipster_03", 0x4E4179C6));
			ModelData.Add(new ModelData("a_m_y_indian_01", 0x2A22FBCE));
			ModelData.Add(new ModelData("a_m_y_jetski_01", 0x2DB7EEF3));
			ModelData.Add(new ModelData("a_m_y_juggalo_01", 0x91CA3E2C));
			ModelData.Add(new ModelData("a_m_y_ktown_01", 0x1AF6542C));
			ModelData.Add(new ModelData("a_m_y_ktown_02", 0x297FF13F));
			ModelData.Add(new ModelData("a_m_y_latino_01", 0x132C1A8E));
			ModelData.Add(new ModelData("a_m_y_methhead_01", 0x696BE0A9));
			ModelData.Add(new ModelData("a_m_y_mexthug_01", 0x3053E555));
			ModelData.Add(new ModelData("a_m_y_motox_01", 0x64FDEA7D));
			ModelData.Add(new ModelData("a_m_y_motox_02", 0x77AC8FDA));
			ModelData.Add(new ModelData("a_m_y_musclbeac_01", 0x4B652906));
			ModelData.Add(new ModelData("a_m_y_musclbeac_02", 0xC923247C));
			ModelData.Add(new ModelData("a_m_y_polynesian_01", 0x8384FC9F));
			ModelData.Add(new ModelData("a_m_y_roadcyc_01", 0xF561A4C6));
			ModelData.Add(new ModelData("a_m_y_runner_01", 0x25305EEE));
			ModelData.Add(new ModelData("a_m_y_runner_02", 0x843D9D0F));
			ModelData.Add(new ModelData("a_m_y_salton_01", 0xD7606C30));
			ModelData.Add(new ModelData("a_m_y_skater_01", 0xC1C46677));
			ModelData.Add(new ModelData("a_m_y_skater_02", 0xAFFAC2E4));
			ModelData.Add(new ModelData("a_m_y_soucent_01", 0xE716BDCB));
			ModelData.Add(new ModelData("a_m_y_soucent_02", 0xACA3C8CA));
			ModelData.Add(new ModelData("a_m_y_soucent_03", 0xC3F0F764));
			ModelData.Add(new ModelData("a_m_y_soucent_04", 0x8A3703F1));
			ModelData.Add(new ModelData("a_m_y_stbla_01", 0xCF92ADE9));
			ModelData.Add(new ModelData("a_m_y_stbla_02", 0x98C7404F));
			ModelData.Add(new ModelData("a_m_y_stlat_01", 0x8674D5FC));
			ModelData.Add(new ModelData("a_m_y_stwhi_01", 0x2418C430));
			ModelData.Add(new ModelData("a_m_y_stwhi_02", 0x36C6E98C));
			ModelData.Add(new ModelData("a_m_y_sunbathe_01", 0xB7292F0C));
			ModelData.Add(new ModelData("a_m_y_surfer_01", 0xEAC2C7EE));
			ModelData.Add(new ModelData("a_m_y_vindouche_01", 0xC19377E7));
			ModelData.Add(new ModelData("a_m_y_vinewood_01", 0x4B64199D));
			ModelData.Add(new ModelData("a_m_y_vinewood_02", 0x5D15BD00));
			ModelData.Add(new ModelData("a_m_y_vinewood_03", 0x1FDF4294));
			ModelData.Add(new ModelData("a_m_y_vinewood_04", 0x31C9E669));
			ModelData.Add(new ModelData("a_m_y_yoga_01", 0xAB0A7155));
			ModelData.Add(new ModelData("cs_amandatownley", 0x95EF18E3));
			ModelData.Add(new ModelData("cs_andreas", 0xE7565327));
			ModelData.Add(new ModelData("cs_ashley", 0x26C3D079));
			ModelData.Add(new ModelData("cs_bankman", 0x9760192E));
			ModelData.Add(new ModelData("cs_barry", 0x69591CF7));
			ModelData.Add(new ModelData("cs_beverly", 0xB46EC356));
			ModelData.Add(new ModelData("cs_brad", 0xEFE5AFE6));
			ModelData.Add(new ModelData("cs_bradcadaver", 0x7228AF60));
			ModelData.Add(new ModelData("cs_carbuyer", 0x8CCE790F));
			ModelData.Add(new ModelData("cs_casey", 0xEA969C40));
			ModelData.Add(new ModelData("cs_chengsr", 0x30DB9D7B));
			ModelData.Add(new ModelData("cs_chrisformage", 0xC1F380E6));
			ModelData.Add(new ModelData("cs_clay", 0xDBCB9834));
			ModelData.Add(new ModelData("cs_dale", 0x0CE81655));
			ModelData.Add(new ModelData("cs_davenorton", 0x8587248C));
			ModelData.Add(new ModelData("cs_debra", 0xECD04FE9));
			ModelData.Add(new ModelData("cs_denise", 0x6F802738));
			ModelData.Add(new ModelData("cs_devin", 0x2F016D02));
			ModelData.Add(new ModelData("cs_dom", 0x4772AF42));
			ModelData.Add(new ModelData("cs_dreyfuss", 0x3C60A153));
			ModelData.Add(new ModelData("cs_drfriedlander", 0xA3A35C2F));
			ModelData.Add(new ModelData("cs_fabien", 0x47035EC1));
			ModelData.Add(new ModelData("cs_fbisuit_01", 0x585C0B52));
			ModelData.Add(new ModelData("cs_floyd", 0x062547E7));
			ModelData.Add(new ModelData("cs_guadalope", 0x0F9513F1));
			ModelData.Add(new ModelData("cs_gurk", 0xC314F727));
			ModelData.Add(new ModelData("cs_hunter", 0x5B44892C));
			ModelData.Add(new ModelData("cs_janet", 0x3034F9E2));
			ModelData.Add(new ModelData("cs_jewelass", 0x4440A804));
			ModelData.Add(new ModelData("cs_jimmyboston", 0x039677BD));
			ModelData.Add(new ModelData("cs_jimmydisanto", 0xB8CC92B4));
			ModelData.Add(new ModelData("cs_joeminuteman", 0xF09D5E29));
			ModelData.Add(new ModelData("cs_johnnyklebitz", 0xFA8AB881));
			ModelData.Add(new ModelData("cs_josef", 0x459762CA));
			ModelData.Add(new ModelData("cs_josh", 0x450EEF9D));
			ModelData.Add(new ModelData("cs_karen_daniels", 0x4BAF381C));
			ModelData.Add(new ModelData("cs_lamardavis", 0x45463A0D));
			ModelData.Add(new ModelData("cs_lazlow", 0x38951A1B));
			ModelData.Add(new ModelData("cs_lestercrest", 0xB594F5C3));
			ModelData.Add(new ModelData("cs_lifeinvad_01", 0x72551375));
			ModelData.Add(new ModelData("cs_magenta", 0x5816C61A));
			ModelData.Add(new ModelData("cs_manuel", 0xFBB374CA));
			ModelData.Add(new ModelData("cs_marnie", 0x574DE134));
			ModelData.Add(new ModelData("cs_martinmadrazo", 0x43595670));
			ModelData.Add(new ModelData("cs_maryann", 0x0998C7AD));
			ModelData.Add(new ModelData("cs_michelle", 0x70AEB9C8));
			ModelData.Add(new ModelData("cs_milton", 0xB76A330F));
			ModelData.Add(new ModelData("cs_molly", 0x45918E44));
			ModelData.Add(new ModelData("cs_movpremf_01", 0x4BBA84D9));
			ModelData.Add(new ModelData("cs_movpremmale", 0x8D67EE7D));
			ModelData.Add(new ModelData("cs_mrk", 0xC3CC9A75));
			ModelData.Add(new ModelData("cs_mrs_thornhill", 0x4F921E6E));
			ModelData.Add(new ModelData("cs_mrsphillips", 0xCBFDA3CF));
			ModelData.Add(new ModelData("cs_natalia", 0x4EFEB1F0));
			ModelData.Add(new ModelData("cs_nervousron", 0x7896DA94));
			ModelData.Add(new ModelData("cs_nigel", 0xE1479C0B));
			ModelData.Add(new ModelData("cs_old_man1a", 0x1EEC7BDC));
			ModelData.Add(new ModelData("cs_old_man2", 0x98F9E770));
			ModelData.Add(new ModelData("cs_omega", 0x8B70B405));
			ModelData.Add(new ModelData("cs_orleans", 0xAD340F5A));
			ModelData.Add(new ModelData("cs_paper", 0x6B38B8F8));
			ModelData.Add(new ModelData("cs_patricia", 0xDF8B1301));
			ModelData.Add(new ModelData("cs_priest", 0x4D6DE57E));
			ModelData.Add(new ModelData("cs_prolsec_02", 0x1E9314A2));
			ModelData.Add(new ModelData("cs_russiandrunk", 0x46521A32));
			ModelData.Add(new ModelData("cs_siemonyetarian", 0xC0937202));
			ModelData.Add(new ModelData("cs_solomon", 0xF6D1E04E));
			ModelData.Add(new ModelData("cs_stevehains", 0xA4E0A1FE));
			ModelData.Add(new ModelData("cs_stretch", 0x893D6805));
			ModelData.Add(new ModelData("cs_tanisha", 0x42FE5370));
			ModelData.Add(new ModelData("cs_taocheng", 0x8864083D));
			ModelData.Add(new ModelData("cs_taostranslator", 0x53536529));
			ModelData.Add(new ModelData("cs_tenniscoach", 0x5C26040A));
			ModelData.Add(new ModelData("cs_terry", 0x3A5201C5));
			ModelData.Add(new ModelData("cs_tom", 0x69E8ABC3));
			ModelData.Add(new ModelData("cs_tomepsilon", 0x8C0FD4E2));
			ModelData.Add(new ModelData("cs_tracydisanto", 0x0609B130));
			ModelData.Add(new ModelData("cs_wade", 0xD266D9D6));
			ModelData.Add(new ModelData("cs_zimbor", 0xEAACAAF0));
			ModelData.Add(new ModelData("csb_abigail", 0x89768941));
			ModelData.Add(new ModelData("csb_agent", 0xD770C9B4));
			ModelData.Add(new ModelData("csb_anita", 0x0703F106));
			ModelData.Add(new ModelData("csb_anton", 0xA5C787B6));
			ModelData.Add(new ModelData("csb_ballasog", 0xABEF0004));
			ModelData.Add(new ModelData("csb_bride", 0x82BF7EA1));
			ModelData.Add(new ModelData("csb_burgerdrug", 0x8CDCC057));
			ModelData.Add(new ModelData("csb_car3guy1", 0x04430687));
			ModelData.Add(new ModelData("csb_car3guy2", 0x1383A508));
			ModelData.Add(new ModelData("csb_chef", 0xA347CA8A));
			ModelData.Add(new ModelData("csb_chef2", 0xAE5BE23A));
			ModelData.Add(new ModelData("csb_chin_goon", 0xA8C22996));
			ModelData.Add(new ModelData("csb_cletus", 0xCAE9E5D5));
			ModelData.Add(new ModelData("csb_cop", 0x9AB35F63));
			ModelData.Add(new ModelData("csb_customer", 0xA44F6F8B));
			ModelData.Add(new ModelData("csb_denise_friend", 0xB58D2529));
			ModelData.Add(new ModelData("csb_fos_rep", 0x1BCC157B));
			ModelData.Add(new ModelData("csb_g", 0xA28E71D7));
			ModelData.Add(new ModelData("csb_groom", 0x7AAB19D2));
			ModelData.Add(new ModelData("csb_grove_str_dlr", 0xE8594E22));
			ModelData.Add(new ModelData("csb_hao", 0xEC9E8F1C));
			ModelData.Add(new ModelData("csb_hugh", 0x6F139B54));
			ModelData.Add(new ModelData("csb_imran", 0xE3420BDB));
			ModelData.Add(new ModelData("csb_jackhowitzer", 0x44BC7BB1));
			ModelData.Add(new ModelData("csb_janitor", 0xC2005A40));
			ModelData.Add(new ModelData("csb_maude", 0xBCC475CB));
			ModelData.Add(new ModelData("csb_money", 0x989DFD9A));
			ModelData.Add(new ModelData("csb_mp_agent14", 0x6DBBFC8B));
			ModelData.Add(new ModelData("csb_mweather", 0x613E626C));
			ModelData.Add(new ModelData("csb_ortega", 0xC0DB04CF));
			ModelData.Add(new ModelData("csb_oscar", 0xF41F399B));
			ModelData.Add(new ModelData("csb_paige", 0x5B1FA0C3));
			ModelData.Add(new ModelData("csb_popov", 0x617D89E2));
			ModelData.Add(new ModelData("csb_porndudes", 0x2F4AFE35));
			ModelData.Add(new ModelData("csb_prologuedriver", 0xF00B49DB));
			ModelData.Add(new ModelData("csb_prolsec", 0x7FA2F024));
			ModelData.Add(new ModelData("csb_ramp_gang", 0xC2800DBE));
			ModelData.Add(new ModelData("csb_ramp_hic", 0x858C94B8));
			ModelData.Add(new ModelData("csb_ramp_hipster", 0x21F58BB4));
			ModelData.Add(new ModelData("csb_ramp_marine", 0x616C97B9));
			ModelData.Add(new ModelData("csb_ramp_mex", 0xF64ED7D0));
			ModelData.Add(new ModelData("csb_rashcosvki", 0x188099A9));
			ModelData.Add(new ModelData("csb_reporter", 0x2E420A24));
			ModelData.Add(new ModelData("csb_roccopelosi", 0xAA64168C));
			ModelData.Add(new ModelData("csb_screen_writer", 0x8BE12CEC));
			ModelData.Add(new ModelData("csb_stripper_01", 0xAEEA76B5));
			ModelData.Add(new ModelData("csb_stripper_02", 0x81441B71));
			ModelData.Add(new ModelData("csb_tonya", 0x6343DD19));
			ModelData.Add(new ModelData("csb_trafficwarden", 0xDE2937F3));
			ModelData.Add(new ModelData("csb_undercover", 0xEF785A6A));
			ModelData.Add(new ModelData("csb_vagspeak", 0x48FF4CA9));
			ModelData.Add(new ModelData("g_f_importexport_01", 0x84A1B11A));
			ModelData.Add(new ModelData("g_f_y_ballas_01", 0x158C439C));
			ModelData.Add(new ModelData("g_f_y_families_01", 0x4E0CE5D3));
			ModelData.Add(new ModelData("g_f_y_lost_01", 0xFD5537DE));
			ModelData.Add(new ModelData("g_f_y_vagos_01", 0x5AA42C21));
			ModelData.Add(new ModelData("g_m_importexport_01", 0xBCA2CCEA));
			ModelData.Add(new ModelData("g_m_m_armboss_01", 0xF1E823A2));
			ModelData.Add(new ModelData("g_m_m_armgoon_01", 0xFDA94268));
			ModelData.Add(new ModelData("g_m_m_armlieut_01", 0xE7714013));
			ModelData.Add(new ModelData("g_m_m_chemwork_01", 0xF6157D8F));
			ModelData.Add(new ModelData("g_m_m_chiboss_01", 0xB9DD0300));
			ModelData.Add(new ModelData("g_m_m_chicold_01", 0x106D9A99));
			ModelData.Add(new ModelData("g_m_m_chigoon_01", 0x7E4F763F));
			ModelData.Add(new ModelData("g_m_m_chigoon_02", 0xFF71F826));
			ModelData.Add(new ModelData("g_m_m_korboss_01", 0x352A026F));
			ModelData.Add(new ModelData("g_m_m_mexboss_01", 0x5761F4AD));
			ModelData.Add(new ModelData("g_m_m_mexboss_02", 0x4914D813));
			ModelData.Add(new ModelData("g_m_y_armgoon_02", 0xC54E878A));
			ModelData.Add(new ModelData("g_m_y_azteca_01", 0x68709618));
			ModelData.Add(new ModelData("g_m_y_ballaeast_01", 0xF42EE883));
			ModelData.Add(new ModelData("g_m_y_ballaorig_01", 0x231AF63F));
			ModelData.Add(new ModelData("g_m_y_ballasout_01", 0x23B88069));
			ModelData.Add(new ModelData("g_m_y_famca_01", 0xE83B93B7));
			ModelData.Add(new ModelData("g_m_y_famdnf_01", 0xDB729238));
			ModelData.Add(new ModelData("g_m_y_famfor_01", 0x84302B09));
			ModelData.Add(new ModelData("g_m_y_korean_01", 0x247502A9));
			ModelData.Add(new ModelData("g_m_y_korean_02", 0x8FEDD989));
			ModelData.Add(new ModelData("g_m_y_korlieut_01", 0x7CCBE17A));
			ModelData.Add(new ModelData("g_m_y_lost_01", 0x4F46D607));
			ModelData.Add(new ModelData("g_m_y_lost_02", 0x3D843282));
			ModelData.Add(new ModelData("g_m_y_lost_03", 0x32B11CDC));
			ModelData.Add(new ModelData("g_m_y_mexgang_01", 0xBDDD5546));
			ModelData.Add(new ModelData("g_m_y_mexgoon_01", 0x26EF3426));
			ModelData.Add(new ModelData("g_m_y_mexgoon_02", 0x31A3498E));
			ModelData.Add(new ModelData("g_m_y_mexgoon_03", 0x964D12DC));
			ModelData.Add(new ModelData("g_m_y_pologoon_01", 0x4F3FBA06));
			ModelData.Add(new ModelData("g_m_y_pologoon_02", 0xA2E86156));
			ModelData.Add(new ModelData("g_m_y_salvaboss_01", 0x905CE0CA));
			ModelData.Add(new ModelData("g_m_y_salvagoon_01", 0x278C8CB7));
			ModelData.Add(new ModelData("g_m_y_salvagoon_02", 0x3273A285));
			ModelData.Add(new ModelData("g_m_y_salvagoon_03", 0x03B8C510));
			ModelData.Add(new ModelData("g_m_y_strpunk_01", 0xFD1C49BB));
			ModelData.Add(new ModelData("g_m_y_strpunk_02", 0x0DA1EAC6));
			ModelData.Add(new ModelData("hc_driver", 0x3B474ADF));
			ModelData.Add(new ModelData("hc_gunman", 0x0B881AEE));
			ModelData.Add(new ModelData("hc_hacker", 0x99BB00F8));
			ModelData.Add(new ModelData("ig_abigail", 0x400AEC41));
			ModelData.Add(new ModelData("ig_agent", 0x246AF208));
			ModelData.Add(new ModelData("ig_amandatownley", 0x6D1E15F7));
			ModelData.Add(new ModelData("ig_andreas", 0x47E4EEA0));
			ModelData.Add(new ModelData("ig_ashley", 0x7EF440DB));
			ModelData.Add(new ModelData("ig_avon", 0xFCE270C2));
			ModelData.Add(new ModelData("ig_ballasog", 0xA70B4A92));
			ModelData.Add(new ModelData("ig_bankman", 0x909D9E7F));
			ModelData.Add(new ModelData("ig_barry", 0x2F8845A3));
			ModelData.Add(new ModelData("ig_benny", 0xC4B715D2));
			ModelData.Add(new ModelData("ig_bestmen", 0x5746CD96));
			ModelData.Add(new ModelData("ig_beverly", 0xBDA21E5C));
			ModelData.Add(new ModelData("ig_brad", 0xBDBB4922));
			ModelData.Add(new ModelData("ig_bride", 0x6162EC47));
			ModelData.Add(new ModelData("ig_car3guy1", 0x84F9E937));
			ModelData.Add(new ModelData("ig_car3guy2", 0x75C34ACA));
			ModelData.Add(new ModelData("ig_casey", 0xE0FA2554));
			ModelData.Add(new ModelData("ig_chef", 0x49EADBF6));
			ModelData.Add(new ModelData("ig_chef2", 0x85889AC3));
			ModelData.Add(new ModelData("ig_chengsr", 0xAAE4EA7B));
			ModelData.Add(new ModelData("ig_chrisformage", 0x286E54A7));
			ModelData.Add(new ModelData("ig_clay", 0x6CCFE08A));
			ModelData.Add(new ModelData("ig_claypain", 0x9D0087A8));
			ModelData.Add(new ModelData("ig_cletus", 0xE6631195));
			ModelData.Add(new ModelData("ig_dale", 0x467415E9));
			ModelData.Add(new ModelData("ig_davenorton", 0x15CD4C33));
			ModelData.Add(new ModelData("ig_denise", 0x820B33BD));
			ModelData.Add(new ModelData("ig_devin", 0x7461A0B0));
			ModelData.Add(new ModelData("ig_dom", 0x9C2DB088));
			ModelData.Add(new ModelData("ig_dreyfuss", 0xDA890932));
			ModelData.Add(new ModelData("ig_drfriedlander", 0xCBFC0DF5));
			ModelData.Add(new ModelData("ig_fabien", 0xD090C350));
			ModelData.Add(new ModelData("ig_fbisuit_01", 0x3AE4A33B));
			ModelData.Add(new ModelData("ig_floyd", 0xB1B196B2));
			ModelData.Add(new ModelData("ig_g", 0x841BA933));
			ModelData.Add(new ModelData("ig_groom", 0xFECE8B85));
			ModelData.Add(new ModelData("ig_hao", 0x65978363));
			ModelData.Add(new ModelData("ig_hunter", 0xCE1324DE));
			ModelData.Add(new ModelData("ig_janet", 0x0D6D9C49));
			ModelData.Add(new ModelData("ig_jay_norris", 0x7A32EE74));
			ModelData.Add(new ModelData("ig_jewelass", 0x0F5D26BB));
			ModelData.Add(new ModelData("ig_jimmyboston", 0xEDA0082D));
			ModelData.Add(new ModelData("ig_jimmydisanto", 0x570462B9));
			ModelData.Add(new ModelData("ig_joeminuteman", 0xBE204C9B));
			ModelData.Add(new ModelData("ig_johnnyklebitz", 0x87CA80AE));
			ModelData.Add(new ModelData("ig_josef", 0xE11A9FB4));
			ModelData.Add(new ModelData("ig_josh", 0x799E9EEE));
			ModelData.Add(new ModelData("ig_karen_daniels", 0xEB51D959));
			ModelData.Add(new ModelData("ig_kerrymcintosh", 0x5B3BD90D));
			ModelData.Add(new ModelData("ig_lamardavis", 0x65B93076));
			ModelData.Add(new ModelData("ig_lazlow", 0xDFE443E5));
			ModelData.Add(new ModelData("ig_lestercrest_2", 0x6E42FD26));
			ModelData.Add(new ModelData("ig_lestercrest", 0x4DA6E849));
			ModelData.Add(new ModelData("ig_lifeinvad_01", 0x5389A93C));
			ModelData.Add(new ModelData("ig_lifeinvad_02", 0x27BD51D4));
			ModelData.Add(new ModelData("ig_magenta", 0xFCDC910A));
			ModelData.Add(new ModelData("ig_malc", 0xF1BCA919));
			ModelData.Add(new ModelData("ig_manuel", 0xFD418E10));
			ModelData.Add(new ModelData("ig_marnie", 0x188232D0));
			ModelData.Add(new ModelData("ig_maryann", 0xA36F9806));
			ModelData.Add(new ModelData("ig_maude", 0x3BE8287E));
			ModelData.Add(new ModelData("ig_michelle", 0xBF9672F4));
			ModelData.Add(new ModelData("ig_milton", 0xCB3059B2));
			ModelData.Add(new ModelData("ig_molly", 0xAF03DDE1));
			ModelData.Add(new ModelData("ig_money", 0x37FACDA6));
			ModelData.Add(new ModelData("ig_mp_agent14", 0xFBF98469));
			ModelData.Add(new ModelData("ig_mrk", 0xEDDCAB6D));
			ModelData.Add(new ModelData("ig_mrs_thornhill", 0x1E04A96B));
			ModelData.Add(new ModelData("ig_mrsphillips", 0x3862EEA8));
			ModelData.Add(new ModelData("ig_natalia", 0xDE17DD3B));
			ModelData.Add(new ModelData("ig_nervousron", 0xBD006AF1));
			ModelData.Add(new ModelData("ig_nigel", 0xC8B7167D));
			ModelData.Add(new ModelData("ig_old_man1a", 0x719D27F4));
			ModelData.Add(new ModelData("ig_old_man2", 0xEF154C47));
			ModelData.Add(new ModelData("ig_omega", 0x60E6A7D8));
			ModelData.Add(new ModelData("ig_oneil", 0x2DC6D3E7));
			ModelData.Add(new ModelData("ig_orleans", 0x61D4C771));
			ModelData.Add(new ModelData("ig_ortega", 0x26A562B7));
			ModelData.Add(new ModelData("ig_paige", 0x154FCF3F));
			ModelData.Add(new ModelData("ig_paper", 0x999B00C6));
			ModelData.Add(new ModelData("ig_patricia", 0xC56E118C));
			ModelData.Add(new ModelData("ig_popov", 0x267630FE));
			ModelData.Add(new ModelData("ig_priest", 0x6437E77D));
			ModelData.Add(new ModelData("ig_prolsec_02", 0x27B3AD75));
			ModelData.Add(new ModelData("ig_ramp_gang", 0xE52E126C));
			ModelData.Add(new ModelData("ig_ramp_hic", 0x45753032));
			ModelData.Add(new ModelData("ig_ramp_hipster", 0xDEEF9F6E));
			ModelData.Add(new ModelData("ig_ramp_mex", 0xE6AC74A4));
			ModelData.Add(new ModelData("ig_rashcosvki", 0x380C4DE6));
			ModelData.Add(new ModelData("ig_roccopelosi", 0xD5BA52FF));
			ModelData.Add(new ModelData("ig_russiandrunk", 0x3D0A5EB1));
			ModelData.Add(new ModelData("ig_screen_writer", 0xFFE63677));
			ModelData.Add(new ModelData("ig_siemonyetarian", 0x4C7B2F05));
			ModelData.Add(new ModelData("ig_solomon", 0x86BDFE26));
			ModelData.Add(new ModelData("ig_stevehains", 0x382121C8));
			ModelData.Add(new ModelData("ig_stretch", 0x36984358));
			ModelData.Add(new ModelData("ig_talina", 0xE793C8E8));
			ModelData.Add(new ModelData("ig_tanisha", 0x0D810489));
			ModelData.Add(new ModelData("ig_taocheng", 0xDC5C5EA5));
			ModelData.Add(new ModelData("ig_taostranslator", 0x7C851464));
			ModelData.Add(new ModelData("ig_tenniscoach", 0xA23B5F57));
			ModelData.Add(new ModelData("ig_terry", 0x67000B94));
			ModelData.Add(new ModelData("ig_tomepsilon", 0xCD777AAA));
			ModelData.Add(new ModelData("ig_tonya", 0xCAC85344));
			ModelData.Add(new ModelData("ig_tracydisanto", 0xDE352A35));
			ModelData.Add(new ModelData("ig_trafficwarden", 0x5719786D));
			ModelData.Add(new ModelData("ig_tylerdix", 0x5265F707));
			ModelData.Add(new ModelData("ig_vagspeak", 0xF9FD068C));
			ModelData.Add(new ModelData("ig_wade", 0x92991B72));
			ModelData.Add(new ModelData("ig_zimbor", 0x0B34D6F5));
			ModelData.Add(new ModelData("mp_f_boatstaff_01", 0x3293B9CE));
			ModelData.Add(new ModelData("mp_f_cardesign_01", 0x242C34A7));
			ModelData.Add(new ModelData("mp_f_chbar_01", 0xC3F6E385));
			ModelData.Add(new ModelData("mp_f_cocaine_01", 0x4B657AF8));
			ModelData.Add(new ModelData("mp_f_counterfeit_01", 0xB788F1F5));
			ModelData.Add(new ModelData("mp_f_deadhooker", 0x73DEA88B));
			ModelData.Add(new ModelData("mp_f_execpa_01", 0x432CA064));
			ModelData.Add(new ModelData("mp_f_execpa_02", 0x5972CCF0));
			ModelData.Add(new ModelData("mp_f_forgery_01", 0x781A3CF8));
			//ModelData.Add(new ModelData("mp_f_freemode_01", 0x9C9EFFD8));
			ModelData.Add(new ModelData("mp_f_helistaff_01", 0x19B6FF06));
			ModelData.Add(new ModelData("mp_f_meth_01", 0xD2B27EC1));
			ModelData.Add(new ModelData("mp_f_misty_01", 0xD128FF9D));
			ModelData.Add(new ModelData("mp_f_stripperlite", 0x2970A494));
			ModelData.Add(new ModelData("mp_f_weed_01", 0xB26573A3));
			ModelData.Add(new ModelData("mp_g_m_pros_01", 0x6C9DD7C9));
			ModelData.Add(new ModelData("mp_m_avongoon", 0x9C13CB95));
			ModelData.Add(new ModelData("mp_m_boatstaff_01", 0xC85F0A88));
			ModelData.Add(new ModelData("mp_m_bogdangoon", 0x4D5696F7));
			ModelData.Add(new ModelData("mp_m_claude_01", 0xC0F371B7));
			ModelData.Add(new ModelData("mp_m_cocaine_01", 0x56D38F95));
			ModelData.Add(new ModelData("mp_m_counterfeit_01", 0x9855C974));
			ModelData.Add(new ModelData("mp_m_exarmy_01", 0x45348DBB));
			ModelData.Add(new ModelData("mp_m_execpa_01", 0x3E8417BC));
			ModelData.Add(new ModelData("mp_m_famdd_01", 0x33A464E5));
			ModelData.Add(new ModelData("mp_m_fibsec_01", 0x5CDEF405));
			ModelData.Add(new ModelData("mp_m_forgery_01", 0x613E709B));
			//ModelData.Add(new ModelData("mp_m_freemode_01", 0x705E61F2)); 
			ModelData.Add(new ModelData("mp_m_g_vagfun_01", 0xC4A617BD));
			ModelData.Add(new ModelData("mp_m_marston_01", 0x38430167));
			ModelData.Add(new ModelData("mp_m_meth_01", 0xEDB42F3F));
			ModelData.Add(new ModelData("mp_m_niko_01", 0xEEDACFC9));
			ModelData.Add(new ModelData("mp_m_securoguard_01", 0xDA2C984E));
			ModelData.Add(new ModelData("mp_m_shopkeep_01", 0x18CE57D0));
			ModelData.Add(new ModelData("mp_m_waremech_01", 0xF7A74139));
			ModelData.Add(new ModelData("mp_m_weapexp_01", 0x36EA5B09));
			ModelData.Add(new ModelData("mp_m_weapwork_01", 0x4186506E));
			ModelData.Add(new ModelData("mp_m_weed_01", 0x917ED459));
			ModelData.Add(new ModelData("mp_s_m_armoured_01", 0xCDEF5408));
			//ModelData.Add(new ModelData("player_one", 0x9B22DBAF));
			//ModelData.Add(new ModelData("player_two", 0x9B810FA2));
			//ModelData.Add(new ModelData("player_zero", 0x0D7114C9));
			ModelData.Add(new ModelData("s_f_m_fembarber", 0x163B875B));
			ModelData.Add(new ModelData("s_f_m_maid_01", 0xE093C5C6));
			ModelData.Add(new ModelData("s_f_m_shop_high", 0xAE47E4B0));
			ModelData.Add(new ModelData("s_f_m_sweatshop_01", 0x312B5BC0));
			ModelData.Add(new ModelData("s_f_y_airhostess_01", 0x5D71A46F));
			ModelData.Add(new ModelData("s_f_y_bartender_01", 0x780C01BD));
			ModelData.Add(new ModelData("s_f_y_baywatch_01", 0x4A8E5536));
			ModelData.Add(new ModelData("s_f_y_cop_01", 0x15F8700D));
			ModelData.Add(new ModelData("s_f_y_factory_01", 0x69F46BF3));
			ModelData.Add(new ModelData("s_f_y_hooker_01", 0x028ABF95));
			ModelData.Add(new ModelData("s_f_y_hooker_02", 0x14C3E407));
			ModelData.Add(new ModelData("s_f_y_hooker_03", 0x031640AC));
			ModelData.Add(new ModelData("s_f_y_migrant_01", 0xD55B2BF5));
			ModelData.Add(new ModelData("s_f_y_movprem_01", 0x2300C816));
			ModelData.Add(new ModelData("s_f_y_rang r_01", 0x9FC7F637));
			ModelData.Add(new ModelData("s_f_y_scrubs_01", 0xAB594AB6));
			ModelData.Add(new ModelData("s_f_y_sheriff_01", 0x4161D042));
			ModelData.Add(new ModelData("s_f_y_shop_low", 0xA96E2604));
			ModelData.Add(new ModelData("s_f_y_shop_mid", 0x3EECBA5D));
			ModelData.Add(new ModelData("s_f_y_stripper_01", 0x52580019));
			ModelData.Add(new ModelData("s_f_y_stripper_02", 0x6E0FB794));
			ModelData.Add(new ModelData("s_f_y_stripperlite", 0x5C14EDFA));
			ModelData.Add(new ModelData("s_f_y_sweatshop_01", 0x8502B6B2));
			ModelData.Add(new ModelData("s_m_m_ammucountry", 0x0DE9A30A));
			ModelData.Add(new ModelData("s_m_m_armoured_01", 0x95C76ECD));
			ModelData.Add(new ModelData("s_m_m_armoured_02", 0x63858A4A));
			ModelData.Add(new ModelData("s_m_m_autoshop_01", 0x040EABE3));
			ModelData.Add(new ModelData("s_m_m_autoshop_02", 0xF06B849D));
			ModelData.Add(new ModelData("s_m_m_bouncer_01", 0x9FD4292D));
			ModelData.Add(new ModelData("s_m_m_ccrew_01", 0xC9E5F56B));
			ModelData.Add(new ModelData("s_m_m_chemsec_01", 0x2EFEAFD5));
			ModelData.Add(new ModelData("s_m_m_ciasec_01", 0x625D6958));
			ModelData.Add(new ModelData("s_m_m_cntrybar_01", 0x1A021B83));
			ModelData.Add(new ModelData("s_m_m_dockwork_01", 0x14D7B4E0));
			ModelData.Add(new ModelData("s_m_m_doctor_01", 0xD47303AC));
			ModelData.Add(new ModelData("s_m_m_fiboffice_01", 0xEDBC7546));
			ModelData.Add(new ModelData("s_m_m_fiboffice_02", 0x26F067AD));
			ModelData.Add(new ModelData("s_m_m_fibsec_01", 0x7B8B434B));
			ModelData.Add(new ModelData("s_m_m_gaffer_01", 0xA956BD9E));
			ModelData.Add(new ModelData("s_m_m_gardener_01", 0x49EA5685));
			ModelData.Add(new ModelData("s_m_m_gentransport", 0x1880ED06));
			ModelData.Add(new ModelData("s_m_m_hairdress_01", 0x418DFF92));
			ModelData.Add(new ModelData("s_m_m_highsec_01", 0xF161D212));
			ModelData.Add(new ModelData("s_m_m_highsec_02", 0x2930C1AB));
			ModelData.Add(new ModelData("s_m_m_janitor", 0xA96BD9EC));
			ModelData.Add(new ModelData("s_m_m_lathandy_01", 0x9E80D2CE));
			ModelData.Add(new ModelData("s_m_m_lifeinvad_01", 0xDE0077FD));
			ModelData.Add(new ModelData("s_m_m_linecook", 0xDB9C0997));
			ModelData.Add(new ModelData("s_m_m_lsmetro_01", 0x765AAAE4));
			ModelData.Add(new ModelData("s_m_m_mariachi_01", 0x7EA4FFA6));
			ModelData.Add(new ModelData("s_m_m_marine_01", 0xF2DAA2ED));
			ModelData.Add(new ModelData("s_m_m_marine_02", 0xF0259D83));
			ModelData.Add(new ModelData("s_m_m_migrant_01", 0xED0CE4C6));
			ModelData.Add(new ModelData("s_m_m_movalien_01", 0x64611296));
			ModelData.Add(new ModelData("s_m_m_movprem_01", 0xD85E6D28));
			ModelData.Add(new ModelData("s_m_m_movspace_01", 0xE7B31432));
			ModelData.Add(new ModelData("s_m_m_paramedic_01", 0xB353629E));
			ModelData.Add(new ModelData("s_m_m_pilot_01", 0xE75B4B1C));
			ModelData.Add(new ModelData("s_m_m_pilot_02", 0xF63DE8E1));
			ModelData.Add(new ModelData("s_m_m_postal_01", 0x62599034));
			ModelData.Add(new ModelData("s_m_m_postal_02", 0x7367324F));
			ModelData.Add(new ModelData("s_m_m_prisguard_01", 0x56C96FC6));
			ModelData.Add(new ModelData("s_m_m_scientist_01", 0x4117D39B));
			ModelData.Add(new ModelData("s_m_m_security_01", 0xD768B228));
			ModelData.Add(new ModelData("s_m_m_snowcop_01", 0x1AE8BB58));
			ModelData.Add(new ModelData("s_m_m_strperf_01", 0x795AC7A8));
			ModelData.Add(new ModelData("s_m_m_strpreach_01", 0x1C0077FB));
			ModelData.Add(new ModelData("s_m_m_strvend_01", 0xCE9113A9));
			ModelData.Add(new ModelData("s_m_m_trucker_01", 0x59511A6C));
			ModelData.Add(new ModelData("s_m_m_ups_01", 0x9FC37F22));
			ModelData.Add(new ModelData("s_m_m_ups_02", 0xD0BDE116));
			ModelData.Add(new ModelData("s_m_o_busker_01", 0xAD9EF1BB));
			ModelData.Add(new ModelData("s_m_y_airworker", 0x62018559));
			ModelData.Add(new ModelData("s_m_y_ammucity_01", 0x9E08633D));
			ModelData.Add(new ModelData("s_m_y_armymech_01", 0x62CC28E2));
			ModelData.Add(new ModelData("s_m_y_autopsy_01", 0xB2273D4E));
			ModelData.Add(new ModelData("s_m_y_barman_01", 0xE5A11106));
			ModelData.Add(new ModelData("s_m_y_baywatch_01", 0x0B4A6862));
			ModelData.Add(new ModelData("s_m_y_blackops_01", 0xB3F3EE34));
			ModelData.Add(new ModelData("s_m_y_blackops_02", 0x7A05FA59));
			ModelData.Add(new ModelData("s_m_y_blackops_03", 0x5076A73B));
			ModelData.Add(new ModelData("s_m_y_busboy_01", 0xD8F9CD47));
			ModelData.Add(new ModelData("s_m_y_chef_01", 0x0F977CEB));
			ModelData.Add(new ModelData("s_m_y_clown_01", 0x04498DDE));
			ModelData.Add(new ModelData("s_m_y_construct_01", 0xD7DA9E99));
			ModelData.Add(new ModelData("s_m_y_construct_02", 0xC5FEFADE));
			ModelData.Add(new ModelData("s_m_y_cop_01", 0x5E3DA4A4));
			ModelData.Add(new ModelData("s_m_y_dealer_01", 0xE497BBEF));
			ModelData.Add(new ModelData("s_m_y_devinsec_01", 0x9B557274));
			ModelData.Add(new ModelData("s_m_y_dockwork_01", 0x867639D1));
			ModelData.Add(new ModelData("s_m_y_doorman_01", 0x22911304));
			ModelData.Add(new ModelData("s_m_y_dwservice_01", 0x75D30A91));
			ModelData.Add(new ModelData("s_m_y_dwservice_02", 0xF5908A06));
			ModelData.Add(new ModelData("s_m_y_factory_01", 0x4163A158));
			ModelData.Add(new ModelData("s_m_y_fireman_01", 0xB6B1EDA8));
			ModelData.Add(new ModelData("s_m_y_garbage", 0xEE75A00F));
			ModelData.Add(new ModelData("s_m_y_grip_01", 0x309E7DEA));
			ModelData.Add(new ModelData("s_m_y_hwaycop_01", 0x739B1EF5));
			ModelData.Add(new ModelData("s_m_y_marine_01", 0x65793043));
			ModelData.Add(new ModelData("s_m_y_marine_02", 0x58D696FE));
			ModelData.Add(new ModelData("s_m_y_marine_03", 0x72C0CAD2));
			ModelData.Add(new ModelData("s_m_y_mime", 0x3CDCA742));
			ModelData.Add(new ModelData("s_m_y_pestcont_01", 0x48114518));
			ModelData.Add(new ModelData("s_m_y_pilot_01", 0xAB300C07));
			ModelData.Add(new ModelData("s_m_y_prismuscl_01", 0x5F2113A1));
			ModelData.Add(new ModelData("s_m_y_prisoner_01", 0xB1BB9B59));
			ModelData.Add(new ModelData("s_m_y_ranger_01", 0xEF7135AE));
			ModelData.Add(new ModelData("s_m_y_robber_01", 0xC05E1399));
			ModelData.Add(new ModelData("s_m_y_sheriff_01", 0xB144F9B9));
			ModelData.Add(new ModelData("s_m_y_shop_mask", 0x6E122C06));
			ModelData.Add(new ModelData("s_m_y_strvend_01", 0x927F2323));
			ModelData.Add(new ModelData("s_m_y_swat_01", 0x8D8F1B10));
			ModelData.Add(new ModelData("s_m_y_uscg_01", 0xCA0050E9));
			ModelData.Add(new ModelData("s_m_y_valet_01", 0x3B96F23E));
			ModelData.Add(new ModelData("s_m_y_waiter_01", 0xAD4C724C));
			ModelData.Add(new ModelData("s_m_y_winclean_01", 0x550D8D9D));
			ModelData.Add(new ModelData("s_m_y_xmech_01", 0x441405EC));
			ModelData.Add(new ModelData("s_m_y_xmech_02_mp", 0x69147A0D));
			ModelData.Add(new ModelData("s_m_y_xmech_02", 0xBE20FA04));
			ModelData.Add(new ModelData("u_f_m_corpse_01", 0x2E140314));
			ModelData.Add(new ModelData("u_f_m_drowned_01", 0xD7F37609));
			ModelData.Add(new ModelData("u_f_m_miranda", 0x414FA27B));
			ModelData.Add(new ModelData("u_f_m_promourn_01", 0xA20899E7));
			ModelData.Add(new ModelData("u_f_o_moviestar", 0x35578634));
			ModelData.Add(new ModelData("u_f_o_prolhost_01", 0xC512DD23));
			ModelData.Add(new ModelData("u_f_y_bikerchic", 0xFA389D4F));
			ModelData.Add(new ModelData("u_f_y_comjane", 0xB6AA85CE));
			ModelData.Add(new ModelData("u_f_y_corpse_01", 0x9C70109D));
			ModelData.Add(new ModelData("u_f_y_corpse_02", 0x0D9C72F8));
			ModelData.Add(new ModelData("u_f_y_hotposh_01", 0x969B6DFE));
			ModelData.Add(new ModelData("u_f_y_jewelass_01", 0xF0D4BE2E));
			ModelData.Add(new ModelData("u_f_y_mistress", 0x5DCA2528));
			ModelData.Add(new ModelData("u_f_y_poppymich", 0x23E9A09E));
			ModelData.Add(new ModelData("u_f_y_princess", 0xD2E3A284));
			ModelData.Add(new ModelData("u_f_y_spyactress", 0x5B81D86C));
			ModelData.Add(new ModelData("u_m_m_aldinapoli", 0xF0EC56E2));
			ModelData.Add(new ModelData("u_m_m_bankman", 0xC306D6F5));
			ModelData.Add(new ModelData("u_m_m_bikehire_01", 0x76474545));
			ModelData.Add(new ModelData("u_m_m_doa_01", 0x621E6BFD));
			ModelData.Add(new ModelData("u_m_m_edtoh", 0x2A797197));
			ModelData.Add(new ModelData("u_m_m_fibarchitect", 0x342333D3));
			ModelData.Add(new ModelData("u_m_m_filmdirector", 0x2B6E1BB6));
			ModelData.Add(new ModelData("u_m_m_glenstank_01", 0x45BB1666));
			ModelData.Add(new ModelData("u_m_m_griff_01", 0xC454BCBB));
			ModelData.Add(new ModelData("u_m_m_jesus_01", 0xCE2CB751));
			ModelData.Add(new ModelData("u_m_m_jewelsec_01", 0xACCCBDB6));
			ModelData.Add(new ModelData("u_m_m_jewelthief", 0xE6CC3CDC));
			ModelData.Add(new ModelData("u_m_m_markfost", 0x1C95CB0B));
			ModelData.Add(new ModelData("u_m_m_partytarget", 0x81F74DE7));
			ModelData.Add(new ModelData("u_m_m_prolsec_01", 0x709220C7));
			ModelData.Add(new ModelData("u_m_m_promourn_01", 0xCE96030B));
			ModelData.Add(new ModelData("u_m_m_rivalpap", 0x60D5D6DA));
			ModelData.Add(new ModelData("u_m_m_spyactor", 0xAC0EA5D8));
			ModelData.Add(new ModelData("u_m_m_streetart_01", 0x6C19E962));
			ModelData.Add(new ModelData("u_m_m_willyfist", 0x90769A8F));
			ModelData.Add(new ModelData("u_m_o_filmnoir", 0x2BACC2DB));
			ModelData.Add(new ModelData("u_m_o_finguru_01", 0x46E39E63));
			ModelData.Add(new ModelData("u_m_o_taphillbilly", 0x9A1E5E52));
			ModelData.Add(new ModelData("u_m_o_tramp_01", 0x6A8F1F9B));
			ModelData.Add(new ModelData("u_m_y_abner", 0xF0AC2626));
			ModelData.Add(new ModelData("u_m_y_antonb", 0xCF623A2C));
			ModelData.Add(new ModelData("u_m_y_babyd", 0xDA116E7E));
			ModelData.Add(new ModelData("u_m_y_baygor", 0x5244247D));
			ModelData.Add(new ModelData("u_m_y_burgerdrug_01", 0x8B7D3766));
			ModelData.Add(new ModelData("u_m_y_chip", 0x24604B2B));
			ModelData.Add(new ModelData("u_m_y_corpse_01", 0x94C2A03F));
			ModelData.Add(new ModelData("u_m_y_cyclist_01", 0x2D0EFCEB));
			ModelData.Add(new ModelData("u_m_y_fibmugger_01", 0x85B9C668));
			ModelData.Add(new ModelData("u_m_y_guido_01", 0xC6B49A2F));
			ModelData.Add(new ModelData("u_m_y_gunvend_01", 0xB3229752));
			ModelData.Add(new ModelData("u_m_y_hippie_01", 0xF041880B));
			ModelData.Add(new ModelData("u_m_y_imporage", 0x348065F5));
			ModelData.Add(new ModelData("u_m_y_juggernaut_01", 0x90EF5134));
			ModelData.Add(new ModelData("u_m_y_justin", 0x7DC3908F));
			ModelData.Add(new ModelData("u_m_y_mani", 0xC8BB1E52));
			ModelData.Add(new ModelData("u_m_y_militarybum", 0x4705974A));
			ModelData.Add(new ModelData("u_m_y_paparazzi", 0x5048B328));
			ModelData.Add(new ModelData("u_m_y_party_01", 0x36E70600));
			ModelData.Add(new ModelData("u_m_y_pogo_01", 0xDC59940D));
			ModelData.Add(new ModelData("u_m_y_prisoner_01", 0x7B9B4BC0));
			ModelData.Add(new ModelData("u_m_y_proldriver_01", 0x855E36A3));
			ModelData.Add(new ModelData("u_m_y_rsranger_01", 0x3C438CD2));
			ModelData.Add(new ModelData("u_m_y_sbike", 0x6AF4185D));
			ModelData.Add(new ModelData("u_m_y_staggrm_01", 0x9194CE03));
			ModelData.Add(new ModelData("u_m_y_tattoo_01", 0x94AE2B8C));
			ModelData.Add(new ModelData("u_m_y_zombie_01", 0xAC4B4506));
		}
		static public string GetSkinFromIndex(int index)
		{
			string[] ped_models = {
            //"a_c_boar",//: 0xCE5FF074,
            //"a_c_cat_01",//: 0x573201B8,
            //"a_c_chickenhawk",// 0xAAB71F62,
            //"a_c_chimp",// 0xA8683715,
            //"a_c_chop",// 0x14EC17EA,
            //"a_c_cormorant",// 0x56E29962,
            //"a_c_cow",// 0xFCFA9E1E,
            //"a_c_coyote",// 0x644AC75E,
            //"a_c_crow",// 0x18012A9F,
            //"a_c_deer",// 0xD86B5A95,
            //"a_c_dolphin",// 0x8BBAB455,
            //"a_c_fish",// 0x2FD800B7,
            //"a_c_hen",// 0x6AF51FAF,
            //"a_c_humpback",// 0x471BE4B2,
            //"a_c_husky",// 0x4E8F95A2,
            //"a_c_killerwhale",// 0x8D8AC8B9,
            //"a_c_mtlion",// 0x1250D7BA,
            //"a_c_pig",// 0xB11BAB56,
            //"a_c_pigeon",// 0x06A20728,
            //"a_c_poodle",// 0x431D501C,
            //"a_c_pug",// 0x6D362854,
            //"a_c_rabbit_01",// 0xDFB55C81,
            //"a_c_rat",// 0xC3B52966,
            //"a_c_retriever",// 0x349F33E1,
            //"a_c_rhesus",// 0xC2D06F53,
            //"a_c_rottweiler",// 0x9563221D,
           //"a_c_seagull",// 0xD3939DFD,
            //"a_c_sharkhammer",// 0x3C831724,
            //"a_c_sharktiger",// 0x06C3F072,
            //"a_c_shepherd",// 0x431FC24C,
            //"a_c_stingray",// 0xA148614D,
            //"a_c_westy",// 0xAD7844BB,

            "mp_m_freemode_01",// 0x705E61F2,
            "mp_f_freemode_01",// 0x9C9EFFD8,

            "player_one",// 0x9B22DBAF,
            "player_two",// 0x9B810FA2,
            "player_zero",// 0x0D7114C9,

            "a_f_m_beach_01",// 0x303638A7,
            "a_f_m_bevhills_01",// 0xBE086EFD,
            "a_f_m_bevhills_02",// 0xA039335F,
            "a_f_m_bodybuild_01",// 0x3BD99114,
            "a_f_m_business_02",// 0x1FC37DBC,
            "a_f_m_downtown_01",// 0x654AD86E,
            "a_f_m_eastsa_01",// 0x9D3DCB7A,
            "a_f_m_eastsa_02",// 0x63C8D891,
            "a_f_m_fatbla_01",// 0xFAB48BCB,
            "a_f_m_fatcult_01",// 0xB5CF80E4,
            "a_f_m_fatwhite_01",// 0x38BAD33B,
            "a_f_m_ktown_01",// 0x52C824DE,
            "a_f_m_ktown_02",// 0x41018151,
            "a_f_m_prolhost_01",// 0x169BD1E1,
            "a_f_m_salton_01",// 0xDE0E0969,
            "a_f_m_skidrow_01",// 0xB097523B,
            "a_f_m_soucent_01",// 0x745855A1,
            "a_f_m_soucent_02",// 0xF322D338,
            "a_f_m_soucentmc_01",// 0xCDE955D2,
            "a_f_m_tourist_01",// 0x505603B9,
            "a_f_m_tramp_01",// 0x48F96F5B,
            "a_f_m_trampbeac_01",// 0x8CA0C266,
            "a_f_o_genstreet_01",// 0x61C81C85,
            "a_f_o_indian_01",// 0xBAD7BB80,
            "a_f_o_ktown_01",// 0x47CF5E96,
            "a_f_o_salton_01",// 0xCCFF7D8A,
            "a_f_o_soucent_01",// 0x3DFA1830,
            "a_f_o_soucent_02",// 0xA56DE716,
            "a_f_y_beach_01",// 0xC79F6928,
            "a_f_y_bevhills_01",// 0x445AC854,
            "a_f_y_bevhills_02",// 0x5C2CF7F8,
            "a_f_y_bevhills_03",// 0x20C8012F,
            "a_f_y_bevhills_04",// 0x36DF2D5D,
            "a_f_y_business_01",// 0x2799EFD8,
            "a_f_y_business_02",// 0x31430342,
            "a_f_y_business_03",// 0xAE86FDB4,
            "a_f_y_business_04",// 0xB7C61032,
            "a_f_y_eastsa_01",// 0xF5B0079D,
            "a_f_y_eastsa_02",// 0x0438A4AE,
            "a_f_y_eastsa_03",// 0x51C03FA4,
            "a_f_y_epsilon_01",// 0x689C2A80,
            "a_f_y_femaleagent",// 0x50610C43,
            "a_f_y_fitness_01",// 0x457C64FB,
            "a_f_y_fitness_02",// 0x13C4818C,
            "a_f_y_genhot_01",// 0x2F4AEC3E,
            "a_f_y_golfer_01",// 0x7DD8FB58,
            "a_f_y_hiker_01",// 0x30830813,
            "a_f_y_hippie_01",// 0x1475B827,
            "a_f_y_hipster_01",// 0x8247D331,
            "a_f_y_hipster_02",// 0x97F5FE8D,
            "a_f_y_hipster_03",// 0xA5BA9A16,
            "a_f_y_hipster_04",// 0x199881DC,
            "a_f_y_indian_01",// 0x092D9CC1,
            "a_f_y_juggalo_01",// 0xDB134533,
            "a_f_y_runner_01",// 0xC7496729,
            "a_f_y_rurmeth_01",// 0x3F789426,
            "a_f_y_scdressy_01",// 0xDB5EC400,
            "a_f_y_skater_01",// 0x695FE666,
            "a_f_y_soucent_01",// 0x2C641D7A,
            "a_f_y_soucent_02",// 0x5A8EF9CF,
            "a_f_y_soucent_03",// 0x87B25415,
            "a_f_y_tennis_01",// 0x550C79C6,
            "a_f_y_topless_01",// 0x9CF26183,
            "a_f_y_tourist_01",// 0x563B8570,
            "a_f_y_tourist_02",// 0x9123FB40,
            "a_f_y_vinewood_01",// 0x19F41F65,
            "a_f_y_vinewood_02",// 0xDAB6A0EB,
            "a_f_y_vinewood_03",// 0x379DDAB8,
            "a_f_y_vinewood_04",// 0xFAE46146,
            "a_f_y_yoga_01",// 0xC41B062E,
            "a_m_m_acult_01",// 0x5442C66B,
            "a_m_m_afriamer_01",// 0xD172497E,
            "a_m_m_beach_01",// 0x403DB4FD,
            "a_m_m_beach_02",// 0x787FA588,
            "a_m_m_bevhills_01",// 0x54DBEE1F,
            "a_m_m_bevhills_02",// 0x3FB5C3D3,
            "a_m_m_business_01",// 0x7E6A64B7,
            "a_m_m_eastsa_01",// 0xF9A6F53F,
            "a_m_m_eastsa_02",// 0x07DD91AC,
            "a_m_m_farmer_01",// 0x94562DD7,
            "a_m_m_fatlatin_01",// 0x61D201B3,
            "a_m_m_genfat_01",// 0x06DD569F,
            "a_m_m_genfat_02",// 0x13AEF042,
            "a_m_m_golfer_01",// 0xA9EB0E42,
            "a_m_m_hasjew_01",// 0x6BD9B68C,
            "a_m_m_hillbilly_01",// 0x6C9B2849,
            "a_m_m_hillbilly_02",// 0x7B0E452F,
            "a_m_m_indian_01",// 0xDDCAAA2C,
            "a_m_m_ktown_01",// 0xD15D7E71,
            "a_m_m_malibu_01",// 0x2FDE6EB7,
            "a_m_m_mexcntry_01",// 0xDD817EAD,
            "a_m_m_mexlabor_01",// 0xB25D16B2,
            "a_m_m_og_boss_01",// 0x681BD012,
            "a_m_m_paparazzi_01",// 0xECCA8C15,
            "a_m_m_polynesian_01",// 0xA9D9B69E,
            "a_m_m_prolhost_01",// 0x9712C38F,
            "a_m_m_rurmeth_01",// 0x3BAD4184,
            "a_m_m_salton_01",// 0x4F2E038A,
            "a_m_m_salton_02",// 0x60F4A717,
            "a_m_m_salton_03",// 0xB28C4A45,
            "a_m_m_salton_04",// 0x964511B7,
            "a_m_m_skater_01",// 0xD9D7588C,
            "a_m_m_skidrow_01",// 0x01EEA6BD,
            "a_m_m_socenlat_01",// 0x0B8D69E3,
            "a_m_m_soucent_01",// 0x6857C9B7,
            "a_m_m_soucent_02",// 0x9F6D37E1,
            "a_m_m_soucent_03",// 0x8BD990BA,
            "a_m_m_soucent_04",// 0xC2FBFEFE,
            "a_m_m_stlat_02",// 0xC2A87702,
            "a_m_m_tennis_01",// 0x546A5344,
            "a_m_m_tourist_01",// 0xC89F0184,
            "a_m_m_tramp_01",// 0x1EC93FD0,
            "a_m_m_trampbeac_01",// 0x53B57EB0,
            "a_m_m_tranvest_01",// 0xE0E69974,
            "a_m_m_tranvest_02",// 0xF70EC5C4,
            "a_m_o_acult_01",// 0x55446010,
            "a_m_o_acult_02",// 0x4BA14CCA,
            "a_m_o_beach_01",// 0x8427D398,
            "a_m_o_genstreet_01",// 0xAD54E7A8,
            "a_m_o_ktown_01",// 0x1536D95A,
            "a_m_o_salton_01",// 0x20208E4D,
            "a_m_o_soucent_01",// 0x2AD8921B,
            "a_m_o_soucent_02",// 0x4086BD77,
            "a_m_o_soucent_03",// 0x0E32D8D0,
            "a_m_o_tramp_01",// 0x174D4245,
            "a_m_y_acult_01",// 0xB564882B,
            "a_m_y_acult_02",// 0x80E59F2E,
            "a_m_y_beach_01",// 0xD1FEB884,
            "a_m_y_beach_02",// 0x23C7DC11,
            "a_m_y_beach_03",// 0xE7A963D9,
            "a_m_y_beachvesp_01",// 0x7E0961B8,
            "a_m_y_beachvesp_02",// 0xCA56FA52,
            "a_m_y_bevhills_01",// 0x76284640,
            "a_m_y_bevhills_02",// 0x668BA707,
            "a_m_y_breakdance_01",// 0x379F9596,
            "a_m_y_busicas_01",// 0x9AD32FE9,
            "a_m_y_business_01",// 0xC99F21C4,
            "a_m_y_business_02",// 0xB3B3F5E6,
            "a_m_y_business_03",// 0xA1435105,
            "a_m_y_cyclist_01",// 0xFDC653C7,
            "a_m_y_dhill_01",// 0xFF3E88AB,
            "a_m_y_downtown_01",// 0x2DADF4AA,
            "a_m_y_eastsa_01",// 0xA4471173,
            "a_m_y_eastsa_02",// 0x168775F6,
            "a_m_y_epsilon_01",// 0x77D41A3E,
            "a_m_y_epsilon_02",// 0xAA82FF9B,
            "a_m_y_gay_01",// 0xD1CCE036,
            "a_m_y_gay_02",// 0xA5720781,
            "a_m_y_genstreet_01",// 0x9877EF71,
            "a_m_y_genstreet_02",// 0x3521A8D2,
            "a_m_y_golfer_01",// 0xD71FE131,
            "a_m_y_hasjew_01",// 0xE16D8F01,
            "a_m_y_hiker_01",// 0x50F73C0C,
            "a_m_y_hippy_01",// 0x7D03E617,
            "a_m_y_hipster_01",// 0x2307A353,
            "a_m_y_hipster_02",// 0x14D506EE,
            "a_m_y_hipster_03",// 0x4E4179C6,
            "a_m_y_indian_01",// 0x2A22FBCE,
            "a_m_y_jetski_01",// 0x2DB7EEF3,
            "a_m_y_juggalo_01",// 0x91CA3E2C,
            "a_m_y_ktown_01",// 0x1AF6542C,
            "a_m_y_ktown_02",// 0x297FF13F,
            "a_m_y_latino_01",// 0x132C1A8E,
            "a_m_y_methhead_01",// 0x696BE0A9,
            "a_m_y_mexthug_01",// 0x3053E555,
            "a_m_y_motox_01",// 0x64FDEA7D,
            "a_m_y_motox_02",// 0x77AC8FDA,
            "a_m_y_musclbeac_01",// 0x4B652906,
            "a_m_y_musclbeac_02",// 0xC923247C,
            "a_m_y_polynesian_01",// 0x8384FC9F,
            "a_m_y_roadcyc_01",// 0xF561A4C6,
            "a_m_y_runner_01",// 0x25305EEE,
            "a_m_y_runner_02",// 0x843D9D0F,
            "a_m_y_salton_01",// 0xD7606C30,
            "a_m_y_skater_01",// 0xC1C46677,
            "a_m_y_skater_02",// 0xAFFAC2E4,
            "a_m_y_soucent_01",// 0xE716BDCB,
            "a_m_y_soucent_02",// 0xACA3C8CA,
            "a_m_y_soucent_03",// 0xC3F0F764,
            "a_m_y_soucent_04",// 0x8A3703F1,
            "a_m_y_stbla_01",// 0xCF92ADE9,
            "a_m_y_stbla_02",// 0x98C7404F,
            "a_m_y_stlat_01",// 0x8674D5FC,
            "a_m_y_stwhi_01",// 0x2418C430,
            "a_m_y_stwhi_02",// 0x36C6E98C,
            "a_m_y_sunbathe_01",// 0xB7292F0C,
            "a_m_y_surfer_01",// 0xEAC2C7EE,
            "a_m_y_vindouche_01",// 0xC19377E7,
            "a_m_y_vinewood_01",// 0x4B64199D,
            "a_m_y_vinewood_02",// 0x5D15BD00,
            "a_m_y_vinewood_03",// 0x1FDF4294,
            "a_m_y_vinewood_04",// 0x31C9E669,
            "a_m_y_yoga_01",// 0xAB0A7155,
            "cs_amandatownley",// 0x95EF18E3,
            "cs_andreas",// 0xE7565327,
            "cs_ashley",// 0x26C3D079,
            "cs_bankman",// 0x9760192E,
            "cs_barry",// 0x69591CF7,
            "cs_beverly",// 0xB46EC356,
            "cs_brad",// 0xEFE5AFE6,
            "cs_bradcadaver",// 0x7228AF60,
            "cs_carbuyer",// 0x8CCE790F,
            "cs_casey",// 0xEA969C40,
            "cs_chengsr",// 0x30DB9D7B,
            "cs_chrisformage",// 0xC1F380E6,
            "cs_clay",// 0xDBCB9834,
            "cs_dale",// 0x0CE81655,
            "cs_davenorton",// 0x8587248C,
            "cs_debra",// 0xECD04FE9,
            "cs_denise",// 0x6F802738,
            "cs_devin",// 0x2F016D02,
            "cs_dom",// 0x4772AF42,
            "cs_dreyfuss",// 0x3C60A153,
            "cs_drfriedlander",// 0xA3A35C2F,
            "cs_fabien",// 0x47035EC1,
            "cs_fbisuit_01",// 0x585C0B52,
            "cs_floyd",// 0x062547E7,
            "cs_guadalope",// 0x0F9513F1,
            "cs_gurk",// 0xC314F727,
            "cs_hunter",// 0x5B44892C,
            "cs_janet",// 0x3034F9E2,
            "cs_jewelass",// 0x4440A804,
            "cs_jimmyboston",// 0x039677BD,
            "cs_jimmydisanto",// 0xB8CC92B4,
            "cs_joeminuteman",// 0xF09D5E29,
            "cs_johnnyklebitz",// 0xFA8AB881,
            "cs_josef",// 0x459762CA,
            "cs_josh",// 0x450EEF9D,
            "cs_karen_daniels",// 0x4BAF381C,
            "cs_lamardavis",// 0x45463A0D,
            "cs_lazlow",// 0x38951A1B,
            "cs_lestercrest",// 0xB594F5C3,
            "cs_lifeinvad_01",// 0x72551375,
            "cs_magenta",// 0x5816C61A,
            "cs_manuel",// 0xFBB374CA,
            "cs_marnie",// 0x574DE134,
            "cs_martinmadrazo",// 0x43595670,
            "cs_maryann",// 0x0998C7AD,
            "cs_michelle",// 0x70AEB9C8,
            "cs_milton",// 0xB76A330F,
            "cs_molly",// 0x45918E44,
            "cs_movpremf_01",// 0x4BBA84D9,
            "cs_movpremmale",// 0x8D67EE7D,
            "cs_mrk",// 0xC3CC9A75,
            "cs_mrs_thornhill",// 0x4F921E6E,
            "cs_mrsphillips",// 0xCBFDA3CF,
            "cs_natalia",// 0x4EFEB1F0,
            "cs_nervousron",// 0x7896DA94,
            "cs_nigel",// 0xE1479C0B,
            "cs_old_man1a",// 0x1EEC7BDC,
            "cs_old_man2",// 0x98F9E770,
            "cs_omega",// 0x8B70B405,
            "cs_orleans",// 0xAD340F5A,
            "cs_paper",// 0x6B38B8F8,
            "cs_patricia",// 0xDF8B1301,
            "cs_priest",// 0x4D6DE57E,
            "cs_prolsec_02",// 0x1E9314A2,
            "cs_russiandrunk",// 0x46521A32,
            "cs_siemonyetarian",// 0xC0937202,
            "cs_solomon",// 0xF6D1E04E,
            "cs_stevehains",// 0xA4E0A1FE,
            "cs_stretch",// 0x893D6805,
            "cs_tanisha",// 0x42FE5370,
            "cs_taocheng",// 0x8864083D,
            "cs_taostranslator",// 0x53536529,
            "cs_tenniscoach",// 0x5C26040A,
            "cs_terry",// 0x3A5201C5,
            "cs_tom",// 0x69E8ABC3,
            "cs_tomepsilon",// 0x8C0FD4E2,
            "cs_tracydisanto",// 0x0609B130,
            "cs_wade",// 0xD266D9D6,
            "cs_zimbor",// 0xEAACAAF0,
            "csb_abigail",// 0x89768941,
            "csb_agent",// 0xD770C9B4,
            "csb_anita",// 0x0703F106,
            "csb_anton",// 0xA5C787B6,
            "csb_ballasog",// 0xABEF0004,
            "csb_bride",// 0x82BF7EA1,
            "csb_burgerdrug",// 0x8CDCC057,
            "csb_car3guy1",// 0x04430687,
            "csb_car3guy2",// 0x1383A508,
            "csb_chef",// 0xA347CA8A,
            "csb_chef2",// 0xAE5BE23A,
            "csb_chin_goon",// 0xA8C22996,
            "csb_cletus",// 0xCAE9E5D5,
            "csb_cop",// 0x9AB35F63,
            "csb_customer",// 0xA44F6F8B,
            "csb_denise_friend",// 0xB58D2529,
            "csb_fos_rep",// 0x1BCC157B,
            "csb_g",// 0xA28E71D7,
            "csb_groom",// 0x7AAB19D2,
            "csb_grove_str_dlr",// 0xE8594E22,
            "csb_hao",// 0xEC9E8F1C,
            "csb_hugh",// 0x6F139B54,
            "csb_imran",// 0xE3420BDB,
            "csb_jackhowitzer",// 0x44BC7BB1,
            "csb_janitor",// 0xC2005A40,
            "csb_maude",// 0xBCC475CB,
            "csb_money",// 0x989DFD9A,
            "csb_mp_agent14",// 0x6DBBFC8B,
            "csb_mweather",// 0x613E626C,
            "csb_ortega",// 0xC0DB04CF,
            "csb_oscar",// 0xF41F399B,
            "csb_paige",// 0x5B1FA0C3,
            "csb_popov",// 0x617D89E2,
            "csb_porndudes",// 0x2F4AFE35,
            "csb_prologuedriver",// 0xF00B49DB,
            "csb_prolsec",// 0x7FA2F024,
            "csb_ramp_gang",// 0xC2800DBE,
            "csb_ramp_hic",// 0x858C94B8,
            "csb_ramp_hipster",// 0x21F58BB4,
            "csb_ramp_marine",// 0x616C97B9,
            "csb_ramp_mex",// 0xF64ED7D0,
            "csb_rashcosvki",// 0x188099A9,
            "csb_reporter",// 0x2E420A24,
            "csb_roccopelosi",// 0xAA64168C,
            "csb_screen_writer",// 0x8BE12CEC,
            "csb_stripper_01",// 0xAEEA76B5,
            "csb_stripper_02",// 0x81441B71,
            "csb_tonya",// 0x6343DD19,
            "csb_trafficwarden",// 0xDE2937F3,
            "csb_undercover",// 0xEF785A6A,
            "csb_vagspeak",// 0x48FF4CA9,
            "g_f_importexport_01",// 0x84A1B11A,
            "g_f_y_ballas_01",// 0x158C439C,
            "g_f_y_families_01",// 0x4E0CE5D3,
            "g_f_y_lost_01",// 0xFD5537DE,
            "g_f_y_vagos_01",// 0x5AA42C21,
            "g_m_importexport_01",// 0xBCA2CCEA,
            "g_m_m_armboss_01",// 0xF1E823A2,
            "g_m_m_armgoon_01",// 0xFDA94268,
            "g_m_m_armlieut_01",// 0xE7714013,
            "g_m_m_chemwork_01",// 0xF6157D8F,
            "g_m_m_chiboss_01",// 0xB9DD0300,
            "g_m_m_chicold_01",// 0x106D9A99,
            "g_m_m_chigoon_01",// 0x7E4F763F,
            "g_m_m_chigoon_02",// 0xFF71F826,
            "g_m_m_korboss_01",// 0x352A026F,
            "g_m_m_mexboss_01",// 0x5761F4AD,
            "g_m_m_mexboss_02",// 0x4914D813,
            "g_m_y_armgoon_02",// 0xC54E878A,
            "g_m_y_azteca_01",// 0x68709618,
            "g_m_y_ballaeast_01",// 0xF42EE883,
            "g_m_y_ballaorig_01",// 0x231AF63F,
            "g_m_y_ballasout_01",// 0x23B88069,
            "g_m_y_famca_01",// 0xE83B93B7,
            "g_m_y_famdnf_01",// 0xDB729238,
            "g_m_y_famfor_01",// 0x84302B09,
            "g_m_y_korean_01",// 0x247502A9,
            "g_m_y_korean_02",// 0x8FEDD989,
            "g_m_y_korlieut_01",// 0x7CCBE17A,
            "g_m_y_lost_01",// 0x4F46D607,
            "g_m_y_lost_02",// 0x3D843282,
            "g_m_y_lost_03",// 0x32B11CDC,
            "g_m_y_mexgang_01",// 0xBDDD5546,
            "g_m_y_mexgoon_01",// 0x26EF3426,
            "g_m_y_mexgoon_02",// 0x31A3498E,
            "g_m_y_mexgoon_03",// 0x964D12DC,
            "g_m_y_pologoon_01",// 0x4F3FBA06,
            "g_m_y_pologoon_02",// 0xA2E86156,
            "g_m_y_salvaboss_01",// 0x905CE0CA,
            "g_m_y_salvagoon_01",// 0x278C8CB7,
            "g_m_y_salvagoon_02",// 0x3273A285,
            "g_m_y_salvagoon_03",// 0x03B8C510,
            "g_m_y_strpunk_01",// 0xFD1C49BB,
            "g_m_y_strpunk_02",// 0x0DA1EAC6,
            "hc_driver",// 0x3B474ADF,
            "hc_gunman",// 0x0B881AEE,
            "hc_hacker",// 0x99BB00F8,
            "ig_abigail",// 0x400AEC41,
            "ig_agent",// 0x246AF208,
            "ig_amandatownley",// 0x6D1E15F7,
            "ig_andreas",// 0x47E4EEA0,
            "ig_ashley",// 0x7EF440DB,
            "ig_avon",// 0xFCE270C2,
            "ig_ballasog",// 0xA70B4A92,
            "ig_bankman",// 0x909D9E7F,
            "ig_barry",// 0x2F8845A3,
            "ig_benny",// 0xC4B715D2,
            "ig_bestmen",// 0x5746CD96,
            "ig_beverly",// 0xBDA21E5C,
            "ig_brad",// 0xBDBB4922,
            "ig_bride",// 0x6162EC47,
            "ig_car3guy1",// 0x84F9E937,
            "ig_car3guy2",// 0x75C34ACA,
            "ig_casey",// 0xE0FA2554,
            "ig_chef",// 0x49EADBF6,
            "ig_chef2",// 0x85889AC3,
            "ig_chengsr",// 0xAAE4EA7B,
            "ig_chrisformage",// 0x286E54A7,
            "ig_clay",// 0x6CCFE08A,
            "ig_claypain",// 0x9D0087A8,
            "ig_cletus",// 0xE6631195,
            "ig_dale",// 0x467415E9,
            "ig_davenorton",// 0x15CD4C33,
            "ig_denise",// 0x820B33BD,
            "ig_devin",// 0x7461A0B0,
            "ig_dom",// 0x9C2DB088,
            "ig_dreyfuss",// 0xDA890932,
            "ig_drfriedlander",// 0xCBFC0DF5,
            "ig_fabien",// 0xD090C350,
            "ig_fbisuit_01",// 0x3AE4A33B,
            "ig_floyd",// 0xB1B196B2,
            "ig_g",// 0x841BA933,
            "ig_groom",// 0xFECE8B85,
            "ig_hao",// 0x65978363,
            "ig_hunter",// 0xCE1324DE,
            "ig_janet",// 0x0D6D9C49,
            "ig_jay_norris",// 0x7A32EE74,
            "ig_jewelass",// 0x0F5D26BB,
            "ig_jimmyboston",// 0xEDA0082D,
            "ig_jimmydisanto",// 0x570462B9,
            "ig_joeminuteman",// 0xBE204C9B,
            "ig_johnnyklebitz",// 0x87CA80AE,
            "ig_josef",// 0xE11A9FB4,
            "ig_josh",// 0x799E9EEE,
            "ig_karen_daniels",// 0xEB51D959,
            "ig_kerrymcintosh",// 0x5B3BD90D,
            "ig_lamardavis",// 0x65B93076,
            "ig_lazlow",// 0xDFE443E5,
            "ig_lestercrest_2",// 0x6E42FD26,
            "ig_lestercrest",// 0x4DA6E849,
            "ig_lifeinvad_01",// 0x5389A93C,
            "ig_lifeinvad_02",// 0x27BD51D4,
            "ig_magenta",// 0xFCDC910A,
            "ig_malc",// 0xF1BCA919,
            "ig_manuel",// 0xFD418E10,
            "ig_marnie",// 0x188232D0,
            "ig_maryann",// 0xA36F9806,
            "ig_maude",// 0x3BE8287E,
            "ig_michelle",// 0xBF9672F4,
            "ig_milton",// 0xCB3059B2,
            "ig_molly",// 0xAF03DDE1,
            "ig_money",// 0x37FACDA6,
            "ig_mp_agent14",// 0xFBF98469,
            "ig_mrk",// 0xEDDCAB6D,
            "ig_mrs_thornhill",// 0x1E04A96B,
            "ig_mrsphillips",// 0x3862EEA8,
            "ig_natalia",// 0xDE17DD3B,
            "ig_nervousron",// 0xBD006AF1,
            "ig_nigel",// 0xC8B7167D,
            "ig_old_man1a",// 0x719D27F4,
            "ig_old_man2",// 0xEF154C47,
            "ig_omega",// 0x60E6A7D8,
            "ig_oneil",// 0x2DC6D3E7,
            "ig_orleans",// 0x61D4C771,
            "ig_ortega",// 0x26A562B7,
            "ig_paige",// 0x154FCF3F,
            "ig_paper",// 0x999B00C6,
            "ig_patricia",// 0xC56E118C,
            "ig_popov",// 0x267630FE,
            "ig_priest",// 0x6437E77D,
            "ig_prolsec_02",// 0x27B3AD75,
            "ig_ramp_gang",// 0xE52E126C,
            "ig_ramp_hic",// 0x45753032,
            "ig_ramp_hipster",// 0xDEEF9F6E,
            "ig_ramp_mex",// 0xE6AC74A4,
            "ig_rashcosvki",// 0x380C4DE6,
            "ig_roccopelosi",// 0xD5BA52FF,
            "ig_russiandrunk",// 0x3D0A5EB1,
            "ig_screen_writer",// 0xFFE63677,
            "ig_siemonyetarian",// 0x4C7B2F05,
            "ig_solomon",// 0x86BDFE26,
            "ig_stevehains",// 0x382121C8,
            "ig_stretch",// 0x36984358,
            "ig_talina",// 0xE793C8E8,
            "ig_tanisha",// 0x0D810489,
            "ig_taocheng",// 0xDC5C5EA5,
            "ig_taostranslator",// 0x7C851464,
            "ig_tenniscoach",// 0xA23B5F57,
            "ig_terry",// 0x67000B94,
            "ig_tomepsilon",// 0xCD777AAA,
            "ig_tonya",// 0xCAC85344,
            "ig_tracydisanto",// 0xDE352A35,
            "ig_trafficwarden",// 0x5719786D,
            "ig_tylerdix",// 0x5265F707,
            "ig_vagspeak",// 0xF9FD068C,
            "ig_wade",// 0x92991B72,
            "ig_zimbor",// 0x0B34D6F5,
            "mp_f_boatstaff_01",// 0x3293B9CE,
            "mp_f_cardesign_01",// 0x242C34A7,
            "mp_f_chbar_01",// 0xC3F6E385,
            "mp_f_cocaine_01",// 0x4B657AF8,
            "mp_f_counterfeit_01",// 0xB788F1F5,
            "mp_f_deadhooker",// 0x73DEA88B,
            "mp_f_execpa_01",// 0x432CA064,
            "mp_f_execpa_02",// 0x5972CCF0,
            "mp_f_forgery_01",// 0x781A3CF8,
            //"mp_f_freemode_01",// 0x9C9EFFD8,
            "mp_f_helistaff_01",// 0x19B6FF06,
            "mp_f_meth_01",// 0xD2B27EC1,
            "mp_f_misty_01",// 0xD128FF9D,
            "mp_f_stripperlite",// 0x2970A494,
            "mp_f_weed_01",// 0xB26573A3,
            "mp_g_m_pros_01",// 0x6C9DD7C9,
            "mp_m_avongoon",// 0x9C13CB95,
            "mp_m_boatstaff_01",// 0xC85F0A88,
            "mp_m_bogdangoon",// 0x4D5696F7,
            "mp_m_claude_01",// 0xC0F371B7,
            "mp_m_cocaine_01",// 0x56D38F95,
            "mp_m_counterfeit_01",// 0x9855C974,
            "mp_m_exarmy_01",// 0x45348DBB,
            "mp_m_execpa_01",// 0x3E8417BC,
            "mp_m_famdd_01",// 0x33A464E5,
            "mp_m_fibsec_01",// 0x5CDEF405,
            "mp_m_forgery_01",// 0x613E709B,
            //"mp_m_freemode_01",// 0x705E61F2,
            "mp_m_g_vagfun_01",// 0xC4A617BD,
            "mp_m_marston_01",// 0x38430167,
            "mp_m_meth_01",// 0xEDB42F3F,
            "mp_m_niko_01",// 0xEEDACFC9,
            "mp_m_securoguard_01",// 0xDA2C984E,
            "mp_m_shopkeep_01",// 0x18CE57D0,
            "mp_m_waremech_01",// 0xF7A74139,
            "mp_m_weapexp_01",// 0x36EA5B09,
            "mp_m_weapwork_01",// 0x4186506E,
            "mp_m_weed_01",// 0x917ED459,
            "mp_s_m_armoured_01",// 0xCDEF5408,
            //"player_one",// 0x9B22DBAF,
            //"player_two",// 0x9B810FA2,
            //"player_zero",// 0x0D7114C9,
            "s_f_m_fembarber",// 0x163B875B,
            "s_f_m_maid_01",// 0xE093C5C6,
            "s_f_m_shop_high",// 0xAE47E4B0,
            "s_f_m_sweatshop_01",// 0x312B5BC0,
            "s_f_y_airhostess_01",// 0x5D71A46F,
            "s_f_y_bartender_01",// 0x780C01BD,
            "s_f_y_baywatch_01",// 0x4A8E5536,
            "s_f_y_cop_01",// 0x15F8700D,
            "s_f_y_factory_01",// 0x69F46BF3,
            "s_f_y_hooker_01",// 0x028ABF95,
            "s_f_y_hooker_02",// 0x14C3E407,
            "s_f_y_hooker_03",// 0x031640AC,
            "s_f_y_migrant_01",// 0xD55B2BF5,
            "s_f_y_movprem_01",// 0x2300C816,
            "s_f_y_ranger_01",// 0x9FC7F637,
            "s_f_y_scrubs_01",// 0xAB594AB6,
            "s_f_y_sheriff_01",// 0x4161D042,
            "s_f_y_shop_low",// 0xA96E2604,
            "s_f_y_shop_mid",// 0x3EECBA5D,
            "s_f_y_stripper_01",// 0x52580019,
            "s_f_y_stripper_02",// 0x6E0FB794,
            "s_f_y_stripperlite",// 0x5C14EDFA,
            "s_f_y_sweatshop_01",// 0x8502B6B2,
            "s_m_m_ammucountry",// 0x0DE9A30A,
            "s_m_m_armoured_01",// 0x95C76ECD,
            "s_m_m_armoured_02",// 0x63858A4A,
            "s_m_m_autoshop_01",// 0x040EABE3,
            "s_m_m_autoshop_02",// 0xF06B849D,
            "s_m_m_bouncer_01",// 0x9FD4292D,
            "s_m_m_ccrew_01",// 0xC9E5F56B,
            "s_m_m_chemsec_01",// 0x2EFEAFD5,
            "s_m_m_ciasec_01",// 0x625D6958,
            "s_m_m_cntrybar_01",// 0x1A021B83,
            "s_m_m_dockwork_01",// 0x14D7B4E0,
            "s_m_m_doctor_01",// 0xD47303AC,
            "s_m_m_fiboffice_01",// 0xEDBC7546,
            "s_m_m_fiboffice_02",// 0x26F067AD,
            "s_m_m_fibsec_01",// 0x7B8B434B,
            "s_m_m_gaffer_01",// 0xA956BD9E,
            "s_m_m_gardener_01",// 0x49EA5685,
            "s_m_m_gentransport",// 0x1880ED06,
            "s_m_m_hairdress_01",// 0x418DFF92,
            "s_m_m_highsec_01",// 0xF161D212,
            "s_m_m_highsec_02",// 0x2930C1AB,
            "s_m_m_janitor",// 0xA96BD9EC,
            "s_m_m_lathandy_01",// 0x9E80D2CE,
            "s_m_m_lifeinvad_01",// 0xDE0077FD,
            "s_m_m_linecook",// 0xDB9C0997,
            "s_m_m_lsmetro_01",// 0x765AAAE4,
            "s_m_m_mariachi_01",// 0x7EA4FFA6,
            "s_m_m_marine_01",// 0xF2DAA2ED,
            "s_m_m_marine_02",// 0xF0259D83,
            "s_m_m_migrant_01",// 0xED0CE4C6,
            "s_m_m_movalien_01",// 0x64611296,
            "s_m_m_movprem_01",// 0xD85E6D28,
            "s_m_m_movspace_01",// 0xE7B31432,
            "s_m_m_paramedic_01",// 0xB353629E,
            "s_m_m_pilot_01",// 0xE75B4B1C,
            "s_m_m_pilot_02",// 0xF63DE8E1,
            "s_m_m_postal_01",// 0x62599034,
            "s_m_m_postal_02",// 0x7367324F,
            "s_m_m_prisguard_01",// 0x56C96FC6,
            "s_m_m_scientist_01",// 0x4117D39B,
            "s_m_m_security_01",// 0xD768B228,
            "s_m_m_snowcop_01",// 0x1AE8BB58,
            "s_m_m_strperf_01",// 0x795AC7A8,
            "s_m_m_strpreach_01",// 0x1C0077FB,
            "s_m_m_strvend_01",// 0xCE9113A9,
            "s_m_m_trucker_01",// 0x59511A6C,
            "s_m_m_ups_01",// 0x9FC37F22,
            "s_m_m_ups_02",// 0xD0BDE116,
            "s_m_o_busker_01",// 0xAD9EF1BB,
            "s_m_y_airworker",// 0x62018559,
            "s_m_y_ammucity_01",// 0x9E08633D,
            "s_m_y_armymech_01",// 0x62CC28E2,
            "s_m_y_autopsy_01",// 0xB2273D4E,
            "s_m_y_barman_01",// 0xE5A11106,
            "s_m_y_baywatch_01",// 0x0B4A6862,
            "s_m_y_blackops_01",// 0xB3F3EE34,
            "s_m_y_blackops_02",// 0x7A05FA59,
            "s_m_y_blackops_03",// 0x5076A73B,
            "s_m_y_busboy_01",// 0xD8F9CD47,
            "s_m_y_chef_01",// 0x0F977CEB,
            "s_m_y_clown_01",// 0x04498DDE,
            "s_m_y_construct_01",// 0xD7DA9E99,
            "s_m_y_construct_02",// 0xC5FEFADE,
            "s_m_y_cop_01",// 0x5E3DA4A4,
            "s_m_y_dealer_01",// 0xE497BBEF,
            "s_m_y_devinsec_01",// 0x9B557274,
            "s_m_y_dockwork_01",// 0x867639D1,
            "s_m_y_doorman_01",// 0x22911304,
            "s_m_y_dwservice_01",// 0x75D30A91,
            "s_m_y_dwservice_02",// 0xF5908A06,
            "s_m_y_factory_01",// 0x4163A158,
            "s_m_y_fireman_01",// 0xB6B1EDA8,
            "s_m_y_garbage",// 0xEE75A00F,
            "s_m_y_grip_01",// 0x309E7DEA,
            "s_m_y_hwaycop_01",// 0x739B1EF5,
            "s_m_y_marine_01",// 0x65793043,
            "s_m_y_marine_02",// 0x58D696FE,
            "s_m_y_marine_03",// 0x72C0CAD2,
            "s_m_y_mime",// 0x3CDCA742,
            "s_m_y_pestcont_01",// 0x48114518,
            "s_m_y_pilot_01",// 0xAB300C07,
            "s_m_y_prismuscl_01",// 0x5F2113A1,
            "s_m_y_prisoner_01",// 0xB1BB9B59,
            "s_m_y_ranger_01",// 0xEF7135AE,
            "s_m_y_robber_01",// 0xC05E1399,
            "s_m_y_sheriff_01",// 0xB144F9B9,
            "s_m_y_shop_mask",// 0x6E122C06,
            "s_m_y_strvend_01",// 0x927F2323,
            "s_m_y_swat_01",// 0x8D8F1B10,
            "s_m_y_uscg_01",// 0xCA0050E9,
            "s_m_y_valet_01",// 0x3B96F23E,
            "s_m_y_waiter_01",// 0xAD4C724C,
            "s_m_y_winclean_01",// 0x550D8D9D,
            "s_m_y_xmech_01",// 0x441405EC,
            "s_m_y_xmech_02_mp",// 0x69147A0D,
            "s_m_y_xmech_02",// 0xBE20FA04,
            "u_f_m_corpse_01",// 0x2E140314,
            "u_f_m_drowned_01",// 0xD7F37609,
            "u_f_m_miranda",// 0x414FA27B,
            "u_f_m_promourn_01",// 0xA20899E7,
            "u_f_o_moviestar",// 0x35578634,
            "u_f_o_prolhost_01",// 0xC512DD23,
            "u_f_y_bikerchic",// 0xFA389D4F,
            "u_f_y_comjane",// 0xB6AA85CE,
            "u_f_y_corpse_01",// 0x9C70109D,
            "u_f_y_corpse_02",// 0x0D9C72F8,
            "u_f_y_hotposh_01",// 0x969B6DFE,
            "u_f_y_jewelass_01",// 0xF0D4BE2E,
            "u_f_y_mistress",// 0x5DCA2528,
            "u_f_y_poppymich",// 0x23E9A09E,
            "u_f_y_princess",// 0xD2E3A284,
            "u_f_y_spyactress",// 0x5B81D86C,
            "u_m_m_aldinapoli",// 0xF0EC56E2,
            "u_m_m_bankman",// 0xC306D6F5,
            "u_m_m_bikehire_01",// 0x76474545,
            "u_m_m_doa_01",// 0x621E6BFD,
            "u_m_m_edtoh",// 0x2A797197,
            "u_m_m_fibarchitect",// 0x342333D3,
            "u_m_m_filmdirector",// 0x2B6E1BB6,
            "u_m_m_glenstank_01",// 0x45BB1666,
            "u_m_m_griff_01",// 0xC454BCBB,
            "u_m_m_jesus_01",// 0xCE2CB751,
            "u_m_m_jewelsec_01",// 0xACCCBDB6,
            "u_m_m_jewelthief",// 0xE6CC3CDC,
            "u_m_m_markfost",// 0x1C95CB0B,
            "u_m_m_partytarget",// 0x81F74DE7,
            "u_m_m_prolsec_01",// 0x709220C7,
            "u_m_m_promourn_01",// 0xCE96030B,
            "u_m_m_rivalpap",// 0x60D5D6DA,
            "u_m_m_spyactor",// 0xAC0EA5D8,
            "u_m_m_streetart_01",// 0x6C19E962,
            "u_m_m_willyfist",// 0x90769A8F,
            "u_m_o_filmnoir",// 0x2BACC2DB,
            "u_m_o_finguru_01",// 0x46E39E63,
            "u_m_o_taphillbilly",// 0x9A1E5E52,
            "u_m_o_tramp_01",// 0x6A8F1F9B,
            "u_m_y_abner",// 0xF0AC2626,
            "u_m_y_antonb",// 0xCF623A2C,
            "u_m_y_babyd",// 0xDA116E7E,
            "u_m_y_baygor",// 0x5244247D,
            "u_m_y_burgerdrug_01",// 0x8B7D3766,
            "u_m_y_chip",// 0x24604B2B,
            "u_m_y_corpse_01",// 0x94C2A03F,
            "u_m_y_cyclist_01",// 0x2D0EFCEB,
            "u_m_y_fibmugger_01",// 0x85B9C668,
            "u_m_y_guido_01",// 0xC6B49A2F,
            "u_m_y_gunvend_01",// 0xB3229752,
            "u_m_y_hippie_01",// 0xF041880B,
            "u_m_y_imporage",// 0x348065F5,
            "u_m_y_juggernaut_01",// 0x90EF5134,
            "u_m_y_justin",// 0x7DC3908F,
            "u_m_y_mani",// 0xC8BB1E52,
            "u_m_y_militarybum",// 0x4705974A,
            "u_m_y_paparazzi",// 0x5048B328,
            "u_m_y_party_01",// 0x36E70600,
            "u_m_y_pogo_01",// 0xDC59940D,
            "u_m_y_prisoner_01",// 0x7B9B4BC0,
            "u_m_y_proldriver_01",// 0x855E36A3,
            "u_m_y_rsranger_01",// 0x3C438CD2,
            "u_m_y_sbike",// 0x6AF4185D,
            "u_m_y_staggrm_01",// 0x9194CE03,
            "u_m_y_tattoo_01",// 0x94AE2B8C,
            "u_m_y_zombie_01"// 0xAC4B4506,
            };

			return ped_models[index];
		}

		static public int GetModelIdFromName(string name)
		{
			switch (name)
			{
				case "mp_m_freemode_01":
					return 0;
				case "mp_f_freemode_01":
					return 1;
				case "player_one":
					return 2;
				case "player_two":
					return 3;
				case "player_zero":
					return 4;
				case "a_f_m_beach_01":
					return 5;
				case "a_f_m_bevhills_01":
					return 6;
				case "a_f_m_bevhills_02":
					return 7;
				case "a_f_m_bodybuild_01":
					return 8;
				case "a_f_m_business_02":
					return 9;
				case "a_f_m_downtown_01":
					return 10;
				case "a_f_m_eastsa_01":
					return 11;
				case "a_f_m_eastsa_02":
					return 12;
				case "a_f_m_fatbla_01":
					return 13;
				case "a_f_m_fatcult_01":
					return 14;
				case "a_f_m_fatwhite_01":
					return 15;
				case "a_f_m_ktown_01":
					return 16;
				case "a_f_m_ktown_02":
					return 17;
				case "a_f_m_prolhost_01":
					return 18;
				case "a_f_m_salton_01":
					return 19;
				case "a_f_m_skidrow_01":
					return 20;
				case "a_f_m_soucent_01":
					return 21;
				case "a_f_m_soucent_02":
					return 22;
				case "a_f_m_soucentmc_01":
					return 23;
				case "a_f_m_tourist_01":
					return 24;
				case "a_f_m_tramp_01":
					return 25;
				case "a_f_m_trampbeac_01":
					return 26;
				case "a_f_o_genstreet_01":
					return 27;
				case "a_f_o_indian_01":
					return 28;
				case "a_f_o_ktown_01":
					return 29;
				case "a_f_o_salton_01":
					return 30;
				case "a_f_o_soucent_01":
					return 31;
				case "a_f_o_soucent_02":
					return 32;
				case "a_f_y_beach_01":
					return 33;
				case "a_f_y_bevhills_01":
					return 34;
				case "a_f_y_bevhills_02":
					return 35;
				case "a_f_y_bevhills_03":
					return 36;
				case "a_f_y_bevhills_04":
					return 37;
				case "a_f_y_business_01":
					return 38;
				case "a_f_y_business_02":
					return 39;
				case "a_f_y_business_03":
					return 40;
				case "a_f_y_business_04":
					return 41;
				case "a_f_y_eastsa_01":
					return 42;
				case "a_f_y_eastsa_02":
					return 43;
				case "a_f_y_eastsa_03":
					return 44;
				case "a_f_y_epsilon_01":
					return 45;
				case "a_f_y_femaleagent":
					return 46;
				case "a_f_y_fitness_01":
					return 47;
				case "a_f_y_fitness_02":
					return 48;
				case "a_f_y_genhot_01":
					return 49;
				case "a_f_y_golfer_01":
					return 50;
				case "a_f_y_hiker_01":
					return 51;
				case "a_f_y_hippie_01":
					return 52;
				case "a_f_y_hipster_01":
					return 53;
				case "a_f_y_hipster_02":
					return 54;
				case "a_f_y_hipster_03":
					return 55;
				case "a_f_y_hipster_04":
					return 56;
				case "a_f_y_indian_01":
					return 57;
				case "a_f_y_juggalo_01":
					return 58;
				case "a_f_y_runner_01":
					return 59;
				case "a_f_y_rurmeth_01":
					return 60;
				case "a_f_y_scdressy_01":
					return 61;
				case "a_f_y_skater_01":
					return 62;
				case "a_f_y_soucent_01":
					return 63;
				case "a_f_y_soucent_02":
					return 64;
				case "a_f_y_soucent_03":
					return 65;
				case "a_f_y_tennis_01":
					return 66;
				case "a_f_y_topless_01":
					return 67;
				case "a_f_y_tourist_01":
					return 68;
				case "a_f_y_tourist_02":
					return 69;
				case "a_f_y_vinewood_01":
					return 70;
				case "a_f_y_vinewood_02":
					return 71;
				case "a_f_y_vinewood_03":
					return 72;
				case "a_f_y_vinewood_04":
					return 73;
				case "a_f_y_yoga_01":
					return 74;
				case "a_m_m_acult_01":
					return 75;
				case "a_m_m_afriamer_01":
					return 76;
				case "a_m_m_beach_01":
					return 77;
				case "a_m_m_beach_02":
					return 78;
				case "a_m_m_bevhills_01":
					return 79;
				case "a_m_m_bevhills_02":
					return 80;
				case "a_m_m_business_01":
					return 81;
				case "a_m_m_eastsa_01":
					return 82;
				case "a_m_m_eastsa_02":
					return 83;
				case "a_m_m_farmer_01":
					return 84;
				case "a_m_m_fatlatin_01":
					return 85;
				case "a_m_m_genfat_01":
					return 86;
				case "a_m_m_genfat_02":
					return 87;
				case "a_m_m_golfer_01":
					return 88;
				case "a_m_m_hasjew_01":
					return 89;
				case "a_m_m_hillbilly_01":
					return 90;
				case "a_m_m_hillbilly_02":
					return 91;
				case "a_m_m_indian_01":
					return 92;
				case "a_m_m_ktown_01":
					return 93;
				case "a_m_m_malibu_01":
					return 94;
				case "a_m_m_mexcntry_01":
					return 95;
				case "a_m_m_mexlabor_01":
					return 96;
				case "a_m_m_og_boss_01":
					return 97;
				case "a_m_m_paparazzi_01":
					return 98;
				case "a_m_m_polynesian_01":
					return 99;
				case "a_m_m_prolhost_01":
					return 100;
				case "a_m_m_rurmeth_01":
					return 101;
				case "a_m_m_salton_01":
					return 102;
				case "a_m_m_salton_02":
					return 103;
				case "a_m_m_salton_03":
					return 104;
				case "a_m_m_salton_04":
					return 105;
				case "a_m_m_skater_01":
					return 106;
				case "a_m_m_skidrow_01":
					return 107;
				case "a_m_m_socenlat_01":
					return 108;
				case "a_m_m_soucent_01":
					return 109;
				case "a_m_m_soucent_02":
					return 110;
				case "a_m_m_soucent_03":
					return 111;
				case "a_m_m_soucent_04":
					return 112;
				case "a_m_m_stlat_02":
					return 113;
				case "a_m_m_tennis_01":
					return 114;
				case "a_m_m_tourist_01":
					return 115;
				case "a_m_m_tramp_01":
					return 116;
				case "a_m_m_trampbeac_01":
					return 117;
				case "a_m_m_tranvest_01":
					return 118;
				case "a_m_m_tranvest_02":
					return 119;
				case "a_m_o_acult_01":
					return 120;
				case "a_m_o_acult_02":
					return 121;
				case "a_m_o_beach_01":
					return 122;
				case "a_m_o_genstreet_01":
					return 123;
				case "a_m_o_ktown_01":
					return 124;
				case "a_m_o_salton_01":
					return 125;
				case "a_m_o_soucent_01":
					return 126;
				case "a_m_o_soucent_02":
					return 127;
				case "a_m_o_soucent_03":
					return 128;
				case "a_m_o_tramp_01":
					return 129;
				case "a_m_y_acult_01":
					return 130;
				case "a_m_y_acult_02":
					return 131;
				case "a_m_y_beach_01":
					return 132;
				case "a_m_y_beach_02":
					return 133;
				case "a_m_y_beach_03":
					return 134;
				case "a_m_y_beachvesp_01":
					return 135;
				case "a_m_y_beachvesp_02":
					return 136;
				case "a_m_y_bevhills_01":
					return 137;
				case "a_m_y_bevhills_02":
					return 138;
				case "a_m_y_breakdance_01":
					return 139;
				case "a_m_y_busicas_01":
					return 140;
				case "a_m_y_business_01":
					return 141;
				case "a_m_y_business_02":
					return 142;
				case "a_m_y_business_03":
					return 143;
				case "a_m_y_cyclist_01":
					return 144;
				case "a_m_y_dhill_01":
					return 145;
				case "a_m_y_downtown_01":
					return 146;
				case "a_m_y_eastsa_01":
					return 147;
				case "a_m_y_eastsa_02":
					return 148;
				case "a_m_y_epsilon_01":
					return 149;
				case "a_m_y_epsilon_02":
					return 150;
				case "a_m_y_gay_01":
					return 151;
				case "a_m_y_gay_02":
					return 152;
				case "a_m_y_genstreet_01":
					return 153;
				case "a_m_y_genstreet_02":
					return 154;
				case "a_m_y_golfer_01":
					return 155;
				case "a_m_y_hasjew_01":
					return 156;
				case "a_m_y_hiker_01":
					return 157;
				case "a_m_y_hippy_01":
					return 158;
				case "a_m_y_hipster_01":
					return 159;
				case "a_m_y_hipster_02":
					return 160;
				case "a_m_y_hipster_03":
					return 161;
				case "a_m_y_indian_01":
					return 162;
				case "a_m_y_jetski_01":
					return 163;
				case "a_m_y_juggalo_01":
					return 164;
				case "a_m_y_ktown_01":
					return 165;
				case "a_m_y_ktown_02":
					return 166;
				case "a_m_y_latino_01":
					return 167;
				case "a_m_y_methhead_01":
					return 168;
				case "a_m_y_mexthug_01":
					return 169;
				case "a_m_y_motox_01":
					return 170;
				case "a_m_y_motox_02":
					return 171;
				case "a_m_y_musclbeac_01":
					return 172;
				case "a_m_y_musclbeac_02":
					return 173;
				case "a_m_y_polynesian_01":
					return 174;
				case "a_m_y_roadcyc_01":
					return 175;
				case "a_m_y_runner_01":
					return 176;
				case "a_m_y_runner_02":
					return 177;
				case "a_m_y_salton_01":
					return 178;
				case "a_m_y_skater_01":
					return 179;
				case "a_m_y_skater_02":
					return 180;
				case "a_m_y_soucent_01":
					return 181;
				case "a_m_y_soucent_02":
					return 182;
				case "a_m_y_soucent_03":
					return 183;
				case "a_m_y_soucent_04":
					return 184;
				case "a_m_y_stbla_01":
					return 185;
				case "a_m_y_stbla_02":
					return 186;
				case "a_m_y_stlat_01":
					return 187;
				case "a_m_y_stwhi_01":
					return 188;
				case "a_m_y_stwhi_02":
					return 189;
				case "a_m_y_sunbathe_01":
					return 190;
				case "a_m_y_surfer_01":
					return 191;
				case "a_m_y_vindouche_01":
					return 192;
				case "a_m_y_vinewood_01":
					return 193;
				case "a_m_y_vinewood_02":
					return 194;
				case "a_m_y_vinewood_03":
					return 195;
				case "a_m_y_vinewood_04":
					return 196;
				case "a_m_y_yoga_01":
					return 197;
				case "cs_amandatownley":
					return 198;
				case "cs_andreas":
					return 199;
				case "cs_ashley":
					return 200;
				case "cs_bankman":
					return 201;
				case "cs_barry":
					return 202;
				case "cs_beverly":
					return 203;
				case "cs_brad":
					return 204;
				case "cs_bradcadaver":
					return 205;
				case "cs_carbuyer":
					return 206;
				case "cs_casey":
					return 207;
				case "cs_chengsr":
					return 208;
				case "cs_chrisformage":
					return 209;
				case "cs_clay":
					return 210;
				case "cs_dale":
					return 211;
				case "cs_davenorton":
					return 212;
				case "cs_debra":
					return 213;
				case "cs_denise":
					return 214;
				case "cs_devin":
					return 215;
				case "cs_dom":
					return 216;
				case "cs_dreyfuss":
					return 217;
				case "cs_drfriedlander":
					return 218;
				case "cs_fabien":
					return 219;
				case "cs_fbisuit_01":
					return 220;
				case "cs_floyd":
					return 221;
				case "cs_guadalope":
					return 222;
				case "cs_gurk":
					return 223;
				case "cs_hunter":
					return 224;
				case "cs_janet":
					return 225;
				case "cs_jewelass":
					return 226;
				case "cs_jimmyboston":
					return 227;
				case "cs_jimmydisanto":
					return 228;
				case "cs_joeminuteman":
					return 229;
				case "cs_johnnyklebitz":
					return 230;
				case "cs_josef":
					return 231;
				case "cs_josh":
					return 232;
				case "cs_karen_daniels":
					return 233;
				case "cs_lamardavis":
					return 234;
				case "cs_lazlow":
					return 235;
				case "cs_lestercrest":
					return 236;
				case "cs_lifeinvad_01":
					return 237;
				case "cs_magenta":
					return 238;
				case "cs_manuel":
					return 239;
				case "cs_marnie":
					return 240;
				case "cs_martinmadrazo":
					return 241;
				case "cs_maryann":
					return 242;
				case "cs_michelle":
					return 243;
				case "cs_milton":
					return 244;
				case "cs_molly":
					return 245;
				case "cs_movpremf_01":
					return 246;
				case "cs_movpremmale":
					return 247;
				case "cs_mrk":
					return 248;
				case "cs_mrs_thornhill":
					return 249;
				case "cs_mrsphillips":
					return 250;
				case "cs_natalia":
					return 251;
				case "cs_nervousron":
					return 252;
				case "cs_nigel":
					return 253;
				case "cs_old_man1a":
					return 254;
				case "cs_old_man2":
					return 255;
				case "cs_omega":
					return 256;
				case "cs_orleans":
					return 257;
				case "cs_paper":
					return 258;
				case "cs_patricia":
					return 259;
				case "cs_priest":
					return 260;
				case "cs_prolsec_02":
					return 261;
				case "cs_russiandrunk":
					return 262;
				case "cs_siemonyetarian":
					return 263;
				case "cs_solomon":
					return 264;
				case "cs_stevehains":
					return 265;
				case "cs_stretch":
					return 266;
				case "cs_tanisha":
					return 267;
				case "cs_taocheng":
					return 268;
				case "cs_taostranslator":
					return 269;
				case "cs_tenniscoach":
					return 270;
				case "cs_terry":
					return 271;
				case "cs_tom":
					return 272;
				case "cs_tomepsilon":
					return 273;
				case "cs_tracydisanto":
					return 274;
				case "cs_wade":
					return 275;
				case "cs_zimbor":
					return 276;
				case "csb_abigail":
					return 277;
				case "csb_agent":
					return 278;
				case "csb_anita":
					return 279;
				case "csb_anton":
					return 280;
				case "csb_ballasog":
					return 281;
				case "csb_bride":
					return 282;
				case "csb_burgerdrug":
					return 283;
				case "csb_car3guy1":
					return 284;
				case "csb_car3guy2":
					return 285;
				case "csb_chef":
					return 286;
				case "csb_chef2":
					return 287;
				case "csb_chin_goon":
					return 288;
				case "csb_cletus":
					return 289;
				case "csb_cop":
					return 290;
				case "csb_customer":
					return 291;
				case "csb_denise_friend":
					return 292;
				case "csb_fos_rep":
					return 293;
				case "csb_g":
					return 294;
				case "csb_groom":
					return 295;
				case "csb_grove_str_dlr":
					return 296;
				case "csb_hao":
					return 297;
				case "csb_hugh":
					return 298;
				case "csb_imran":
					return 299;
				case "csb_jackhowitzer":
					return 300;
				case "csb_janitor":
					return 301;
				case "csb_maude":
					return 302;
				case "csb_money":
					return 303;
				case "csb_mp_agent14":
					return 304;
				case "csb_mweather":
					return 305;
				case "csb_ortega":
					return 306;
				case "csb_oscar":
					return 307;
				case "csb_paige":
					return 308;
				case "csb_popov":
					return 309;
				case "csb_porndudes":
					return 310;
				case "csb_prologuedriver":
					return 311;
				case "csb_prolsec":
					return 312;
				case "csb_ramp_gang":
					return 313;
				case "csb_ramp_hic":
					return 314;
				case "csb_ramp_hipster":
					return 315;
				case "csb_ramp_marine":
					return 316;
				case "csb_ramp_mex":
					return 317;
				case "csb_rashcosvki":
					return 318;
				case "csb_reporter":
					return 319;
				case "csb_roccopelosi":
					return 320;
				case "csb_screen_writer":
					return 321;
				case "csb_stripper_01":
					return 322;
				case "csb_stripper_02":
					return 323;
				case "csb_tonya":
					return 324;
				case "csb_trafficwarden":
					return 325;
				case "csb_undercover":
					return 326;
				case "csb_vagspeak":
					return 327;
				case "g_f_importexport_01":
					return 328;
				case "g_f_y_ballas_01":
					return 329;
				case "g_f_y_families_01":
					return 330;
				case "g_f_y_lost_01":
					return 331;
				case "g_f_y_vagos_01":
					return 332;
				case "g_m_importexport_01":
					return 333;
				case "g_m_m_armboss_01":
					return 334;
				case "g_m_m_armgoon_01":
					return 335;
				case "g_m_m_armlieut_01":
					return 336;
				case "g_m_m_chemwork_01":
					return 337;
				case "g_m_m_chiboss_01":
					return 338;
				case "g_m_m_chicold_01":
					return 339;
				case "g_m_m_chigoon_01":
					return 340;
				case "g_m_m_chigoon_02":
					return 341;
				case "g_m_m_korboss_01":
					return 342;
				case "g_m_m_mexboss_01":
					return 343;
				case "g_m_m_mexboss_02":
					return 344;
				case "g_m_y_armgoon_02":
					return 345;
				case "g_m_y_azteca_01":
					return 346;
				case "g_m_y_ballaeast_01":
					return 347;
				case "g_m_y_ballaorig_01":
					return 348;
				case "g_m_y_ballasout_01":
					return 349;
				case "g_m_y_famca_01":
					return 350;
				case "g_m_y_famdnf_01":
					return 351;
				case "g_m_y_famfor_01":
					return 352;
				case "g_m_y_korean_01":
					return 353;
				case "g_m_y_korean_02":
					return 354;
				case "g_m_y_korlieut_01":
					return 355;
				case "g_m_y_lost_01":
					return 356;
				case "g_m_y_lost_02":
					return 357;
				case "g_m_y_lost_03":
					return 358;
				case "g_m_y_mexgang_01":
					return 359;
				case "g_m_y_mexgoon_01":
					return 360;
				case "g_m_y_mexgoon_02":
					return 361;
				case "g_m_y_mexgoon_03":
					return 362;
				case "g_m_y_pologoon_01":
					return 363;
				case "g_m_y_pologoon_02":
					return 364;
				case "g_m_y_salvaboss_01":
					return 365;
				case "g_m_y_salvagoon_01":
					return 366;
				case "g_m_y_salvagoon_02":
					return 367;
				case "g_m_y_salvagoon_03":
					return 368;
				case "g_m_y_strpunk_01":
					return 369;
				case "g_m_y_strpunk_02":
					return 370;
				case "hc_driver":
					return 371;
				case "hc_gunman":
					return 372;
				case "hc_hacker":
					return 373;
				case "ig_abigail":
					return 374;
				case "ig_agent":
					return 375;
				case "ig_amandatownley":
					return 376;
				case "ig_andreas":
					return 377;
				case "ig_ashley":
					return 378;
				case "ig_avon":
					return 379;
				case "ig_ballasog":
					return 380;
				case "ig_bankman":
					return 381;
				case "ig_barry":
					return 382;
				case "ig_benny":
					return 383;
				case "ig_bestmen":
					return 384;
				case "ig_beverly":
					return 385;
				case "ig_brad":
					return 386;
				case "ig_bride":
					return 387;
				case "ig_car3guy1":
					return 388;
				case "ig_car3guy2":
					return 389;
				case "ig_casey":
					return 390;
				case "ig_chef":
					return 391;
				case "ig_chef2":
					return 392;
				case "ig_chengsr":
					return 393;
				case "ig_chrisformage":
					return 394;
				case "ig_clay":
					return 395;
				case "ig_claypain":
					return 396;
				case "ig_cletus":
					return 397;
				case "ig_dale":
					return 398;
				case "ig_davenorton":
					return 399;
				case "ig_denise":
					return 400;
				case "ig_devin":
					return 401;
				case "ig_dom":
					return 402;
				case "ig_dreyfuss":
					return 403;
				case "ig_drfriedlander":
					return 404;
				case "ig_fabien":
					return 405;
				case "ig_fbisuit_01":
					return 406;
				case "ig_floyd":
					return 407;
				case "ig_g":
					return 408;
				case "ig_groom":
					return 409;
				case "ig_hao":
					return 410;
				case "ig_hunter":
					return 411;
				case "ig_janet":
					return 412;
				case "ig_jay_norris":
					return 413;
				case "ig_jewelass":
					return 414;
				case "ig_jimmyboston":
					return 415;
				case "ig_jimmydisanto":
					return 416;
				case "ig_joeminuteman":
					return 417;
				case "ig_johnnyklebitz":
					return 418;
				case "ig_josef":
					return 419;
				case "ig_josh":
					return 420;
				case "ig_karen_daniels":
					return 421;
				case "ig_kerrymcintosh":
					return 422;
				case "ig_lamardavis":
					return 423;
				case "ig_lazlow":
					return 424;
				case "ig_lestercrest_2":
					return 425;
				case "ig_lestercrest":
					return 426;
				case "ig_lifeinvad_01":
					return 427;
				case "ig_lifeinvad_02":
					return 428;
				case "ig_magenta":
					return 429;
				case "ig_malc":
					return 430;
				case "ig_manuel":
					return 431;
				case "ig_marnie":
					return 432;
				case "ig_maryann":
					return 433;
				case "ig_maude":
					return 434;
				case "ig_michelle":
					return 435;
				case "ig_milton":
					return 436;
				case "ig_molly":
					return 437;
				case "ig_money":
					return 438;
				case "ig_mp_agent14":
					return 439;
				case "ig_mrk":
					return 440;
				case "ig_mrs_thornhill":
					return 441;
				case "ig_mrsphillips":
					return 442;
				case "ig_natalia":
					return 443;
				case "ig_nervousron":
					return 444;
				case "ig_nigel":
					return 445;
				case "ig_old_man1a":
					return 446;
				case "ig_old_man2":
					return 447;
				case "ig_omega":
					return 448;
				case "ig_oneil":
					return 449;
				case "ig_orleans":
					return 450;
				case "ig_ortega":
					return 451;
				case "ig_paige":
					return 452;
				case "ig_paper":
					return 453;
				case "ig_patricia":
					return 454;
				case "ig_popov":
					return 455;
				case "ig_priest":
					return 456;
				case "ig_prolsec_02":
					return 457;
				case "ig_ramp_gang":
					return 458;
				case "ig_ramp_hic":
					return 459;
				case "ig_ramp_hipster":
					return 460;
				case "ig_ramp_mex":
					return 461;
				case "ig_rashcosvki":
					return 462;
				case "ig_roccopelosi":
					return 463;
				case "ig_russiandrunk":
					return 464;
				case "ig_screen_writer":
					return 465;
				case "ig_siemonyetarian":
					return 466;
				case "ig_solomon":
					return 467;
				case "ig_stevehains":
					return 468;
				case "ig_stretch":
					return 469;
				case "ig_talina":
					return 470;
				case "ig_tanisha":
					return 471;
				case "ig_taocheng":
					return 472;
				case "ig_taostranslator":
					return 473;
				case "ig_tenniscoach":
					return 474;
				case "ig_terry":
					return 475;
				case "ig_tomepsilon":
					return 476;
				case "ig_tonya":
					return 477;
				case "ig_tracydisanto":
					return 478;
				case "ig_trafficwarden":
					return 479;
				case "ig_tylerdix":
					return 480;
				case "ig_vagspeak":
					return 481;
				case "ig_wade":
					return 482;
				case "ig_zimbor":
					return 483;
				case "mp_f_boatstaff_01":
					return 484;
				case "mp_f_cardesign_01":
					return 485;
				case "mp_f_chbar_01":
					return 486;
				case "mp_f_cocaine_01":
					return 487;
				case "mp_f_counterfeit_01":
					return 488;
				case "mp_f_deadhooker":
					return 489;
				case "mp_f_execpa_01":
					return 490;
				case "mp_f_execpa_02":
					return 491;
				case "mp_f_forgery_01":
					return 492;
				case "mp_f_freemode_02": ///////////////////
					return 493;
				case "mp_f_helistaff_01":
					return 494;
				case "mp_f_meth_01":
					return 495;
				case "mp_f_misty_01":
					return 496;
				case "mp_f_stripperlite":
					return 497;
				case "mp_f_weed_01":
					return 498;
				case "mp_g_m_pros_01":
					return 499;
				case "mp_m_avongoon":
					return 500;
				case "mp_m_boatstaff_01":
					return 501;
				case "mp_m_bogdangoon":
					return 502;
				case "mp_m_claude_01":
					return 503;
				case "mp_m_cocaine_01":
					return 504;
				case "mp_m_counterfeit_01":
					return 505;
				case "mp_m_exarmy_01":
					return 506;
				case "mp_m_execpa_01":
					return 507;
				case "mp_m_famdd_01":
					return 508;
				case "mp_m_fibsec_01":
					return 509;
				case "mp_m_forgery_01":
					return 510;
				case "mp_m_freemode_02": //////////////////////////
					return 511;
				case "mp_m_g_vagfun_01":
					return 512;
				case "mp_m_marston_01":
					return 513;
				case "mp_m_meth_01":
					return 514;
				case "mp_m_niko_01":
					return 515;
				case "mp_m_securoguard_01":
					return 516;
				case "mp_m_shopkeep_01":
					return 517;
				case "mp_m_waremech_01":
					return 518;
				case "mp_m_weapexp_01":
					return 519;
				case "mp_m_weapwork_01":
					return 520;
				case "mp_m_weed_01":
					return 521;
				case "mp_s_m_armoured_01":
					return 522;
				case "player_one2": //////////////////////////////////////
					return 523;
				case "player_two2"://////////////////////////////////////
					return 524;
				case "player_zero2"://////////////////////////////////////
					return 525;
				case "s_f_m_fembarber":
					return 526;
				case "s_f_m_maid_01":
					return 527;
				case "s_f_m_shop_high":
					return 528;
				case "s_f_m_sweatshop_01":
					return 529;
				case "s_f_y_airhostess_01":
					return 530;
				case "s_f_y_bartender_01":
					return 531;
				case "s_f_y_baywatch_01":
					return 532;
				case "s_f_y_cop_01":
					return 533;
				case "s_f_y_factory_01":
					return 534;
				case "s_f_y_hooker_01":
					return 535;
				case "s_f_y_hooker_02":
					return 536;
				case "s_f_y_hooker_03":
					return 537;
				case "s_f_y_migrant_01":
					return 538;
				case "s_f_y_movprem_01":
					return 539;
				case "s_f_y_ranger_01":
					return 540;
				case "s_f_y_scrubs_01":
					return 541;
				case "s_f_y_sheriff_01":
					return 542;
				case "s_f_y_shop_low":
					return 543;
				case "s_f_y_shop_mid":
					return 544;
				case "s_f_y_stripper_01":
					return 545;
				case "s_f_y_stripper_02":
					return 546;
				case "s_f_y_stripperlite":
					return 547;
				case "s_f_y_sweatshop_01":
					return 548;
				case "s_m_m_ammucountry":
					return 549;
				case "s_m_m_armoured_01":
					return 550;
				case "s_m_m_armoured_02":
					return 551;
				case "s_m_m_autoshop_01":
					return 552;
				case "s_m_m_autoshop_02":
					return 553;
				case "s_m_m_bouncer_01":
					return 554;
				case "s_m_m_ccrew_01":
					return 555;
				case "s_m_m_chemsec_01":
					return 556;
				case "s_m_m_ciasec_01":
					return 557;
				case "s_m_m_cntrybar_01":
					return 558;
				case "s_m_m_dockwork_01":
					return 559;
				case "s_m_m_doctor_01":
					return 560;
				case "s_m_m_fiboffice_01":
					return 561;
				case "s_m_m_fiboffice_02":
					return 562;
				case "s_m_m_fibsec_01":
					return 563;
				case "s_m_m_gaffer_01":
					return 564;
				case "s_m_m_gardener_01":
					return 565;
				case "s_m_m_gentransport":
					return 566;
				case "s_m_m_hairdress_01":
					return 567;
				case "s_m_m_highsec_01":
					return 568;
				case "s_m_m_highsec_02":
					return 569;
				case "s_m_m_janitor":
					return 570;
				case "s_m_m_lathandy_01":
					return 571;
				case "s_m_m_lifeinvad_01":
					return 572;
				case "s_m_m_linecook":
					return 573;
				case "s_m_m_lsmetro_01":
					return 574;
				case "s_m_m_mariachi_01":
					return 575;
				case "s_m_m_marine_01":
					return 576;
				case "s_m_m_marine_02":
					return 577;
				case "s_m_m_migrant_01":
					return 578;
				case "s_m_m_movalien_01":
					return 579;
				case "s_m_m_movprem_01":
					return 580;
				case "s_m_m_movspace_01":
					return 581;
				case "s_m_m_paramedic_01":
					return 582;
				case "s_m_m_pilot_01":
					return 583;
				case "s_m_m_pilot_02":
					return 584;
				case "s_m_m_postal_01":
					return 585;
				case "s_m_m_postal_02":
					return 586;
				case "s_m_m_prisguard_01":
					return 587;
				case "s_m_m_scientist_01":
					return 588;
				case "s_m_m_security_01":
					return 589;
				case "s_m_m_snowcop_01":
					return 590;
				case "s_m_m_strperf_01":
					return 591;
				case "s_m_m_strpreach_01":
					return 592;
				case "s_m_m_strvend_01":
					return 593;
				case "s_m_m_trucker_01":
					return 594;
				case "s_m_m_ups_01":
					return 595;
				case "s_m_m_ups_02":
					return 596;
				case "s_m_o_busker_01":
					return 597;
				case "s_m_y_airworker":
					return 598;
				case "s_m_y_ammucity_01":
					return 599;
				case "s_m_y_armymech_01":
					return 600;
				case "s_m_y_autopsy_01":
					return 601;
				case "s_m_y_barman_01":
					return 602;
				case "s_m_y_baywatch_01":
					return 603;
				case "s_m_y_blackops_01":
					return 604;
				case "s_m_y_blackops_02":
					return 605;
				case "s_m_y_blackops_03":
					return 606;
				case "s_m_y_busboy_01":
					return 607;
				case "s_m_y_chef_01":
					return 608;
				case "s_m_y_clown_01":
					return 609;
				case "s_m_y_construct_01":
					return 610;
				case "s_m_y_construct_02":
					return 611;
				case "s_m_y_cop_01":
					return 612;
				case "s_m_y_dealer_01":
					return 613;
				case "s_m_y_devinsec_01":
					return 614;
				case "s_m_y_dockwork_01":
					return 615;
				case "s_m_y_doorman_01":
					return 616;
				case "s_m_y_dwservice_01":
					return 617;
				case "s_m_y_dwservice_02":
					return 618;
				case "s_m_y_factory_01":
					return 619;
				case "s_m_y_fireman_01":
					return 620;
				case "s_m_y_garbage":
					return 621;
				case "s_m_y_grip_01":
					return 622;
				case "s_m_y_hwaycop_01":
					return 623;
				case "s_m_y_marine_01":
					return 624;
				case "s_m_y_marine_02":
					return 625;
				case "s_m_y_marine_03":
					return 626;
				case "s_m_y_mime":
					return 627;
				case "s_m_y_pestcont_01":
					return 628;
				case "s_m_y_pilot_01":
					return 629;
				case "s_m_y_prismuscl_01":
					return 630;
				case "s_m_y_prisoner_01":
					return 631;
				case "s_m_y_ranger_01":
					return 632;
				case "s_m_y_robber_01":
					return 633;
				case "s_m_y_sheriff_01":
					return 634;
				case "s_m_y_shop_mask":
					return 635;
				case "s_m_y_strvend_01":
					return 636;
				case "s_m_y_swat_01":
					return 637;
				case "s_m_y_uscg_01":
					return 638;
				case "s_m_y_valet_01":
					return 639;
				case "s_m_y_waiter_01":
					return 640;
				case "s_m_y_winclean_01":
					return 641;
				case "s_m_y_xmech_01":
					return 642;
				case "s_m_y_xmech_02_mp":
					return 643;
				case "s_m_y_xmech_02":
					return 644;
				case "u_f_m_corpse_01":
					return 645;
				case "u_f_m_drowned_01":
					return 646;
				case "u_f_m_miranda":
					return 647;
				case "u_f_m_promourn_01":
					return 648;
				case "u_f_o_moviestar":
					return 649;
				case "u_f_o_prolhost_01":
					return 650;
				case "u_f_y_bikerchic":
					return 651;
				case "u_f_y_comjane":
					return 652;
				case "u_f_y_corpse_01":
					return 653;
				case "u_f_y_corpse_02":
					return 654;
				case "u_f_y_hotposh_01":
					return 655;
				case "u_f_y_jewelass_01":
					return 656;
				case "u_f_y_mistress":
					return 657;
				case "u_f_y_poppymich":
					return 658;
				case "u_f_y_princess":
					return 659;
				case "u_f_y_spyactress":
					return 660;
				case "u_m_m_aldinapoli":
					return 661;
				case "u_m_m_bankman":
					return 662;
				case "u_m_m_bikehire_01":
					return 663;
				case "u_m_m_doa_01":
					return 664;
				case "u_m_m_edtoh":
					return 665;
				case "u_m_m_fibarchitect":
					return 666;
				case "u_m_m_filmdirector":
					return 667;
				case "u_m_m_glenstank_01":
					return 668;
				case "u_m_m_griff_01":
					return 669;
				case "u_m_m_jesus_01":
					return 670;
				case "u_m_m_jewelsec_01":
					return 671;
				case "u_m_m_jewelthief":
					return 672;
				case "u_m_m_markfost":
					return 673;
				case "u_m_m_partytarget":
					return 674;
				case "u_m_m_prolsec_01":
					return 675;
				case "u_m_m_promourn_01":
					return 676;
				case "u_m_m_rivalpap":
					return 677;
				case "u_m_m_spyactor":
					return 678;
				case "u_m_m_streetart_01":
					return 679;
				case "u_m_m_willyfist":
					return 680;
				case "u_m_o_filmnoir":
					return 681;
				case "u_m_o_finguru_01":
					return 682;
				case "u_m_o_taphillbilly":
					return 683;
				case "u_m_o_tramp_01":
					return 684;
				case "u_m_y_abner":
					return 685;
				case "u_m_y_antonb":
					return 686;
				case "u_m_y_babyd":
					return 687;
				case "u_m_y_baygor":
					return 688;
				case "u_m_y_burgerdrug_01":
					return 689;
				case "u_m_y_chip":
					return 690;
				case "u_m_y_corpse_01":
					return 691;
				case "u_m_y_cyclist_01":
					return 692;
				case "u_m_y_fibmugger_01":
					return 693;
				case "u_m_y_guido_01":
					return 694;
				case "u_m_y_gunvend_01":
					return 695;
				case "u_m_y_hippie_01":
					return 696;
				case "u_m_y_imporage":
					return 697;
				case "u_m_y_juggernaut_01":
					return 698;
				case "u_m_y_justin":
					return 699;
				case "u_m_y_mani":
					return 700;
				case "u_m_y_militarybum":
					return 701;
				case "u_m_y_paparazzi":
					return 702;
				case "u_m_y_party_01":
					return 703;
				case "u_m_y_pogo_01":
					return 704;
				case "u_m_y_prisoner_01":
					return 705;
				case "u_m_y_proldriver_01":
					return 706;
				case "u_m_y_rsranger_01":
					return 707;
				case "u_m_y_sbike":
					return 708;
				case "u_m_y_staggrm_01":
					return 709;
				case "u_m_y_tattoo_01":
					return 710;
				default:
					return -1;
			};
		}
	}
}
