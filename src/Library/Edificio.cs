namespace Library;

public class Edificio : IEdificio
{
    // Referencia al jugador que es dueño del edificio
    public Jugador Propietario { get; protected set; }
    // Ubicacion del edificio en el mapa
    public Point Posicion { get; set; }
    // Vida actual del edificio
    public int Vida { get; set; }

    // Encapsula el tiempo necesario para que el edificio este construido
    protected TiempoConstruccion  tiempoconstruccion = new TiempoConstruccion(10);

    public int TiempoConstruccionTotal => tiempoconstruccion.TiempoTotal;
    public int TiempoConstruccionRestante => tiempoconstruccion.TiempoRestante;
    public bool EstaConstruido => tiempoconstruccion.EstaCompleta;

    // Constructor que inicializa el propietario y la vida del edificio
    public Edificio(Jugador propietario)
    {
        Propietario = propietario;
        Vida = 10000;
    }
}