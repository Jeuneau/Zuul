class Enemy {
    private int health;
  


    public Enemy() {
        health = 100;
    }
    public void TakeDamage(int Damage) {
        health -= Damage;   
    }
   public bool IsDead() {
        if (health <= 0) {
            Console.WriteLine("The dragon falls to your feet.");
            return true;
        }
        return false;
   }
}