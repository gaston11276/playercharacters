using CitizenFX.Core;
using CitizenFX.Core.Native;
using Gaston11276.SimpleUi;

namespace Gaston11276.Playercharacters.Client
{
	public class UiHeadBlendData : UiAppearance
	{
		// Columns
		UiPanel uiColumnLabels = new UiPanel();
		UiPanel uiColumnValues = new UiPanel();
		UiPanel uiColumnDecrease = new UiPanel();
		UiPanel uiColumnIncrease = new UiPanel();


		Textbox valueParent1 = new Textbox();
		Textbox valueParent2 = new Textbox();
		Textbox valueShapeMix = new Textbox();
		Textbox valueSkinMix = new Textbox();

		public UiHeadBlendData()
		{
			cameraMode = CameraMode.Face;
		}

		public override void CreateColumns()
		{
			UiPanel uiCenterPanel = new UiPanel();
			uiCenterPanel.SetPadding(new UiRectangle(defaultPadding));
			contentFrame.AddElement(uiCenterPanel);

			CreateColumn(uiCenterPanel, HGravity.Left, uiColumnLabels);
			CreateColumn(uiCenterPanel, HGravity.Right, uiColumnValues);
			CreateColumn(uiCenterPanel, HGravity.Center, uiColumnDecrease);
			CreateColumn(uiCenterPanel, HGravity.Center, uiColumnIncrease);
		}

		public override void CreateContent()
		{
			uiHeader.SetText("Head Blend Data");

			Textbox labelParent1 = new Textbox();
			labelParent1.SetText("Parent 1");
			labelParent1.SetPadding(new UiRectangle(defaultPadding));
			labelParent1.SetFlags(UiElement.NO_DRAW);
			uiColumnLabels.AddElement(labelParent1);

			valueParent1.SetText($"{0}/{45}");
			valueParent1.SetPadding(new UiRectangle(defaultPadding));
			valueParent1.SetFlags(UiElement.NO_DRAW);
			uiColumnValues.AddElement(valueParent1);

			Textbox btnShape1Decrease = new Textbox();
			btnShape1Decrease.SetText("-");
			btnShape1Decrease.SetPadding(new UiRectangle(defaultPadding));
			btnShape1Decrease.SetProperties(UiElement.CANFOCUS);
			btnShape1Decrease.RegisterOnLMBRelease(DecreaseParent1);
			inputsOnMouseMove.Add(btnShape1Decrease.OnCursorMove);
			inputsOnLeftMouseButton.Add(btnShape1Decrease.OnLeftMouseButton);
			uiColumnDecrease.AddElement(btnShape1Decrease);

			Textbox btnShape1Increase = new Textbox();
			btnShape1Increase.SetText("+");
			btnShape1Increase.SetPadding(new UiRectangle(defaultPadding));
			btnShape1Increase.SetProperties(UiElement.CANFOCUS);
			btnShape1Increase.RegisterOnLMBRelease(IncreaseParent1);
			inputsOnMouseMove.Add(btnShape1Increase.OnCursorMove);
			inputsOnLeftMouseButton.Add(btnShape1Increase.OnLeftMouseButton);
			uiColumnIncrease.AddElement(btnShape1Increase);

			Textbox labelParent2 = new Textbox();
			labelParent2.SetText("Parent 2");
			labelParent2.SetPadding(new UiRectangle(defaultPadding));
			labelParent2.SetFlags(UiElement.NO_DRAW);
			uiColumnLabels.AddElement(labelParent2);

			valueParent2.SetText($"{0}/{45}");
			valueParent2.SetPadding(new UiRectangle(defaultPadding));
			valueParent2.SetFlags(UiElement.NO_DRAW);
			uiColumnValues.AddElement(valueParent2);

			Textbox btnShape2Decrease = new Textbox();
			btnShape2Decrease.SetText("-");
			btnShape2Decrease.SetPadding(new UiRectangle(defaultPadding));
			btnShape2Decrease.SetProperties(UiElement.CANFOCUS);
			btnShape2Decrease.RegisterOnLMBRelease(DecreaseParent2);
			inputsOnMouseMove.Add(btnShape2Decrease.OnCursorMove);
			inputsOnLeftMouseButton.Add(btnShape2Decrease.OnLeftMouseButton);
			uiColumnDecrease.AddElement(btnShape2Decrease);

			Textbox btnShape2Increase = new Textbox();
			btnShape2Increase.SetText("+");
			btnShape2Increase.SetPadding(new UiRectangle(defaultPadding));
			btnShape2Increase.SetProperties(UiElement.CANFOCUS);
			btnShape2Increase.RegisterOnLMBRelease(IncreaseParent2);
			inputsOnMouseMove.Add(btnShape2Increase.OnCursorMove);
			inputsOnLeftMouseButton.Add(btnShape2Increase.OnLeftMouseButton);
			uiColumnIncrease.AddElement(btnShape2Increase);

			Textbox labelShapeMix = new Textbox();
			labelShapeMix.SetText("Shape Mix");
			labelShapeMix.SetPadding(new UiRectangle(defaultPadding));
			labelShapeMix.SetFlags(UiElement.NO_DRAW);
			uiColumnLabels.AddElement(labelShapeMix);

			valueShapeMix.SetText($"{string.Format("{0:0.0#}", 0.0f)}");
			valueShapeMix.SetPadding(new UiRectangle(defaultPadding));
			valueShapeMix.SetFlags(UiElement.NO_DRAW);
			uiColumnValues.AddElement(valueShapeMix);

			Textbox btnShapeMixDecrease = new Textbox();
			btnShapeMixDecrease.SetText("-");
			btnShapeMixDecrease.SetPadding(new UiRectangle(defaultPadding));
			btnShapeMixDecrease.SetProperties(UiElement.CANFOCUS);
			btnShapeMixDecrease.RegisterOnLMBRelease(DecreaseShapeMix);
			inputsOnMouseMove.Add(btnShapeMixDecrease.OnCursorMove);
			inputsOnLeftMouseButton.Add(btnShapeMixDecrease.OnLeftMouseButton);
			uiColumnDecrease.AddElement(btnShapeMixDecrease);

			Textbox btnShapeMixIncrease = new Textbox();
			btnShapeMixIncrease.SetText("+");
			btnShapeMixIncrease.SetPadding(new UiRectangle(defaultPadding));
			btnShapeMixIncrease.SetProperties(UiElement.CANFOCUS);
			btnShapeMixIncrease.RegisterOnLMBRelease(IncreaseShapeMix);
			inputsOnMouseMove.Add(btnShapeMixIncrease.OnCursorMove);
			inputsOnLeftMouseButton.Add(btnShapeMixIncrease.OnLeftMouseButton);
			uiColumnIncrease.AddElement(btnShapeMixIncrease);

			Textbox labelSkinMix = new Textbox();
			labelSkinMix.SetText("Skin Mix");
			labelSkinMix.SetPadding(new UiRectangle(defaultPadding));
			labelSkinMix.SetFlags(UiElement.NO_DRAW);
			uiColumnLabels.AddElement(labelSkinMix);

			valueSkinMix.SetText($"{string.Format("{0:0.0#}", 0.0f)}");
			valueSkinMix.SetPadding(new UiRectangle(defaultPadding));
			valueSkinMix.SetFlags(UiElement.NO_DRAW);
			uiColumnValues.AddElement(valueSkinMix);

			Textbox btnSkinMixDecrease = new Textbox();
			btnSkinMixDecrease.SetText("-");
			btnSkinMixDecrease.SetPadding(new UiRectangle(defaultPadding));
			btnSkinMixDecrease.SetProperties(UiElement.CANFOCUS);
			btnSkinMixDecrease.RegisterOnLMBRelease(DecreaseSkinMix);
			inputsOnMouseMove.Add(btnSkinMixDecrease.OnCursorMove);
			inputsOnLeftMouseButton.Add(btnSkinMixDecrease.OnLeftMouseButton);
			uiColumnDecrease.AddElement(btnSkinMixDecrease);

			Textbox btnSkinMixIncrease = new Textbox();
			btnSkinMixIncrease.SetText("+");
			btnSkinMixIncrease.SetPadding(new UiRectangle(defaultPadding));
			btnSkinMixIncrease.SetProperties(UiElement.CANFOCUS);
			btnSkinMixIncrease.RegisterOnLMBRelease(IncreaseSkinMix);
			inputsOnMouseMove.Add(btnSkinMixIncrease.OnCursorMove);
			inputsOnLeftMouseButton.Add(btnSkinMixIncrease.OnLeftMouseButton);
			uiColumnIncrease.AddElement(btnSkinMixIncrease);
		}

		public override void SetUi()
		{
			valueParent1.SetText($"{character.PedHeadBlendData.Parent1}/{45}");
			valueParent2.SetText($"{character.PedHeadBlendData.Parent2}/{45}");
			valueShapeMix.SetText($"{string.Format("{0:0.0#}", character.PedHeadBlendData.ShapeMix)}");
			valueSkinMix.SetText($"{string.Format("{0:0.0#}", character.PedHeadBlendData.SkinMix)}");
		}

		private void IncreaseParent1()
		{
			if (character.PedHeadBlendData.Parent1 < 44)
			{
				character.PedHeadBlendData.Parent1++;
				valueParent1.SetText($"{character.PedHeadBlendData.Parent1}/{45}");
				ApplyToPed();
			}
		}
		private void DecreaseParent1()
		{
			if (character.PedHeadBlendData.Parent1 > 0)
			{
				character.PedHeadBlendData.Parent1--;

				valueParent1.SetText($"{character.PedHeadBlendData.Parent1}/{45}");
				ApplyToPed();
			}
		}
		private void IncreaseParent2()
		{
			if (character.PedHeadBlendData.Parent2 < 44)
			{
				character.PedHeadBlendData.Parent2++;
				valueParent2.SetText($"{character.PedHeadBlendData.Parent2}/{45}");
				ApplyToPed();
			}
		}
		private void DecreaseParent2()
		{
			if (character.PedHeadBlendData.Parent2 > 0)
			{
				character.PedHeadBlendData.Parent2--;

				valueParent2.SetText($"{character.PedHeadBlendData.Parent2}/{45}");
				ApplyToPed();
			}
		}
		private void IncreaseShapeMix()
		{
			if (character.PedHeadBlendData.ShapeMix < 1f)
			{
				character.PedHeadBlendData.ShapeMix += 0.1f;
				valueShapeMix.SetText($"{string.Format("{0:0.0#}", character.PedHeadBlendData.ShapeMix)}");
				ApplyToPed();
			}
		}
		private void DecreaseShapeMix()
		{
			if (character.PedHeadBlendData.ShapeMix > 0)
			{
				character.PedHeadBlendData.ShapeMix -= 0.1f;

				valueShapeMix.SetText($"{string.Format("{0:0.0#}", character.PedHeadBlendData.ShapeMix)}");
				ApplyToPed();
			}
		}
		private void IncreaseSkinMix()
		{
			if (character.PedHeadBlendData.SkinMix < 1f)
			{
				character.PedHeadBlendData.SkinMix += 0.1f;
				valueSkinMix.SetText($"{string.Format("{0:0.0#}", character.PedHeadBlendData.SkinMix)}");
				UpdatePed();
			}
		}
		private void DecreaseSkinMix()
		{
			if (character.PedHeadBlendData.SkinMix > 0)
			{
				character.PedHeadBlendData.SkinMix -= 0.1f;

				valueSkinMix.SetText($"{string.Format("{0:0.0#}", character.PedHeadBlendData.SkinMix)}");
				UpdatePed();
			}
		}

		public void ApplyToPed()
		{
			API.SetPedHeadBlendData(Game.PlayerPed.Handle,
									character.PedHeadBlendData.Parent1,
									character.PedHeadBlendData.Parent2,
									0,
									character.PedHeadBlendData.Parent1,
									character.PedHeadBlendData.Parent2,
									0,
									character.PedHeadBlendData.ShapeMix,
									character.PedHeadBlendData.SkinMix,
									0f,
									false);
		}

		private void UpdatePed()
		{
			API.UpdatePedHeadBlendData(Game.PlayerPed.Handle,
									character.PedHeadBlendData.ShapeMix,
									character.PedHeadBlendData.SkinMix,
									0f);
		}
	}
}
