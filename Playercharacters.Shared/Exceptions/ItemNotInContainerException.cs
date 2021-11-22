using System;

namespace Gaston11276.Playercharacters.Shared.Exceptions
{
	public class ItemNotInContainerException : Exception
	{
		public Guid ItemId { get; }
		public Guid ContainerId { get; }

		public ItemNotInContainerException(Guid itemId, Guid containerId)
		{
			this.ItemId = itemId;
			this.ContainerId = containerId;
		}
	}
}
