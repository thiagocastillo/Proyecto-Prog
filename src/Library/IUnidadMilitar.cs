namespace Library;

public interface IUnidadMilitar : IUnidad
{
    int TiempoDeCreacion { get; }
    public string AtacarEdificio(IEdificio objetivo);

    public string AtacarUnidad(Jugador atacante, string tipoUnidad, int cantidad, Point coordenada, Mapa mapa,
        List<Jugador> jugadores);
}