namespace Library;

public class Cuartel : IEdificio
{
    public Jugador Propietario { get; private set; } //Jugador dueño del cuartel
    public Point Posicion { get; set; } //Ubicacion
    public int Vida { get; set; } //Vida del cuartel
    
    private TiempoConstruccion tiempoconstruccion;

    public int TiempoConstruccionTotal => tiempoconstruccion.TiempoTotal;
    public int TiempoConstruccionRestante => tiempoconstruccion.TiempoRestante;
    public bool EstaConstruido => tiempoconstruccion.EstaCompleta;

    public Cuartel(Jugador propietario) //Constructor
    {
        Propietario = propietario; //Asigna al jugador
        Vida = 10000; // Inicializa la vida del cuartel
    }
}