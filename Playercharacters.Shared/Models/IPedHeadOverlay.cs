using NFive.SDK.Core.Models;

namespace Gaston11276.Playercharacters.Shared.Models
{
	public interface IPedHeadOverlays : IIdentityModel
	{
		PedHeadOverlay Blemishes { get; set; }
		PedHeadOverlay FacialHair { get; set; }

		PedHeadOverlay Eyebrows { get; set; }
		PedHeadOverlay Ageing { get; set; }
		PedHeadOverlay Makeup { get; set; }
		PedHeadOverlay Blush { get; set; }


		PedHeadOverlay Complexion { get; set; }
		PedHeadOverlay SunDamage { get; set; }

		PedHeadOverlay Lipstick { get; set; }
		PedHeadOverlay MolesAndFreckles { get; set; }
		PedHeadOverlay ChestHair { get; set; }

		PedHeadOverlay BodyBlemishes { get; set; }

		int AddBodyBlemishes { get; set; }
	}
}
