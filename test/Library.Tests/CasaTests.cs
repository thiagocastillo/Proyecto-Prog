namespace Library.Tests;

public class CasaTests
{
    private Jugador jugador;

    [SetUp]
    public void Setup()
    {
        var civilizacion = new Civilizacion("Mayas", new List<string>(), "Arquero");
        jugador = new Jugador("Maria", civilizacion);
    }

    [Test]
    public void Constructor_AumentaPoblacionMaxima()
    {
        int poblacionAntes = jugador.PoblacionMaxima;
        var casa = new Casa(jugador);
        
        Assert.AreEqual(poblacionAntes + 5, jugador.PoblacionMaxima);
    }

    [Test]
    public void CantidadAldeano_MenosQueMaximo_True()
    {
        var casa = new Casa(jugador);
        casa.CantidadAldeanos = 10;
        Assert.IsTrue(casa.CantidadAldeano());
    }

    [Test]
    public void CantidadAldeano_MayorQueMaximo_False()
    {
        var casa = new Casa(jugador);
        casa.CantidadAldeanos = 25;
        Assert.IsFalse(casa.CantidadAldeano());
    }

    [Test]
    public void CantidadUnidadMilitar_MenosQueMaximo_True()
    {
        var casa = new Casa(jugador);
        casa.CantidadUnidadesMilitar = 10;
        Assert.IsTrue(casa.CantidadUnidadMilitar());
    }

    [Test]
    public void CantidadUnidadMilitar_MayorQueMaximo_False()
    {
        var casa = new Casa(jugador);
        casa.CantidadUnidadesMilitar = 40;
        Assert.IsFalse(casa.CantidadUnidadMilitar());
    }
}