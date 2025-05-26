namespace Library;

public interface IUnidad
{
    Jugador Propietario { get; }
    int Ataque { get; }
    int Defensa { get; }
    double Velocidad { get; }
    Point Posicion { get; set; }
    void Mover(Point destino);
    void Atacar(IUnidad objetivo);
}