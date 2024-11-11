class Player
{
    // auto property
    public Room CurrentRoom { get; set; }
    public Room currentRoom;
    public int health { get; set; }
    public int amount { get; set; }
    public bool isAlive;
    public bool hasKey { get; set; }
    private Inventory backpack;
    private Item item;

    // constructor
    public Player()
    {
        CurrentRoom = null;
        health = 100;
        amount = 30;
        backpack = new Inventory(25);
        item = new Item();
    }

    public void Damage(int amount)
    {
        health -= amount;
    }
   
   public void Heal(int amount)
   {
       health += amount;
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
        // TODO implement:
        // Remove the Item from the Room
        currentRoom.chest.RemoveItem(itemName);

        // Put it in your backpack.
        backpack.Put(itemName, item);

        // Inspect returned values.
        Console.WriteLine("You have taken " + item.Description + " from the chest.");
        
        // If the item doesn't fit your backpack, put it back in the chest.
        // Communicate to the user what's happening.
        // Return true/false for success/failure
        // Put it in your backpack.
        if (item.Weight > backpack.freeweight)
        {
            currentRoom.chest.Put(itemName, item);
            Console.WriteLine("You don't have enough space in your inventory.");
            return false;
        }
        else
        {
            Console.WriteLine("You have enough space in your inventory.");
            return true;
        }
    }
    public bool DropToChest(string itemName)
    {
        // TODO implement:
        // Remove Item from your inventory.
        backpack.RemoveItem(itemName);

        // Add the Item to the Room
        currentRoom.chest.Put(itemName, item);

        // Inspect returned values
        // Communicate to the user what's happening
        Console.WriteLine("You have dropped " + item.Description + " into the chest.");
        
        // Return true/false for success/failure
        if(item == null)
        {
            Console.WriteLine("You don't have this item in your inventory.");
            return false;
        }
        else
        {
            Console.WriteLine("You have dropped " + item.Description + " into the chest.");
            return true;
        }
    }
}