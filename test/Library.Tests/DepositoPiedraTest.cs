namespace Library.Tests;

public class DepositoPiedraTest
{
    private Jugador jugador;

    [SetUp]

    public void SetUp()
    {
        var civilizacion = new Civilizacion("Chinos", new List<string>(), "Caballeria");
        jugador = new Jugador("Gaston", civilizacion);
    }
    
    [Test]
    public void Constructor_ValoresInicialesCorrectos()
    {
        var depositoPiedra = new DepositoPiedra(jugador);
        
        Assert.AreEqual(jugador, depositoPiedra.Propietario); // Verifica que el propietario sea el correcto
        Assert.AreEqual(5000, depositoPiedra.Vida); // Verifica que la vida inicial sea 5000
        Assert.AreEqual(500, depositoPiedra.CapacidadMaxima); // Verifica que la capacidad maxima sea 500
    }

    [Test]
    public void Eficiencia_CortaDistancia_100Porciento()
    {
        var depositoPiedra = new DepositoPiedra(jugador);
        double eficiencia = depositoPiedra.Eficiencia(1);
        Assert.AreEqual(1.0, eficiencia); // Debe devolver 1.0 de eficiencia
    }

    [Test]
    public void Eficiencia_LargaDistancia_10Porciento()
    {
        var depositoPiedra = new DepositoPiedra(jugador);
        double eficiencia = depositoPiedra.Eficiencia(10);
        Assert.AreEqual(0.1, eficiencia); // Debe devolver 0.1 de eficiencia
    }

    [Test]
    public void Eficiencia_DistanciaIntermedia_CalculoCorrecto()
    {
        var depositoPiedra = new DepositoPiedra(jugador);
        double eficiencia = depositoPiedra.Eficiencia(5);
        Assert.AreEqual(0.5, eficiencia); // Se espera que sea 0.5 (50%)
    }
}