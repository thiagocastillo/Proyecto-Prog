namespace Library;


public class Arquero : IUnidadMilitar
{
    public Jugador Propietario { get; private set; }
    public int Ataque { get; set; } = 8; // Añadimos 'set;'
    public int Defensa { get; private set; } = 3;
    public double Velocidad { get; private set; } = 1.2;
    public Point Posicion { get; set; }
    
    public int TiempoDeCreacion { get; private set; } = 10;

    public Arquero(Jugador propietario)
    {
        Propietario = propietario;
        if (propietario.Civilizacion.Nombre == "Armenios" && propietario.Civilizacion.UnidadEspecial == "Arquero Compuesto")
        {
            Ataque += 2;
        }
    }

    public void Mover(Point destino)
    {
        Posicion = destino;
    }

    public void Atacar(IUnidad objetivo)
    {
        int daño = Ataque - objetivo.Defensa;
        // Registrar daño
    }
}