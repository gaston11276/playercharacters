using NFive.SDK.Core.Models;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Server.Models
{
	public class PedDecorations	:IdentityModel, IPedDecorations
	{
		public PedDecoration ChestTop { get; set; }
		public PedDecoration ChestTopLeft { get; set; }
		public PedDecoration ChestTopRight { get; set; }

		public PedDecorations()
		{
			ChestTop = new PedDecoration();
			ChestTopLeft = new PedDecoration();
			ChestTopRight = new PedDecoration();
		}
	}
}
