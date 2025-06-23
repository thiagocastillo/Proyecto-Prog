namespace Library;

public class CentroCivico : IAlmacenamiento , IEdificio 
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
        Posicion = new Point { X = 0, Y = 0 }; // Posición inicial por defecto

        // Inicializa recursos con 100 de alimento y 100 de madera
        Recursos["alimento"] = 100;
        Recursos["madera"] = 100;
        
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