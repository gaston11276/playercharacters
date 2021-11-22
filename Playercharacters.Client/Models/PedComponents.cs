using System;
using NFive.SDK.Core.Models;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Client.Models
{
	public class PedComponents : IdentityModel, IPedComponents
	{
		public PedComponent Face { get; set; }
		public PedComponent Mask { get; set; }
		public PedComponent Hair { get; set; }
		public PedComponent Torso { get; set; }
		public PedComponent Legs { get; set; }
		public PedComponent Back { get; set; }
		public PedComponent Shoes { get; set; }
		public PedComponent Accessory { get; set; }
		public PedComponent Undershirt { get; set; }
		public PedComponent Kevlar { get; set; }
		public PedComponent Badge { get; set; }
		public PedComponent Torso2 { get; set; }
	}
}
