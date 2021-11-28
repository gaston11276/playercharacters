using System.Collections.Generic;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using NFive.SDK.Core.Diagnostics;

namespace Gaston11276.SimpleUi
{
	public class UiPanel: UiElement, IUiElement
	{
		static public ILogger Logger;
		public System.Guid Id;

		public delegate void OnSelectId(System.Guid id);
		protected List<OnSelectId> callbacksOnSelectId;

		public UiPanel()
		{
			Type = UiElementType.Rectangle;
			callbacksOnSelectId = new List<OnSelectId>();
		}

		public void SetLogger(ILogger Logger)
		{
			UiPanel.Logger = Logger;
		}

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

		public new void OnDisabled()
		{
			currentColorBackground = colorDisabled;
		}

		public new void OffDisabled()
		{
			currentColorBackground = colorBackground;
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

		public void Select()
		{
			SetFlags(SELECTED);
		}

		public void Deselect()
		{
			ClearFlags(SELECTED);
		}

		public virtual void Draw()
		{
			if ((flags & HIDDEN) != 0)
			{
				return;
			}

			if ((flags & TRANSPARENT) == 0)
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
