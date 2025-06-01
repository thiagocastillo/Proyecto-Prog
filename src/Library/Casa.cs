namespace Library;

public class Casa : IEdificio
{ 
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public const int AumentoPoblacion = 5;
    public int Vida { get; set; }

    private int max_aldeanos = 20;
    private int max_unidadMilitar = 30;
    
    public int CantidadAldeanos { get; set; }
    public int CantidadUnidadesMilitar { get; set; }
    
    public Casa(Jugador propietario)
    {
        Propietario = propietario;
        Vida = 10000;

        propietario.AumentarPoblacionMaxima(AumentoPoblacion);
    }

    public bool CantidadAldeano()
    {
        return CantidadAldeanos <= max_aldeanos;
    }

    public bool CantidadUnidadMilitar()
    {
        return CantidadUnidadesMilitar <= max_unidadMilitar;
    }
}