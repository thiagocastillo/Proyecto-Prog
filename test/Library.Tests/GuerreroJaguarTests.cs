namespace Library.Domain.Tests;

public class GuerreroJaguarTests
{
    private Jugador jugadorAzteca;
    private Jugador jugadorRival;
    private GuerreroJaguar guerreroJaguar;
    private Mapa mapa;

    [SetUp]
    public void SetUp()
    {
        Civilizacion civilizacionAzteca = new Civilizacion("aztecas", new List<string>(), "Guerrero Jaguar");
        jugadorAzteca = new Jugador("Azteca", civilizacionAzteca);

        Civilizacion civilizacionRival = new Civilizacion("armenios", new List<string>(), "Arquero Compuesto");
        jugadorRival = new Jugador("Rival", civilizacionRival);

        guerreroJaguar = new GuerreroJaguar(jugadorAzteca) { Posicion = new Point(0, 0), Salud = 100 };
        mapa = new Mapa();
    }

    [Test]
    public void Mover_DestinoValido_MueveCorrectamente()
    {
        Point destino = new Point(10, 10);

        bool resultado = guerreroJaguar.Mover(destino, mapa);

        Assert.IsTrue(resultado);
        Assert.That(guerreroJaguar.Posicion.X, Is.EqualTo(10));
        Assert.That(guerreroJaguar.Posicion.Y, Is.EqualTo(10));
    }
    
    [Test]
    public void AtacarUnidad_ContraCaballeria_AplicaBonus()
    {
        Caballeria caballeria = new Caballeria(jugadorRival) { Salud = 100, Posicion = new Point(1, 1) };
        jugadorRival.Unidades.Add(caballeria);
        string resultado = guerreroJaguar.AtacarUnidad(
                                                        jugadorAzteca,
                                                "caballeria",
                                                 1,
                                                        caballeria.Posicion,
                                                        mapa,
                                                new List<Jugador> { jugadorAzteca, jugadorRival }
        );

        Assert.That(caballeria.Salud, Is.LessThan(100));
        Assert.IsTrue(resultado.Contains("GuerreroJaguar atacó a Caballeria"));
    }

    [Test]
    public void AtacarUnidad_ContraInfanteriaAztecaGuerreroJaguar_AplicaBonus()
    {
        Infanteria infanteriaObjetivo = new Infanteria(jugadorRival) { Salud = 100, Posicion = new Point(2, 2) };
        jugadorRival.Unidades.Add(infanteriaObjetivo);

        string resultado = guerreroJaguar.AtacarUnidad(
            jugadorAzteca,
            "infanteria",
            1,
            infanteriaObjetivo.Posicion,
            mapa,
            new List<Jugador> { jugadorAzteca, jugadorRival }
        );

        Assert.That(infanteriaObjetivo.Salud, Is.LessThan(100));
        Assert.IsTrue(resultado.Contains("GuerreroJaguar atacó"));
    }

    [Test]
    public void AtacarEdificio_ContraEdificio_CausaDaño()
    {
        Casa casa = new Casa(jugadorRival) { Vida = 1000 };

        string resultado = guerreroJaguar.AtacarEdificio(casa);

        Assert.That(casa.Vida, Is.LessThan(1000));
        Assert.IsTrue(resultado.Contains("atacó el edificio"));
    }

    [Test]
    public void AtacarUnidad_UnidadEliminada_SeEliminaDeLista()
    {
        Arquero objetivo = new Arquero(jugadorRival) { Salud = 1, Posicion = new Point(3, 3) };
        jugadorRival.Unidades.Add(objetivo);

        string resultado = guerreroJaguar.AtacarUnidad(
            jugadorAzteca,
            "arquero",
            1,
            objetivo.Posicion,
            mapa,
            new List<Jugador> { jugadorAzteca, jugadorRival }
        );

        Assert.IsFalse(jugadorRival.Unidades.Contains(objetivo));
        Assert.IsTrue(resultado.Contains("fue destruida"));
    }
}
