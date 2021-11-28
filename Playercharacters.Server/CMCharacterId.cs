using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFive.SDK.Server.Communications;
using NFive.SDK.Core.Diagnostics;
using NFive.SDK.Core.Helpers;
using NFive.SDK.Core.Models;
using NFive.SDK.Core.Models.Player;
using NFive.SDK.Core.Utilities;
using NFive.SDK.Server.Controllers;
using NFive.SDK.Server.Communications;
using NFive.SDK.Server.Events;

using Gaston11276.Playercharacters.Server.Models;

namespace Gaston11276.Playercharacters.Shared
{
	public class CMCharacterId : ICommunicationMessage
	{
		public IClient Client { get; }
		public User User { get; }
		public Session Session { get; }

		public Guid CharacterId;

		public void Reply(params object[] payloads)
		{ }
	}
}
