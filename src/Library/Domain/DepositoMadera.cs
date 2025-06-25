using System.Drawing;

namespace Library.Domain;

// Representa un depósito de madera con tiempo de construcción de 30 segundos
public class DepositoMadera : IAlmacenamiento ,IEdificio
{
    // Jugador propietario del depósito
    public Jugador Propietario { get; private set; }
    // Posición en el mapa
    public Point Posicion { get; set; }
    // Capacidad máxima de almacenamiento
    public int CapacidadMaxima { get; private set; } = 500;
    // Vida actual del depósito
    public int Vida { get; set; }
    // Recursos almacenados
    public Dictionary<string, int> Recursos { get; private set; } = new Dictionary<string, int>();

    // Objeto que gestiona el tiempo de construcción
    private TiempoConstruccion tiempoconstruccion;

    // Tiempo total de construcción en segundos
    public int TiempoConstruccionTotal => tiempoconstruccion.TiempoTotalSegundos;
    // Tiempo restante de construcción en segundos
    public int TiempoConstruccionRestante => tiempoconstruccion.TiempoRestanteSegundos;
    // Indica si el depósito ya está construido
    public bool EstaConstruido => tiempoconstruccion.EstaCompleta;

    // Constructor: inicializa el depósito y su tiempo de construcción (3 segundos)
    public DepositoMadera(Jugador propietario)
    {
        Propietario = propietario;
        Vida = 5000;
        tiempoconstruccion = new TiempoConstruccion(30); // 30 segundos para construir
    }

    // Calcula la eficiencia según la distancia
    public double Eficiencia(int distancia)
    {
        if (distancia <= 1) return 1.0;
        if (distancia >= 10) return 0.1;
        return 1.0 - (distancia * 0.1);
    }
}