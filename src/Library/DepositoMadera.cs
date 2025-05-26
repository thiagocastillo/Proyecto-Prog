namespace Library;

public class DepositoMadera : IAlmacenamiento
{
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public int CapacidadMaxima { get; private set; } = 500;

    public DepositoMadera(Jugador propietario)
    {
        Propietario = propietario;
    }
}