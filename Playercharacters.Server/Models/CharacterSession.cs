using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using NFive.SDK.Core.Models.Player;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Server.Models
{
	public class CharacterSession : ICharacterSession
	{
		[Key]
		[Required]
		public Guid Id { get; set; }

		[Required]
		public DateTime Created { get; set; } = DateTime.UtcNow;
		public DateTime? Connected { get; set; }
		public DateTime? Disconnected { get; set; }

		[Required]
		[ForeignKey("Character")]
		public Guid CharacterId { get; set; }

		[JsonIgnore]
		public virtual Character Character { get; set; }

		[Required]
		[ForeignKey("Session")]
		public Guid SessionId { get; set; }

		[JsonIgnore]
		public virtual Session Session { get; set; }

		[JsonIgnore]
		public bool IsConnected => this.Connected.HasValue && !this.Disconnected.HasValue;
	}
}
