using NFive.SDK.Core.Models;

namespace Gaston11276.Playercharacters.Shared.Models
{
	public interface IPedFaceFeatures : IIdentityModel
	{
		float NoseWidth { get; set; }
		float NosePeakHeight { get; set; }
		float NosePeakLength { get; set; }
		float NoseBoneHeight { get; set; }
		float NosePeakLowering { get; set; }
		float NoseBoneTwist { get; set; }
		float EyeBrowHeight { get; set; }
		float EyeBrowForward { get; set; }
		float CheekBoneHeight { get; set; }
		float CheekBoneWidth { get; set; }
		float CheekWidth { get; set; }
		float EyesOpening { get; set; }
		float LipThickness { get; set; }
		float JawBoneWidth { get; set; }
		float JawBoneLength { get; set; }
		float ChinBoneLowering { get; set; }
		float ChinBoneLength { get; set; }
		float ChinBoneWidth { get; set; }
		float ChinDimple { get; set; }
		float NeckThickness { get; set; }
	}
}
