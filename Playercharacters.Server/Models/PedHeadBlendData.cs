using NFive.SDK.Core.Models;
using NFive.SDK.Core.Helpers;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Server.Models
{
	public class PedHeadBlendData : IdentityModel, IPedHeadBlendData
	{
		public int Parent1 { get; set; }
		public int Parent2 { get; set; }
		public float ShapeMix { get; set; }
		public float SkinMix { get; set; }

		public PedHeadBlendData()
		{
			this.Id = GuidGenerator.GenerateTimeBasedGuid();
		}
	}
}
