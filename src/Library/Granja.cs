namespace Library;

public class Granja : IAlmacenamiento
{
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public int CapacidadMaxima { get; private set; } = 500;
    public int Vida { get; set; }
    public Dictionary<string, int> Recursos { get; private set; } = new Dictionary<string, int>();

    public Granja(Jugador propietario)
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