namespace Library;

public class Player
{
    public string Name { get; set; }
    public int Food { get; set; }
    public int Wood { get; set; }
    public int Gold { get; set; }

    public Player(string name)
    {
        Name = name;
        Food = 0;
        Wood = 0;
        Gold = 0;
    }

    public void CollectFood(int amount)
    {
        Food += amount;
    }

    public void CollectWood(int amount)
    {
        Wood += amount;
    }

    public void CollectGold(int amount)
    {
        Gold += amount;
    }
}