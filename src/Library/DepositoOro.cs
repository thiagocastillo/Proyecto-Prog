namespace DefaultNamespace;

public class DepositoOro : IAlmacenamiento
{
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public int CapacidadMaxima { get; private set; } = 500;

    public DepositoOro(Jugador propietario)
    {
        Propietario = propietario;
    }
}