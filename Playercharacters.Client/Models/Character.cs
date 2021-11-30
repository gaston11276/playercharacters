using System;
using Newtonsoft.Json;
using CitizenFX.Core;
using NFive.SDK.Core.Models;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Client.Models
{
	public class Character : IdentityModel, ICharacter
	{
		public string Forename { get; set; }
		public string Middlename { get; set; }
		public string Surname { get; set; }
		public DateTime DateOfBirth { get; set; }
		public short Gender { get; set; }
		public bool Alive { get; set; }
		public int Health { get; set; }
		public int Armor { get; set; }
		public int Ssn { get; set; }
		public Position Position { get; set; }
		public string Model { get; set; }
		public string AnimationSet { get; set; }
		public Guid PedHeadBlendDataId { get; set; }
		public PedHeadBlendData PedHeadBlendData { get; set; }
		public Guid PedFaceFeaturesId{ get; set; }
		public PedFaceFeatures PedFaceFeatures { get; set; }
		public Guid PedHeadOverlaysId{ get; set; }
		public PedHeadOverlays PedHeadOverlays { get; set; }
		public Guid PedDecorationsId { get; set; }
		public PedDecorations PedDecorations { get; set; }
		public Guid PedComponentsId { get; set; }
		public PedComponents PedComponents { get; set; }
		public int PedHairColor { get; set; }
		public int PedSecondHairColor { get; set; }
		public PedEyeColor PedEyeColor { get; set; }
		public Guid PedPropsId { get; set; }
		public PedProps PedProps { get; set; }


		public DateTime? LastPlayed { get; set; }
		public Guid UserId { get; set; }

		[JsonIgnore] public string FullName => $"{this.Forename} {this.Middlename} {this.Surname}".Replace("  ", " ");

		[JsonIgnore] public PedHash ModelHash => (PedHash)Convert.ToUInt32(this.Model);
	}
}
