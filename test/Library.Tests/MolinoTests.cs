namespace Library.Tests;

public class MolinoTests
{
    private Jugador jugador;

    [SetUp]
    public void Setup()
    {
        var civilizacion = new Civilizacion("Chinos", new List<string>(), "Caballeria");
        jugador = new Jugador("Gaston", civilizacion);
    }

    [Test]
    public void Constructor_ValoresInicialesCorrectos()
    {
        var molino = new Molino(jugador);
        
        Assert.AreEqual(jugador, molino.Propietario);
        Assert.AreEqual(10000, molino.Vida);
        Assert.AreEqual(500, molino.CapacidadMaxima);
    }

    [Test]
    public void Eficiencia_CortaDistancia_100Porciento()
    {
        var molino = new Molino(jugador);
        double eficiencia = molino.Eficiencia(1);
        Assert.AreEqual(1.0, eficiencia);
    }

    [Test]
    public void Eficiencia_LargaDistancia_10Porciento()
    {
        var molino = new Molino(jugador);
        double eficiencia = molino.Eficiencia(10);
        Assert.AreEqual(0.1, eficiencia);
    }

    [Test]
    public void Eficiencia_DistanciaIntermedia_CalculoCorrecto()
    {
        var molino = new Molino(jugador);
        double eficiencia = molino.Eficiencia(5);
        Assert.AreEqual(0.5, eficiencia);
    }
}