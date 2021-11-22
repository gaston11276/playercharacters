using NFive.SDK.Core.Diagnostics;
using Gaston11276.SimpleUi;

namespace Gaston11276.Playercharacters.Client
{
	class UiEntryHair
	{
		ILogger Logger;
		
		public Textbox uiHairLabel;
		public Textbox uiHairIndex;
		public Textbox btnIndexDecrease;
		public Textbox btnIndexIncrease;

		public Textbox uiColorId;
		public Textbox btnColorIdDecrease;
		public Textbox btnColorIdIncrease;

		public Textbox uiSecondaryColorId;
		public Textbox btnSecondaryColorIdDecrease;
		public Textbox btnSecondaryColorIdIncrease;


		public delegate void SetHairIndex(int index);
		public SetHairIndex SetIndex;
		public delegate int GetHairIndex();
		public GetHairIndex GetIndex;
		public GetHairIndex GetNumberOfHairIndex;

		public delegate void SetHairColor(int colorId);
		public SetHairColor SetColor;
		public delegate int GetHairColor();
		public GetHairColor GetColor;
		public GetHairColor GetNumberOfHairColors;

		public delegate void SetSecondaryHairColor(int colorId);
		public SetSecondaryHairColor SetSecondaryColor;
		public delegate int GetSecondaryHairColor();
		public GetSecondaryHairColor GetSecondaryColor;


		public UiEntryHair()
		{
			uiHairLabel = new Textbox();
			uiHairIndex = new Textbox();
			btnIndexDecrease = new Textbox();
			btnIndexIncrease = new Textbox();

			uiColorId = new Textbox();
			btnColorIdDecrease = new Textbox();
			btnColorIdIncrease = new Textbox();

			uiSecondaryColorId = new Textbox();
			btnSecondaryColorIdDecrease = new Textbox();
			btnSecondaryColorIdIncrease = new Textbox();
		}

		public void SetLogger(ILogger Logger)
		{
			this.Logger = Logger;
		}

		public void SetUi()
		{
			int index = GetIndex();
			int indexMax = GetNumberOfHairIndex();
			uiHairIndex.SetText($"{index}/{indexMax}");

			int colorId = GetColor();
			int colorMax = GetNumberOfHairColors();
			uiColorId.SetText($"{colorId}/{colorMax}");

			int secondaryColorId = GetSecondaryColor();
			int secondaryColorMax = GetNumberOfHairColors();
			uiSecondaryColorId.SetText($"{secondaryColorId}/{secondaryColorMax}");
		}

		public void IncreaseIndex()
		{
			int index = GetIndex();
			int indexMax = GetNumberOfHairIndex();
			index++;

			if (index > indexMax)
			{
				index = indexMax;
			}

			uiHairIndex.SetText($"{index}/{indexMax}");
			SetIndex(index);
		}

		public void DecreaseIndex()
		{
			int index = GetIndex();
			int indexMax = GetNumberOfHairIndex();
			index--;

			if (index < 0)
			{
				index = 0;
			}

			uiHairIndex.SetText($"{index}/{indexMax}");
			SetIndex(index);
		}

		public void IncreaseColor()
		{
			int colorId = GetColor();
			int colorMax = GetNumberOfHairColors();
			colorId++;

			if (colorId > colorMax)
			{
				colorId = colorMax;
			}

			uiColorId.SetText($"{colorId}/{colorMax}");
			SetColor(colorId);
		}

		public void DecreaseColor()
		{
			int colorId = GetColor();
			int colorMax = GetNumberOfHairColors();
			colorId--;

			if (colorId < 0)
			{
				colorId = 0;
			}

			uiColorId.SetText($"{colorId}/{colorMax}");
			SetColor(colorId);
		}

		public void IncreaseSecondaryColor()
		{
			int colorId = GetSecondaryColor();
			int colorMax = GetNumberOfHairColors();
			colorId++;

			if (colorId > colorMax)
			{
				colorId = colorMax;
			}

			uiSecondaryColorId.SetText($"{colorId}/{colorMax}");
			SetSecondaryColor(colorId);
		}

		public void DecreaseSecondaryColor()
		{
			int colorId = GetSecondaryColor();
			int secondaryColorMax = GetNumberOfHairColors();
			colorId--;

			if (colorId < 0)
			{
				colorId = 0;
			}

			uiSecondaryColorId.SetText($"{colorId}/{secondaryColorMax}");
			SetSecondaryColor(colorId);
		}
	}
}
