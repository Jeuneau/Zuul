class Enemy
{
    private string name;
    private int health;
    public Enemy()
    {
        name = "dragon";
        health = 100;  
    }

    public string EnemyName
    {
        get
        {
            return name;
        }
    }
}