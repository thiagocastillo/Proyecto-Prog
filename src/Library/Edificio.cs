namespace Library;

public abstract class Edificio : IEdificio
{
    public Jugador Propietario { get; protected set; }
    public Point Posicion { get; set; }
    public int Vida { get; set; }

    protected TiempoConstruccion  tiempoconstruccion = new TiempoConstruccion(10);

    public int TiempoConstruccionTotal => tiempoconstruccion.TiempoTotal;
    public int TiempoConstruccionRestante => tiempoconstruccion.TiempoRestante;
    public bool EstaConstruido => tiempoconstruccion.EstaCompleta;

    public Edificio(Jugador propietario)
    {
        Propietario = propietario;
        Vida = 10000;
    }
}