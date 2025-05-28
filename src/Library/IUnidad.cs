namespace Library;

public interface IUnidad
{
    Jugador Propietario { get; }
    int Ataque { get; }
    int Defensa { get; }
    double Velocidad { get; }
    Point Posicion { get; set; }
    bool Mover(Point destino, Mapa mapa);
    void Atacar(IUnidad objetivo);
    
    int TiempoDeCreacion { get; }
}