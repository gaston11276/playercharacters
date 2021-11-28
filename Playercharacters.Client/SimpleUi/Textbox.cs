using System.Collections.Generic;
using static CitizenFX.Core.Native.API;

namespace Gaston11276.SimpleUi
{
	public class Textbox : UiPanel
	{		
		Argb textColor;

		protected int textFlags;
		protected string text;
		private float textWidth;
		private float textHeight;
		private bool needRefresh;

		List<fpVoid> callbacksOnTextChanged;

		public Textbox()
		{
			Type = UiElementType.Textbox;
			textFlags = 0;
			textColor = new Argb(0xFFFFFFFF);
			text = "";
			needRefresh = true;

			callbacksOnTextChanged = new List<fpVoid>();
		}

		public void SetTextFlags(int flags)
		{
			textFlags |= flags;
		}

		public void ClearTextFlags(int flags)
		{
			textFlags &= ~flags;
		}

		public void RegisterOnTextChanged(fpVoid OnTextChanged)
		{
			callbacksOnTextChanged.Add(OnTextChanged);
		}

		private void RunOnTextChangedCallbacks()
		{
			foreach (fpVoid OnTextChanged in callbacksOnTextChanged)
			{
				OnTextChanged();
			}
		}

		protected void TextChanged()
		{
			needRefresh = true;
			Refresh();
			Redraw();
			RunOnTextChangedCallbacks();
		}
		
		public void SetFont(string fontName, int fontSize)
		{
			SetTextFont(0);
			TextChanged();
		}

		public override void SetText(string text)
		{
			this.text = text;
			TextChanged();
		}

		public string GetText()
		{
			return this.text;
		}

		public void ClearText()
		{
			this.text = "";
			TextChanged();
		}


		public void SetTextColor(int ARGB)
		{
			textColor.SetARGB(ARGB);
		}

		protected override float GetContentWidth()
		{
			BeginTextCommandWidth("STRING");
			AddTextComponentString(this.text);
			textWidth = EndTextCommandGetWidth(false) * 0.3f;
			return textWidth;
		}
		protected override float GetContentHeight()
		{
			textHeight = 0.3f * 0.03f;
			return textHeight;
		}

		public override void Draw()
		{
			base.Draw();

			if ((textFlags & TRANSPARENT) != 0)
			{
				return;
			}

			float textX = 0f;
			if (hGravity == HGravity.Left)
			{
				textX = drawingRectangle.Left() - (Padding.Left() * screenBoundaries.Width());
			}
			else if (hGravity == HGravity.Center)
			{
				textX = drawingRectangle.CenterX();
			}
			else if (hGravity == HGravity.Right)
			{
				textX = drawingRectangle.Right() - (Padding.Right() * screenBoundaries.Width());
			}

			BeginTextCommandDisplayText("STRING");
			SetTextScale(1f, 0.3f);

			float y_adjust = -0.012f;
			float text_x = textX;
			float text_y = drawingRectangle.CenterY() + y_adjust;

			if (this.hGravity == HGravity.Left)
			{
				SetTextJustification(1);//0 center, 1 left, 2 right
			}
			else if (this.hGravity == HGravity.Center)
			{
				SetTextJustification(0);
			}
			else if (this.hGravity == HGravity.Right)
			{
				SetTextJustification(2);
			}
			
			SetTextColour(255, 255, 255, 255);
			AddTextComponentString(this.text);
			EndTextCommandDisplayText(text_x, text_y);
			//ResetScriptGfxAlign();


			if (this.needRefresh)
			{
				needRefresh = false;
			}
		}
	}
}
