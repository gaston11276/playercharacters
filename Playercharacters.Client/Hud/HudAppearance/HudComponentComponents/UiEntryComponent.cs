using System.Threading.Tasks;
using Gaston11276.SimpleUi;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Client
{
	class UiEntryComponent : HudEntry
	{
		public PedComponentType type;
		int index;

		public Textbox uiComponentLabel;
		public Textbox uiComponentIndex;
		public Textbox btnComponentDecrease;
		public Textbox btnComponentIncrease;

		public Textbox uiTextureId;
		public Textbox btnTextureDecrease;
		public Textbox btnTextureIncrease;

		public delegate void SetComponentIndex(PedComponentType type, int index);
		public SetComponentIndex SetIndex;
		public delegate int GetComponentIndex(PedComponentType type);
		public GetComponentIndex GetIndex;
		public GetComponentIndex GetIndexMax;

		public delegate void SetComponentTexture(PedComponentType type, int textureId);
		public SetComponentTexture SetTexture;
		public delegate int GetComponentTexture(PedComponentType type);
		public GetComponentTexture GetTexture;
		public delegate int GetComponentTextureMax(PedComponentType type, int index);
		public GetComponentTextureMax GetTextureMax;

		public UiEntryComponent()
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

		public void Hide()
		{
			uiComponentLabel.SetFlags(UiElement.HIDDEN);
			uiComponentIndex.SetFlags(UiElement.HIDDEN);
			btnComponentDecrease.SetFlags(UiElement.HIDDEN);
			btnComponentIncrease.SetFlags(UiElement.HIDDEN);
			uiTextureId.SetFlags(UiElement.HIDDEN);
			btnTextureDecrease.SetFlags(UiElement.HIDDEN);
			btnTextureIncrease.SetFlags(UiElement.HIDDEN);
		}

		public void Show()
		{
			uiComponentLabel.ClearFlags(UiElement.HIDDEN);
			uiComponentIndex.ClearFlags(UiElement.HIDDEN);
			btnComponentDecrease.ClearFlags(UiElement.HIDDEN);
			btnComponentIncrease.ClearFlags(UiElement.HIDDEN);
			uiTextureId.ClearFlags(UiElement.HIDDEN);
			btnTextureDecrease.ClearFlags(UiElement.HIDDEN);
			btnTextureIncrease.ClearFlags(UiElement.HIDDEN);
		}

		public async Task SetUi()
		{
			index = GetIndex(type);
			int indexMax = GetIndexMax(type);
			uiComponentIndex.SetText($"{index+1}/{indexMax}");

			int textureId = GetTexture(type);
			int textureIdMax = GetTextureMax(type, index);
			uiTextureId.SetText($"{textureId+1}/{textureIdMax}");
			await Delay(HudComponent.delayMs);
		}

		public void IncreaseIndex()
		{
			int index = GetIndex(type);
			int indexMax = GetIndexMax(type);
			index++;

			if (index > indexMax-1)
			{
				index = indexMax-1;
			}

			uiComponentIndex.SetText($"{index+1}/{indexMax}");
			SetIndex(type, index);

			int textureId = 0;
			int textureIdMax = GetTextureMax(type, index);
			uiTextureId.SetText($"{textureId+1}/{textureIdMax}");
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

			uiComponentIndex.SetText($"{index+1}/{indexMax}");
			SetIndex(type, index);

			int textureId = 0;
			int textureIdMax = GetTextureMax(type, index);
			uiTextureId.SetText($"{textureId+1}/{textureIdMax}");
		}

		public void IncreaseTexture()
		{
			int textureId = GetTexture(type);
			int textureMax = GetTextureMax(type, index);
			textureId++;

			if (textureId > textureMax-1)
			{
				textureId = textureMax-1;
			}

			uiTextureId.SetText($"{textureId+1}/{textureMax}");
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

			uiTextureId.SetText($"{textureId+1}/{textureMax}");
			SetTexture(type, textureId);
		}
	}
}
