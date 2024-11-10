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

        // Check the Weight of the Item and check
        // for enough space in the Inventory
        // Does the Item fit?
        TotalWeight();
        FreeWeight();
        
        // Put Item in the items Dictionary
        items.Add(itemName, item);
        
        // Return true/false for success/failure
        if (item.Weight <= maxWeight)
        {
            return true;
        }
        else
        {
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
        if (TotalWeight() <= maxWeight)
        {
            return maxWeight - TotalWeight();
        }
        else
        {
            return 0;
        }
    }

    public void RemoveItem(string itemName)
    {
        // TODO implement:
        // remove Item from items Dictionary
        items.Remove(itemName);
    }
    
}