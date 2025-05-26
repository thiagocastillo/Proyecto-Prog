namespace Library;

public class Civilization
{
    public string Name { get; set; }
    public List<string> Bonifications { get; set; }
    public List<string> SpecialUnits { get; set; }
    
    public Civilization(string name, List<string> bonifications, List<string> specialUnits)
    {
        Name = name;
        Bonifications = bonifications;
        SpecialUnits = specialUnits;
    }
}