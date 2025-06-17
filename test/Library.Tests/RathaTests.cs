namespace Library.Tests;

public class RathaTests
{
    private Jugador jugador;
    private Mapa mapa;

    [SetUp]
    public void Setup()
    {
        var civilizacion = new Civilizacion("Mayas", new List<string>(), "Ratha");
        jugador = new Jugador("Pedro", civilizacion);
        mapa = new Mapa();
    }

    [Test]
    public void Mover_PosicionValida_MueveCorrectamente()
    {
        var ratha = new Ratha(jugador);
        var destino = new Point(2, 2);

        bool resultado = ratha.Mover(destino, mapa);
        
        Assert.IsTrue(resultado);
        Assert.AreEqual(destino, ratha.Posicion);
    }

    [Test]
    public void Mover_PosicionInvalida_DevuelveFalse()
    {
        var ratha = new Ratha(jugador);
        var destino = new Point(-5, 100);

        bool resultado = ratha.Mover(destino, mapa);
        
        Assert.IsFalse(resultado);
    }

    [Test]
    public void AtacarUnidad_ContraArquero_BonusAplicado()
    {
        var ratha = new Ratha(jugador);
        var enemigo = new Arquero(new Jugador("Otro", jugador.Civilizacion)) { Salud = 50 };

        string resultado = ratha.AtacarUnidad(enemigo);
        
        Assert.IsTrue(resultado.Contains("Ataco a Arquero"));
        Assert.Less(enemigo.Salud, 50);
    }

    [Test]
    public void AtacarEdificio_DisminuyeVida()
    {
        var ratha = new Ratha(jugador);
        var casa = new Casa(new Jugador("Otro", jugador.Civilizacion));
        int vidaInicial = casa.Vida;

        string resultado = ratha.AtacarEdificio(casa);
        
        Assert.IsTrue(resultado.Contains("Ataco el edificio Casa"));
        Assert.Less(casa.Vida, vidaInicial);
    }
}