using NFive.SDK.Core.Diagnostics;
using Gaston11276.SimpleUi;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Client
{
	class UiEntrySpawnPosition
	{
		ILogger Logger;

		public Textbox uiLabel = new Textbox();
		public Textbox uiSpawnName = new Textbox();
		public Textbox uiSpawnIndex = new Textbox();
		public Textbox btnIndexDecrease = new Textbox();
		public Textbox btnIndexIncrease = new Textbox();

		
		public delegate void fpSetInt(int index);
		public fpSetInt SetIndex;
		public delegate int fpGetInt();
		public fpGetInt GetIndex;
		public fpGetInt GetIndexMax;

		public delegate string fpGetString();
		public fpGetString GetName;

		public UiEntrySpawnPosition()
		{
			
		}

		public void SetLogger(ILogger Logger)
		{
			this.Logger = Logger;
		}

		public void SetUi()
		{
			uiSpawnIndex.SetText($"{GetIndex()}/{GetIndexMax()}");
			uiSpawnName.SetText($"{GetName()}");
		}

		public void IncreaseIndex()
		{
			int index = GetIndex();
			int indexMax = GetIndexMax();
			index++;

			if (index > indexMax)
			{
				index = indexMax;
			}

			uiSpawnIndex.SetText($"{index}/{indexMax}");
			SetIndex(index);

			string spawnName = GetName();
			uiSpawnName.SetText($"{spawnName}");
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

			uiSpawnIndex.SetText($"{index}/{indexMax}");
			SetIndex(index);

			string spawnName = GetName();
			uiSpawnName.SetText($"{spawnName}");
		}
	}
}
