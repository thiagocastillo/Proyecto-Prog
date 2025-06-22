namespace Library;

// Clase que representa la unidad especial Guerrero Jaguar, hereda de Infanteria
public class GuerreroJaguar : Infanteria, IUnidadMilitar
{
    // Constructor: inicializa el Guerrero Jaguar con su propietario
    public GuerreroJaguar(Jugador propietario) : base(propietario)
    {
    }

    // Calcula el daño que este Guerrero Jaguar inflige a otra unidad
    public double CalcularDaño(IUnidad objetivo)
    {
        double dañoBase = this.Ataque - objetivo.Defensa;
        // Bonificación: +5 de daño extra contra cualquier infantería que no sea otro Guerrero Jaguar
        if (objetivo is Infanteria && !(objetivo is GuerreroJaguar))
        {
            dañoBase += 5;
        }
        // El daño no puede ser negativo
        if (dañoBase < 0)
        {
            dañoBase = 0;
        }
        return dañoBase;
    }

    // Mueve la unidad a una nueva posición si es válida (sobrescribe el método base)
    public new bool Mover(Point destino, Mapa mapa)
    {
        // Verifica que el destino esté dentro de los límites del mapa
        if (destino.X < 0 || destino.X >= mapa.Ancho || destino.Y < 0 || destino.Y >= mapa.Alto)
        {
            return false;
        }
        // Asigna la nueva posición
        Posicion = destino;
        return true;
    }

    // Ataca unidades enemigas en una coordenada específica (sobrescribe el método base)
    public new string AtacarUnidad(Jugador atacante, string tipoUnidad, int cantidad, Point coordenada, Mapa mapa, List<Jugador> jugadores)
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

    // Ataca un edificio enemigo (sobrescribe el método base)
    public new string AtacarEdificio(IEdificio objetivo)
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