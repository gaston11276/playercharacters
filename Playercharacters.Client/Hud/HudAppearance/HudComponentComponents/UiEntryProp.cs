using System.Threading.Tasks;
using Gaston11276.SimpleUi;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Client
{
	class UiEntryProp : HudEntry
	{
		public PedPropType type;
		int index;

		public Textbox uiComponentLabel;
		public Textbox uiComponentIndex;
		public Textbox btnComponentDecrease;
		public Textbox btnComponentIncrease;

		public Textbox uiTextureId;
		public Textbox btnTextureDecrease;
		public Textbox btnTextureIncrease;


		public delegate void SetComponentIndex(PedPropType type, int index);
		public SetComponentIndex SetIndex;
		public delegate int GetComponentIndex(PedPropType type);
		public GetComponentIndex GetIndex;
		public GetComponentIndex GetIndexMax;

		public delegate void SetComponentTexture(PedPropType type, int textureId);
		public SetComponentTexture SetTexture;
		public delegate int GetComponentTexture(PedPropType type);
		public GetComponentTexture GetTexture;
		public delegate int GetComponentTextureMax(PedPropType type, int index);
		public GetComponentTextureMax GetTextureMax;

		public delegate void fpDettachProp(PedPropType type);
		public fpDettachProp DettachProp;

		public UiEntryProp()
		{
			type = 0;
			index = 0;

			uiComponentLabel = new Textbox();
			uiComponentIndex = new Textbox();

			btnComponentDecrease = new Textbox();
			btnComponentIncrease = new Textbox();

			uiTextureId = new Textbox();
			btnTextureDecrease = new Textbox();
			btnTextureIncrease = new Textbox();
		}

		public async Task SetUi()
		{
			index = GetIndex(type);
			int indexMax = GetIndexMax(type);
			uiComponentIndex.SetText($"{index}/{indexMax}");

			int textureId = GetTexture(type);
			int textureIdMax = GetTextureMax(type, index);
			uiTextureId.SetText($"{textureId}/{textureIdMax}");
			await Delay(HudComponent.delayMs);
		}

		public void IncreaseIndex()
		{
			index = GetIndex(type);
			int indexMax = GetIndexMax(type);
			index++;

			if (index > indexMax)
			{
				index = indexMax;
			}

			uiComponentIndex.SetText($"{index}/{indexMax}");
			SetIndex(type, index);

			int textureId = 0;
			int textureIdMax = GetTextureMax(type, index);
			uiTextureId.SetText($"{textureId}/{textureIdMax}");
		}

		public void DecreaseIndex()
		{
			index = GetIndex(type);
			int indexMax = GetIndexMax(type);
			index--;

			if (index < 0)
			{
				index = 0;
				DettachProp(type);
			}

			uiComponentIndex.SetText($"{index}/{indexMax}");
			SetIndex(type, index);

			int textureId = 0;
			int textureIdMax = GetTextureMax(type, index);
			uiTextureId.SetText($"{textureId}/{textureIdMax}");
		}

		public void IncreaseTexture()
		{
			int textureId = GetTexture(type);
			int textureMax = GetTextureMax(type, index);
			textureId++;

			if (textureId > textureMax)
			{
				textureId = textureMax;
			}

			uiTextureId.SetText($"{textureId}/{textureMax}");
			SetTexture(type, textureId);
		}

		public void DecreaseTexture()
		{
			int textureId = GetTexture(type);
			int textureMax = GetTextureMax(type, index);
			textureId--;

			if (textureId < 0)
			{
				textureId = 0;
			}

			uiTextureId.SetText($"{textureId}/{textureMax}");
			SetTexture(type, textureId);
		}
	}
}
