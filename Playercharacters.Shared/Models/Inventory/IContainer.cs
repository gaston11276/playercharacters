using System;
using NFive.SDK.Core.Models;

namespace Gaston11276.Playercharacters.Shared.Models
{
	public interface IContainer : IIdentityModel
	{
		Guid? ParentContainerId { get; set; }

		string Name { get; set; }

		float MaxWeight { get; set; }

		int Width { get; set; }

		int Height { get; set; }
	}
}
