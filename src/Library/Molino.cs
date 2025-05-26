namespace DefaultNamespace;

public class Molino : IAlmacenamiento
{
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public int CapacidadMaxima { get; private set; } = 500;

    public Molino(Jugador propietario)
    {
        Propietario = propietario;
    }
}   