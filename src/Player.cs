class Player
{
    // auto property
    public Room CurrentRoom { get; set; }
    public Room currentRoom;
    public int health { get; set; }
    public int amount { get; set; }
    public bool isAlive;
    public bool hasKey { get; set; }
    public Inventory backpack;
    

    // constructor
    public Player()
    {
        CurrentRoom = null;
        health = 100;
        amount = 30;
        backpack = new Inventory(25);
    }

    public int Damage(int amount)
    {
        health -= amount;
        return health;
    }
   
   public int Heal(int amount)
   {
       health += amount;
       return health;
   }

   public void IsAlive()
   {
       if (health <= 0)
       {
            isAlive = false;
            Console.WriteLine("You have died.");
       }
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
        if (item.Weight > backpack.FreeWeight())
        {
            Console.WriteLine("You don't have enough space in your inventory.");
            return false;
        }

        // Remove the item from the chest
        currentRoom.Chest.RemoveItem(itemName);

        // Put it in your backpack
        backpack.Put(itemName, item);

        // Communicate to the user what's happening
        Console.WriteLine("You have taken " + item.Description + " from the chest.");
        return true;
    }
    public bool DropToChest(string itemName)
    {
        // Check if the item exists in the backpack
        Item item = backpack.Get(itemName);
        if (item == null)
        {
            Console.WriteLine("You don't have this item in your inventory.");
            return false;
        }

        // Remove the item from the backpack
        backpack.RemoveItem(itemName);

        // Add the item to the chest
        currentRoom.Chest.Put(itemName, item);

        // Communicate to the user what's happening
        Console.WriteLine("You have dropped " + item.Description + " into the chest.");
        return true;
}

    public string Use(string itemName)
    {
        // TODO implement:
        // Check if the Item is in your Inventory
        Item item = backpack.Get(itemName);
        
        // If it is, use it
        if (item != null)
        {
            // If it's a key, unlock the door
            if (item.Description == "key")
            {
                currentRoom.Unlock();
                return "You have unlocked the door.";
            }
            // If it's a health potion, heal yourself
            else if (item.Description == "health potion")
            {
                Heal(20);
                return "You have healed yourself.";
            }
            // If it's a weapon, attack the enemy
            else if (item.Description == "weapon")
            {
                return "You have attacked the enemy.";
            }
            // If it's a flashlight, light up the room
            else if (item.Description == "flashlight")
            {
                return "You have lit up the room.";
            }
            // If it's a map, show the map
            else if (item.Description == "map")
            {
                return "You have shown the map.";
            }
            // If it's a compass, show the compass
            else if (item.Description == "compass")
            {
                return "You have shown the compass.";
            }
        }
        return "Item not found in inventory.";
    }
}