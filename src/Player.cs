class Player
{
    // auto property
    public Room CurrentRoom { get; set; }
    public Room currentRoom;
    public int health { get; set; }
    public int amount { get; set; }
    public bool isAlive;
    public bool hasKey { get; set; }

    // constructor
    public Player()
    {
        CurrentRoom = null;
        health = 100;
        amount = 30;
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
}