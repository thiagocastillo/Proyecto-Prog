namespace Library;

using System.Collections.Generic;
using System.Linq;

public class JuegoFachada
{
    // Instancia de la partida actual en juego
    private Partida? _partidaActual;
    // Lista de civilizaciones disponibles al momento de crear jugadores
    private List<Civilizacion> _civilizacionesDisponibles = new List<Civilizacion>()
    {
        new Civilizacion("armenios", new List<string> { "Infantería +10 vida", "Arqueros +10 ataque" }, "Arquero Compuesto"),
        new Civilizacion("aztecas", new List<string> { "Aldeanos cargan +3 recursos", "Unidades militares +10% velocidad de creación" }, "Guerrero Jaguar"),
        new Civilizacion("bengalies", new List<string> { "Al construir CC, +1 aldeano", "Caballeria +10 velocidad" }, "Ratha")
    };

    // Inicializa una nueva partida
    public void CrearNuevaPartida()
    {
        _partidaActual = new Partida();
    }

    // Devuelve la lista de nombres de civilizaciones disponibles
    public List<string> ObtenerCivilizacionesDisponibles()
    {
        return _civilizacionesDisponibles.Select(c => c.Nombre).ToList();
    }
    
    // Devuelve el estado visual del mapa, si existe
    public string MostrarMapa()
    {
        if (_partidaActual == null || _partidaActual.Mapa == null)
            return "No hay partida o mapa disponible. Use 'crearpartida' antes de mostrar el mapa.";
        return _partidaActual.Mapa.MostrarMapa(_partidaActual.Jugadores);
    }
    
    // Lista todos los recursos que hay en el mapa con sus ubicaciones
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

    // Devuelve la lista de jugadores en la partida actual
    public List<Jugador> ObtenerJugadores()
    {
        return _partidaActual?.Jugadores!;
    }
    
    // Agrega un nuevo jugador a la partida con la civilizacion especificada
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
    
    // Devuelve los recursos actuales de un jugador
    public Dictionary<string, int> ObtenerRecursosJugador(string nombreJugador)
    {
        Jugador jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        return jugador?.ObtenerResumenRecursosTotales() ?? new Dictionary<string, int>();
    }

    // Ordena a un aldeano especifico recolectar recursos en una ubicacion
    public void OrdenarRecolectar(string nombreJugador, int idAldeano, int x, int y)
    {
        Jugador jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        Aldeano aldeano = jugador?.Aldeanos.ElementAtOrDefault(idAldeano);

        if (aldeano != null && _partidaActual != null)
        {
            aldeano.RecolectarEn(new Point(x, y), _partidaActual.Mapa);
        }
    }    
    
    // Construye un edificio en una posicion si el jugador tiene recursos suficientes
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
        // Determina el tipo de edificio y verifica recursos
        switch (tipoEdificio.ToLower())
        {
            case "casa":
                if (!recursosTotales.ContainsKey("madera") || recursosTotales["madera"] < 50)
                    throw new InvalidOperationException("No hay suficiente madera para construir una casa.");
                nuevoEdificio = new Casa(jugador) { Posicion = posicion };
                jugador.Recursos["Madera"] -= 50;
                break;
            // Casos similares para cuartel, molino, depositos ...
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
    
    // Entrena una unidad del tipo indicado, si hay poblacion y recursos
    public void EntrenarUnidad(string nombreJugador, string tipoUnidad)
    {
        Jugador jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);

        if (jugador != null && jugador.PoblacionActual < jugador.PoblacionMaxima)
        {
            Cuartel cuartel = jugador.Edificios.OfType<Cuartel>().FirstOrDefault();

            if (cuartel != null)
            {
                var recursosTotales = jugador.ObtenerResumenRecursosTotales();
                IUnidad? nuevaUnidad = null;
                // Entrena unidades segun tipo y civilizacion especial
                switch (tipoUnidad.ToLower())
                {
                    case "aldeano":
                        if (jugador.PuedeCrearAldeano() && recursosTotales.ContainsKey("Alimento") && recursosTotales["Alimento"] >= 50)
                        {
                            Aldeano nuevoAldeano = new Aldeano(jugador) { Posicion = jugador.CentroCivico.Posicion };
                            jugador.Aldeanos.Add(nuevoAldeano);
                            jugador.Unidades.Add(nuevoAldeano);
                            jugador.Recursos["Alimento"] -= 50;
                            jugador.PoblacionActual++;
                        }
                        break;
                    // Casos para infanteria, arquero, caballeria ...

                    case "infanteria":
                        if (recursosTotales.ContainsKey("Alimento") && recursosTotales["Alimento"] >= 60)
                        {
                            nuevaUnidad = new Infanteria(jugador) { Posicion = cuartel.Posicion };
                            if (jugador.Civilizacion.UnidadEspecial == "Guerrero Jaguar")
                            {
                                nuevaUnidad = new GuerreroJaguar(jugador) { Posicion = cuartel.Posicion };
                            }
                            jugador.Recursos["Alimento"] -= 60;
                        }
                        break;

                    case "arquero":
                        if (recursosTotales.ContainsKey("Madera") && recursosTotales["Madera"] >= 70)
                        {
                            nuevaUnidad = new Arquero(jugador) { Posicion = cuartel.Posicion };
                            if (jugador.Civilizacion.UnidadEspecial == "Arquero Compuesto")
                            {
                                nuevaUnidad = new ArqueroCompuesto(jugador) { Posicion = cuartel.Posicion };
                            }
                            jugador.Recursos["Madera"] -= 70;
                        }
                        break;

                    case "caballeria":
                        if (recursosTotales.ContainsKey("Alimento") && recursosTotales["Alimento"] >= 80 &&
                            recursosTotales.ContainsKey("Madera") && recursosTotales["Madera"] >= 60)
                        {
                            nuevaUnidad = new Caballeria(jugador) { Posicion = cuartel.Posicion };
                            if (jugador.Civilizacion.UnidadEspecial == "Ratha")
                            {
                                nuevaUnidad = new Ratha(jugador) { Posicion = cuartel.Posicion };
                            }
                            jugador.Recursos["Alimento"] -= 80;
                            jugador.Recursos["Madera"] -= 60;
                        }
                        break;
                }

                if (nuevaUnidad != null)
                {
                    jugador.AgregarUnidad(nuevaUnidad);
                }
            }
        }
    }
    // Mueve una unidad del jugador a una posicion especifica
    public void MoverUnidad(string nombreJugador, int idUnidad, Point destino)
    {
        Jugador jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        IUnidad unidad = jugador?.Unidades.ElementAtOrDefault(idUnidad);

        if (unidad != null)
        {
            unidad.Mover(destino, _partidaActual?.Mapa);
        }
    }

    // Ordena a una unidad atacar a otra unidad enemiga
    public string AtacarUnidad(string nombreJugador, int idUnidadAtacante, int idUnidadObjetivo)
    {
        Jugador jugadorAtacante = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        IUnidad unidadAtacante = jugadorAtacante?.Unidades.ElementAtOrDefault(idUnidadAtacante);
        IUnidad unidadObjetivo = _partidaActual?.Jugadores.SelectMany(j => j.Unidades).ElementAtOrDefault(idUnidadObjetivo);

        if (unidadAtacante != null && unidadObjetivo != null && unidadAtacante.Propietario != unidadObjetivo.Propietario)
        {
            return unidadAtacante.AtacarUnidad(unidadObjetivo);
        }
        
        return "Ataque fallido: unidad atacante o objetivo no válidas. No se pudo realizar el ataque.";
    }
    
    // Ordena a una unidad atacar un edificio enemigo
    public string AtacarEdificio(string nombreJugador, int idUnidadAtacante, int idEdificioObjetivo)
    {
        Jugador jugadorAtacante = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        IUnidad unidadAtacante = jugadorAtacante?.Unidades.ElementAtOrDefault(idUnidadAtacante);
        IEdificio edificioObjetivo = _partidaActual?.Jugadores.SelectMany(j => j.Edificios).ElementAtOrDefault(idEdificioObjetivo);

        if (unidadAtacante != null && edificioObjetivo != null && unidadAtacante.Propietario != edificioObjetivo.Propietario)
        {
            return unidadAtacante.AtacarEdificio(edificioObjetivo);
        }
        
        return "Ataque fallido: unidad atacante o edificio objetivo no válidos. No se pudo realizar el ataque.";
    }

    // Devuelve todas las unidades del jugador
    public List<IUnidad> ObtenerUnidadesJugador(string nombreJugador)
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

    // Devuelve todos los edificios del jugador
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