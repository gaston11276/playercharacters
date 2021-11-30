using System.Threading.Tasks;
using Gaston11276.SimpleUi;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Client
{
	class UiEntrySpawnLocation: HudEntry
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

		public UiEntrySpawnLocation()
		{
			
		}

		public async Task SetUi()
		{
			uiIndex.SetText($"{GetIndex()}");
			await Delay(HudComponent.delayMs);
		}

		public void Increase()
		{
			int index = GetIndex();
			index++;

			if (index > GetIndexMax()-1)
			{
				index = GetIndexMax() - 1;
			}

			uiIndex.SetText($"{index}");
			SetIndex(index);
		}

		public void Decrease()
		{
			int index = GetIndex();
			index--;

			if (index < 01)
			{
				index = 0;
			}

			uiIndex.SetText($"{index}");
			SetIndex(index);
		}
	}
}
