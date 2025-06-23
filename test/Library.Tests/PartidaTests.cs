namespace Library.Tests;

public class PartidaTests
{
     private Partida partida;

    [SetUp]
    public void Setup()
    {
        partida = new Partida();
    }

    [Test]
    public void AgregarJugador_MenosDeDos()
    {
        Civilizacion civilizacion = new Civilizacion("Chinos", new List<string>(), "Samurai");
        Jugador jugador1 = new Jugador("Nicolas", civilizacion);
        string resultado = partida.AgregarJugador(jugador1);

        Assert.AreEqual("Jugador agregado correctamente", resultado);
        Assert.Contains(jugador1, partida.Jugadores);
    }

    [Test]
    public void AgregarJugador_TercerJugador_NoSePermitenMasDeDos() //Verifica que no haya mas de dos jugadores
    {
        Civilizacion civilizacion1 = new Civilizacion("Chinos", new List<string>(), "Samurai");
        Civilizacion civilizacion2 = new Civilizacion("Chinos", new List<string>(), "Samurai");
        Civilizacion civilizacion3 = new Civilizacion("Chinos", new List<string>(), "Samurai");
        
        Jugador jugador1 = new Jugador("Nicolas", civilizacion1);
        Jugador jugador2 = new Jugador("Tiago", civilizacion2);
        Jugador jugador3 = new Jugador("Nicolas", civilizacion3);

        partida.AgregarJugador(jugador1);
        partida.AgregarJugador(jugador2);
        string resultado = partida.AgregarJugador(jugador3);

        Assert.AreEqual("Solo se permiten dos jugadores", resultado);
        Assert.AreEqual(3, partida.Jugadores.Count); 
    }

    [Test]
    public void VerificarGanador_JugadorPierdeCentroCivico() //Gana el jugador que aun tenga su centro civico
    {
        Civilizacion civilizacion1 = new Civilizacion("Chinos", new List<string>(), "Samurai");
        Civilizacion civilizacion2 = new Civilizacion("Vikingos", new List<string>(), "Dragon");
        
        Jugador jugador1 = new Jugador("Nicolas", civilizacion1);
        jugador1.Edificios.Add(new CentroCivico(propietario:jugador1));

        Jugador jugador2 = new Jugador("Tiago", civilizacion2); // no tiene centro cívico

        partida.AgregarJugador(jugador1);
        partida.AgregarJugador(jugador2);

        Jugador ganador = partida.VerificarGanador();

        Assert.AreEqual(jugador1, ganador);
    }

    [Test]
    public void VerificarGanador_AmbosTienenCentroCivico() //No pueden ganar, ambos tienen centro civico
    {
        Civilizacion civilizacion1 = new Civilizacion("Chinos", new List<string>(), "Samurai");
        Civilizacion civilizacion2 = new Civilizacion("Vikingos", new List<string>(), "Dragon");
        
        Jugador jugador1 = new Jugador("Nicolas", civilizacion1);
        jugador1.Edificios.Add(new CentroCivico(propietario:jugador1));

        Jugador jugador2 = new Jugador("Tiago", civilizacion2);
        jugador2.Edificios.Add(new CentroCivico(propietario:jugador2));

        partida.AgregarJugador(jugador1);
        partida.AgregarJugador(jugador2);

        Jugador ganador = partida.VerificarGanador();

        Assert.IsNull(ganador);
    }

    [Test]
    public void VerificarGanador_NoHayGanador() //Los dos no tienen centro civico, no hay ganador
    {
        Civilizacion civilizacion1 = new Civilizacion("Chinos", new List<string>(), "Samurai");
        Civilizacion civilizacion2 = new Civilizacion("Vikingos", new List<string>(), "Samurai");
        
        Jugador jugador1 = new Jugador("Nicolas", civilizacion1); 
        Jugador jugador2 = new Jugador("Tiago", civilizacion2); 
        partida.AgregarJugador(jugador1);
        partida.AgregarJugador(jugador2);

        Jugador ganador = partida.VerificarGanador();

        Assert.IsNull(ganador);
    }
}