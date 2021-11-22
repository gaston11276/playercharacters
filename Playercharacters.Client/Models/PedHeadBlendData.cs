using System;
using NFive.SDK.Core.Models;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Client.Models
{
	public class PedHeadBlendData : IdentityModel, IPedHeadBlendData
	{
		public int Parent1 { get; set; }
		public int Parent2 { get; set; }
		public float ShapeMix { get; set; }
		public float SkinMix { get; set; }
	}
}
