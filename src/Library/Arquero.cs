namespace DefaultNamespace;

public class Arquero : IUnidadMilitar
{
    public Jugador Propietario { get; private set; }
    public int Ataque { get; private set; } = 8;
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
