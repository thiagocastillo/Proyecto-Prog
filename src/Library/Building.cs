namespace Library;

public class Building
{
    public BuildingType Type { get; set; }
    public Player Owner { get; set; }
    public int Capacity { get; set; }
    
    public Building(BuildingType type, Player owner, int capacity)
    {
        Type = type;
        Owner = owner;
        Capacity = capacity;
    }
}