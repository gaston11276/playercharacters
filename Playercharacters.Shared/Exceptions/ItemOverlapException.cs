using System;
using Gaston11276.Playercharacters.Shared.Models;

namespace Gaston11276.Playercharacters.Shared.Exceptions
{
	public class ItemOverlapException : Exception
	{
		public IItem Item { get; }
		public IContainer Container { get; }
		public Guid OverlappingItemId { get; }

		public ItemOverlapException(IItem item, IContainer container, Guid overlappingItemId)
		{
			this.Item = item;
			this.Container = container;
			this.OverlappingItemId = overlappingItemId;
		}
	}
}
