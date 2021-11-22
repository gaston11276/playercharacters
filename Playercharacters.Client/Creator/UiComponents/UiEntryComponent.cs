using NFive.SDK.Core.Diagnostics;
using Gaston11276.SimpleUi;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Client
{
	class UiEntryComponent
	{
		ILogger Logger;

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

		public void SetLogger(ILogger Logger)
		{
			this.Logger = Logger;
		}

		public void SetUi()
		{
			index = GetIndex(type);
			int indexMax = GetIndexMax(type);
			uiComponentIndex.SetText($"{index}/{indexMax}");

			int textureId = GetTexture(type);
			int textureIdMax = GetTextureMax(type, index);
			uiTextureId.SetText($"{textureId}/{textureIdMax}");
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

			uiComponentIndex.SetText($"{index}/{indexMax}");
			SetIndex(type, index);

			int textureId = GetTexture(type);
			int textureIdMax = GetTextureMax(type, index);
			uiTextureId.SetText($"{textureId}/{textureIdMax}");
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

			uiComponentIndex.SetText($"{index}/{indexMax}");
			SetIndex(type, index);

			int textureId = GetTexture(type);
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
