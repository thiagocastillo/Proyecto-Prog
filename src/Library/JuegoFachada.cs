namespace Library;

using System.Collections.Generic;
using System.Linq;

public class JuegoFachada
{
    private Partida? _partidaActual;
    private List<Civilizacion> _civilizacionesDisponibles = new List<Civilizacion>()
    {
        new Civilizacion("Armenios", new List<string> { "Infantería +10 vida", "Arqueros +10 ataque" }, "Arquero Compuesto"),
        new Civilizacion("Aztecas", new List<string> { "Aldeanos cargan +3 recursos", "Unidades militares +10% velocidad de creación" }, "Guerrero Jaguar"),
        new Civilizacion("Bengalíes", new List<string> { "Al construir CC, +1 aldeano", "Caballeria +10 velocidad" }, "Ratha")
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

    //public Dictionary<Recurso.TipoRecurso, int> ObtenerRecursosJugador(string nombreJugador)
    public Dictionary<string, int> ObtenerRecursosJugador(string nombreJugador)
    {
        var jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        //return jugador?.Recursos ?? new Dictionary<Recurso.TipoRecurso, int>();
        return jugador?.Recursos ?? new Dictionary<string, int>();
    }

    //public void OrdenarRecolectar(string nombreJugador, int idAldeano, Recurso.TipoRecurso tipoRecurso)
    public void OrdenarRecolectar(string nombreJugador, int idAldeano, ITipoRecurso tipoRecurso)
    {
        var jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        var aldeano = jugador?.Aldeanos.ElementAtOrDefault(idAldeano);
        if (aldeano != null)
        {
            aldeano.Recolectar(tipoRecurso, null); // Necesitaríamos lógica para el almacén
        }
    }

    public void ConstruirEdificio(string nombreJugador, string tipoEdificio, Point posicion)
    {
        var jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        if (jugador != null)
        {
            IEdificio? nuevoEdificio = null;
            switch (tipoEdificio.ToLower())
            {
                case "casa":
                    //if (jugador.Recursos[Recurso.TipoRecurso.Madera] >= 50)
                    if (jugador.Recursos["Madera"] >= 50)
                    {
                        nuevoEdificio = new Casa(jugador) { Posicion = posicion };
                        //jugador.Recursos[Recurso.TipoRecurso.Madera] -= 50;
                        jugador.Recursos["Madera"] -= 50;
                    }
                    break;
                case "cuartel":
                    //if (jugador.Recursos[Recurso.TipoRecurso.Madera] >= 100)
                    if(jugador.Recursos["Madera"] >= 100)
                    {
                        nuevoEdificio = new Cuartel(jugador) { Posicion = posicion };
                        //jugador.Recursos[Recurso.TipoRecurso.Madera] -= 100;
                        jugador.Recursos["Madera"] -= 100;
                    }
                    break;
                
                case "molino":
                    //if (jugador.Recursos[Recurso.TipoRecurso.Madera] >= 75)
                    if (jugador.Recursos["Madera"] >= 75)
                    {
                        nuevoEdificio = new Molino(jugador) { Posicion = posicion };
                        //jugador.Recursos[Recurso.TipoRecurso.Madera] -= 75;
                        jugador.Recursos["Madera"] -= 75;
                    }
                    break;
                
                case "depositomadera":
                    
                    //if (jugador.Recursos[Recurso.TipoRecurso.Madera] >= 60)
                    if (jugador.Recursos["Madera"] >= 60)
                    {
                        nuevoEdificio = new DepositoMadera(jugador) { Posicion = posicion };
                        //jugador.Recursos[Recurso.TipoRecurso.Madera] -= 60;
                        jugador.Recursos["Madera"] -= 60;
                    }
                    break;
                
                case "depositooro":
                    //if (jugador.Recursos[Recurso.TipoRecurso.Madera] >= 60)
                    if (jugador.Recursos["Madera"] >= 60)
                    {
                        nuevoEdificio = new DepositoOro(jugador) { Posicion = posicion };
                        //jugador.Recursos[Recurso.TipoRecurso.Madera] -= 60;
                        jugador.Recursos["Madera"] -= 60;
                    }
                    break;
                
                case "depositopiedra":
                    
                    //if (jugador.Recursos[Recurso.TipoRecurso.Madera] >= 60)
                    if (jugador.Recursos["Madera"] >= 60)
                    {
                        nuevoEdificio = new DepositoPiedra(jugador) { Posicion = posicion };
                        //jugador.Recursos[Recurso.TipoRecurso.Madera] -= 60;
                        jugador.Recursos["Madera"] -= 60;
                    }
                    break;
                
                case "centroCivico":
                    
                    //if (jugador.Recursos[Recurso.TipoRecurso.Madera] >= 200)
                    if (jugador.Recursos["Madera"] >= 200)
                    {
                        nuevoEdificio = new CentroCivico(jugador) { Posicion = posicion };
                        //jugador.Recursos[Recurso.TipoRecurso.Madera] -= 200;
                        jugador.Recursos["Madera"] -= 200;
                    }
                    break;
                // Añadir más casos para otros edificios
            }
            
            if (nuevoEdificio != null)
            {
                jugador.AgregarEdificio(nuevoEdificio);
            }
            else
            {
                // Manejar el caso de no poder construir (recursos insuficientes, etc.)
            }
        }
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
                        //if (jugador.PuedeCrearAldeano() && jugador.Recursos[Recurso.TipoRecurso.Alimento] >= 50)
                        if(jugador.PuedeCrearAldeano() && jugador.Recursos["Alimento"] >= 50)
                        {
                            var nuevoAldeano = new Aldeano(jugador) { Posicion = jugador.CentroCivico.Posicion };
                            jugador.Aldeanos.Add(nuevoAldeano);
                            jugador.Unidades.Add(nuevoAldeano);
                            //nuevaUnidad = new Aldeano(jugador) { Posicion = jugador.CentroCivico.Posicion };  
                            //LA LINEA DE ARRIBA NOS PUEDE CAGAR SI EL JUGADOR NO LLEGA A TENER CENTRO CIVICO X ALGUNA RAZON
                            //LO PODEMOS VER DE HACER IGUAL, PENSAMOS OTRA MANERA, LO VEMOS!
                            jugador.Recursos["Alimento"] -= 50;
                            jugador.PoblacionActual++;
                        }
                        break;
                    
                    case "infanteria":
                        
                        //if (jugador.Recursos[Recurso.TipoRecurso.Alimento] >= 60)
                        if (jugador.Recursos["Alimento"] >= 60)
                        {
                            nuevaUnidad = new Infanteria(jugador) { Posicion = cuartel.Posicion };
                            if (jugador.Civilizacion.UnidadEspecial == "Guerrero Jaguar")
                            {
                                nuevaUnidad = new GuerreroJaguar(jugador) { Posicion = cuartel.Posicion };
                            }
                            //jugador.Recursos[Recurso.TipoRecurso.Alimento] -= 60;
                            jugador.Recursos["Alimento"] -= 60;
                        }
                        break;
                    
                    case "arquero":
                       
                        //if (jugador.Recursos[Recurso.TipoRecurso.Madera] >= 70)
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
                        
                        //if (jugador.Recursos[Recurso.TipoRecurso.Alimento] >= 80 && jugador.Recursos[Recurso.TipoRecurso.Madera] >= 60)
                        if (jugador.Recursos["Alimento"] >= 80 && jugador.Recursos["Madera"] >= 60)
                        {
                            nuevaUnidad = new Caballeria(jugador) { Posicion = cuartel.Posicion };
                            if (jugador.Civilizacion.UnidadEspecial == "Ratha")
                            {
                                nuevaUnidad = new Ratha(jugador) { Posicion = cuartel.Posicion };
                            }
                            //jugador.Recursos[Recurso.TipoRecurso.Alimento] -= 80;
                            //jugador.Recursos[Recurso.TipoRecurso.Madera] -= 60;
                            jugador.Recursos["Alimento"] -= 80;
                            jugador.Recursos["Madera"] -= 60;
                        }
                        break;
                    // Añadir más unidades si las hubiera
                }
                if (nuevaUnidad != null)
                {
                    jugador.AgregarUnidad(nuevaUnidad);
                }
                else
                {
                    // Manejar el caso de no poder entrenar (recursos, sin cuartel, población)
                }
            }
            else
            {
                // Informar que se necesita un cuartel
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

    public void AtacarUnidad(string nombreJugador, int idUnidadAtacante, int idUnidadObjetivo)
    {
        var jugadorAtacante = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        var unidadAtacante = jugadorAtacante?.Unidades.ElementAtOrDefault(idUnidadAtacante);
        var unidadObjetivo = _partidaActual?.Jugadores.SelectMany(j => j.Unidades).ElementAtOrDefault(idUnidadObjetivo); // Buscar en todas las unidades

        if (unidadAtacante != null && unidadObjetivo != null && unidadAtacante.Propietario != unidadObjetivo.Propietario)
        {
            unidadAtacante.AtacarU(unidadObjetivo);
            // Aquí podríamos añadir lógica para ver si la unidad objetivo fue destruida
        }
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

    // Métodos adicionales según las historias de usuario (visualizar mapa, etc.) irían aquí
}