namespace Library.Domain;

public class Arquero : IUnidadMilitar
{
    public Jugador Propietario { get; private set; }
    public int Ataque { get; set; } = 300; 
    public int Defensa { get; set; } = 3;
    public double Velocidad { get; private set; } = 1.2;
    public int Salud { get; set; }
    public Point Posicion { get; set; }

    // Nuevo: tiempo de generación asociado a la unidad
    public TiempoDeGeneracion TiempoGeneracion { get; private set; }

    // Implementación requerida por la interfaz
    public int TiempoDeCreacion => TiempoGeneracion.TiempoTotalSegundos;

    public Arquero(Jugador propietario)
    {
        Propietario = propietario;
        TiempoGeneracion = new TiempoDeGeneracion(15);

        if (propietario.Civilizacion.Nombre == "Armenios" && propietario.Civilizacion.UnidadEspecial == "Arquero Compuesto")
        {
            Ataque += 2;
        }
    }

    public double CalcularDaño(IUnidad objetivo)
    {
        double dañoBase = Math.Max(0, this.Ataque - objetivo.Defensa);
        if (objetivo is Infanteria)
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
        if (destino.X < 0 || destino.X >= mapa.Ancho || destino.Y < 0 || destino.Y >= mapa.Alto)
        {
            return false; 
        }
        Posicion = destino;
        return true;
    }

    public string AtacarUnidad(Jugador atacante, string tipoUnidad, int cantidad, Point coordenada, Mapa mapa, List<Jugador> jugadores)
    {
        List<IUnidad> unidadesEnCoordenada = mapa.ObtenerUnidadesEn(coordenada, jugadores)
            .Where(u => u.Propietario != atacante && u.GetType().Name.ToLower() == tipoUnidad.ToLower())
            .Take(cantidad)
            .ToList();

        if (!unidadesEnCoordenada.Any())
        {
            return $"No se encontraron unidades de tipo {tipoUnidad} en la coordenada ({coordenada.X},{coordenada.Y}).";
        }

        string resultado = "";
        foreach (IUnidad unidad in unidadesEnCoordenada)
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