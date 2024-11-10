class Item
{
    private string v1;
    private int v2;

    // fields
    public int Weight { get; }
    public string Description { get; }
    // constructor
    public Item(int weight, string description)
    {
    Weight = weight;
    Description = description;
    }

    public Item(string v1, int v2)
    {
        this.v1 = v1;
        this.v2 = v2;
    }

    public Item()
    {
    }
}