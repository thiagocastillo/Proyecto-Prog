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
    string AtacarU(IUnidad objetivo); 
    string AtacarE(IEdificio objetivo); 
    int TiempoDeCreacion { get; }
    TipoUnidad Tipo { get; }
    double CalcularDaño(IUnidad objetivo);
}