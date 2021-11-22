using NFive.SDK.Core.Models;
using NFive.SDK.Core.Helpers;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Server.Models
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

		public PedHeadOverlays()
		{
			this.Id = GuidGenerator.GenerateTimeBasedGuid();

			Blemishes = new PedHeadOverlay();
			Blemishes.Type = PedHeadOverlayType.Blemishes;
			FacialHair = new PedHeadOverlay();
			FacialHair.Type = PedHeadOverlayType.FacialHair;
			FacialHair.ColorType = PedHeadOverlayColorType.Hair;
			Eyebrows = new PedHeadOverlay();
			Eyebrows.Type = PedHeadOverlayType.Eyebrows;
			Eyebrows.ColorType = PedHeadOverlayColorType.Hair;
			Ageing = new PedHeadOverlay();
			Ageing.Type = PedHeadOverlayType.Ageing;
			Makeup = new PedHeadOverlay();
			Makeup.Type = PedHeadOverlayType.Makeup;
			Makeup.ColorType = PedHeadOverlayColorType.Makeup;
			Blush = new PedHeadOverlay();
			Blush.Type = PedHeadOverlayType.Blush;
			Blush.ColorType = PedHeadOverlayColorType.Makeup;
			Complexion = new PedHeadOverlay();
			Complexion.Type = PedHeadOverlayType.Complexion;
			SunDamage = new PedHeadOverlay();
			SunDamage.Type = PedHeadOverlayType.SunDamage;
			Lipstick = new PedHeadOverlay();
			Lipstick.Type = PedHeadOverlayType.Lipstick;
			Lipstick.ColorType = PedHeadOverlayColorType.Makeup;
			MolesAndFreckles = new PedHeadOverlay();
			MolesAndFreckles.Type = PedHeadOverlayType.MolesAndFreckles;
			ChestHair = new PedHeadOverlay();
			ChestHair.Type = PedHeadOverlayType.ChestHair;
			ChestHair.ColorType = PedHeadOverlayColorType.Hair;
			BodyBlemishes = new PedHeadOverlay();
			BodyBlemishes.Type = PedHeadOverlayType.BodyBlemishes;
		}
	}
}
