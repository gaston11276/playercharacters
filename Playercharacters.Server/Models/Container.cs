using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using NFive.SDK.Core.Models;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Server.Models
{
	public class Container : IdentityModel, IContainer
	{
		[JsonIgnore]
		public virtual Container ParentContainer { get; set; }

		[ForeignKey("ParentContainer")]
		public Guid? ParentContainerId { get; set; }

		public string Name { get; set; }

		[JsonIgnore]
		public virtual List<Item> Items { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

		public float MaxWeight { get; set; }

		public Container()
		{
			this.Items = new List<Item>();
		}
	}
}
