using NFive.SDK.Core.Models;

namespace Gaston11276.Playercharacters.Shared.Models
{
	public interface IPedHeadBlendData : IIdentityModel
	{
		int Parent1 { get; set; }
		int Parent2 { get; set; }
		float ShapeMix{ get; set; }
		float SkinMix { get; set; }
	}
}
