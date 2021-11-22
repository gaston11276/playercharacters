using NFive.SDK.Core.Models;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Client.Models
{
	public class PedHeadOverlays : IdentityModel, IPedHeadOverlays
	{
		public PedHeadOverlay Blemishes { get; set; }
		public PedHeadOverlay FacialHair { get; set; }
		public PedHeadOverlay Eyebrows { get; set; }
		public PedHeadOverlay Ageing { get; set; }
		public PedHeadOverlay Makeup { get; set; }
		public PedHeadOverlay Blush { get; set; }
		public PedHeadOverlay Complexion { get; set; }
		public PedHeadOverlay SunDamage { get; set; }
		public PedHeadOverlay Lipstick { get; set; }
		public PedHeadOverlay MolesAndFreckles { get; set; }
		public PedHeadOverlay ChestHair { get; set; }
		public PedHeadOverlay BodyBlemishes { get; set; }
		public int AddBodyBlemishes { get; set; }
	}
}
