namespace Library.Domain;

public class CentroCivico : IAlmacenamiento , IEdificio 
{
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public int MaxAldeanos { get; private set; } = 10;
    public int Vida { get; set; }
    public int VidaMaxima { get; private set; }
    public int CapacidadMaxima { get; private set; } = 500;
    public Dictionary<string, int> Recursos { get; private set; } = new Dictionary<string, int>();

    public CentroCivico()
    {
        
    }
    public CentroCivico(Jugador propietario)
    {
        Propietario = propietario;
        VidaMaxima = 100000;
        Vida = VidaMaxima;
        Posicion = new Point { X = 0, Y = 0 }; // Posición inicial por defecto

        // Inicializa recursos con 100 de alimento y 100 de madera
        Recursos["Alimento"] = 1000000;
        Recursos["Madera"] = 10000000;
        Recursos["Piedra"] = 10000000;
        Recursos["Oro"] = 10000000;
        
        // Bonificacion especial para Bangalies (Comienzan con un aldeano adicional)
        if (propietario.Civilizacion.Nombre == "Bengalies")
        {
            propietario.Aldeanos.Add(new Aldeano(propietario) { Posicion = new Point { X = Posicion.X + 1, Y = Posicion.Y } });
            propietario.PoblacionActual++;
        }
    }

    // Eficiencia de recoleccion segun la distancia del recurso
    public double Eficiencia(int distancia)
    {
        if (distancia <= 1) return 1.0;
        if (distancia >= 10) return 0.1;
        return 1.0 - (distancia * 0.1);
    }
}