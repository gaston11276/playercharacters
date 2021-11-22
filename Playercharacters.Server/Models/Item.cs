using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using NFive.SDK.Core.Models;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Server.Models
{
	public class Item : IdentityModel, IItem
	{
		[Required]
		[ForeignKey("ItemDefinition")]
		public Guid ItemDefinitionId { get; set; }

		[JsonIgnore]
		public virtual ItemDefinition ItemDefinition { get; set; }

		[JsonIgnore]
		public virtual Container Container { get; set; }

		public Guid? ContainerId { get; set; }

		public float Weight { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

		public int? X { get; set; }

		public int? Y { get; set; }

		public bool Rotated { get; set; }

		public int UsesRemaining { get; set; }
	}
}
