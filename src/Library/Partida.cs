namespace Library;

public class Partida
{
    public Mapa Mapa { get; private set; }
    public List<Jugador> Jugadores { get; private set; } = new List<Jugador>();

    public Partida(int tamañoMapa, int cantidadJugadores)
    {
        Mapa = new Mapa(tamañoMapa, tamañoMapa);
    }

    public string AgregarJugador(Jugador jugador)
    {
        Jugadores.Add(jugador);
        if (Jugadores.Count != 2)
        {
           return "solo se permiten dos jugadores";
            }
        return "Jugador agregado correctamente";
    }
}