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
        new Civilizacion("bengalíes", new List<string> { "Al construir CC, +1 aldeano", "Caballeria +10 velocidad" }, "Ratha")
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
            return "No hay partida o mapa disponible.";
        return _partidaActual.Mapa.MostrarMapa(_partidaActual.Jugadores);
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
    }

    public string MostrarEstadoJugador(string nombreJugador)
    {
        Jugador jugadorEncontrado = null;

        foreach (Jugador j in _partidaActual.Jugadores)
        {
            if (j.Nombre == nombreJugador)
            {
                jugadorEncontrado = j;
                break;
            }
        }

        if (jugadorEncontrado == null)
        {
            return "Jugador" + nombreJugador + "no encontrado";
        }

        string resultado = "Estado del jugador: " + nombreJugador + "\n";
        resultado += "Recursos: \n";

        foreach (var recurso in jugadorEncontrado.Recursos)
        {
            resultado += " - " + recurso.Key + ": " + recurso.Value + "\n";
        }

        resultado += "\n Edificios:\n";
        foreach (var edificio in jugadorEncontrado.Edificios)
        {
            resultado += " - " + edificio.GetType().Name + " en (" + edificio.Posicion.X + ", " + edificio.Posicion.Y + ")\n";
        }

        resultado += "\n Unidades: \n";
        int i = 0;
        foreach (var unidad  in jugadorEncontrado.Unidades)
        {
            resultado += "  #" + i + " - " + unidad.GetType().Name + " en (" + unidad.Posicion.X + ", " + unidad.Posicion.Y + ") | Vida: " + unidad.Salud + "\n";
            i++;
        }

        return resultado;
    }
    public Dictionary<string, int> ObtenerRecursosJugador(string nombreJugador)
    {
        var jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        return jugador?.Recursos ?? new Dictionary<string, int>();
    }

    public void OrdenarRecolectar(string nombreJugador, int idAldeano, string nombreRecurso)
    {
        var jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        var aldeano = jugador?.Aldeanos.ElementAtOrDefault(idAldeano);

        if (aldeano != null && _partidaActual != null)
        {
            var recurso = _partidaActual.Mapa.Recursos
                .OfType<RecursoNatural>()
                .FirstOrDefault(r => r.Nombre == nombreRecurso && !r.EstaAgotado());

            if (recurso != null)
            {
                aldeano.Recolectar(recurso, null); 
            }
        }
    }

    public void ConstruirEdificio(string nombreJugador, string tipoEdificio, Point posicion)
    {
        if (_partidaActual == null)
            throw new InvalidOperationException("No hay partida activa.");

        var jugador = _partidaActual.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        
        if (jugador == null)
            throw new ArgumentException("Jugador no encontrado.");

        bool ocupado = _partidaActual.Jugadores
            .SelectMany(j => j.Edificios)
            .Any(e => e.Posicion.X == posicion.X && e.Posicion.Y == posicion.Y);

        if (ocupado)
            throw new InvalidOperationException("Ya hay un edificio en esa posición.");

        IEdificio? nuevoEdificio = null;
        
        switch (tipoEdificio.ToLower())
        {
            case "casa":
                
                if (jugador.Recursos["Madera"] < 50)
                    throw new InvalidOperationException("No hay suficiente madera para construir una casa.");
                nuevoEdificio = new Casa(jugador) { Posicion = posicion };
                
                jugador.Recursos["Madera"] -= 50;
                break;

            case "cuartel":
                
                if (jugador.Recursos["Madera"] < 100)
                    throw new InvalidOperationException("No hay suficiente madera para construir un cuartel.");
                
                nuevoEdificio = new Cuartel(jugador) { Posicion = posicion };
                jugador.Recursos["Madera"] -= 100;
                break;

            case "molino":
                
                if (jugador.Recursos["Madera"] < 75)
                    throw new InvalidOperationException("No hay suficiente madera para construir un molino.");
                
                nuevoEdificio = new Molino(jugador) { Posicion = posicion };        
                jugador.Recursos["Madera"] -= 75;
                break;

            case "depositomadera":
                
                if (jugador.Recursos["Madera"] < 60)
                    throw new InvalidOperationException("No hay suficiente madera para construir un depósito de madera.");
                
                nuevoEdificio = new DepositoMadera(jugador) { Posicion = posicion };
                jugador.Recursos["Madera"] -= 60;
                break;

            case "depositooro":
                
                if (jugador.Recursos["Madera"] < 60)
                    throw new InvalidOperationException("No hay suficiente madera para construir un depósito de oro.");
                
                nuevoEdificio = new DepositoOro(jugador) { Posicion = posicion };
                jugador.Recursos["Madera"] -= 60;
                break;

            case "depositopiedra":
                
                if (jugador.Recursos["Madera"] < 60)
                    throw new InvalidOperationException("No hay suficiente madera para construir un depósito de piedra.");
                
                nuevoEdificio = new DepositoPiedra(jugador) { Posicion = posicion };
                jugador.Recursos["Madera"] -= 60;
                break;

            case "centrocivico":
                
                if (jugador.Recursos["Madera"] < 200)
                    throw new InvalidOperationException("No hay suficiente madera para construir un centro cívico.");
                
                nuevoEdificio = new CentroCivico(jugador) { Posicion = posicion };
                jugador.Recursos["Madera"] -= 200;
                break;

            default:
                
                throw new ArgumentException("Tipo de edificio no válido.");
        } 
        jugador.AgregarEdificio(nuevoEdificio);
    }
    public void EntrenarUnidad(string nombreJugador, string tipoUnidad)
    {
        var jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);

        if (jugador != null && jugador.PoblacionActual < jugador.PoblacionMaxima)
        {
            var cuartel = jugador.Edificios.OfType<Cuartel>().FirstOrDefault();

            if (cuartel != null)
            {
                IUnidad? nuevaUnidad = null;

                switch (tipoUnidad.ToLower())
                {
                    case "aldeano":

                        if (jugador.PuedeCrearAldeano() && jugador.Recursos["Alimento"] >= 50)
                        {
                            var nuevoAldeano = new Aldeano(jugador) { Posicion = jugador.CentroCivico.Posicion };
                            jugador.Aldeanos.Add(nuevoAldeano);
                            jugador.Unidades.Add(nuevoAldeano);
                            jugador.Recursos["Alimento"] -= 50;
                            jugador.PoblacionActual++;
                        }

                        break;

                    case "infanteria":

                        if (jugador.Recursos["Alimento"] >= 60)
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

                        if (jugador.Recursos["Madera"] >= 70)
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

                        if (jugador.Recursos["Alimento"] >= 80 && jugador.Recursos["Madera"] >= 60)
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

    public void MoverUnidad(string nombreJugador, int idUnidad, Point destino)
    {
        var jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        var unidad = jugador?.Unidades.ElementAtOrDefault(idUnidad);

        if (unidad != null)
        {
            unidad.Mover(destino, _partidaActual?.Mapa);
        }
    }

    public string AtacarUnidad(string nombreJugador, int idUnidadAtacante, int idUnidadObjetivo)
    {
        var jugadorAtacante = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        var unidadAtacante = jugadorAtacante?.Unidades.ElementAtOrDefault(idUnidadAtacante);
        var unidadObjetivo = _partidaActual?.Jugadores.SelectMany(j => j.Unidades).ElementAtOrDefault(idUnidadObjetivo);

        if (unidadAtacante != null && unidadObjetivo != null && unidadAtacante.Propietario != unidadObjetivo.Propietario)
        {
            return unidadAtacante.AtacarU(unidadObjetivo);
        }
        
        return "Ataque fallido: unidad atacante o objetivo no válidas. No se pudo realizar el ataque.";
    }

    public List<IUnidad> ObtenerUnidadesJugador(string nombreJugador)
    {
        var jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        return jugador?.Unidades.ToList() ?? new List<IUnidad>();
    }

    public List<IEdificio> ObtenerEdificiosJugador(string nombreJugador)
    {
        var jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        return jugador?.Edificios.ToList() ?? new List<IEdificio>();
    }
}