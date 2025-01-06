class Player
{
    // auto property
    public Room currentRoom;
    public int Health { get; set; }
    public int Amount { get; set; }
    public Inventory Backpack  { get; set; }
    public bool HasKey { get; internal set; }




    // constructor
    public Player()
    {
        Health = 100;
        Amount = 30;
        Backpack = new Inventory(16);
    }
   
   public int Heal(int amount)
   {
       Health += amount;
       return Health;
   }

    public bool TakeFromChest(string itemName)
    {
        // Check if the item exists in the chest
        Item item = currentRoom.Chest.Get(itemName);
        if (item == null)
        {
            Console.WriteLine("Item not found in the chest.");
            return false;
        }

        // Check if the item fits in the backpack
        if (item.Weight > Backpack.FreeWeight())
        {
            Console.WriteLine("You don't have enough space in your inventory.");
            return false;
        }

        // Remove the item from the chest
        currentRoom.Chest.RemoveItem(itemName);

        // Put it in your backpack
        Backpack.Put(itemName, item);

        // Communicate to the user what's happening
        Console.WriteLine("You have taken " + item.Description + " from the chest.");
        return true;
    }

    public bool DropToChest(string itemName)
    {
        // Check if the item exists in the backpack
        Item item = Backpack.Get(itemName);
        if (item == null)
        {
            Console.WriteLine("You don't have this item in your inventory.");
            return false;
        }

        // Remove the item from the backpack
        Backpack.RemoveItem(itemName);

        // Add the item to the chest
        currentRoom.Chest.Put(itemName, item);

        // Communicate to the user what's happening
        Console.WriteLine("You have dropped " + item.Description + " into the chest.");
        return true;
    }

    public void Put(Command command)
	    {
		if (command.SecondWord == null)
		{
			Console.WriteLine("Put what?");
			return;
		}

		string itemName = command.SecondWord; // Assuming the item name is the second word in the command
		Item backpackItem = Backpack.Get(itemName);

		if (backpackItem != null)
		{
			Backpack.Put(itemName, backpackItem);
			Console.WriteLine("You have put " + backpackItem.Description + " in the chest.");
		}
		else
		{
			Console.WriteLine("Item not found in your inventory.");
		}
	}

	public void Get(Command command)
	{
		if (command.SecondWord == null)
		{
			Console.WriteLine("Get what?");
			return;
		}

		string itemName = command.SecondWord; // Assuming the item name is the second word in the command
		Item chestItem = currentRoom.Chest.Get(itemName);

		if (chestItem != null)
		{
			Backpack.Get(itemName);
			Console.WriteLine("You have fetched " + chestItem.Description + " from the chest.");
		}
		else
		{
			Console.WriteLine("Item not found in the chest.");
		}
	}
}