using JetBrains.Annotations;
using NFive.SDK.Core.Models;

namespace Gaston11276.Playercharacters.Shared.Models
{
	[PublicAPI]
	public interface IPedDecorations : IIdentityModel
	{
		PedDecoration ChestTop { get; set; }
		PedDecoration ChestTopLeft { get; set; }
		PedDecoration ChestTopRight{ get; set; }
	}
}
