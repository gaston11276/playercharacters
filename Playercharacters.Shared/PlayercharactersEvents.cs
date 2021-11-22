namespace Gaston11276.Playercharacters.Shared
{
	public static class PlayercharactersEvents
	{
		public const string Configuration = "gaston11276:playercharactersEvents:configuration";
		public const string Disconnecting = "gaston11276:playercharactersEvents:disconnecting";
		
		public const string GetCharactersForUser = "gaston11276:playercharactersEvents:getcharactersforuser";

		public const string CreateCharacter = "gaston11276:playercharactersEvents:create";
		public const string CharacterCreated = "gaston11276:playercharactersEvents:created";
		//public const string MessageEntered= "gaston11276:playercharactersEvents:MessageEntered";
		public const string DeleteCharacter = "gaston11276:playercharactersEvents:delete";
		public const string Select = "gaston11276:playercharactersEvents:select";
		//public const string DeselectAll = "gaston11276:playercharactersEvents:deselectall";

		public const string SaveCharacter = "gaston11276:playercharactersEvents:save:character";
		public const string SavePosition = "gaston11276:playercharactersEvents:save:position";

		public const string GetCharacterInventories = "gaston11276:playercharactersEvents:inventories";

		public const string Selecting = "gaston11276:playercharactersEvents:selecting";
		public const string Selected = "gaston11276:playercharactersEvents:selected";
		public const string Deselecting = "gaston11276:playercharactersEvents:deselecting";
		public const string Deselected = "gaston11276:playercharactersEvents:deselected";

		public const string GetActive = "gaston11276:playercharactersEvents:getactive";
		public const string GetActiveClientCharacter = "gaston11276:playercharactersEvents:getactiveclientcharacter";

		// Inventory
		public const string CreateContainer = "gaston11276:playercharactersEvents:container:create";
		public const string DeleteContainer = "gaston11276:playercharactersEvents:container:delete";
		public const string UpdateContainer = "gaston11276:playercharactersEvents:container:update";

		public const string CreateItem = "gaston11276:playercharactersEvents:item:create";
		public const string DeleteItem = "gaston11276:playercharactersEvents:item:delete";
		public const string UpdateItem = "gaston11276:playercharactersEvents:item:update";
		public const string UseItem = "gaston11276:playercharactersEvents:item:use";
		public const string TransferItemToWorld = "gaston11276:playercharactersEvents:item:movetoworld";

		public const string CreateWorldItem = "gaston11276:playercharactersEvents:worlditem:create";
		public const string DeleteWorldItem = "gaston11276:playercharactersEvents:worlditem:delete";
		public const string UpdateWorldItem = "gaston11276:playercharactersEvents:worlditem:update";

		public const string ContainerAddItem = "gaston11276:playercharactersEvents:container:add";
		public const string ContainerRemoveItem = "gaston11276:playercharactersEvents:container:remove";
		public const string ContainerMoveItem = "gaston11276:playercharactersEvents:container:move";
		public const string ContainerTransferItem = "gaston11276:playercharactersEvents:container:transfer";
	}
}
