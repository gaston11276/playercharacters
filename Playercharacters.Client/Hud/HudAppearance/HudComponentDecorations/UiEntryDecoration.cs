using System.Threading.Tasks;
using Gaston11276.SimpleUi;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Client
{
	class UiEntryDecoration : HudEntry
	{
		public DecorationZone zone;

		public Textbox uiZoneLabel = new Textbox();
		public Textbox uiTattooIndex = new Textbox();
		public Textbox uiTattooName = new Textbox();
		public Textbox btnDecrease = new Textbox();
		public Textbox btnIncrease = new Textbox();

		public delegate void fpSetTattooIndex(DecorationZone zone, int index);
		public fpSetTattooIndex SetIndex;

		public delegate int fpGetTattooIndex(DecorationZone zone);
		public fpGetTattooIndex GetIndex;
		public fpGetTattooIndex GetIndexMax;

		public delegate void fpSetCamera(DecorationZone zone);
		public fpSetCamera SetCamera;

		public delegate string fpGetName(DecorationZone zone, int index);
		public fpGetName GetName;

		public UiEntryDecoration()
		{ }

		public async Task SetUi()
		{
			int index = GetIndex(zone);
			int indexMax = GetIndexMax(zone) -1;
			uiTattooIndex.SetText($"{index + 1}/{indexMax + 1}");
			uiTattooName.SetText($"{GetName(zone, index)}");
			await Delay(HudComponent.delayMs);
		}

		public void Increase()
		{
			int index = GetIndex(zone);
			int indexMax = GetIndexMax(zone) -1;
			index++;

			if (index > indexMax)
			{
				index = indexMax;
			}

			SetCamera(zone);
			uiTattooIndex.SetText($"{index + 1}/{indexMax + 1}");
			SetIndex(zone, index);

			uiTattooName.SetText($"{GetName(zone, index)}");
		}

		public void Decrease()
		{
			int index = GetIndex(zone);
			int indexMax = GetIndexMax(zone) -1;
			index--;

			if (index < -1)
			{
				index = -1;
			}

			SetCamera(zone);
			uiTattooIndex.SetText($"{index + 1}/{indexMax + 1}");
			SetIndex(zone, index);

			uiTattooName.SetText($"{GetName(zone, index)}");
		}
	}
}
