using System;
using Gaston11276.Playercharacters.Server.Models;
using JetBrains.Annotations;

namespace Gaston11276.Playercharacters.Server.Events
{
	[PublicAPI]
	public class CharacterEventArgs : EventArgs
	{
		public Character Character { get; }

		public CharacterEventArgs(Character character)
		{
			this.Character = character;
		}
	}
}
