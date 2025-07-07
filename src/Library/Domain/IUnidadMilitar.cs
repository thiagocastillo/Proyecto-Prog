namespace Library.Domain;

public interface IUnidadMilitar : IUnidad
{
    TiempoDeGeneracion TiempoGeneracion { get; }
    int TiempoDeCreacion { get; }
    string AtacarEdificio(IEdificio objetivo);
    string AtacarUnidad(Jugador atacante, string tipoUnidad, int cantidad, Point coordenada, Mapa mapa, List<Jugador> jugadores);
}