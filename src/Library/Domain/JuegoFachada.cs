namespace Library.Domain;
using System.Collections.Generic;
using System.Linq;

// Fachada principal para gestionar la lógica del juego y exponer operaciones de alto nivel
public class JuegoFachada
{
    // Referencia a la partida actual en curso
    private Partida? _partidaActual;
    private static readonly JuegoFachada _instancia = new JuegoFachada();
    public static JuegoFachada Instancia => _instancia;
    // Lista de civilizaciones disponibles para elegir al crear jugadores
    private List<Civilizacion> _civilizacionesDisponibles = new List<Civilizacion>()
    {
        new Civilizacion("armenios", new List<string> { "Infantería +10 vida", "Arqueros +10 ataque" }, "Arquero Compuesto"),
        new Civilizacion("aztecas", new List<string> { "Aldeanos cargan +3 recursos", "Unidades militares +10% velocidad de creación" }, "Guerrero Jaguar"),
        new Civilizacion("bengalies", new List<string> { "Al construir CC, +1 aldeano", "Caballeria +10 velocidad" }, "Ratha")
    };

    // Crea una nueva partida
    public void CrearNuevaPartida()
    {
        _partidaActual = new Partida();
    }

    // Devuelve los nombres de las civilizaciones disponibles
    public List<string> ObtenerCivilizacionesDisponibles()
    {
        return _civilizacionesDisponibles.Select(c => c.Nombre).ToList();
    }

    // Muestra el mapa actual de la partida
    public string MostrarMapaTXT()
    {
        if (_partidaActual == null || _partidaActual.Mapa == null)
            return "No hay partida o mapa disponible. Use 'crearpartida' antes de mostrar el Mapa.";
        
        return _partidaActual.Mapa.MostrarMapaTXT(_partidaActual.Jugadores);
    }
    public string MostrarMapaHTML()
    {
        if (_partidaActual == null || _partidaActual.Mapa == null)
            return "No hay partida o mapa disponible. Use 'crearpartida' antes de mostrar el Mapa.";
        
        return _partidaActual.Mapa.MostrarMapaHtml(_partidaActual.Jugadores);
    }
    
    // Lista todos los recursos presentes en el mapa
    /*public string ListarRecursos()
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
*/
    // Devuelve la lista de jugadores de la partida actual
    public List<Jugador> ObtenerJugadores()
    {
        return _partidaActual?.Jugadores!;
    }
    
    // Agrega un nuevo jugador a la partida con la civilización elegida
    public void AgregarJugadorAPartida(string nombreJugador, string nombreCivilizacion)
    {
        if (_partidaActual != null)
        {
            if(_partidaActual.Jugadores.Any(j => j.Nombre.Equals(nombreJugador, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Ya existe un jugador con el nombre '{nombreJugador}'.");
            
            // Busca la civilización por nombre, ignorando mayusculas/minusculas dentro de la lista de civilizaciones disponibles
            Civilizacion civilizacion = _civilizacionesDisponibles.FirstOrDefault(c => c.Nombre.Equals(nombreCivilizacion, StringComparison.OrdinalIgnoreCase));

            // Si la civilización no es válida, lanza una excepción
            if (civilizacion == null)
                throw new ArgumentException($"La civilización '{nombreCivilizacion}' no es válida.");
            
            if (civilizacion != null)
            {
                if (_partidaActual.Jugadores.Count >= 2)
                {
                    throw new InvalidOperationException("Solo se permiten dos jugadores en la partida.");
                }
                _partidaActual.AgregarJugador(new Jugador(nombreJugador, civilizacion));
            }
        }
        else
        {
            throw new InvalidOperationException("No hay partida activa al momento. Cree una con el comando 'CrearPartida'.");
        }
    }
    
    // Devuelve el resumen de recursos de un jugador
    public Dictionary<string, int> ObtenerRecursosJugador(string nombreJugador)
    {
        Jugador jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        return jugador?.ObtenerResumenRecursosTotales() ?? new Dictionary<string, int>();
    }

    // Ordena a un aldeano recolectar recursos en una posición específica
    public void OrdenarRecolectar(string nombreJugador, int idAldeano, int x, int y)
    {
        Jugador jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        Aldeano aldeano = jugador?.Aldeanos.ElementAtOrDefault(idAldeano);

        if (aldeano != null && _partidaActual != null)
        {
            aldeano.RecolectarEn(new Point(x, y), _partidaActual.Mapa);
        }
    }    
    
    // Permite a un jugador construir un edificio en una posición si tiene recursos y la posición está libre
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

    Dictionary<string, int> recursosTotales = jugador.ObtenerResumenRecursosTotales();
    IEdificio? nuevoEdificio = null;

    switch (tipoEdificio.ToLower())
    {
        case "casa":
            if (!recursosTotales.ContainsKey("Madera") || recursosTotales["Madera"] < 50)
                throw new InvalidOperationException("No hay suficiente Madera para construir una casa.");
            jugador.Recursos["Madera"] -= 50;
            var casa = new Casa(jugador) { Posicion = posicion };
            jugador.EdificiosEnConstruccion.Add(casa);
            // No agregar a Edificios aún
            break;

        case "cuartel":
            if (!recursosTotales.ContainsKey("Madera") || recursosTotales["Madera"] < 100)
                throw new InvalidOperationException("No hay suficiente Madera para construir un cuartel.");
            nuevoEdificio = new Cuartel(jugador) { Posicion = posicion };
            jugador.Recursos["Madera"] -= 100;
            break;

        case "molino":
            if (!recursosTotales.ContainsKey("Madera") || recursosTotales["Madera"] < 75)
                throw new InvalidOperationException("No hay suficiente Madera para construir un molino.");
            nuevoEdificio = new Molino(jugador) { Posicion = posicion };
            jugador.Recursos["Madera"] -= 75;
            break;

        case "depositomadera":
            if (!recursosTotales.ContainsKey("Madera") || recursosTotales["Madera"] < 60)
                throw new InvalidOperationException("No hay suficiente Madera para construir un depósito de madera.");
            nuevoEdificio = new DepositoMadera(jugador) { Posicion = posicion };
            jugador.Recursos["Madera"] -= 60;
            break;

        case "depositooro":
            if (!recursosTotales.ContainsKey("Madera") || recursosTotales["Madera"] < 60)
                throw new InvalidOperationException("No hay suficiente Madera para construir un depósito de oro.");
            nuevoEdificio = new DepositoOro(jugador) { Posicion = posicion };
            jugador.Recursos["Madera"] -= 60;
            break;

        case "depositopiedra":
            if (!recursosTotales.ContainsKey("Madera") || recursosTotales["Madera"] < 60)
                throw new InvalidOperationException("No hay suficiente Madera para construir un depósito de piedra.");
            nuevoEdificio = new DepositoPiedra(jugador) { Posicion = posicion };
            jugador.Recursos["Madera"] -= 60;
            break;

        case "centrocivico":
            if (!recursosTotales.ContainsKey("Madera") || recursosTotales["Madera"] < 200)
                throw new InvalidOperationException("No hay suficiente Madera para construir un centro cívico.");
            nuevoEdificio = new CentroCivico(jugador) { Posicion = posicion };
            jugador.Recursos["Madera"] -= 200;
            break;

        default:
            throw new ArgumentException("Tipo de edificio no válido.");
    }

    // Solo agrega a la lista principal si no es una casa (las casas van a EdificiosEnConstruccion)
    if (nuevoEdificio != null)
    {
        jugador.AgregarEdificio(nuevoEdificio);
    }
}
    // Permite a un jugador entrenar una unidad en una posición si tiene recursos y la posición está libre
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

        // Selecciona el tipo de unidad y descuenta recursos
        switch (tipoUnidad.ToLower())
        {
            case "aldeano":
                
                if (jugador.PuedeCrearAldeano() && recursosTotales.ContainsKey("Alimento") && recursosTotales["Alimento"] >= 50)
                {
                    //var nuevoAldeano = new Aldeano(jugador) { Posicion = posicion };
                    Aldeano nuevoAldeano = new Aldeano(jugador) { Posicion = posicion };
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
    }

    // Busca una posición libre adyacente a un punto dado para colocar una unidad o edificio
    private Point BuscarPosicionLibreCercana(Point origen, Jugador jugador)
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

    // Mueve una unidad de un jugador a una nueva posición
    public void MoverUnidad(string nombreJugador, int idUnidad, Point destino)
    {
        Jugador jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
        IUnidad unidad = jugador?.Unidades.ElementAtOrDefault(idUnidad);

        if(jugador == null)
            throw new ArgumentException($"El jugador '{nombreJugador}' no existe en la partida actual.");
        
        if (unidad == null)
            throw new ArgumentException($"La unidad con ID {idUnidad} no existe para el jugador '{nombreJugador}'.");
        
        if (unidad != null)
        {
            unidad.Mover(destino, _partidaActual?.Mapa);
        }
    }
    
    public List<string> MoverUnidades(string nombreJugador, List<int> idsUnidades, Point destino)
    {
        // Verifica que la partida actual y el jugador existan
        Jugador jugador = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);

        // Si no se encuentra el jugador, lanza una excepción
        if (jugador == null)
            throw new ArgumentException("Jugador no encontrado.");

        // Si no hay unidades, devuelve un mensaje
        List<string> mensaje = new();
        int xDestino = destino.X;
        int yDestino = destino.Y;

        //Itera sobre los IDs de unidades y mueve cada una a la posición indicada
        for (int i = 0; i < idsUnidades.Count; i++)
        {
            int id = idsUnidades[i];

            // Verifica que el ID de unidad sea válido
            if (id < 0 || id >= jugador.Unidades.Count)
            {
                // Si el ID no es válido, agrega un mensaje de error y continúa con la siguiente unidad
                mensaje.Add($"Unidad con ID {id} no encontrada.");
                continue;
            }

            var unidad = jugador.Unidades[id];
            
            //Calcula la nueva posición destino sumando el índice al destino original evitando superposiciones
            Point destinoUnitario = new Point(xDestino + i, yDestino);
            
            //Intenta mover la unidad a la nueva posición
            bool exito = unidad.Mover(destinoUnitario, _partidaActual.Mapa);
            
            // Agrega un mensaje de éxito o error según el resultado del movimiento
            mensaje.Add(exito
                ? $"Unidad {unidad.GetType().Name} (ID {id}) movida a ({destinoUnitario.X},{destinoUnitario.Y})."
                : $"Unidad {unidad.GetType().Name} (ID {id}) no pudo moverse a ({destinoUnitario.X},{destinoUnitario.Y}).");
        }
        // Devuelve la lista de mensajes generados durante el movimiento
        return mensaje;
    }
    
   public string AtacarUnidad(string nombreJugadorAtacante, int idUnidadAtacante, string nombreJugadorObjetivo, int idUnidadObjetivo)
   {
       // Obtener jugador atacante y su unidad
       var jugadorAtacante = _partidaActual?.Jugadores
           .FirstOrDefault(j => j.Nombre.Equals(nombreJugadorAtacante, StringComparison.OrdinalIgnoreCase));
       var unidadAtacante = jugadorAtacante?.Unidades
           .ElementAtOrDefault(idUnidadAtacante) as IUnidadMilitar;

       // Obtener jugador objetivo y la unidad objetivo
       var jugadorObjetivo = _partidaActual?.Jugadores
           .FirstOrDefault(j => j.Nombre.Equals(nombreJugadorObjetivo, StringComparison.OrdinalIgnoreCase));
       var unidadObjetivo = jugadorObjetivo?.Unidades
           .ElementAtOrDefault(idUnidadObjetivo);

       // Validar que las unidades existan, sean de distintos jugadores, y que la atacante sea militar
       if (unidadAtacante != null && unidadObjetivo != null && unidadAtacante.Propietario != unidadObjetivo.Propietario)
       {
           // Calcular daño según el sistema de ventajas
           int daño = (int)unidadAtacante.CalcularDaño(unidadObjetivo);
           unidadObjetivo.Salud -= daño;

           string resultado = $"{unidadAtacante.GetType().Name} atacó a {unidadObjetivo.GetType().Name} causando {daño} de daño. " +
                              $"Salud restante: {Math.Max(0, unidadObjetivo.Salud)}.";

           // Si la unidad fue destruida, eliminarla
           if (unidadObjetivo.Salud <= 0)
           {
               jugadorObjetivo.Unidades.Remove(unidadObjetivo);
               resultado += " La unidad fue destruida.";
           }
           return resultado;
       }
       return "Ataque fallido: unidad atacante o objetivo no válidas. No se pudo realizar el ataque.";
   }
   
    // Ordena a una unidad militar atacar un edificio enemigo
    public string AtacarEdificio(string nombreJugadorAtacante, int idUnidadAtacante, string nombreJugadorObjetivo, int idEdificioObjetivo)
    {
        var jugadorAtacante = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugadorAtacante);
        var unidadAtacante = jugadorAtacante?.Unidades.ElementAtOrDefault(idUnidadAtacante) as IUnidadMilitar;

        var jugadorObjetivo = _partidaActual?.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugadorObjetivo);
        var edificioObjetivo = jugadorObjetivo?.Edificios.ElementAtOrDefault(idEdificioObjetivo);

        if (unidadAtacante != null && edificioObjetivo != null && unidadAtacante.Propietario != edificioObjetivo.Propietario)
        {
            string resultado = unidadAtacante.AtacarEdificio(edificioObjetivo);
            
            // Notificación si el CentroCivico está cerca de ser destruido
            if (edificioObjetivo is CentroCivico cc)
            {
                double porcentajeVida = (double)cc.Vida / cc.VidaMaxima;
                if (porcentajeVida > 0 && porcentajeVida <= 0.20)  // Si la vida es menor o igual al 20% y aun no está destruido -> advertencia
                {
                    resultado += $"\n¡Atención! El Centro Cívico de {jugadorObjetivo.Nombre} está a punto de ser destruido ({cc.Vida} de {cc.VidaMaxima} de vida).";
                }
            }
            
            // Si el edificio destruido es un CentroCivico, verifica si hay un ganador
            if (edificioObjetivo is CentroCivico && edificioObjetivo.Vida <= 0)
            {
                var jugadoresConCC = _partidaActual.Jugadores
                    .Where(j => j.Edificios.Any(e => e is CentroCivico && e.Vida > 0))
                    .ToList();
                    
                if (jugadoresConCC.Count == 1)
                {
                    resultado += $"\n¡{jugadoresConCC[0].Nombre} ganó la partida! Muchas gracias por jugar.";
                    //_partidaActual = null; // Termina la partida
                    
                }
            }
            return resultado;
        }
        return "Ataque fallido: unidad atacante o edificio objetivo no válidos. No se pudo realizar el ataque.";
    }

    // Devuelve la lista de unidades de un jugador
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

    // Devuelve la lista de edificios de un jugador
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