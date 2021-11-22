using NFive.SDK.Core.Helpers;
using NFive.SDK.Core.Models;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Server.Models
{
	public class PedComponents : IdentityModel, IPedComponents
	{
	
		public PedComponent Face { get; set; }
		public PedComponent Mask { get; set; }
		public PedComponent Hair { get; set; }
		public PedComponent Torso { get; set; }
		public PedComponent Legs { get; set; }
		public PedComponent Back { get; set; }
		public PedComponent Shoes { get; set; }
		public PedComponent Accessory { get; set; }
		public PedComponent Undershirt { get; set; }
		public PedComponent Kevlar { get; set; }
		public PedComponent Badge { get; set; }
		public PedComponent Torso2 { get; set; }

		public PedComponents()
		{
			this.Id = GuidGenerator.GenerateTimeBasedGuid();

			this.Face = new PedComponent();
			this.Mask = new PedComponent();
			this.Hair = new PedComponent();
			this.Torso = new PedComponent();
			this.Legs = new PedComponent();
			this.Back = new PedComponent();
			this.Shoes = new PedComponent();
			this.Accessory = new PedComponent();
			this.Undershirt = new PedComponent();
			this.Kevlar = new PedComponent();
			this.Badge = new PedComponent();
			this.Torso2 = new PedComponent();
		}
	}
}
