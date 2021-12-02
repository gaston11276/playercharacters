using System.Collections.Generic;
using Gaston11276.SimpleUi;
using CitizenFX.Core;
using Gaston11276.Playercharacters.Client.Models;

namespace Gaston11276.Playercharacters.Client
{
	public class HudComponentNewCharacter : HudComponent
	{
		// Columns Spawn location
		UiPanel uiColumnLabels = new UiPanel();
		UiPanel uiColumnIndex = new UiPanel();
		UiPanel uiColumnName= new UiPanel();
		UiPanel uiColumnDecrease = new UiPanel();
		UiPanel uiColumnIncrease = new UiPanel();

		private Editbox uiEditForename = new Editbox();
		private Editbox uiEditMiddlename = new Editbox();
		private Editbox uiEditLastname = new Editbox();
		private Editbox uiEditGender = new Editbox();
		private Editbox uiEditDateOfBirth = new Editbox();
		private Textbox uiCreateCharacterButton = new Textbox();

		private UiEntrySpawnPosition uiEntrySpawnLocation;

		fpVoid OnCreateCallback;

		int spawnIndex = 0;
		List<SpawnPosition> spawnPositions = new List<SpawnPosition>();

		public HudComponentNewCharacter()
		{
			CreateSpawnPositions(ref spawnPositions);
		}

		public override async void SetUi()
		{
			await uiEntrySpawnLocation.SetUi();
		}

		public void SetCharacterInfo(Character character)
		{
			character.Forename = uiEditForename.GetText();
			character.Middlename = uiEditMiddlename.GetText();
			character.Surname = uiEditLastname.GetText();

			if (string.IsNullOrWhiteSpace(character.Middlename)) character.Middlename = null;

			string genderText = uiEditGender.GetText();

			if (genderText.StartsWith("F"))
			{
				character.Gender = 1;
				character.Model = ((uint)PedHash.FreemodeFemale01).ToString();
			}
			else
			{
				character.Gender = 0;
				character.Model = ((uint)PedHash.FreemodeMale01).ToString();
			}

			character.AnimationSet = "move_m@drunk@verydrunk";

			System.DateTime birthdate = new System.DateTime(1977, 05, 20);
			character.DateOfBirth = birthdate;

			NFive.SDK.Core.Models.Position pos = new NFive.SDK.Core.Models.Position(spawnPositions[spawnIndex].position.X, spawnPositions[spawnIndex].position.Y, spawnPositions[spawnIndex].position.Z);
			character.Position = pos;
		}

		public void ClearCharacterListEdit()
		{
			uiEditForename.ClearText();
			uiEditMiddlename.ClearText();
			uiEditLastname.ClearText();
			uiEditGender.ClearText();
			uiEditDateOfBirth.ClearText();
		}

		public void RegisterOnCreateCallback(fpVoid OnCreateCallback)
		{
			this.OnCreateCallback = OnCreateCallback;
		}

		private UiEntrySpawnPosition CreateEntry(string label)
		{
			float defaultPadding = 0.0025f;

			UiEntrySpawnPosition entry = new UiEntrySpawnPosition();
			entry.SetDelay(Delay);

			entry.uiLabel.SetPadding(new UiRectangle(defaultPadding));
			entry.uiLabel.SetText(label);
			entry.uiLabel.SetGravity(HGravity.Left);
			entry.uiLabel.SetFlags(UiElement.TRANSPARENT);
			uiColumnLabels.AddElement(entry.uiLabel);

			entry.uiIndex.SetPadding(new UiRectangle(defaultPadding));
			//entry.uiIndex.SetFlags(UiElement.TRANSPARENT);
			uiColumnIndex.AddElement(entry.uiIndex);

			entry.uiName.SetPadding(new UiRectangle(defaultPadding));
			entry.uiName.SetFlags(UiElement.TRANSPARENT);
			uiColumnName.AddElement(entry.uiName);

			entry.btnDecrease.SetText("-");
			entry.btnDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnDecrease.RegisterOnLMBRelease(entry.DecreaseIndex);
			inputsOnMouseMove.Add(entry.btnDecrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnDecrease.OnMouseButton);
			uiColumnDecrease.AddElement(entry.btnDecrease);

			entry.btnIncrease.SetText("+");
			entry.btnIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnIncrease.RegisterOnLMBRelease(entry.IncreaseIndex);
			inputsOnMouseMove.Add(entry.btnIncrease.OnCursorMove);
			inputsOnMouseButton.Add(entry.btnIncrease.OnMouseButton);
			uiColumnIncrease.AddElement(entry.btnIncrease);

			entry.GetIndexMax = GetNumberOfSpawnLocations;
			entry.GetIndex = GetSpawnLocationIndex;
			entry.SetIndex = SetSpawnLocationIndex;
			entry.GetName = GetSpawnLocationName;

			return entry;
		}

		protected override void CreateColumns()
		{
			
		}

		protected override void CreateContent()
		{
			uiHeader.SetText("New Character");
			uiAppearanceMain.SetHDimension(Dimension.Absolute);
			uiAppearanceMain.SetWidth(0.4f);
			uiAppearanceMain.SetAlignment(HAlignment.Right);
			uiAppearanceMain.SetAlignment(VAlignment.Top);


			UiPanel uiNewCharacterEditPanel = new UiPanel();
			uiNewCharacterEditPanel.SetPadding(new UiRectangle(0f));
			uiNewCharacterEditPanel.SetFlags(UiElement.TRANSPARENT);
			contentFrame.AddElement(uiNewCharacterEditPanel);

			UiPanel uiNewCharacterEditPanelLeft = new UiPanel();
			uiNewCharacterEditPanelLeft.SetOrientation(Orientation.Vertical);
			uiNewCharacterEditPanelLeft.SetGravity(HGravity.Left);
			uiNewCharacterEditPanelLeft.SetMargin(new UiRectangle(0f, 0f, 0.002f, 0f));
			uiNewCharacterEditPanelLeft.SetPadding(new UiRectangle(0f));
			uiNewCharacterEditPanelLeft.SetFlags(UiElement.TRANSPARENT);
			uiNewCharacterEditPanel.AddElement(uiNewCharacterEditPanelLeft);

			UiPanel uiNewCharacterEditPanelRight = new UiPanel();
			uiNewCharacterEditPanelRight.SetOrientation(Orientation.Vertical);
			uiNewCharacterEditPanelRight.SetHDimension(Dimension.Max);
			uiNewCharacterEditPanelRight.SetGravity(HGravity.Left);
			uiNewCharacterEditPanelRight.SetPadding(new UiRectangle(0f));
			uiNewCharacterEditPanelRight.SetMargin(new UiRectangle(-0.002f, 0f, 0f, 0f));
			uiNewCharacterEditPanelRight.SetFlags(UiElement.TRANSPARENT);
			uiNewCharacterEditPanel.AddElement(uiNewCharacterEditPanelRight);

			Textbox uiLabelForename = new Textbox();
			uiLabelForename.SetText("Forename");
			uiLabelForename.SetGravity(HGravity.Left);
			uiLabelForename.SetFlags(UiElement.TRANSPARENT);
			uiLabelForename.SetPadding(new UiRectangle(defaultPadding));
			uiNewCharacterEditPanelLeft.AddElement(uiLabelForename);
			Textbox uiLabelMiddlename = new Textbox();
			uiLabelMiddlename.SetText("Middle name");
			uiLabelMiddlename.SetGravity(HGravity.Left);
			uiLabelMiddlename.SetFlags(UiElement.TRANSPARENT);
			uiLabelMiddlename.SetPadding(new UiRectangle(defaultPadding));
			uiNewCharacterEditPanelLeft.AddElement(uiLabelMiddlename);
			Textbox uiLabelLastname = new Textbox();
			uiLabelLastname.SetText("Lastname");
			uiLabelLastname.SetGravity(HGravity.Left);
			uiLabelLastname.SetFlags(UiElement.TRANSPARENT);
			uiLabelLastname.SetPadding(new UiRectangle(defaultPadding));
			uiNewCharacterEditPanelLeft.AddElement(uiLabelLastname);
			Textbox uiLabelGender = new Textbox();
			uiLabelGender.SetText("Gender");
			uiLabelGender.SetGravity(HGravity.Left);
			uiLabelGender.SetFlags(UiElement.TRANSPARENT);
			uiLabelGender.SetPadding(new UiRectangle(defaultPadding));
			uiNewCharacterEditPanelLeft.AddElement(uiLabelGender);
			Textbox uiLabelAge = new Textbox();
			uiLabelAge.SetText("Date of birth");
			uiLabelAge.SetGravity(HGravity.Left);
			uiLabelAge.SetFlags(UiElement.TRANSPARENT);
			uiLabelAge.SetPadding(new UiRectangle(defaultPadding));
			uiNewCharacterEditPanelLeft.AddElement(uiLabelAge);

			uiEditForename = new Editbox();
			uiEditForename.SetText("");
			uiEditForename.SetPadding(new UiRectangle(defaultPadding));
			uiEditForename.SetHDimension(Dimension.Max);
			uiEditForename.SetGravity(HGravity.Left);
			uiEditForename.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			uiEditForename.RegisterOnTextChanged(OnTextChanged);
			inputsOnMouseMove.Add(uiEditForename.OnCursorMove);
			inputsOnMouseButton.Add(uiEditForename.OnMouseButton);
			inputsOnKey.Add(uiEditForename.OnKey);
			uiNewCharacterEditPanelRight.AddElement(uiEditForename);

			uiEditMiddlename = new Editbox();
			uiEditMiddlename.SetText("");
			uiEditMiddlename.SetPadding(new UiRectangle(defaultPadding));
			uiEditMiddlename.SetHDimension(Dimension.Max);
			uiEditMiddlename.SetGravity(HGravity.Left);
			uiEditMiddlename.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			uiEditMiddlename.RegisterOnTextChanged(OnTextChanged);
			inputsOnMouseMove.Add(uiEditMiddlename.OnCursorMove);
			inputsOnMouseButton.Add(uiEditMiddlename.OnMouseButton);
			inputsOnKey.Add(uiEditMiddlename.OnKey);
			uiNewCharacterEditPanelRight.AddElement(uiEditMiddlename);

			uiEditLastname = new Editbox();
			uiEditLastname.SetText("");
			uiEditLastname.SetPadding(new UiRectangle(defaultPadding));
			uiEditLastname.SetHDimension(Dimension.Max);
			uiEditLastname.SetGravity(HGravity.Left);
			uiEditLastname.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			uiEditLastname.RegisterOnTextChanged(OnTextChanged);
			inputsOnMouseMove.Add(uiEditLastname.OnCursorMove);
			inputsOnMouseButton.Add(uiEditLastname.OnMouseButton);
			inputsOnKey.Add(uiEditLastname.OnKey);
			uiNewCharacterEditPanelRight.AddElement(uiEditLastname);

			uiEditGender = new Editbox();
			uiEditGender.SetText("M/F");
			uiEditGender.SetPadding(new UiRectangle(defaultPadding));
			uiEditGender.SetHDimension(Dimension.Max);
			uiEditGender.SetGravity(HGravity.Left);
			uiEditGender.SetProperties(UiElement.CANFOCUS | UiElement.SELECTABLE);
			uiEditGender.RegisterOnTextChanged(OnTextChanged);
			inputsOnMouseMove.Add(uiEditGender.OnCursorMove);
			inputsOnMouseButton.Add(uiEditGender.OnMouseButton);
			inputsOnKey.Add(uiEditGender.OnKey);
			uiNewCharacterEditPanelRight.AddElement(uiEditGender);

			uiEditDateOfBirth = new Editbox();
			uiEditDateOfBirth.SetText("2000-01-01");
			uiEditDateOfBirth.SetPadding(new UiRectangle(defaultPadding));
			uiEditDateOfBirth.SetHDimension(Dimension.Max);
			uiEditDateOfBirth.SetGravity(HGravity.Left);
			uiNewCharacterEditPanelRight.AddElement(uiEditDateOfBirth);

			// Spawn Location
			UiPanel uiSLPanel = new UiPanel();
			uiSLPanel.SetMargin(new UiRectangle(0f, -0.002f, 0f, 0f));
			uiSLPanel.SetPadding(new UiRectangle(0f));
			uiSLPanel.SetHDimension(Dimension.Max);
			uiSLPanel.SetFlags(UiElement.TRANSPARENT);
			contentFrame.AddElement(uiSLPanel);

			UiPanel uiSLPanelLeft = new UiPanel();
			uiSLPanelLeft.SetPadding(new UiRectangle(0f));
			uiSLPanelLeft.SetGravity(HGravity.Left);
			uiSLPanelLeft.SetFlags(UiElement.TRANSPARENT);
			uiSLPanel.AddElement(uiSLPanelLeft);
			UiPanel uiSLPanelRight = new UiPanel();
			uiSLPanelRight.SetGravity(HGravity.Right);
			uiSLPanelRight.SetPadding(new UiRectangle(0f));
			uiSLPanelRight.SetHDimension(Dimension.Max);
			uiSLPanel.AddElement(uiSLPanelRight);

			CreateColumn(uiSLPanelLeft, HGravity.Left, uiColumnLabels);
			uiColumnLabels.SetPadding(new UiRectangle(0f));
			CreateColumn(uiSLPanelRight, HGravity.Left, uiColumnName);
			uiColumnName.SetPadding(new UiRectangle(0f));
			CreateColumn(uiSLPanelRight, HGravity.Right, uiColumnIndex);
			uiColumnIndex.SetPadding(new UiRectangle(0f));
			CreateColumn(uiSLPanelRight, HGravity.Center, uiColumnDecrease);
			uiColumnDecrease.SetPadding(new UiRectangle(0f));
			CreateColumn(uiSLPanelRight, HGravity.Center, uiColumnIncrease);
			uiColumnIncrease.SetPadding(new UiRectangle(0f));
			uiEntrySpawnLocation = CreateEntry("Spawn Location");

			// Button
			UiPanel uiNewCharacterButtonsPanel = new UiPanel();
			uiNewCharacterButtonsPanel.SetMargin(new UiRectangle(0f, -0.002f, 0f, 0f));
			uiNewCharacterButtonsPanel.SetPadding(new UiRectangle(defaultPadding));
			uiNewCharacterButtonsPanel.SetHDimension(Dimension.Max);
			uiNewCharacterButtonsPanel.SetGravity(HGravity.Left);
			contentFrame.AddElement(uiNewCharacterButtonsPanel);

			uiCreateCharacterButton = new Textbox();
			uiCreateCharacterButton.SetText("Create");
			uiCreateCharacterButton.SetPadding(new UiRectangle(defaultPadding));
			uiCreateCharacterButton.SetFlags(UiElement.DISABLED);
			uiCreateCharacterButton.SetProperties(UiElement.CANFOCUS);
			uiCreateCharacterButton.RegisterOnDisableCallback(uiCreateCharacterButton.OnDisabled);
			uiCreateCharacterButton.RegisterOffDisableCallback(uiCreateCharacterButton.OffDisabled);
			uiCreateCharacterButton.RegisterOnLMBRelease(OnCreate);
			inputsOnMouseMove.Add(uiCreateCharacterButton.OnCursorMove);
			inputsOnMouseButton.Add(uiCreateCharacterButton.OnMouseButton);
			uiNewCharacterButtonsPanel.AddElement(uiCreateCharacterButton);
		}

		private void OnTextChanged()
		{
			if (uiEditForename.GetText().Length > 0 && uiEditLastname.GetText().Length > 0)
			{
				uiCreateCharacterButton.ClearFlags(UiElement.DISABLED);
			}
			else
			{
				uiCreateCharacterButton.SetFlags(UiElement.DISABLED);
			}
		}

		private void OnCreate()
		{
			OnCreateCallback();
		}

		private int GetNumberOfSpawnLocations()
		{
			return spawnPositions.Count - 1;
		}
		private int GetSpawnLocationIndex()
		{
			return spawnIndex;
		}
		private void SetSpawnLocationIndex(int index)
		{
			spawnIndex = index;
		}

		private string GetSpawnLocationName()
		{
			return spawnPositions[spawnIndex].name;
		}

		private void CreateSpawnPositions(ref List<SpawnPosition> spawnPositions)
		{
			int index = 0;
			spawnPositions.Add(new SpawnPosition("Michael's house", new Vector3(-802.311f, 175.056f, 72.8446f), index++));
			spawnPositions.Add(new SpawnPosition("Franklin's mom", new Vector3(-9.96562f, -1438.54f, 31.1015f), index++));
			spawnPositions.Add(new SpawnPosition("Franklin's house", new Vector3(0.916756f, 528.485f, 174.628f), index++));
			spawnPositions.Add(new SpawnPosition("Winewood Hills, House at Dame", new Vector3(-181.615f, 852.8f, 232.701f), index++));
			spawnPositions.Add(new SpawnPosition("Amphi theatre 1", new Vector3(657.723f, 457.342f, 144.641f), index++));
			spawnPositions.Add(new SpawnPosition("Amphi theatre 2", new Vector3(134.387f, 1150.31f, 231.594f), index++));
			spawnPositions.Add(new SpawnPosition("Winewood sign", new Vector3(726.14f, 1196.91f, 326.262f), index++));
			spawnPositions.Add(new SpawnPosition("Winewood Hills, radio towers", new Vector3(740.792f, 1283.62f, 360.297f), index++));
			spawnPositions.Add(new SpawnPosition("Observatory", new Vector3(-437.009f, 1059.59f, 327.331f), index++));
			spawnPositions.Add(new SpawnPosition("Baytree Canyon View", new Vector3(-428.771f, 1596.8f, 356.338f), index++));
			spawnPositions.Add(new SpawnPosition("Winewood Hills, road (Deserted house)", new Vector3(-1348.78f, 723.87f, 186.45f), index++));
			spawnPositions.Add(new SpawnPosition("Richman Glen, mansion", new Vector3(-1543.24f, 830.069f, 182.132f), index++));
			spawnPositions.Add(new SpawnPosition("Pacific Bluffs", new Vector3(-2150.48f, 222.019f, 184.602f), index++));
			spawnPositions.Add(new SpawnPosition("Pacific Bluffs, resort", new Vector3(-3032.13f, 22.2157f, 10.1184f), index++));
			spawnPositions.Add(new SpawnPosition("Mount Gordo", new Vector3(3063.97f, 5608.88f, 209.245f), index++));
			spawnPositions.Add(new SpawnPosition("Tongva Hills, Hotel", new Vector3(-2614.35f, 1872.49f, 167.32f), index++));
			spawnPositions.Add(new SpawnPosition("Tongva Hills, Wine mansion", new Vector3(-1873.94f, 2088.73f, 140.994f), index++));
			spawnPositions.Add(new SpawnPosition("Great Chaparral, mine", new Vector3(-597.177f, 2092.16f, 131.413f), index++));
			spawnPositions.Add(new SpawnPosition("Redwood Lights Track", new Vector3(967.126f, 2226.99f, 54.0588f), index++));
			spawnPositions.Add(new SpawnPosition("Great Chaparral, Church", new Vector3(-338.043f, 2829f, 56.0871f), index++));
			spawnPositions.Add(new SpawnPosition("Mirror Park", new Vector3(1082.25f, -696.921f, 58.0099f), index++));
			spawnPositions.Add(new SpawnPosition("Land Act Dam", new Vector3(1658.31f, -13.9234f, 169.992f), index++));
			spawnPositions.Add(new SpawnPosition("N.O.O.S.E Parking", new Vector3(2522.98f, -384.436f, 92.9928f), index++));
			spawnPositions.Add(new SpawnPosition("Pacific Ocean", new Vector3(2826.27f, -656.489f, 1.87841f), index++));
			spawnPositions.Add(new SpawnPosition("Palmer-Taylor Power Station", new Vector3(2851.12f, 1467.5f, 24.5554f), index++));
			spawnPositions.Add(new SpawnPosition("Ron Alternates Wind Farm", new Vector3(2336.33f, 2535.39f, 46.5177f), index++));
			spawnPositions.Add(new SpawnPosition("Grand Senora Desert", new Vector3(2410.46f, 3077.88f, 48.1529f), index++));
			spawnPositions.Add(new SpawnPosition("Sandy Shores", new Vector3(2451.15f, 3768.37f, 41.3477f), index++));
			spawnPositions.Add(new SpawnPosition("Mount Gordo, Sea", new Vector3(3337.78f, 5174.8f, 18.2108f), index++));
			spawnPositions.Add(new SpawnPosition("Chiliad Mountain State Wilderness", new Vector3(-1119.33f, 4978.52f, 186.26f), index++));
			spawnPositions.Add(new SpawnPosition("Mount Gordo 2", new Vector3(2877.3f, 5911.57f, 369.618f), index++));
			spawnPositions.Add(new SpawnPosition("Mount Gordo 3", new Vector3(2942.1f, 5306.73f, 101.52f), index++));
			spawnPositions.Add(new SpawnPosition("Mount Chiliad Farm", new Vector3(2211.29f, 5577.94f, 53.872f), index++));
			spawnPositions.Add(new SpawnPosition("Mount Gordo 4", new Vector3(1602.39f, 6623.02f, 15.8417f), index++));
			spawnPositions.Add(new SpawnPosition("Pacific Ocean", new Vector3(66.0113f, 7203.58f, 3.16f), index++));
			spawnPositions.Add(new SpawnPosition("Paletto Bay", new Vector3(-219.201f, 6562.82f, 10.9706f), index++));
			spawnPositions.Add(new SpawnPosition("Paletto Bay 2", new Vector3(-45.1562f, 6301.64f, 31.6114f), index++));
			spawnPositions.Add(new SpawnPosition("Chiliad Mountain State Wilderness 2", new Vector3(-1004.77f, 4854.32f, 274.606f), index++));
			spawnPositions.Add(new SpawnPosition("Paletto Cove", new Vector3(-1580.01f, 5173.3f, 19.5813f), index++));
			spawnPositions.Add(new SpawnPosition("Paletto Cove 2", new Vector3(-1467.95f, 5416.2f, 23.5959f), index++));
			spawnPositions.Add(new SpawnPosition("Fort Zancudo", new Vector3(-2359.31f, 3243.83f, 92.9037f), index++));
			spawnPositions.Add(new SpawnPosition("North Cumash", new Vector3(-2612.96f, 3555.03f, 4.85649f), index++));
			spawnPositions.Add(new SpawnPosition("Lagh Zancudo", new Vector3(-2083.27f, 2616.94f, 3.08396f), index++));
			spawnPositions.Add(new SpawnPosition("Raton Canyon", new Vector3(-524.471f, 4195f, 193.731f), index++));
			spawnPositions.Add(new SpawnPosition("Raton Canyon 2", new Vector3(-840.713f, 4183.18f, 215.29f), index++));
			spawnPositions.Add(new SpawnPosition("Tongva Valley", new Vector3(-1576.24f, 2103.87f, 67.576f), index++));
			spawnPositions.Add(new SpawnPosition("Richman", new Vector3(-1634.37f, 209.816f, 60.6413f), index++));
			spawnPositions.Add(new SpawnPosition("Richman 2", new Vector3(-1495.07f, 142.697f, 55.6527f), index++));
			spawnPositions.Add(new SpawnPosition("Pacific Bluffs", new Vector3(-1715.41f, -197.722f, 57.698f), index++));
			spawnPositions.Add(new SpawnPosition("Richard Majestic", new Vector3(-1181.07f, -505.544f, 35.5661f), index++));
			spawnPositions.Add(new SpawnPosition("Del Perro Beach", new Vector3(-1712.37f, -1082.91f, 13.0801f), index++));
			spawnPositions.Add(new SpawnPosition("Vespucci Beach", new Vector3(-1352.43f, -1542.75f, 4.42268f), index++));
			spawnPositions.Add(new SpawnPosition("Richman 3", new Vector3(-1756.89f, 427.531f, 127.685f), index++));
			spawnPositions.Add(new SpawnPosition("Pacific Ocean 2", new Vector3(3060.2f, 2113.2f, 1.6613f), index++));
			spawnPositions.Add(new SpawnPosition("Mount Chiliad 3", new Vector3(501.646f, 5604.53f, 797.91f), index++));
			spawnPositions.Add(new SpawnPosition("Alamo Sea", new Vector3(714.109f, 4151.15f, 35.7792f), index++));
			spawnPositions.Add(new SpawnPosition("Pillbox Hill, Crane", new Vector3(-103.651f, -967.93f, 296.52f), index++));
			spawnPositions.Add(new SpawnPosition("Elysian Island, Bridge", new Vector3(-265.333f, -2419.35f, 122.366f), index++));
			spawnPositions.Add(new SpawnPosition("Sandy Shores", new Vector3(1788.25f, 3890.34f, 34.3849f), index++));

		}
	}
}
