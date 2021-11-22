using System.Collections.Generic;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;

using CitizenFX.Core;

using NFive.SDK.Core.Diagnostics;




namespace Gaston11276.SimpleUi
{
	public class UiPanel: UiElement, IUiElement
	{
		static public ILogger Logger;
		public System.Guid Id;

		Rectangle rectangle;
		//Argb color;

		public delegate void OnSelectId(System.Guid id);
		protected List<OnSelectId> callbacksOnSelectId;

		public UiPanel()
		{
			Type = UiElementType.Rectangle;

			//color = colorBackground;
			callbacksOnSelectId = new List<OnSelectId>();

			// API Specific
			rectangle = new Rectangle();
		}

		/*
		public new void AddElement(UiElement element)
		{
			element.Parent = this;
			elements.Add(element);
		}
		*/

		/*
		public override void SetBackgroundColor(int alpha, int red, int green, int blue)
		{
			//colorBackground.SetARGB(alpha, red, green, blue);
			//currentColorBackground = color;
		}
		*/

		public void RegisterOnSelectIdCallback(OnSelectId OnSelectId)
		{
			callbacksOnSelectId.Add(OnSelectId);
		}

		protected override void OnFocus()
		{
			if ((flags & UiElement.SELECTED) == 0)
			{
				currentColorBackground = colorFocus;
			}
		}

		protected override void OffFocus()
		{
			if ((flags & UiElement.SELECTED) == 0)
			{
				currentColorBackground = colorBackground;
			}
		}

		protected override void OnSelect()
		{
			currentColorBackground = colorSelected;
		}

		protected override void OffSelect()
		{
			currentColorBackground = colorBackground;
		}

		public new void OnDisable()
		{
			currentColorBackground = colorDisabled;
			//Redraw();
		}

		public new void OffDisable()
		{
			currentColorBackground = colorBackground;
			//Redraw();
		}

		protected override void RunOnSelectCallbacks()
		{
			base.RunOnSelectCallbacks();

			foreach (OnSelectId OnSelectId in callbacksOnSelectId)
			{
				OnSelectId(Id);
			}
		}

		public bool GetSelection(ref UiPanel selectedElement)
		{
			foreach (UiElement element in elements)
			{
				if ((element.GetFlags() & SELECTED) != 0)
				{
					selectedElement = (UiPanel)element;
					return true;
				}
			}
			return false;
		}

		public void Enable()
		{
			ClearFlags(DISABLED);
		}

		public void Disable()
		{
			SetFlags(DISABLED);
		}

		public void SetLogger(ILogger Logger)
		{
			UiPanel.Logger = Logger;
		}

		/*
		protected override void Redraw()
		{
			
		}
		*/

		public virtual void Draw()
		{
			//Logger.Debug($"1 Draw(): {name}");
			if ((flags & (HIDDEN | DISABLED)) != 0)
			{
				return;
			}

			//System.Console.WriteLine($"{name}: Draw()");
			//rectangle.X = (int)drawingRectangle.Left();
			//rectangle.Y = (int)drawingRectangle.Top();
			//rectangle.Width = (int)drawingRectangle.Width();
			//rectangle.Height = (int)drawingRectangle.Height();

			//System.Console.WriteLine($"X {rectangle.X} Y: {rectangle.Y} W: {rectangle.Width} H: {rectangle.Height}");


			//Vector3 vec3 = rectangle.Position;


			//Logger.Debug($"2 Draw(): {name}");

			if ((flags & NO_DRAW) == 0)
			{
				API.DrawRect(drawingRectangle.CenterX(),
							drawingRectangle.CenterY(),
							drawingRectangle.Width(),
							drawingRectangle.Height(),
							currentColorBackground.GetRed(),
							currentColorBackground.GetGreen(),
							currentColorBackground.GetBlue(),
							currentColorBackground.GetAlpha());
			}
			

			foreach (UiPanel element in elements)
			{
				element.Draw();
			}
		}
	} 
}
