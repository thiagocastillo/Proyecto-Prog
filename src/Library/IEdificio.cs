namespace Library;

public interface IEdificio
{
    Jugador Propietario { get; }
    Point Posicion { get; set; }
}