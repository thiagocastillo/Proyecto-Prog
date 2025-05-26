namespace DefaultNamespace;

public class Casa : IEdificio
{
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public const int AumentoPoblacion = 5;

    public Casa(Jugador propietario)
    {
        Propietario = propietario;
        propietario.AumentarPoblacionMaxima(AumentoPoblacion);
    }
}