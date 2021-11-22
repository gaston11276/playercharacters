
using JetBrains.Annotations;
using NFive.SDK.Core.Models;

namespace Gaston11276.Playercharacters.Shared.Models
{
	[PublicAPI]
	public interface IPedProps : IIdentityModel
	{
		PedProp Hat { get; set; }
		PedProp Glasses { get; set; }
		PedProp Ear { get; set; }
		PedProp Watch { get; set; }
		PedProp Bracelet { get; set; }
	}
}
