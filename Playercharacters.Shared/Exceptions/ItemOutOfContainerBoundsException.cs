using System;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Shared.Exceptions
{
	public class ItemOutOfContainerBoundsException : Exception
	{
		public IContainer Container { get; }
		public IItem Item { get; set; }

		public ItemOutOfContainerBoundsException(IContainer container, IItem item)
		{
			this.Container = container;
			this.Item = item;
		}


	}
}
