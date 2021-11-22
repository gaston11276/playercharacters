using System;
using JetBrains.Annotations;

using Gaston11276.Playercharacters.Server.Models;

namespace Gaston11276.Playercharacters.Server.Events
{
	[PublicAPI]
	public class CharacterSessionEventArgs : EventArgs
	{
		public CharacterSession CharacterSession { get; }

		public CharacterSessionEventArgs(CharacterSession session)
		{
			this.CharacterSession = session;
		}
	}
}
