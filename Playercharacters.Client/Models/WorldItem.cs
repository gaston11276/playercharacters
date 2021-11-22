using System;
using NFive.SDK.Core.Models;
using Newtonsoft.Json;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Client.Models
{
	public class WorldItem : IdentityModel, IWorldItem
	{
		[JsonIgnore]
		public Item Item { get; set; }

		public Guid ItemId { get; set; }

		public Position Position { get; set; }

	}
}
