namespace Library;

public class CentroCivico : IAlmacenamiento
{
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public int MaxAldeanos { get; private set; } = 10;
    public int Vida { get; set; }
    public int CapacidadMaxima { get; private set; } = 500;
    public Dictionary<string, int> Recursos { get; private set; } = new Dictionary<string, int>();

    public CentroCivico(Jugador propietario)
    {
        Propietario = propietario;
        Vida = 100000;

        // Inicializa recursos con 100 de alimento y 100 de madera
        Recursos["alimento"] = 100;
        Recursos["madera"] = 100;

        if (propietario.Civilizacion.Nombre == "Bengalíes")
        {
            propietario.Aldeanos.Add(new Aldeano(propietario) { Posicion = new Point { X = Posicion.X + 1, Y = Posicion.Y } });
            propietario.PoblacionActual++;
        }
    }

    public double Eficiencia(int distancia)
    {
        if (distancia <= 1) return 1.0;
        if (distancia >= 10) return 0.1;
        return 1.0 - (distancia * 0.1);
    }
}