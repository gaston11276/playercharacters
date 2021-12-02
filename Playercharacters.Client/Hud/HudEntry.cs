using NFive.SDK.Core.Diagnostics;
namespace Gaston11276.Playercharacters.Client
{
	public abstract class HudEntry
	{
		protected fpDelay Delay;
		protected ILogger Logger;

		public HudEntry()
		{ }

		public void SetLogger(ILogger Logger)
		{
			this.Logger = Logger;
		}

		public void SetDelay(fpDelay Delay)
		{
			this.Delay = Delay;
		}
	}
}
