namespace Library.Tests;

public class CuartelTests
{
    private Jugador jugador;

    [SetUp]
    public void Setup()
    {
        var civilizacion = new Civilizacion("Aztecas", new List<string>(), "GuerreroJaguar");
        jugador = new Jugador("Luis", civilizacion);
    }

    [Test]
    public void Constructor_AsignacionCorrecta()
    {
        var cuartel = new Cuartel(jugador);
        
        Assert.AreEqual(jugador, cuartel.Propietario);
        Assert.AreEqual(10000, cuartel.Vida);
    }

    [Test]
    public void TiempoConstruccion_Default_NoConstruido()
    {
        var cuartel = new Cuartel(jugador);
        
        Assert.IsFalse(cuartel.EstaConstruido);
    }
}