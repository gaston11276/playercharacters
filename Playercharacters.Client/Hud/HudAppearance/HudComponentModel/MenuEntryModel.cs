using System.Threading.Tasks;
using Gaston11276.SimpleUi;

namespace Gaston11276.Playercharacters.Client
{
	public class MenuEntryModel : HudEntry
	{
		public Textbox uiLabel = new Textbox();
		public Textbox uiName = new Textbox();
		public Textbox uiIndex = new Textbox();
		public Textbox btnIndexDecrease = new Textbox();
		public Textbox btnIndexIncrease = new Textbox();

		public delegate void fpSetInt(int index);
		public fpSetInt SetIndex;
		public delegate int fpGetInt();
		public fpGetInt GetIndex;
		public fpGetInt GetIndexMax;

		public delegate string fpGetString();
		public fpGetString GetName;

		public MenuEntryModel()
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
			index++;
			if (index > GetIndexMax())
			{
				index = GetIndexMax();
			}
			SetIndex(index);
			uiIndex.SetText($"{index}/{GetIndexMax()}");
			uiName.SetText($"{GetName()}");
		}

		public void DecreaseIndex()
		{
			int index = GetIndex();
			index--;
			if (index < 0)
			{
				index = 0;
			}
			SetIndex(index);
			uiIndex.SetText($"{index}/{GetIndexMax()}");
			uiName.SetText($"{GetName()}");
		}
	}
}
