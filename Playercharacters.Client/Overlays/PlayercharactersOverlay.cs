using NFive.SDK.Client.Interface;
using System;
using System.Web;
using System.Collections.Generic;

namespace Gaston11276.Playercharacters.Client.Overlays
{
	public abstract class PlayercharactersOverlay0 : Overlay
	{
		public PlayercharactersOverlay0(IOverlayManager manager) : base(manager)
		{ }
		//public abstract void On<T1, T2, TReturn>(string @event, Func<T1, T2, TReturn> action);
		//public abstract void On<T1, T2, T3, TReturn>(string @event, Func<T1, T2, T3, TReturn> action);
	}
	public class PlayercharactersOverlay : Overlay
	{
		public event EventHandler<OverlayEventArgs> OnTest;
		public event EventHandler<OnKeyOverlayEventArgs> OnKey;
		public event EventHandler<OnMouseButtonOverlayEventArgs> OnMouseButton;
		public event EventHandler<OverlayEventArgs> OnMouseMove;

		

		

		public void On<T1, T2>(string @event, Action<T1, T2> action)
		{
		
		}

		public void On<T1, T2, T3>(string @event, Action<T1, T2, T2> action)
		{

		}

		//public void On<T1, T2, T3, TReturn>(string @event, OnMouseMove action)
		//{ }




		public PlayercharactersOverlay(IOverlayManager manager) : base(manager)
		{
			On("onTest", () => this.OnTest?.Invoke(this, new OverlayEventArgs(this)));

			On<int>("OnKeyPress", (keycode) => this.OnKey?.Invoke(this, new OnKeyOverlayEventArgs(1, keycode, this)));
			On<int>("OnKeyDown", (keycode) => this.OnKey?.Invoke(this, new OnKeyOverlayEventArgs(2, keycode, this)));
			On<int>("OnKeyUp", (keycode) => this.OnKey?.Invoke(this, new OnKeyOverlayEventArgs(3, keycode, this)));

			//On<int>("OnMouseButtonPress", (button) => this.OnMouseButton?.Invoke(this, new OnMouseButtonOverlayEventArgs(1, button, this)));
			On<int>("OnMouseButtonDown", (button) => this.OnMouseButton?.Invoke(this, new OnMouseButtonOverlayEventArgs(1, button, this)));
			On<int>("OnMouseButtonRelease", (button) => this.OnMouseButton?.Invoke(this, new OnMouseButtonOverlayEventArgs(3, button, this)));
			On("OnMouseMove", () => this.OnMouseMove?.Invoke(this, new OverlayEventArgs(this)));


			

		}

		public void test()
		{
		}
	}

	public class OnKeyOverlayEventArgs : OverlayEventArgs
	{
		public int state { get; set; }
		public int keycode { get; set; }

		public OnKeyOverlayEventArgs(int state, int keycode, Overlay overlay) : base(overlay)
		{
			this.state = state;
			this.keycode = keycode;
		}
	}

	public class OnMouseButtonOverlayEventArgs : OverlayEventArgs
	{
		public int state { get; set; }
		public int button { get; set; }
		
		public OnMouseButtonOverlayEventArgs(int state, int button,  Overlay overlay) : base(overlay)
		{
			this.state = state;
			this.button = button;
		}
	}

	public class OnMouseMoveOverlayEventArgs : OverlayEventArgs
	{
		public int x { get; set; }
		public int y { get; set; }


		public OnMouseMoveOverlayEventArgs(int x, int y, Overlay overlay) : base(overlay)
		{
			this.x = x;
			this.y = y;
		}
	}

}
