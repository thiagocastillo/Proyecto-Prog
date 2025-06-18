namespace Library.Tests;

public class CentroCivicoTests
{
    private Jugador jugador;

    [SetUp]
    public void Setup()
    {
        Civilizacion civilizacion = new Civilizacion("Bengalies", new List<string>(), "Ratha");
        jugador = new Jugador("Ana", civilizacion);
    }

    [Test]
    public void Constructor_AsignacionCorrecta()
    {
        CentroCivico centro = new CentroCivico(jugador);
        Assert.AreEqual(jugador, centro.Propietario); // El propietario debe ser el jugador creado
        Assert.AreEqual(10000, centro.Vida); // La vida inicial debe ser 10000
    }

    [Test]
    public void Constructor_Bengalies_AgregaAldeanoInicial()
    {
        int antes = jugador.Aldeanos.Count;
        CentroCivico centro = new CentroCivico(jugador);
        int despues = jugador.Aldeanos.Count;
        
        Assert.AreEqual(antes + 1, despues); // Debe haberse agregado un aldeano mas
    }
}