using System;
using System.Collections.Generic;
using NFive.SDK.Core.Models;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Client.Models
{
	public class PedProps : IdentityModel, IPedProps
	{
		
		//public List<PedProp> Props { get; set; }

		public PedProp Hat { get; set; }
		public PedProp Glasses { get; set; }
		public PedProp Ear { get; set; }
		public PedProp Watch { get; set; }
		public PedProp Bracelet { get; set; }
	}
}
