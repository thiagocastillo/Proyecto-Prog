namespace Library.Tests;

public class RathaTests
{
    private Jugador jugador;
    private Mapa mapa;

    [SetUp]
    public void Setup()
    {
        Civilizacion civilizacion = new Civilizacion("Mayas", new List<string>(), "Ratha");
        jugador = new Jugador("Pedro", civilizacion);
        mapa = new Mapa();
    }

    [Test]
    public void Mover_PosicionValida_MueveCorrectamente()
    {
        Ratha ratha = new Ratha(jugador);
        Point destino = new Point(2, 2);

        bool resultado = ratha.Mover(destino, mapa);
        
        Assert.IsTrue(resultado); // Verifica que el movimiento fue exitoso
        Assert.AreEqual(destino, ratha.Posicion); // Verifica que la posicion de la unidad cambio al destino
    }

    [Test]
    public void Mover_PosicionInvalida_DevuelveFalse()
    {
        Ratha ratha = new Ratha(jugador);
        Point destino = new Point(-5, 100);

        bool resultado = ratha.Mover(destino, mapa);
        
        Assert.IsFalse(resultado); // Se espera que devuelva false por ser fuera del mapa
    }

    [Test]
    public void AtacarUnidad_ContraArquero_BonusAplicado()
    {
        Ratha ratha = new Ratha(jugador);
        Arquero enemigo = new Arquero(new Jugador("Otro", jugador.Civilizacion)) { Salud = 50 };

        string resultado = ratha.AtacarUnidad(enemigo);
        
        Assert.IsTrue(resultado.Contains("ataco a Arquero")); // Se espera que el mensaje contenga "Ataco a Arquero"
        Assert.Less(enemigo.Salud, 50); // Se espera que el arquero haya perdido salud
    }

    [Test]
    public void AtacarEdificio_DisminuyeVida()
    {
        Ratha ratha = new Ratha(jugador);
        Casa casa = new Casa(new Jugador("Otro", jugador.Civilizacion));
        int vidaInicial = casa.Vida;

        string resultado = ratha.AtacarEdificio(casa);
        
        Assert.IsTrue(resultado.Contains("ataco el edificio Casa")); // Se espera que el mensaje contenga el texto correcto
        Assert.Less(casa.Vida, vidaInicial); // Se espera que la vida del edificio haya disminuido
    }
}