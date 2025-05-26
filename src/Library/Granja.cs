namespace Library;

public class Granja : IAlmacenamiento
{
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public int CapacidadMaxima { get; private set; } = 500;

    public Granja(Jugador propietario)
    {
        Propietario = propietario;
    }
}  