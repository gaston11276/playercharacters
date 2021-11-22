using System;
using JetBrains.Annotations;
using NFive.SDK.Core.Models;

namespace Gaston11276.Playercharacters.Shared.Models
{
	[PublicAPI]
	public interface ICharacter : IIdentityModel
	{
		string Forename { get; set; }
		string Middlename { get; set; }
		string Surname { get; set; }
		DateTime DateOfBirth { get; set; }
		short Gender { get; set; }
		bool Alive { get; set; }
		int Health { get; set; }
		int Armor { get; set; }
		int Ssn { get; set; }
		Position Position { get; set; }
		string Model { get; set; }
		string AnimationSet { get; set; }
		DateTime? LastPlayed { get; set; }
		int PedHairColor { get; set; }
		int PedSecondHairColor { get; set; }
		PedEyeColor PedEyeColor { get; set; }
	}
}
