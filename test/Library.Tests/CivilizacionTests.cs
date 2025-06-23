namespace Library.Tests;

public class CivilizacionTests
{
    private Civilizacion civilizacion;
    private List<string> bonificaciones;

    [SetUp]
    public void Setup()
    {
        bonificaciones = new List<string>{"Recolectar mas rapido", "Infanteria mas fuerte"};
        civilizacion = new Civilizacion("Vikingos", bonificaciones, "Guerrero Berserker");
    }

}