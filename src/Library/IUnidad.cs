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
    void AtacarU(IUnidad objetivo); //cambiar a string
    void AtacarE(IEdificio objetivo); //cambiar a string
    int TiempoDeCreacion { get; }
    TipoUnidad Tipo { get; }
    double CalcularDaño(IUnidad objetivo);
}