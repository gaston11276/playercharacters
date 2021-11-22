using System;

namespace Gaston11276.Playercharacters.Shared.Models
{
	public interface ICharacterInventory
	{
		Guid CharacterId { get; set; }
		Guid ContainerId { get; set; }
	}
}
