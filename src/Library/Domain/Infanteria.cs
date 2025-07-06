namespace Library.Domain;

// Clase que representa la unidad de Infantería, implementa la interfaz IUnidadMilitar
public class Infanteria : IUnidadMilitar
{
    // Propietario de la unidad
    public Jugador Propietario { get; private set; }
    // Valor de ataque de la unidad
    public int Ataque { get; private set; } = 10000;
    // Valor de defensa de la unidad
    public int Defensa { get; private set; } = 5;
    // Velocidad de movimiento
    public double Velocidad { get; private set; } = 1.0;
    // Salud actual de la unidad
    public int Salud { get; set; } = 100;
    // Posición actual en el mapa
    public Point Posicion { get; set; }
    // Tiempo necesario para crear la unidad
    public int TiempoDeCreacion { get; private set; } = 10;
    
    // Constructor: inicializa la unidad y la posiciona cerca del Centro Cívico
    public Infanteria(Jugador propietario)
    {
        Propietario = propietario;

        // Obtiene la posición del Centro Cívico del jugador
        var ccPos = propietario.CentroCivico.Posicion;
        // Calcula posiciones adyacentes al Centro Cívico
        var adyacentes = new List<Point>
        {
            new Point { X = ccPos.X + 1, Y = ccPos.Y },
            new Point { X = ccPos.X - 1, Y = ccPos.Y },
            new Point { X = ccPos.X, Y = ccPos.Y + 1 },
            new Point { X = ccPos.X, Y = ccPos.Y - 1 },
            new Point { X = ccPos.X + 1, Y = ccPos.Y + 1 },
            new Point { X = ccPos.X - 1, Y = ccPos.Y - 1 },
            new Point { X = ccPos.X + 1, Y = ccPos.Y - 1 },
            new Point { X = ccPos.X - 1, Y = ccPos.Y + 1 }
        };

        // Obtiene las posiciones ya ocupadas por otras unidades del jugador
        var ocupadas = propietario.Unidades.Select(u => u.Posicion).ToHashSet();

        // Asigna la primera posición adyacente libre, o el Centro Cívico si todas están ocupadas
        this.Posicion = adyacentes.FirstOrDefault(p => !ocupadas.Contains(p), ccPos);

        // Si la civilización es "Armenios", aumenta la salud
        if (propietario.Civilizacion.Nombre == "Armenios")
        {
            Salud = Salud + 20; 
        }
    }

    // Calcula el daño que esta unidad inflige a otra unidad
    public double CalcularDaño(IUnidad objetivo)
    {
        double dañoBase = this.Ataque - objetivo.Defensa;
        // Hace más daño a caballería
        if (objetivo is Caballeria)
        {
            dañoBase += 2;
        }
        // El daño no puede ser negativo
        if (dañoBase < 0)
        {
            dañoBase = 0;
        }
        return dañoBase;
    }

    // Mueve la unidad a una nueva posición si es válida
    public bool Mover(Point destino, Mapa mapa)
    {
        try
        {
            if(mapa == null)
            {
                throw new ArgumentNullException(nameof(mapa), "El mapa no puede ser nulo.");
            }
            if (destino == null)
            {
                throw new ArgumentNullException(nameof(destino), "El destino no puede ser nulo.");
            }
            // Verifica que el destino esté dentro de los límites del mapa
            if (destino.X < 0 || destino.X >= mapa.Ancho || destino.Y < 0 || destino.Y >= mapa.Alto)
            {
                throw new ArgumentOutOfRangeException("Destino fuera de los límites del mapa.");
                return false;
            }
            // Asigna la nueva posición
            Posicion = destino;
            return true;
        }
        catch(Exception e)
        {
            throw new InvalidOperationException("No se pudo mover la unidad.", e);
            return false;
        }
    }

    // Ataca unidades enemigas en una coordenada específica
    public string AtacarUnidad(Jugador atacante, string tipoUnidad, int cantidad, Point coordenada, Mapa mapa, List<Jugador> jugadores)
    {
        // Busca unidades enemigas del tipo indicado en la coordenada
        var unidadesEnCoordenada = mapa.ObtenerUnidadesEn(coordenada, jugadores)
            .Where(u => u.Propietario != atacante && u.GetType().Name.ToLower() == tipoUnidad.ToLower())
            .Take(cantidad)
            .ToList();

        if (!unidadesEnCoordenada.Any())
            return $"No se encontraron unidades de tipo {tipoUnidad} en la coordenada ({coordenada.X},{coordenada.Y}).";

        string resultado = "";
        
        foreach (var unidad in unidadesEnCoordenada)
        {
            int daño = (int)CalcularDaño(unidad);
            unidad.Salud -= daño;
            resultado += $"{GetType().Name} atacó a {unidad.GetType().Name} causando {daño} de daño. Salud restante: {Math.Max(0, unidad.Salud)}.";
            // Si la unidad muere, se elimina de la lista del propietario
            if (unidad.Salud <= 0)
            {
                unidad.Propietario.Unidades.Remove(unidad);
                resultado += " La unidad fue destruida.";
            }
            resultado += "\n";
        }
        return resultado;
    }
   
    // Ataca un edificio enemigo
    public string AtacarEdificio(IEdificio objetivo)
    {
        int daño = this.Ataque;
        objetivo.Vida -= daño;
        string info = $"{GetType().Name} atacó el edificio {objetivo.GetType().Name} causando {daño} de daño. Vida restante del edificio: {Math.Max(0, objetivo.Vida)}.";
        // Si el edificio es destruido, se elimina de la lista del propietario
        if (objetivo.Vida <= 0)
        {
            objetivo.Propietario.Edificios.Remove(objetivo);
            info += " El edificio fue destruido.";
        }
        return info;
    }
}