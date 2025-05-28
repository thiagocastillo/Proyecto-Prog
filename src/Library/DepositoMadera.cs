namespace Library;

public class DepositoMadera : IAlmacenamiento
{
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public int CapacidadMaxima { get; private set; } = 500;
    public int Vida { get; set; }

    public DepositoMadera(Jugador propietario)
    {
        Propietario = propietario;
        Vida= 5000;
        
    }
    
    public double Eficiencia(int distancia)
    {
        if (distancia <= 1) return 1.0;
        if (distancia >= 10) return 0.1;
        return 1.0 - (distancia * 0.1);
    }
}