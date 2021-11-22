using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFive.SDK.Core.Models;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Client.Models
{
	public class PedDecorations : IdentityModel, IPedDecorations
	{
		public PedDecoration ChestTop { get; set; }
		public PedDecoration ChestTopLeft { get; set; }
		public PedDecoration ChestTopRight { get; set; }
	}
}
