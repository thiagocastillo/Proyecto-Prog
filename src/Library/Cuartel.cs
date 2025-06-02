namespace Library;

public class Cuartel : IEdificio
{
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public int Vida { get; set; }
    
    private TiempoConstruccion tiempoconstruccion;

    public int TiempoConstruccionTotal => tiempoconstruccion.TiempoTotal;
    public int TiempoConstruccionRestante => tiempoconstruccion.TiempoRestante;
    public bool EstaConstruido => tiempoconstruccion.EstaCompleta;

    public Cuartel(Jugador propietario)
    {
        Propietario = propietario;
        Vida = 10000;
    }
}