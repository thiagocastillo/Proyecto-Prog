namespace Library;

public class Infanteria : IUnidadMilitar
{
    public Jugador Propietario { get; private set; }
    public int Ataque { get; private set; } = 100000;
    public int Defensa { get; private set; } = 5;
    
    public double Velocidad { get; private set; } = 1.0;
    
    public int Salud { get; set; } = 100;
    public Point Posicion { get; set; }

    public int TiempoDeCreacion { get; private set; } = 10;
    
    public Infanteria(Jugador propietario)
    {
        Propietario = propietario;

        var ccPos = propietario.CentroCivico.Posicion;
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

        var ocupadas = propietario.Unidades.Select(u => u.Posicion).ToHashSet();

        this.Posicion = adyacentes.FirstOrDefault(p => !ocupadas.Contains(p), ccPos);

        if (propietario.Civilizacion.Nombre == "Armenios")
        {
            Salud= Salud + 20; 
        }
    }

    public double CalcularDaño(IUnidad objetivo)
    {
        double dañoBase = this.Ataque - objetivo.Defensa;
        if (objetivo is Caballeria)
        {
            dañoBase += 2;
        }

        if (dañoBase < 0)
        {
            dañoBase = 0;
        }

        return dañoBase;
    }
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
            
            if (destino.X < 0 || destino.X >= mapa.Ancho || destino.Y < 0 || destino.Y >= mapa.Alto)
            {
                throw new ArgumentOutOfRangeException("Destino fuera de los límites del mapa.");
                return false;
            }

            Posicion = destino;
            return true;
        }
        catch(Exception e)
        {
            throw new InvalidOperationException("No se pudo mover la unidad.", e);
            return false;
        }
    }

    
    public string AtacarUnidad(Jugador atacante, string tipoUnidad, int cantidad, Point coordenada, Mapa mapa, List<Jugador> jugadores)
    {
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
            if (unidad.Salud <= 0)
            {
                unidad.Propietario.Unidades.Remove(unidad);
                resultado += " La unidad fue destruida.";
            }
            resultado += "\n";
        }
        return resultado;
    }
   
    public string AtacarEdificio(IEdificio objetivo)
    {
        int daño = this.Ataque;
        objetivo.Vida -= daño;
        string info = $"{GetType().Name} atacó el edificio {objetivo.GetType().Name} causando {daño} de daño. Vida restante del edificio: {Math.Max(0, objetivo.Vida)}.";
        if (objetivo.Vida <= 0)
        {
            objetivo.Propietario.Edificios.Remove(objetivo);
            info += " El edificio fue destruido.";
        }
        return info;
    }
}