using System.Threading.Tasks;
using Gaston11276.SimpleUi;

namespace Gaston11276.Playercharacters.Client
{
	class UiEntryEyeColor : HudEntry
	{
		public Textbox uiEyeColorLabel = new Textbox();
		public Textbox uiEyeColorIndex = new Textbox();
		public Textbox btnIndexDecrease = new Textbox();
		public Textbox btnIndexIncrease = new Textbox();

		public delegate void SetEyeColor(int index);
		public SetEyeColor SetColor;
		public delegate int GetEyeColor();
		public GetEyeColor GetColor;
		public GetEyeColor GetNumberOfEyeColors;
		public UiEntryEyeColor()
		{
		}
		public async Task SetUi()
		{
			int index = GetColor();
			int indexMax = GetNumberOfEyeColors();
			uiEyeColorIndex.SetText($"{index}/{indexMax}");
			await Delay(HudComponent.delayMs);
		}

		public void IncreaseIndex()
		{
			int index = GetColor();
			int indexMax = GetNumberOfEyeColors();
			index++;

			if (index > indexMax)
			{
				index = indexMax;
			}

			uiEyeColorIndex.SetText($"{index}/{indexMax}");
			SetColor(index);
		}

		public void DecreaseIndex()
		{
			int index = GetColor();
			int indexMax = GetNumberOfEyeColors();
			index--;

			if (index < 0)
			{
				index = 0;
			}

			uiEyeColorIndex.SetText($"{index}/{indexMax}");
			SetColor(index);
		}
	}
}
