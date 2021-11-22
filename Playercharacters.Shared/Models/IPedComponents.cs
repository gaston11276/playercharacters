using JetBrains.Annotations;
using NFive.SDK.Core.Models;

namespace Gaston11276.Playercharacters.Shared.Models
{
	[PublicAPI]
	public interface IPedComponents : IIdentityModel
	{
		PedComponent Face { get; set; }
		PedComponent Mask { get; set; }
		PedComponent Hair { get; set; }
		PedComponent Torso { get; set; }
		PedComponent Legs { get; set; }
		PedComponent Back { get; set; }
		PedComponent Shoes { get; set; }
		PedComponent Accessory { get; set; }
		PedComponent Undershirt { get; set; }
		PedComponent Kevlar { get; set; }
		PedComponent Badge { get; set; }
		PedComponent Torso2 { get; set; }
	}
}
