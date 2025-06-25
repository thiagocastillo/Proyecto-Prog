namespace Library.Domain;

public interface IEdificio
{
    Jugador Propietario { get; }
    Point Posicion { get; set; }
    int Vida { get; set; }
}