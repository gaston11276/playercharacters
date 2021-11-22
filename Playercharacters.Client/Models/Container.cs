using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NFive.SDK.Core.Models;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Client.Models
{
	public class Container : IdentityModel, IContainer
	{
		[JsonIgnore]
		public virtual Container ParentContainer { get; set; }

		public Guid? ParentContainerId { get; set; }

		public string Name { get; set; }

		[JsonIgnore]
		public virtual List<Item> Items { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

		public float MaxWeight { get; set; }
	}
}
