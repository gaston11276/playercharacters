using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using NFive.SDK.Core.Models;
using NFive.SDK.Core.Models.Player;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Server.Models
{
	public class Character : IdentityModel, ICharacter
	{
		[Required]
		[StringLength(100, MinimumLength = 2)]
		public string Forename { get; set; }

		[StringLength(100, MinimumLength = 0)]
		public string Middlename { get; set; }

		[Required]
		[StringLength(100, MinimumLength = 2)]
		public string Surname { get; set; }

		[Required]
		public DateTime DateOfBirth { get; set; }

		[Required]
		[Range(0, 1)]
		public short Gender { get; set; }

		[Required]
		public bool Alive { get; set; }

		[Required]
		[Range(0, 10000)]
		public int Health { get; set; }

		[Required]
		[Range(0, 100)]
		public int Armor { get; set; }

		[Required]
		[Range(1000000, 999999999)]
		public int Ssn { get; set; }

		[Required]
		public Position Position { get; set; }

		[Required]
		[StringLength(200)] // TODO
		public string Model { get; set; }

		[Required]
		[StringLength(200)] // TODO
		public string AnimationSet { get; set; }

		[Required]
		[ForeignKey("PedHeadBlendData")]
		public Guid PedHeadBlendDataId { get; set; }
		public virtual PedHeadBlendData PedHeadBlendData { get; set; }

		[Required]
		[ForeignKey("PedFaceFeatures")]
		public Guid PedFaceFeaturesId { get; set; }
		public virtual PedFaceFeatures PedFaceFeatures { get; set; }

		[Required]
		[ForeignKey("PedHeadOverlays")]
		public Guid PedHeadOverlaysId { get; set; }
		public virtual PedHeadOverlays PedHeadOverlays { get; set; }

		[Required]
		[ForeignKey("PedDecorations")]
		public Guid PedDecorationsId { get; set; }
		public virtual PedDecorations PedDecorations { get; set; }

		[Required]
		[ForeignKey("PedComponents")]
		public Guid PedComponentsId { get; set; }
		public virtual PedComponents PedComponents { get; set; }

		public int PedHairColor { get; set; }
		public int PedSecondHairColor { get; set; }
		public PedEyeColor PedEyeColor { get; set; }

		[Required]
		[ForeignKey("PedProps")]
		public Guid PedPropsId { get; set; }
		public virtual PedProps PedProps{ get; set; }

		public DateTime? LastPlayed { get; set; }

		[Required]
		[ForeignKey("User")]
		public Guid UserId { get; set; }
		public virtual User User { get; set; }

		[JsonIgnore]
		public string FullName => $"{this.Forename} {this.Middlename} {this.Surname}".Replace("  ", " ");

		public static int GenerateSsn()
		{
			var rng = new Random();

			int ssn;
			do
			{
				ssn = rng.Next(1000000, 999999999);
			} while (Regex.IsMatch(ssn.ToString(), "^(?!b(d)1+b)(?!123456789|219099999|078051120)(?!666|000|9d{2})d{3}(?!00)d{2}(?!0{4})d{4}$")); // Validate its a valid USA SSN

			// TODO: Validate SSN isn't already used

			return ssn;
		}
	}

}
