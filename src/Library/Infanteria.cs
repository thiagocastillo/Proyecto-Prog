namespace Library;

public class Infanteria : IUnidadMilitar
{
    public Jugador Propietario { get; private set; }
    public int Ataque { get; private set; } = 10;
    public int Defensa { get; private set; } = 5;
    public double Velocidad { get; private set; } = 1.0;
    public Point Posicion { get; set; }

    public int TiempoDeCreacion { get; private set; } = 10;
    public Infanteria(Jugador propietario)
    {
        Propietario = propietario;
        if (propietario.Civilizacion.Nombre == "Armenios")
        {
            // Podríamos manejar la bonificación de vida de otra manera con interfaces si fuera necesario
        }
    }

    public bool Mover(Point destino, Mapa mapa)
    {
        if (destino.X < 0 || destino.X >= mapa.Ancho || destino.Y < 0 || destino.Y >= mapa.Alto)
        {
            return false; 
        }
        Posicion = destino;
        return true;
    }

    public void Atacar(IUnidad objetivo)
    {
        int ataqueFinal = Ataque;
        if (objetivo is Infanteria && Propietario.Civilizacion.Nombre == "Aztecas" && Propietario.Civilizacion.UnidadEspecial == "Guerrero Jaguar")
        {
            ataqueFinal += 3;
        }
        int daño = ataqueFinal - objetivo.Defensa;
        // Registrar daño
    }
}