using System.Drawing;

namespace Library.Domain;

// Representa un molino que almacena recursos y tiene tiempo de construcción
public class Molino : IAlmacenamiento ,IEdificio
{
    // Jugador propietario del molino
    public Jugador Propietario { get; private set; }
    // Posición del molino en el mapa
    public Point Posicion { get; set; }
    // Capacidad máxima de almacenamiento del molino
    public int CapacidadMaxima { get; private set; } = 500;
    // Vida actual del molino
    public int Vida { get; set; }
    // Diccionario de recursos almacenados (clave: tipo de recurso, valor: cantidad)
    public Dictionary<string, int> Recursos { get; private set; } = new Dictionary<string, int>();

    // Objeto que gestiona el tiempo de construcción del molino
    private TiempoDeGeneracion tiempoconstruccion;

    // Tiempo total de construcción en segundos
    public int TiempoConstruccionTotal => tiempoconstruccion.TiempoTotalSegundos;
    // Tiempo restante de construcción en segundos
    public int TiempoConstruccionRestante => tiempoconstruccion.TiempoRestanteSegundos;
    // Indica si el molino ya está construido
    public bool EstaConstruido => tiempoconstruccion.EstaCompleta;

    // Constructor: inicializa el molino con su propietario, vida y tiempo de construcción
    public Molino()
    {
        
    }
    public Molino(Jugador propietario)
    {
        Propietario = propietario;
        Vida = 10000;
        tiempoconstruccion = new TiempoDeGeneracion(40); // 40 segundos para construir
    }
    
    // Calcula la eficiencia del molino según la distancia al recurso
    // Si está al lado, eficiencia máxima; si está muy lejos, eficiencia mínima
    public double Eficiencia(int distancia)
    {
        if (distancia <= 1) return 1.0;   // 100% de eficiencia si está al lado
        if (distancia >= 10) return 0.1;  // 10% de eficiencia si está muy lejos
        return 1.0 - (distancia * 0.1);   // Decrece linealmente en los casos intermedios
    }
}