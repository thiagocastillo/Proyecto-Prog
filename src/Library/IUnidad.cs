namespace Library;

public interface IUnidad
{
    Jugador Propietario { get; }
    int Ataque { get; }
    int Defensa { get; }
    double Velocidad { get; }
    int Salud { get; set; }
    Point Posicion { get; set; }
    bool Mover(Point destino, Mapa mapa);
    string AtacarUnidad(IUnidad objetivo); 
    string AtacarEdificio(IEdificio objetivo); 
    int TiempoDeCreacion { get; }
    double CalcularDaño(IUnidad objetivo);
}