using System.Threading.Tasks;
using Gaston11276.SimpleUi;

namespace Gaston11276.Playercharacters.Client
{
	class UiEntrySpawnPosition : HudEntry
	{
		public Textbox uiLabel = new Textbox();
		public Textbox uiName = new Textbox();
		public Textbox uiIndex = new Textbox();
		public Textbox btnDecrease = new Textbox();
		public Textbox btnIncrease = new Textbox();

		
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

		public async Task SetUi()
		{
			uiIndex.SetText($"{GetIndex()}/{GetIndexMax()}");
			uiName.SetText($"{GetName()}");
			await Delay(HudComponent.delayMs);
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

			uiIndex.SetText($"{index}/{indexMax}");
			SetIndex(index);

			string spawnName = GetName();
			uiName.SetText($"{spawnName}");
			
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

			string spawnName = GetName();
			uiName.SetText($"{spawnName}");
		}
	}
}
