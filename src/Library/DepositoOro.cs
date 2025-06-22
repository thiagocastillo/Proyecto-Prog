namespace Library;

public class DepositoOro : IAlmacenamiento
{
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public int CapacidadMaxima { get; private set; } = 500;
    public int Vida { get; set; }
    public Dictionary<string, int> Recursos { get; private set; } = new Dictionary<string, int>();

    // Objeto que controla el tiempo de construccion
    private TiempoConstruccion tiempoconstruccion;

    public int TiempoConstruccionTotal => tiempoconstruccion.TiempoTotal;
    public int TiempoConstruccionRestante => tiempoconstruccion.TiempoRestante;
    public bool EstaConstruido => tiempoconstruccion.EstaCompleta;

    public DepositoOro(Jugador propietario)
    {
        Propietario = propietario;
        Vida = 5000;
        tiempoconstruccion = new TiempoConstruccion(3); // 3 turnos para construir
    }

    public double Eficiencia(int distancia)
    {
        // Devuelve la eficiencia de recoleccion segun la distancia al edificio
        if (distancia <= 1) return 1.0;
        if (distancia >= 10) return 0.1;
        return 1.0 - (distancia * 0.1);
    }
}