namespace Library.Tests;

public class JugadorTests
{
    private Jugador jugador;
    private Civilizacion civilizacion;
    
    [SetUp]
    public void Setup()
    {
        civilizacion = new Civilizacion("armenios", new List<string>(), "Arquero Compuesto");
        jugador = new Jugador("TestPlayer", civilizacion);
    }
    
    [Test]
    public void Jugador_InicializaConCentroCivicoY3Aldeanos()
    {
        Assert.That(jugador.Edificios.Count, Is.GreaterThanOrEqualTo(1));
        Assert.That(jugador.Edificios[0], Is.TypeOf<CentroCivico>());
        Assert.That(jugador.Aldeanos.Count, Is.EqualTo(3));
        Assert.That(jugador.Unidades.Count, Is.EqualTo(3));
    }

    [Test]
    public void AumentarPoblacionMaxima_ValidaIncremento()
    {
        jugador.AumentarPoblacionMaxima(10);
        Assert.That(jugador.PoblacionMaxima, Is.EqualTo(15));
    }
    
    [Test]
    public void AumentarPoblacionMaxima_ConValorNegativo_NoDebeModificar()
    {
        int poblacionAntes = jugador.PoblacionMaxima;
        jugador.AumentarPoblacionMaxima(-5);
        Assert.That(jugador.PoblacionMaxima, Is.EqualTo(poblacionAntes));
    }
    
    
    /*[Test]
    public void AgregarRecurso_SumaCantidad()
    {
        Arbol recurso = new Arbol(100, new Point { X = 0, Y = 0 });
        jugador.AgregarRecurso(recurso, 50);
        Assert.That(jugador.Recursos["Madera"], Is.EqualTo(150));
    }*/
    
    [Test]
    public void AgregarRecurso_CantidadInvalida_LanzaExcepcion()
    {
        Arbol recurso = new Arbol(100, new Point { X = 0, Y = 0});
        Assert.Throws<ArgumentException>(() => jugador.AgregarRecurso(recurso, 0));
    }
    
    [Test]
    public void PuedeCrearAldeano_DevuelveTrueSiTieneCentroCivicoYMenosDeLimite()
    {
        Assert.IsTrue(jugador.PuedeCrearAldeano());
    }
}