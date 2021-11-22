using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.UI;

using Gaston11276.SimpleUi;

namespace Gaston11276.SimpleUi
{
	public class Textbox : UiPanel//UiElement//, IUiElement
	{
		
		Argb textColor;


		//private float scale;
		protected int textFlags;

		protected string text;
		private float textWidth;
		private float textHeight;
		private bool needRefresh;

		public delegate void OnTextChanged();
		List<OnTextChanged> callbacksOnTextChanged;

		// https://docs.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-obtain-font-metrics?view=netframeworkdesktop-4.8
		//FontFamily fontFamily;
		//Font font;
		//SolidBrush fontBrush;
		//StringFormat stringFormat;

		public Textbox()
		{
			Type = UiElementType.Textbox;
			textFlags = 0;
			textColor = new Argb(0xFFFFFFFF);
			text = "";
			needRefresh = true;

			callbacksOnTextChanged = new List<OnTextChanged>();

			CalculateTextWidth();
			CalculateTextHeight();

			// API Specific
			//fontFamily = new FontFamily("Arial");
			//font = new Font(fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);
			//fontBrush = new SolidBrush(Color.FromArgb(textColor.GetARGB()));
			//stringFormat = new StringFormat();
			//stringFormat.Alignment = StringAlignment.Center;
			//stringFormat.LineAlignment = StringAlignment.Center;
		}

		public void SetTextFlags(int flags)
		{
			textFlags |= flags;
		}

		public void ClearTextFlags(int flags)
		{
			textFlags &= ~flags;
		}

		protected void CalculateTextWidth()
		{

		}

		protected void CalculateTextHeight()
		{
			//textHeight = font.Height / screenResolutionY;
		}

		public void RegisterOnTextChanged(OnTextChanged OnTextChanged)
		{
			callbacksOnTextChanged.Add(OnTextChanged);
		}

		private void RunOnTextChangedCallbacks()
		{
			foreach (OnTextChanged OnTextChanged in callbacksOnTextChanged)
			{
				OnTextChanged();
			}
		}

		protected void TextChanged()
		{
			needRefresh = true;
			Build();
			Redraw();
			RunOnTextChangedCallbacks();
		}

		public override void SetAlignment(HAlignment hAlignment)
		{
			base.SetAlignment(hAlignment);

			// API specific
			if (this.hAlignment == HAlignment.Left)
			{
				//stringFormat.Alignment = StringAlignment.Near;
			}
			else if (this.hAlignment == HAlignment.Center)
			{
				//stringFormat.Alignment = StringAlignment.Center;
			}
			else if (this.hAlignment == HAlignment.Right)
			{
				//stringFormat.Alignment = StringAlignment.Far;
			}
		}
		public override void SetAlignment(VAlignment vAlignment)
		{
			base.SetAlignment(vAlignment);

			// API specific
			if (this.vAlignment == VAlignment.Top)
			{
				//stringFormat.LineAlignment = StringAlignment.Near;
			}
			else if (this.vAlignment == VAlignment.Center)
			{
				//stringFormat.LineAlignment = StringAlignment.Center;
			}
			else if (this.vAlignment == VAlignment.Bottom)
			{
				//stringFormat.LineAlignment = StringAlignment.Far;
			}

		}
		public override void SetGravity(HGravity hGravity)
		{
			base.SetGravity(hGravity);

			// API specific
			if (this.hGravity == HGravity.Left)
			{
				//stringFormat.Alignment = StringAlignment.Near;
			}
			else if (this.hGravity == HGravity.Center)
			{
				//stringFormat.Alignment = StringAlignment.Center;
			}
			else if (this.hGravity == HGravity.Right)
			{
				//stringFormat.Alignment = StringAlignment.Far;
			}
		}
		public override void SetGravity(VGravity vGravity)
		{
			this.vGravity = vGravity;

			// API specific
			if (this.vGravity == VGravity.Top)
			{
				//stringFormat.LineAlignment = StringAlignment.Near;
			}
			else if (this.vGravity == VGravity.Center)
			{
				//stringFormat.LineAlignment = StringAlignment.Center;
			}
			else if (this.vGravity == VGravity.Bottom)
			{
				//stringFormat.LineAlignment = StringAlignment.Far;
			}
		}
		
		public void SetFont(string fontName, int fontSize)
		{
			SetTextFont(0);// fontIndex);
						   // API Specific
						   //fontFamily = new FontFamily(fontName);
						   //font = new Font(fontFamily, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);
			TextChanged();
			//needRefresh = true;
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
			//base.SetTextColor(ARGB);
			textColor.SetARGB(ARGB);
			//fontBrush = new SolidBrush(Color.FromArgb(textColor.GetARGB()));
		}

		protected override float GetContentWidth()
		{
			BeginTextCommandWidth("STRING");
			AddTextComponentString(this.text); // ADD_TEXT_COMPONENT_FLOAT(69.420f, 2);
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

			if ((textFlags & NO_DRAW) != 0)
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


			//text = "HEJ ALLIHOPA";
			//BeginTextCommandDisplayText("STRING");
			//SetTextScale(0f, 0.3f);
			//SetTextJustification(1);
			//SetTextColour(255, 255, 255, 255);
			//AddTextComponentString(text);
			//EndTextCommandDisplayText(0f, 0f);// x, y);
			//ResetScriptGfxAlign();
			//return;

			//BeginTextCommandLineCount(
			//EndTextCommandLineCount
			//SetTextEdge(


			//SetTextCentre(true);
			//SetTextEntryForWidth(this.text);
			//SetTextLeading(true);

			//SetTextRightJustify(true);

			BeginTextCommandDisplayText("STRING");
			SetTextScale(1f, 0.3f);// this.scale);

			//if ((this.textFlags & TEXT_DROPSHADOW) != 0)
			//{
			//	SetTextDropShadow();
		//	}
			//if ((this.textFlags & TEXT_OUTLINE) != 0)
			//{
			//	SetTextOutline();
			//}

			//float x_adjust = -0.01f;
			float y_adjust = -0.012f; //last -0.013
			float text_x = textX;// drawingRectangle.CenterX();
			float text_y = drawingRectangle.CenterY() + y_adjust;

			if (this.hGravity == HGravity.Left)
			{
				//text_x += GetLeftInner();
				//SetTextWrap(0f, 1f);
				SetTextJustification(1);//0 center, 1 left, 2 right
			}
			else if (this.hGravity == HGravity.Center)
			{
				//text_x = this.x;
				//SetTextWrap(GetLeftInner(), GetRightInner());
				SetTextJustification(0);
			}
			else if (this.hGravity == HGravity.Right)
			{
				//text_x += GetRightInner();
				//SetTextWrap(this.x + GetRightInner(), 0f);
				SetTextJustification(2);
			}
			
			SetTextColour(255, 255, 255, 255);
			AddTextComponentString(this.text);
			EndTextCommandDisplayText(text_x, text_y);
			//ResetScriptGfxAlign();


			// Draw string to screen
			//e.Graphics.DrawString(text, font, fontBrush, (int)drawingRectangle.CenterX(), (int)drawingRectangle.CenterY(), stringFormat);

			//CharacterRange[] characterRanges = { new CharacterRange(0, text.Length), new CharacterRange(0, 1) };
			//stringFormat.SetMeasurableCharacterRanges(characterRanges);

			if (this.needRefresh)
			{
				//SizeF size = e.Graphics.MeasureString(text, font);
				//textWidth = size.Width / 1280f;
				//textHeight = size.Height / 720f;
				//SetWidthInner(textWidth);
				//SetHeightInner(textHeight);
				//Build();
				needRefresh = false;
			}
		}
	}
}
