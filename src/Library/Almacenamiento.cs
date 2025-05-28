namespace Library;

public class Almacenamiento
{
    public int CapacidadMaxima { get; private set; }

    public Almacenamiento(int capacidad)
    {
        CapacidadMaxima = capacidad;
    }

    public double Eficiencia(int distancia)
    {
        if (distancia <= 1) return 1.0;
        if (distancia >= 10) return 0.1;
        return 1.0 - (distancia * 0.1);
    }
}