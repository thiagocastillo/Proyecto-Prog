namespace Library.Tests;

public class JuegoFachadaTests
{
    /*
    [Test]
    public void CrearNuevaPartida_InstanciaCorrecta()
    {
        JuegoFachada fachada = new JuegoFachada();

        fachada.CrearNuevaPartida();

        List<Jugador> jugadores = fachada.ObtenerJugadores();
        Assert.That(jugadores, Is.Not.Null);
    }
    
    [Test]
    public void ObtenerCivilizacionesDisponibles()
    {
        JuegoFachada fachada = new JuegoFachada();

        var civilizaciones = fachada.ObtenerCivilizacionesDisponibles();

        Assert.That(civilizaciones, Is.Not.Null.And.Not.Empty);
        Assert.Contains("armenios", civilizaciones);
    }
    
    [Test]
    public void AgregarJugadorAPartida_ConDatosValidos()
    {
        JuegoFachada fachada = new JuegoFachada();
        fachada.CrearNuevaPartida();

        fachada.AgregarJugadorAPartida("Carlos", "armenios");

        List<Jugador> jugadores = fachada.ObtenerJugadores();
        Assert.That(jugadores.Any(j => j.Nombre == "Carlos"), Is.True);
    }
    
    [Test]
    public void ObtenerRecursosJugadorExistente()
    {
        JuegoFachada fachada = new JuegoFachada();
        fachada.CrearNuevaPartida();
        fachada.AgregarJugadorAPartida("Carlos", "armenios");

        Dictionary<string, int> recursos = fachada.ObtenerRecursosJugador("Carlos");

        Assert.That(recursos.ContainsKey("Madera"));
        Assert.That(recursos["Madera"], Is.EqualTo(100));
    }
    
    [Test]
    public void ConstruirEdificio_Casa_SeAgregaEdificioYDescuentaRecursos()
    {
        JuegoFachada fachada = new JuegoFachada();
        fachada.CrearNuevaPartida();
        fachada.AgregarJugadorAPartida("Carlos", "armenios");

        fachada.ConstruirEdificio("Carlos", "casa", new Point(5, 5));
    
        List<IEdificio> edificios = fachada.ObtenerEdificiosJugador("Carlos");

        Assert.That(edificios.Any(e => e is Casa), Is.True);
        Assert.That(fachada.ObtenerRecursosJugador("Carlos")["Madera"], Is.EqualTo(50)); // 100 - 50
    }
    
    [Test]
    public void AtacarUnidad_UnidadAtacaAOtra_DevuelveMensajeDeCombate()
    {
        JuegoFachada fachada = new JuegoFachada();
        fachada.CrearNuevaPartida();

        fachada.AgregarJugadorAPartida("Jugador1", "armenios");
        fachada.AgregarJugadorAPartida("Jugador2", "aztecas");

        fachada.ConstruirEdificio("Jugador1", "cuartel", new Point(1, 1));
        fachada.EntrenarUnidad("Jugador1", "infanteria");

        fachada.ConstruirEdificio("Jugador2", "cuartel", new Point(2, 2));
        fachada.EntrenarUnidad("Jugador2", "infanteria");

        // Obtenemos índices correctos
        List<IUnidad> unidadesJugador1 = fachada.ObtenerUnidadesJugador("Jugador1");
        List<IUnidad> unidadesJugador2 = fachada.ObtenerUnidadesJugador("Jugador2");

        int idAtacante = fachada.ObtenerUnidadesJugador("Jugador1").Count - 1; // la última unidad creada
        int idObjetivoGlobal = fachada.ObtenerJugadores()[0].Unidades.Count + unidadesJugador2.Count - 1;

        // Ejecutamos ataque
        string resultado = fachada.AtacarUnidad("Jugador1", idAtacante, idObjetivoGlobal);

        Assert.That(resultado, Does.Contain("atacó a"));
    }
*/
}