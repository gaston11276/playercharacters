using System.Collections.Generic;
using NFive.SDK.Core.Models;
using NFive.SDK.Core.Helpers;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Server.Models
{
	public class PedProps : IdentityModel, IPedProps
	{
		//public List<PedProp> Props { get; set; }

		public PedProp Hat { get; set; }
		public PedProp Glasses { get; set; }
		public PedProp Ear { get; set; }
		public PedProp Watch { get; set; }
		public PedProp Bracelet { get; set; }

		public PedProps()
		{
			this.Id = GuidGenerator.GenerateTimeBasedGuid();

			/*
			Props = new List<PedProp>();

			for (int i = 0; i < (int)PedPropType.NumberOfTypes; i++)
			{
				PedProp Prop = new PedProp();
				Prop.Type = (PedPropType)i;
				Props.Add(Prop);
			}
			*/
			

			this.Hat = new PedProp();
			this.Glasses = new PedProp();
			this.Ear = new PedProp();
			this.Watch = new PedProp();
			this.Bracelet = new PedProp();
		}
	}
}
