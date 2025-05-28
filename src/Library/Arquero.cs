namespace Library;


public class Arquero : IUnidadMilitar
{
    public Jugador Propietario { get; private set; }
    public int Ataque { get; set; } = 8; // Añadimos 'set;'
    public int Defensa { get; private set; } = 3;
    public double Velocidad { get; private set; } = 1.2;
    public Point Posicion { get; set; }

    public Arquero(Jugador propietario)
    {
        Propietario = propietario;
        if (propietario.Civilizacion.Nombre == "Armenios" && propietario.Civilizacion.UnidadEspecial == "Arquero Compuesto")
        {
            Ataque += 2;
        }
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

    public void AtacarU(IUnidad objetivo)
    {
        int daño = Ataque - objetivo.Defensa;
        // Registrar daño
    }
    public void AtacarE(IEdificio objetivo)
    {
        int daño = Ataque - objetivo.Vida;
        // Registrar daño
    }
}