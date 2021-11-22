using System;
using NFive.SDK.Core.Models;

namespace Gaston11276.Playercharacters.Shared.Models
{
	public interface IWorldItem : IIdentityModel
	{
		Guid ItemId { get; set; }
		Position Position { get; set; }
	}
}
