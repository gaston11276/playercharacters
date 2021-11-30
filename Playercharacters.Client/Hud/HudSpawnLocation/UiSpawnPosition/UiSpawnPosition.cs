using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Gaston11276.SimpleUi;

namespace Gaston11276.Playercharacters.Client
{
	public class SpawnPosition
	{
		public int index;
		public string name;
		public Vector3 position;
		
		public SpawnPosition(string name, Vector3 position, int index)
		{
			this.name = name;
			this.position = position;
			this.index = index;
		}
	}

	public class UiSpawnPosition: HudComponent
	{
		// Upper panel columns
		UiPanel uiColumnLabels = new UiPanel();
		UiPanel uiColumnNames = new UiPanel();
		UiPanel uiColumnIndex = new UiPanel();
		UiPanel uiColumnDecrease = new UiPanel();
		UiPanel uiColumnIncrease = new UiPanel();

		UiEntrySpawnPosition uiEntrySpawnPosition;
		List<SpawnPosition> spawnPositions = new List<SpawnPosition>();

		int spawnIndex = 0;

		public UiSpawnPosition()
		{
			cameraMode = CameraMode.Front;
			CreateSpawnPositions(ref spawnPositions);
		}

		public override async void SetUi()
		{
			await uiEntrySpawnPosition.SetUi();
			base.SetUi();
		}

		protected override void CreateColumns()
		{
			// Upper panel
			UiPanel uiCenterPanel = new UiPanel();
			uiCenterPanel.SetPadding(new UiRectangle(defaultPadding));
			contentFrame.AddElement(uiCenterPanel);

			CreateColumn(uiCenterPanel, HGravity.Left, uiColumnLabels, "");
			CreateColumn(uiCenterPanel, HGravity.Left, uiColumnNames, "");
			CreateColumn(uiCenterPanel, HGravity.Right, uiColumnIndex, "");
			CreateColumn(uiCenterPanel, HGravity.Center, uiColumnDecrease, "");
			CreateColumn(uiCenterPanel, HGravity.Center, uiColumnIncrease, "");
		}

		protected override void CreateContent()
		{
			uiHeader.SetText("Spawn Position");
			uiEntrySpawnPosition = CreateEntry("Location");

			// Buttons
			UiPanel uiButtonsPanel = new UiPanel();
			uiButtonsPanel.SetPadding(new UiRectangle(defaultPadding));
			uiButtonsPanel.SetGravity(HGravity.Left);
			uiButtonsPanel.SetFlags(UiElement.TRANSPARENT);
			contentFrame.AddElement(uiButtonsPanel);

			Textbox uiButtonSpawn= new Textbox();
			uiButtonSpawn.SetText("Spawn");
			uiButtonSpawn.SetPadding(new UiRectangle(defaultPadding));
			uiButtonSpawn.SetProperties(UiElement.CANFOCUS);
			uiButtonSpawn.RegisterOnLMBRelease(Respawn);
			inputsOnMouseMove.Add(uiButtonSpawn.OnCursorMove);
			inputsOnMouseButton.Add(uiButtonSpawn.OnMouseButton);
			uiButtonsPanel.AddElement(uiButtonSpawn);

			Textbox uiButtonCancel = new Textbox();
			uiButtonCancel.SetText("Cancel");
			uiButtonCancel.SetPadding(new UiRectangle(defaultPadding));
			uiButtonCancel.SetProperties(UiElement.CANFOCUS);
			uiButtonCancel.RegisterOnLMBRelease(Close);
			inputsOnMouseMove.Add(uiButtonCancel.OnCursorMove);
			inputsOnMouseButton.Add(uiButtonCancel.OnMouseButton);
			uiButtonsPanel.AddElement(uiButtonCancel);
		}

		private UiEntrySpawnPosition CreateEntry(string label)
		{
			float defaultPadding = 0.0025f;


			UiEntrySpawnPosition entry = new UiEntrySpawnPosition();
			entry.SetLogger(Logger);

			entry.uiLabel.SetPadding(new UiRectangle(defaultPadding));
			entry.uiLabel.SetText(label);
			entry.uiLabel.SetFlags(UiElement.TRANSPARENT);
			uiColumnLabels.AddElement(entry.uiLabel);

			entry.uiName.SetPadding(new UiRectangle(defaultPadding));
			entry.uiName.SetText(label);
			entry.uiName.SetFlags(UiElement.TRANSPARENT);
			uiColumnNames.AddElement(entry.uiName);

			entry.uiIndex.SetText(label);
			entry.uiIndex.SetPadding(new UiRectangle(defaultPadding));
			entry.uiIndex.SetProperties(UiElement.CANFOCUS);
			entry.uiIndex.SetFlags(UiElement.TRANSPARENT);
			uiColumnIndex.AddElement(entry.uiIndex);

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

			

			entry.GetIndexMax = GetMaxSpawnIndex;
			entry.GetIndex = GetSpawnIndex;
			entry.SetIndex = SetSpawnIndex;
			entry.GetName = GetSpawnName;

			return entry;
		}

		public int GetMaxSpawnIndex()
		{
			return spawnPositions.Count - 1;
		}
		public int GetSpawnIndex()
		{
			return spawnIndex;
		}

		public string GetSpawnName()
		{			
			return spawnPositions[spawnIndex].name;
		}

		public void SetSpawnIndex(int index)
		{
			spawnIndex = index;
			contentFrame.Refresh();
		}

		private void Respawn()
		{
			Vector3 pos = spawnPositions[spawnIndex].position;
			RespawnPed(Game.PlayerPed.Handle, pos.X, pos.Y, pos.Z, 0f);

			Game.Player.CanControlCharacter = false;
			API.FreezePedCameraRotation(Game.PlayerPed.Handle); // Hud still open
		}

		public async Task SpawnPlayer(float x, float y, float z, float heading)
		{
			API.DoScreenFadeOut(500);

			while (API.IsScreenFadingOut())
			{
				await Task.Delay(1);
			}

			//await Game.Player.ChangeModel(GetHashKey(skin));
			//SetPedDefaultComponentVariation(GetPlayerPed(-1));
			API.RequestCollisionAtCoord(x, y, z);

			int ped = API.GetPlayerPed(-1);

			RespawnPed(ped, x, y, z, heading);

			while (!API.HasCollisionLoadedAroundEntity(ped))
			{
				await Task.Delay(1);
			}

			//ShutdownLoadingScreen();
			API.SetEntityCollision(ped, true, true);
			//FreezeEntityPosition(ped, false);
			//SetPlayerControl(PlayerId(), true, 0);



			API.DoScreenFadeIn(500);

			while (API.IsScreenFadingIn())
			{
				await Task.Delay(1);
			}



			//FreezePlayer(PlayerId(), false);

			//TriggerEvent("playerSpawned", PlayerId());

			//_spawnLock = false;
		}

		private static void RespawnPed(int ped_id, float x, float y, float z, float heading)
		{
			API.SetEntityCoordsNoOffset(ped_id, x, y, z, false, false, false);
			API.NetworkResurrectLocalPlayer(x, y, z, heading, true, true);
			API.ClearPedTasksImmediately(ped_id);
			API.ClearPlayerWantedLevel(Game.Player.Handle);
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
