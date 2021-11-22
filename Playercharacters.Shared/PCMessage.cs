using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using JetBrains.Annotations;
using NFive.SDK.Core.Helpers;
using NFive.SDK.Core.Models;
using NFive.SDK.Core.Models.Player;

namespace Gaston11276.Playercharacters.Shared
{
	/// <summary>
	/// Represents a chat message sent from a <see cref="User"/>.
	/// </summary>
	[PublicAPI]
	public class PCMessage
	{
		/// <summary>
		/// Gets or sets the unique identifier of the message.
		/// </summary>
		/// <value>
		/// The unique identifier of the message.
		/// </value>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="User"/> sending the message.
		/// </summary>
		/// <value>
		/// The <see cref="User"/> sending the message.
		/// </value>
		public User Sender { get; set; }

		public string Style { get; set; }

		public string Template { get; set; }

		public string[] Values { get; set; }

		public string TestMessage { get; set; }

		public Vector3 Location { get; set; }

		public float? Radius { get; set; }

		public PCMessage()
		{
			this.Id = GuidGenerator.GenerateTimeBasedGuid();
		}
	}
}


