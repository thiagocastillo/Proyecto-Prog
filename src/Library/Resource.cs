using System.Runtime.Versioning;

namespace Library;

public class Resource
{
    public int Food { get; set; }
    public int Wood { get; set; }
    public int Gold { get; set; }

    public Resource()
    {
        Food = 0;
        Wood = 0;
        Gold = 0;
    }

    public void AddFood(int amount)
    {
        Food += amount;
    }

    public void AddWood(int amount)
    {
        Wood += amount;
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }    
}