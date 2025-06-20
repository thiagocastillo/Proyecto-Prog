namespace Library;
public interface IAlmacenamiento : IEdificio
{
    int CapacidadMaxima { get; }
    double Eficiencia(int distancia);
    Dictionary<string, int> Recursos { get; }
}