namespace DefaultNamespace;

public class JuegoFachada
{
    private Partida? _partidaActual;
    private List<Civilizacion> _civilizacionesDisponibles = new List<Civilizacion>()
    {
        new Civilizacion("Armenios", new List<string> { "Infantería +10 vida" }, "Arquero Compuesto"),
        new Civilizacion("Aztecas", new List<string> { "Aldeanos cargan +3 recursos", "Unidades militares +10% velocidad de creación" }, "Guerrero Jaguar"),
        new Civilizacion("Bengalíes", new List<string> { "Al construir CC, +1 aldeano" }, "Ratha")
    };

    public void CrearNuevaPartida(int tamañoMapa, int cantidadJugadores)
    {
        _partidaActual = new Partida(tamañoMapa, cantidadJugadores);
    }

    public List<string> ObtenerCivilizacionesDisponibles()
    {
        return _civilizacionesDisponibles.Select(c => c.Nombre).ToList();
    }

    public bool SeleccionarCivilizacion(string nombreJugador, string nombreCivilizacion)
    {
        if (_partidaActual == null) return false;
        var civilizacion = _civilizacionesDisponibles.FirstOrDefault(c => c.Nombre == nombreCivilizacion);
        if (civilizacion == null) return false;
        var jugador = _partidaActual.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        if (jugador != null)
        {
            jugador.Civilizacion = civilizacion;
            return true;
        }
        return false;
    }