using System.Dynamic;
using System.IO.Compression;

class Inventory
{
    // fields
    private int maxWeight;
    private Dictionary<string, Item> items;
  
	
  

   
   

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
        // Check if the item fits in the inventory
        if (item.Weight > FreeWeight())
        {
            Console.WriteLine("You don't have enough space in your inventory.");
            return false;
        }
        // Add Item to items Dictionary
        items.Add(itemName, item);
        return true;
    }

    public Item Get(string itemName)
    {
        // TODO implement:
        // Find Item in items Dictionary
        // remove Item from items Dictionary if found
        // return Item or null
        if (items.ContainsKey(itemName))
        {
            Item item = items[itemName];
            items.Remove(itemName);
            return item;
        }
        return null;
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
        int freeweight = maxWeight - TotalWeight();
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

    public bool HasItem(string itemName)
    {
        // TODO implement:
        // Check if the item exists in the inventory
        return items.ContainsKey(itemName);
    }
}