namespace Library;

public class Molino : IAlmacenamiento
{
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public int CapacidadMaxima { get; private set; } = 500;
    public int Vida { get; set; }

    public Molino(Jugador propietario)
    {
        Propietario = propietario;
        Vida = 10000;
    }
    
    public double Eficiencia(int distancia)
    {
        if (distancia <= 1) return 1.0;
        if (distancia >= 10) return 0.1;
        return 1.0 - (distancia * 0.1);
    }
}   