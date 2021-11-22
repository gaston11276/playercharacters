using System;
using NFive.SDK.Core.Models;
using Gaston11276.Playercharacters.Shared.Models;


namespace Gaston11276.Playercharacters.Server.Models
{
	public class CharacterInventory : IdentityModel, ICharacterInventory
	{
		public Guid CharacterId { get; set; }

		public virtual Character Character { get; set; }

		public Guid ContainerId { get; set; }

		public virtual Container Container { get; set; }
	}
}
