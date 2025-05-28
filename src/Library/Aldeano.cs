namespace Library;

public class Aldeano : IUnidad, IRecolector
{
    public Jugador Propietario { get; private set; }
    public int Ataque { get; private set; } = 0;
    public int Defensa { get; private set; } = 0;
    public double Velocidad { get; private set; } = 1.0;
    public Point Posicion { get; set; }
    
    public int TiempoDeCreacion { get; private set; } = 10;

    public Aldeano(Jugador propietario)
    {
        Propietario = propietario;
    }

    public void Mover(Point destino)
    {
        Posicion = destino;
    }

    public void Atacar(IUnidad objetivo)
    {
        // Los aldeanos normalmente no atacan
    }

    public void Recolectar(Recurso.TipoRecurso tipoRecurso, IAlmacenamiento? almacenCercano)
    {
        int cantidadRecolectada = 10;
        if (Propietario.Civilizacion.Nombre == "Aztecas")
        {
            cantidadRecolectada += 3;
        }
        Propietario.Recursos[tipoRecurso] += cantidadRecolectada;
    }
}