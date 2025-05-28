namespace Library;

public class DepositoOro : IAlmacenamiento
{
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public int CapacidadMaxima { get; private set; } = 500;

    public DepositoOro(Jugador propietario)
    {
        Propietario = propietario;
    }
    
    public double Eficiencia(int distancia)
    {
        if (distancia <= 1) return 1.0;
        if (distancia >= 10) return 0.1;
        return 1.0 - (distancia * 0.1);
    }
}