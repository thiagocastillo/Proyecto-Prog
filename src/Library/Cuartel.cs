namespace Library;

public class Cuartel : IEdificio
{
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public int Vida { get; set; }


    public Cuartel(Jugador propietario)
    {
        Propietario = propietario;
        Vida = 10000;
    }
}