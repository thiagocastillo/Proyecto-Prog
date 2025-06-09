namespace Library;

public class Partida
{
    public Mapa Mapa { get; private set; }
    public List<Jugador> Jugadores { get; private set; } = new List<Jugador>();

    public Partida()
    {
        Mapa = new Mapa();
    }

    public string AgregarJugador(Jugador jugador)
    {
        Jugadores.Add(jugador);
        if (Jugadores.Count != 2)
        {
           return "Solo se permiten dos jugadores";
        }
        return "Jugador agregado correctamente";
    }
    public Jugador? VerificarGanador()
    {
        foreach (var jugador in Jugadores)
        {
            bool tieneCentroCivico = jugador.Edificios.Any(e => e is CentroCivico);
            if (!tieneCentroCivico)
            {
                return Jugadores.FirstOrDefault(j => j != jugador);
            }
        }
        return null; 
    }
}