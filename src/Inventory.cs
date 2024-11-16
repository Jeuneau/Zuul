using System.IO.Compression;

class Inventory
{
    // fields
    private int maxWeight;
    private Dictionary<string, Item> items;
    public int freeweight;
   

    // constructor
    public Inventory(int maxWeight)
    {
    this.maxWeight = maxWeight;
    this.items = new Dictionary<string, Item>();
    }

    // methods
    public bool Put(string itemName, Item item)
    {
        // TODO implement:
        // Check the Weight of the Item and check
        // for enough space in the Inventory
        // Does the Item fit?
        if (item.Weight < freeweight)
        {
            Console.WriteLine("You have enough space in your inventory.");
            // Put Item in the items Dictionary
            items.Add(itemName, item);
            // Return true for success
            return true;
        }
        else
        {
            Console.WriteLine("You don't have enough space in your inventory.");
            // Return false for failure
            return false;
        }
    }
    
    public Item Get(string itemName)
    {
        // TODO implement:
        // Find Item in items Dictionary
        foreach (string key in items.Keys)
        {
            if (key == itemName)
            {
                Item item = items[key];
                Console.WriteLine("You fetched " + item.Description + " out of your inventory.");
                return items[key];
            }
        }
        // remove Item from items Dictionary if found
        items.Remove(itemName);
        // return Item or null
        if (items.ContainsKey(itemName))
        {
            return items[itemName];
        }
        else
        {
            return null;
        
        }
    }

    public int TotalWeight()
    {
        int total = 0;
        // TODO implement:
        // loop through the items, and add all the weights
        foreach (string key in items.Keys)
        {
            total += items[key].Weight;
        }
        return total;
    }
    
    public int FreeWeight()
    {
        // TODO implement:
        // compare MaxWeight and TotalWeight()
        freeweight = maxWeight - TotalWeight();
        return freeweight;
    }

    public Item RemoveItem(string itemName)
    {
        if (items.TryGetValue(itemName, out Item item))
        {
        items.Remove(itemName);
        return item;
        }
        return null; // or throw an exception if the item is not found   
    }

    public string Show()
    {
        // TODO implement:
        // loop through the items and return a string
        // with all the items in the Inventory
        string inventory = "";
        foreach (string key in items.Keys)
        {
            inventory += items[key].Description + ", ";
        }
        return inventory;
    }
}