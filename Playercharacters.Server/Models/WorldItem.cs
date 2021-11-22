using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using NFive.SDK.Core.Models;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Server.Models
{
	public class WorldItem : IdentityModel, IWorldItem
	{
		[JsonIgnore]
		public Item Item { get; set; }

		[ForeignKey("Item")]
		public Guid ItemId { get; set; }

		[Required]
		public Position Position { get; set; }
	}
}
