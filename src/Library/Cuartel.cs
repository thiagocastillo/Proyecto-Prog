using System.Drawing;

namespace Library;

// Representa un cuartel que tarda 60 segundos en construirse
public class Cuartel : IEdificio
{
    // Jugador dueño del cuartel
    public Jugador Propietario { get; private set; }
    // Ubicación del cuartel en el mapa
    public Point Posicion { get; set; }
    // Vida actual del cuartel
    public int Vida { get; set; }

    // Objeto que gestiona el tiempo de construcción
    private TiempoConstruccion tiempoconstruccion;

    // Tiempo total de construcción en segundos
    public int TiempoConstruccionTotal => tiempoconstruccion.TiempoTotalSegundos;
    // Tiempo restante de construcción en segundos
    public int TiempoConstruccionRestante => tiempoconstruccion.TiempoRestanteSegundos;
    // Indica si el cuartel ya está construido
    public bool EstaConstruido => tiempoconstruccion.EstaCompleta;

    // Constructor: inicializa el cuartel y su tiempo de construcción (60 segundos)
    public Cuartel(Jugador propietario)
    {
        Propietario = propietario;
        Vida = 10000;
        tiempoconstruccion = new TiempoConstruccion(60); // 60 segundos para construir
    }
}