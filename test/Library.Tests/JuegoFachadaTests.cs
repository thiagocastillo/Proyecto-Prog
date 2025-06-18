namespace Library.Tests;

public class JuegoFachadaTests
{
    /*
    [Test]
    public void CrearNuevaPartida_InstanciaCorrecta()
    {
        var fachada = new JuegoFachada();

        fachada.CrearNuevaPartida();

        var jugadores = fachada.ObtenerJugadores();
        Assert.That(jugadores, Is.Not.Null);
    }
    
    [Test]
    public void ObtenerCivilizacionesDisponibles()
    {
        var fachada = new JuegoFachada();

        var civilizaciones = fachada.ObtenerCivilizacionesDisponibles();

        Assert.That(civilizaciones, Is.Not.Null.And.Not.Empty);
        Assert.Contains("armenios", civilizaciones);
    }
    
    [Test]
    public void AgregarJugadorAPartida_ConDatosValidos()
    {
        var fachada = new JuegoFachada();
        fachada.CrearNuevaPartida();

        fachada.AgregarJugadorAPartida("Carlos", "armenios");

        var jugadores = fachada.ObtenerJugadores();
        Assert.That(jugadores.Any(j => j.Nombre == "Carlos"), Is.True);
    }
    
    [Test]
    public void ObtenerRecursosJugadorExistente()
    {
        var fachada = new JuegoFachada();
        fachada.CrearNuevaPartida();
        fachada.AgregarJugadorAPartida("Carlos", "armenios");

        var recursos = fachada.ObtenerRecursosJugador("Carlos");

        Assert.That(recursos.ContainsKey("Madera"));
        Assert.That(recursos["Madera"], Is.EqualTo(100));
    }
    
    [Test]
    public void ConstruirEdificio_Casa_SeAgregaEdificioYDescuentaRecursos()
    {
        var fachada = new JuegoFachada();
        fachada.CrearNuevaPartida();
        fachada.AgregarJugadorAPartida("Carlos", "armenios");

        fachada.ConstruirEdificio("Carlos", "casa", new Point(5, 5));
    
        var edificios = fachada.ObtenerEdificiosJugador("Carlos");

        Assert.That(edificios.Any(e => e is Casa), Is.True);
        Assert.That(fachada.ObtenerRecursosJugador("Carlos")["Madera"], Is.EqualTo(50)); // 100 - 50
    }
    
    [Test]
    public void AtacarUnidad_UnidadAtacaAOtra_DevuelveMensajeDeCombate()
    {
        var fachada = new JuegoFachada();
        fachada.CrearNuevaPartida();

        fachada.AgregarJugadorAPartida("Jugador1", "armenios");
        fachada.AgregarJugadorAPartida("Jugador2", "aztecas");

        fachada.ConstruirEdificio("Jugador1", "cuartel", new Point(1, 1));
        fachada.EntrenarUnidad("Jugador1", "infanteria");

        fachada.ConstruirEdificio("Jugador2", "cuartel", new Point(2, 2));
        fachada.EntrenarUnidad("Jugador2", "infanteria");

        // Obtenemos índices correctos
        var unidadesJugador1 = fachada.ObtenerUnidadesJugador("Jugador1");
        var unidadesJugador2 = fachada.ObtenerUnidadesJugador("Jugador2");

        int idAtacante = fachada.ObtenerUnidadesJugador("Jugador1").Count - 1; // la última unidad creada
        int idObjetivoGlobal = fachada.ObtenerJugadores()[0].Unidades.Count + unidadesJugador2.Count - 1;

        // Ejecutamos ataque
        string resultado = fachada.AtacarUnidad("Jugador1", idAtacante, idObjetivoGlobal);

        Assert.That(resultado, Does.Contain("atacó a"));
    }
*/
}