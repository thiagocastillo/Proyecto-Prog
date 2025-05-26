namespace DefaultNamespace;

public interface IEdificio
{
    Jugador Propietario { get; }
    Point Posicion { get; set; }
}