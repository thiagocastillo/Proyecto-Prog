namespace Library;

using System.Collections.Generic;
using System.Linq;

public class JuegoFachada
{
    private Partida? _partidaActual;
    private List<Civilizacion> _civilizacionesDisponibles = new List<Civilizacion>()
    {
        new Civilizacion("armenios", new List<string> { "Infantería +10 vida", "Arqueros +10 ataque" }, "Arquero Compuesto"),
        new Civilizacion("aztecas", new List<string> { "Aldeanos cargan +3 recursos", "Unidades militares +10% velocidad de creación" }, "Guerrero Jaguar"),
        new Civilizacion("bengalies", new List<string> { "Al construir CC, +1 aldeano", "Caballeria +10 velocidad" }, "Ratha")
    };

    public void CrearNuevaPartida()
    {
        _partidaActual = new Partida();
    }

    public List<string> ObtenerCivilizacionesDisponibles()
    {
        return _civilizacionesDisponibles.Select(c => c.Nombre).ToList();
    }
    public string MostrarMapa()
    {
        if (_partidaActual == null || _partidaActual.Mapa == null)
            return "No hay partida o mapa disponible. Use 'crearpartida' antes de mostrar el mapa.";
        return _partidaActual.Mapa.MostrarMapa(_partidaActual.Jugadores);
    }
    public string ListarRecursos()
    {
        if (_partidaActual == null || _partidaActual.Mapa == null || _partidaActual.Mapa.Recursos == null || !_partidaActual.Mapa.Recursos.Any())
            return "No hay recursos en el mapa.";

        var sb = new System.Text.StringBuilder();
        foreach (var recurso in _partidaActual.Mapa.Recursos)
        {
            sb.AppendLine($"{recurso.Nombre} en ({recurso.Ubicacion.X}, {recurso.Ubicacion.Y})");
        }
        return sb.ToString().TrimEnd();
    }

    public List<Jugador> ObtenerJugadores()
    {
        return _partidaActual?.Jugadores!;
    }
    
    public void AgregarJugadorAPartida(string nombreJugador, string nombreCivilizacion)
    {
        if (_partidaActual != null)
        {
            var civilizacion = _civilizacionesDisponibles.FirstOrDefault(c => c.Nombre == nombreCivilizacion);
            
            if (civilizacion != null)
            {
                _partidaActual.AgregarJugador(new Jugador(nombreJugador, civilizacion));
            }
        }
        else
        {
            throw new InvalidOperationException("No hay partida activa al momento.");
        }
    }
    
    public Dictionary<string, int> ObtenerRecursosJugador(string nombreJugador)
    {
        Jugador jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        return jugador?.ObtenerResumenRecursosTotales() ?? new Dictionary<string, int>();
    }

    public void OrdenarRecolectar(string nombreJugador, int idAldeano, int x, int y)
    {
        Jugador jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        Aldeano aldeano = jugador?.Aldeanos.ElementAtOrDefault(idAldeano);

        if (aldeano != null && _partidaActual != null)
        {
            aldeano.RecolectarEn(new Point(x, y), _partidaActual.Mapa);
        }
    }    
    
    
    public void ConstruirEdificio(string nombreJugador, string tipoEdificio, Point posicion)
    {
        if (_partidaActual == null)
            throw new InvalidOperationException("No hay partida activa.");

        Jugador jugador = _partidaActual.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);

        if (jugador == null)
            throw new ArgumentException("Jugador no encontrado.");

        bool ocupado = _partidaActual.Jugadores
            .SelectMany(j => j.Edificios)
            .Any(e => e.Posicion.X == posicion.X && e.Posicion.Y == posicion.Y);

        if (ocupado)
            throw new InvalidOperationException("Ya hay un edificio en esa posición.");

        var recursosTotales = jugador.ObtenerResumenRecursosTotales();
        IEdificio? nuevoEdificio = null;

        switch (tipoEdificio.ToLower())
        {
            case "casa":
                if (!recursosTotales.ContainsKey("madera") || recursosTotales["madera"] < 50)
                    throw new InvalidOperationException("No hay suficiente madera para construir una casa.");
                nuevoEdificio = new Casa(jugador) { Posicion = posicion };
                jugador.Recursos["Madera"] -= 50;
                break;

            case "cuartel":
                if (!recursosTotales.ContainsKey("madera") || recursosTotales["madera"] < 100)
                    throw new InvalidOperationException("No hay suficiente madera para construir un cuartel.");
                nuevoEdificio = new Cuartel(jugador) { Posicion = posicion };
                jugador.Recursos["Madera"] -= 100;
                break;

            case "molino":
                if (!recursosTotales.ContainsKey("madera") || recursosTotales["madera"] < 75)
                    throw new InvalidOperationException("No hay suficiente madera para construir un molino.");
                nuevoEdificio = new Molino(jugador) { Posicion = posicion };
                jugador.Recursos["Madera"] -= 75;
                break;

            case "depositomadera":
                if (!recursosTotales.ContainsKey("madera") || recursosTotales["madera"] < 60)
                    throw new InvalidOperationException("No hay suficiente madera para construir un depósito de madera.");
                nuevoEdificio = new DepositoMadera(jugador) { Posicion = posicion };
                jugador.Recursos["Madera"] -= 60;
                break;

            case "depositooro":
                if (!recursosTotales.ContainsKey("madera") || recursosTotales["madera"] < 60)
                    throw new InvalidOperationException("No hay suficiente madera para construir un depósito de oro.");
                nuevoEdificio = new DepositoOro(jugador) { Posicion = posicion };
                jugador.Recursos["Madera"] -= 60;
                break;

            case "depositopiedra":
                if (!recursosTotales.ContainsKey("madera") || recursosTotales["madera"] < 60)
                    throw new InvalidOperationException("No hay suficiente madera para construir un depósito de piedra.");
                nuevoEdificio = new DepositoPiedra(jugador) { Posicion = posicion };
                jugador.Recursos["Madera"] -= 60;
                break;

            case "centrocivico":
                if (!recursosTotales.ContainsKey("madera") || recursosTotales["madera"] < 200)
                    throw new InvalidOperationException("No hay suficiente madera para construir un centro cívico.");
                nuevoEdificio = new CentroCivico(jugador) { Posicion = posicion };
                jugador.Recursos["Madera"] -= 200;
                break;

            default:
                throw new ArgumentException("Tipo de edificio no válido.");
        }
        jugador.AgregarEdificio(nuevoEdificio);
    }    
public void EntrenarUnidad(string nombreJugador, string tipoUnidad, Point posicion)
{
    if (_partidaActual == null)
        throw new InvalidOperationException("No hay partida activa.");

    Jugador jugador = _partidaActual.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);

    if (jugador == null)
        throw new ArgumentException("Jugador no encontrado.");

    // Verifica si la posición está ocupada por otra unidad
    bool ocupado = _partidaActual.Jugadores
        .SelectMany(j => j.Unidades)
        .Any(u => u.Posicion.X == posicion.X && u.Posicion.Y == posicion.Y);

    if (ocupado)
        throw new InvalidOperationException("Ya hay una unidad en esa posición.");

    var recursosTotales = jugador.ObtenerResumenRecursosTotales();
    IUnidad? nuevaUnidad = null;

    switch (tipoUnidad.ToLower())
    {
        case "aldeano":
            if (jugador.PuedeCrearAldeano() && recursosTotales.ContainsKey("Alimento") && recursosTotales["Alimento"] >= 50)
            {
                var nuevoAldeano = new Aldeano(jugador) { Posicion = posicion };
                jugador.Aldeanos.Add(nuevoAldeano);
                jugador.Unidades.Add(nuevoAldeano);
                jugador.Recursos["Alimento"] -= 50;
                jugador.PoblacionActual++;
            }
            break;

        case "guerrerojaguar":
            if (recursosTotales.ContainsKey("Alimento") && recursosTotales["Alimento"] >= 60)
            {
                nuevaUnidad = new GuerreroJaguar(jugador) { Posicion = posicion };
                jugador.Recursos["Alimento"] -= 60;
            }
            break;

        case "arquerocompuesto":
            if (recursosTotales.ContainsKey("Madera") && recursosTotales["Madera"] >= 70)
            {
                nuevaUnidad = new ArqueroCompuesto(jugador) { Posicion = posicion };
                jugador.Recursos["Madera"] -= 70;
            }
            break;

        case "ratha":
            if (recursosTotales.ContainsKey("Alimento") && recursosTotales["Alimento"] >= 80 &&
                recursosTotales.ContainsKey("Madera") && recursosTotales["Madera"] >= 60)
            {
                nuevaUnidad = new Ratha(jugador) { Posicion = posicion };
                jugador.Recursos["Alimento"] -= 80;
                jugador.Recursos["Madera"] -= 60;
            }
            break;

        case "infanteria":
            if (recursosTotales.ContainsKey("Alimento") && recursosTotales["Alimento"] >= 60)
            {
                nuevaUnidad = new Infanteria(jugador) { Posicion = posicion };
                jugador.Recursos["Alimento"] -= 60;
            }
            break;

        case "arquero":
            if (recursosTotales.ContainsKey("Madera") && recursosTotales["Madera"] >= 70)
            {
                nuevaUnidad = new Arquero(jugador) { Posicion = posicion };
                jugador.Recursos["Madera"] -= 70;
            }
            break;

        case "caballeria":
            if (recursosTotales.ContainsKey("Alimento") && recursosTotales["Alimento"] >= 80 &&
                recursosTotales.ContainsKey("Madera") && recursosTotales["Madera"] >= 60)
            {
                nuevaUnidad = new Caballeria(jugador) { Posicion = posicion };
                jugador.Recursos["Alimento"] -= 80;
                jugador.Recursos["Madera"] -= 60;
            }
            break;
    }

    if (nuevaUnidad != null)
    {
        jugador.Unidades.Add(nuevaUnidad);
        jugador.PoblacionActual++;
    }
}private Point BuscarPosicionLibreCercana(Point origen, Jugador jugador)
{
    var adyacentes = new List<Point>
    {
        new Point { X = origen.X + 1, Y = origen.Y },
        new Point { X = origen.X - 1, Y = origen.Y },
        new Point { X = origen.X, Y = origen.Y + 1 },
        new Point { X = origen.X, Y = origen.Y - 1 },
        new Point { X = origen.X + 1, Y = origen.Y + 1 },
        new Point { X = origen.X - 1, Y = origen.Y - 1 },
        new Point { X = origen.X + 1, Y = origen.Y - 1 },
        new Point { X = origen.X - 1, Y = origen.Y + 1 }
    };
    var ocupadas = jugador.Unidades.Select(u => u.Posicion).ToHashSet();
    return adyacentes.FirstOrDefault(p => !ocupadas.Contains(p), origen);
}   
public void MoverUnidad(string nombreJugador, int idUnidad, Point destino)
    {
        Jugador jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        IUnidad unidad = jugador?.Unidades.ElementAtOrDefault(idUnidad);

        if (unidad != null)
        {
            unidad.Mover(destino, _partidaActual?.Mapa);
        }
    }

    public string AtacarUnidad(string nombreJugador, int idUnidadAtacante, string tipoUnidad, int cantidad, Point coordenada)
    {
        var jugadorAtacante = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        var unidadAtacante = jugadorAtacante?.Unidades.ElementAtOrDefault(idUnidadAtacante) as IUnidadMilitar;

        if (jugadorAtacante != null && unidadAtacante != null)
        {
            return unidadAtacante.AtacarUnidad(
                jugadorAtacante,
                tipoUnidad,
                cantidad,
                coordenada,
                _partidaActual.Mapa,
                _partidaActual.Jugadores
            );
        }

        return "Ataque fallido: unidad atacante no válida. No se pudo realizar el ataque.";
    }

    public string AtacarEdificio(string nombreJugadorAtacante, int idUnidadAtacante, string nombreJugadorObjetivo, int idEdificioObjetivo)
    {
        var jugadorAtacante = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugadorAtacante);
        var unidadAtacante = jugadorAtacante?.Unidades.ElementAtOrDefault(idUnidadAtacante) as IUnidadMilitar;

        var jugadorObjetivo = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugadorObjetivo);
        var edificioObjetivo = jugadorObjetivo?.Edificios.ElementAtOrDefault(idEdificioObjetivo);

        if (unidadAtacante != null && edificioObjetivo != null && unidadAtacante.Propietario != edificioObjetivo.Propietario)
        {
            string resultado = unidadAtacante.AtacarEdificio(edificioObjetivo);

            // Si el edificio destruido es un CentroCivico
            if (edificioObjetivo is CentroCivico && edificioObjetivo.Vida <= 0)
            {
                var jugadoresConCC = _partidaActual.Jugadores
                    .Where(j => j.Edificios.Any(e => e is CentroCivico && e.Vida > 0))
                    .ToList();

                if (jugadoresConCC.Count == 1)
                {
                    resultado += $"\n¡{jugadoresConCC[0].Nombre} ganó la partida! Muchas gracias por jugar.";
                }
            }

            return resultado;
        }

        return "Ataque fallido: unidad atacante o edificio objetivo no válidos. No se pudo realizar el ataque.";
    }    public List<IUnidad> ObtenerUnidadesJugador(string nombreJugador)
    {
        try
        {
            Jugador jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);

            if (nombreJugador != jugador.Nombre || jugador == null)
                throw new ArgumentException("Jugador no encontrado.");
            
            return jugador?.Unidades.ToList() ?? new List<IUnidad>();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error al obtener las unidades del jugador.", ex);
        }
    }

    public List<IEdificio> ObtenerEdificiosJugador(string nombreJugador)
    {
        if(_partidaActual == null)
            throw new InvalidOperationException("No hay partida activa, cree una con el comando correspondiente.");
        
        if (_partidaActual.Jugadores.Count == 0)
            throw new InvalidOperationException("No hay jugadores en la partida actual, cree al menos uno con el comando correspondiente.");
        
        Jugador jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        
        return jugador?.Edificios.ToList() ?? new List<IEdificio>();
    }
}