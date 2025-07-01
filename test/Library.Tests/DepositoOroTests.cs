namespace Library.Domain.Tests;

public class DepositoOroTests
{
    private Jugador jugador;

    [SetUp]
    public void SetUp()
    {
        Civilizacion civilizacion = new Civilizacion("Chinos", new List<string>(), "Caballeria");
        jugador = new Jugador("Gaston", civilizacion);
    }
    
    [Test]
    public void Constructor_ValoresInicialesCorrectos()
    {
        DepositoOro depositoOro = new DepositoOro(jugador);
        
        Assert.AreEqual(jugador, depositoOro.Propietario); // Verifica que el propietario sea el correcto
        Assert.AreEqual(5000, depositoOro.Vida); // Verifica que la vida inicial sea 5000
        Assert.AreEqual(500, depositoOro.CapacidadMaxima); // Verifica que la capacidad maxima sea 500
    }

    [Test]
    public void Eficiencia_CortaDistancia_100Porciento()
    {
        DepositoOro depositoOro = new DepositoOro(jugador);
        double eficiencia = depositoOro.Eficiencia(1);
        Assert.AreEqual(1.0, eficiencia); // Debe devolver 1.0 de eficiencia
    }

    [Test]
    public void Eficiencia_LargaDistancia_10Porciento()
    {
        DepositoOro depositoOro = new DepositoOro(jugador);
        double eficiencia = depositoOro.Eficiencia(10);
        Assert.AreEqual(0.1, eficiencia); // Debe devolver 0.1 de eficiencia
    }

    [Test]
    public void Eficiencia_DistanciaIntermedia_CalculoCorrecto()
    {
        DepositoOro depositoOro = new DepositoOro(jugador);
        double eficiencia = depositoOro.Eficiencia(5);
        Assert.AreEqual(0.5, eficiencia); // Se espera que sea 0.5 (50%)
    }
}
