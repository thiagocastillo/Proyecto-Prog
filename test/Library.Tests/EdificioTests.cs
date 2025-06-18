namespace Library.Tests;

public class EdificioTests
{
    private Jugador jugador;

    [SetUp]
    public void Setup()
    {
        var civilizacion = new Civilizacion("Mayas", new List<string>(), "Arquero");
        jugador = new Jugador("Diego", civilizacion);
    }

    [Test]
    public void Constructor_AsignacionCorrecta_VidaInicial()
    {
        var edificio = new Edificio(jugador);
        
        Assert.AreEqual(jugador, edificio.Propietario);
        Assert.AreEqual(10000, edificio.Vida);
    }

    [Test]
    public void Propiedades_TiempoConstruccion_DevuelvenValoresCorrectos()
    {
        var edificio = new Edificio(jugador);
        
        Assert.AreEqual(10, edificio.TiempoConstruccionTotal); // Segun el constructor TiempoConstruccion(10), entonces TiempoConstruccionTotal deberia de ser 10
        Assert.AreEqual(10, edificio.TiempoConstruccionRestante); //TiempoConstruccionRestante deberia empezar igual que TiempoConstruccionTotal
        Assert.IsFalse(edificio.EstaConstruido); // En principio no deberia de estar construido
    }
}