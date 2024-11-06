class Player
{
    // auto property
    public Room CurrentRoom { get; set; }
    public Room currentRoom;
    public int health;
    private int amount;

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
           Console.WriteLine("You have died.");
       }
   }
}