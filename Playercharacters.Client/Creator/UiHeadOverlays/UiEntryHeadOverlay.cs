using NFive.SDK.Core.Diagnostics;
using Gaston11276.SimpleUi;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Client
{
	class UiEntryHeadOverlay
	{
		ILogger Logger;

		public PedHeadOverlayType type;
		public Textbox uiOverlayLabel;
		public Textbox uiOverlayIndex;
		public Textbox btnIndexDecrease;
		public Textbox btnIndexIncrease;

		public Textbox uiColorId;
		public Textbox btnColorIdDecrease;
		public Textbox btnColorIdIncrease;

		public Textbox uiOpacity;
		public Textbox btnOpacityDecrease;
		public Textbox btnOpacityIncrease;

		public delegate void SetHeadOverlayIndex(PedHeadOverlayType type, int index);
		public SetHeadOverlayIndex SetIndex;
		public delegate int GetHeadOverlayIndex(PedHeadOverlayType type);
		public GetHeadOverlayIndex GetIndex;
		public GetHeadOverlayIndex GetIndexMax;

		public delegate void SetHeadOverlayColor(PedHeadOverlayType type, int colorId);
		public SetHeadOverlayColor SetColor;
		public delegate int GetHeadOverlayColor(PedHeadOverlayType type);
		public GetHeadOverlayColor GetColor;
		public GetHeadOverlayColor GetColorMax;

		public delegate void SetHeadOverlayOpacity(PedHeadOverlayType type, float opacity);
		public SetHeadOverlayOpacity SetOpacity;
		public delegate float GetHeadOverlayOpacity(PedHeadOverlayType type);
		public GetHeadOverlayOpacity GetOpacity;

		public UiEntryHeadOverlay()
		{
			type = 0;

			uiOverlayLabel = new Textbox();
			uiOverlayIndex = new Textbox();
			btnIndexDecrease = new Textbox();
			btnIndexIncrease = new Textbox();

			uiColorId = new Textbox();
			btnColorIdDecrease = new Textbox();
			btnColorIdIncrease = new Textbox();

			uiOpacity = new Textbox();
			btnOpacityDecrease = new Textbox();
			btnOpacityIncrease = new Textbox();
		}

		public void SetLogger(ILogger Logger)
		{
			this.Logger = Logger;
		}

		public void SetUi()
		{
			int index = GetIndex(type);
			int indexMax = GetIndexMax(type);
			uiOverlayIndex.SetText($"{index}/{indexMax}");

			int colorId = GetColor(type);
			int colorMax = GetColorMax(type);
			uiColorId.SetText($"{colorId}/{colorMax}");

			float opacity = GetOpacity(type);
			uiOpacity.SetText($"{string.Format("{0:0.0#}", opacity)}");
		}

		public void IncreaseIndex()
		{
			int index = GetIndex(type);
			int indexMax = GetIndexMax(type);
			index++;

			if (index > indexMax)
			{
				index = indexMax;
			}

			uiOverlayIndex.SetText($"{index}/{indexMax}");
			SetIndex(type, index);
		}

		public void DecreaseIndex()
		{
			int index = GetIndex(type);
			int indexMax = GetIndexMax(type);
			index--;

			if (index < 0)
			{
				index = 0;
			}

			uiOverlayIndex.SetText($"{index}/{indexMax}");
			SetIndex(type, index);
		}

		public void IncreaseColor()
		{
			int colorId = GetColor(type);
			int colorMax = GetColorMax(type);
			colorId++;

			if (colorId > colorMax)
			{
				colorId = colorMax;
			}

			uiColorId.SetText($"{colorId}/{colorMax}");
			SetColor(type, colorId);
		}

		public void DecreaseColor()
		{
			int colorId = GetColor(type);
			int colorMax = GetColorMax(type);
			colorId--;

			if (colorId < 0)
			{
				colorId = 0;
			}

			uiColorId.SetText($"{colorId}/{colorMax}");
			SetColor(type, colorId);
		}

		public void IncreaseOpacity()
		{
			float opacity = GetOpacity(type);
			opacity += 0.1f;

			if (opacity > 1f)
			{
				opacity = 1f;
			}

			uiOpacity.SetText($"{string.Format("{0:0.0#}", opacity)}");
			SetOpacity(type, opacity);
		}

		public void DecreaseOpacity()
		{
			float opacity = GetOpacity(type);
			opacity -= 0.1f;

			if (opacity < 0f)
			{
				opacity = 0f;
			}

			uiOpacity.SetText($"{string.Format("{0:0.0#}", opacity)}");
			SetOpacity(type, opacity);
		}
	}
}
