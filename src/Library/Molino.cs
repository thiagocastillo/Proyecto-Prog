namespace Library;

public class Molino : IAlmacenamiento
{
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public int CapacidadMaxima { get; private set; } = 500;
    public int Vida { get; set; }
    public Dictionary<string, int> Recursos { get; private set; } = new Dictionary<string, int>();

    private TiempoConstruccion tiempoconstruccion;

    public int TiempoConstruccionTotal => tiempoconstruccion.TiempoTotal;
    public int TiempoConstruccionRestante => tiempoconstruccion.TiempoRestante;
    public bool EstaConstruido => tiempoconstruccion.EstaCompleta;

    public Molino(Jugador propietario)
    {
        Propietario = propietario;
        Vida = 10000;
        tiempoconstruccion = new TiempoConstruccion(3);
    }
    
    // Calcula la eficiencia del molino segun la distancia del recurso 
    // Cuanto mas lejos, menor la eficiencia
    public double Eficiencia(int distancia)
    {
        if (distancia <= 1) return 1.0; // 100% de eficiencia si esta al lado
        if (distancia >= 10) return 0.1; // 10% de eficiencia si esta muy lejos
        return 1.0 - (distancia * 0.1); // En el resto de los casos, decrece linealmente
    }
}