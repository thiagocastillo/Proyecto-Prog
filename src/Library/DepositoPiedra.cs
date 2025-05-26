namespace Library;

public class DepositoPiedra : IAlmacenamiento
{
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public int CapacidadMaxima { get; private set; } = 500;

    public DepositoPiedra(Jugador propietario)
    {
        Propietario = propietario;
    }
}