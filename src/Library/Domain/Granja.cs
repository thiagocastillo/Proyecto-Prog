namespace Library.Domain;

public class Granja : IAlmacenamiento, IEdificio
{
    public Jugador Propietario { get; private set; }  //Granja del jugador
    public Point Posicion { get; set; }  //Ubicacion de la granja
    public int CapacidadMaxima { get; private set; } = 500; //Maxima capacidad
    public int Vida { get; set; } //Vida
    public Dictionary<string, int> Recursos { get; private set; } = new Dictionary<string, int>();  //Mapea el nombre del recurso

    public int TiempoConstruccionTotal { get; } = 20; // Tiempo de construcción en segundos

    public Granja()
    {
        
    }
    public Granja(Jugador propietario)  //Metodo constructor
    {
        Propietario = propietario;
        Vida = 10000;
    }

    public double Eficiencia(int distancia)  //Eficiencia de recoleccion segun distancia
    {
        if (distancia <= 1) return 1.0;
        if (distancia >= 10) return 0.1;
        return 1.0 - (distancia * 0.1);
    }
}