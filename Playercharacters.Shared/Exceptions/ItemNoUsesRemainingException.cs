using System;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Shared.Exceptions
{
	public class ItemNoUsesRemainingException : Exception
	{
		public IItem Item { get; protected set; }

		public ItemNoUsesRemainingException(IItem item)
		{
			this.Item = item;
		}
	}
}
