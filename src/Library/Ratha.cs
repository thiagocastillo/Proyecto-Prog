namespace DefaultNamespace;

public class Ratha : IUnidadMilitar
{
    public Jugador Propietario { get; private set; }
    public int Ataque { get; private set; } = 10;
    public int Defensa { get; private set; } = 6;
    public double Velocidad { get; private set; } = 1.6;
    public Point Posicion { get; set; }

    public Ratha(Jugador propietario)
    {
        Propietario = propietario;
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
