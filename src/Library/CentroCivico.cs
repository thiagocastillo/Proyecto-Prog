namespace Library;

public class CentroCivico : IEdificio
{
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public int MaxAldeanos { get; private set; } = 10;

    public CentroCivico(Jugador propietario)
    {
        Propietario = propietario;
        if (propietario.Civilizacion.Nombre == "Bengalíes")
        {
            propietario.Aldeanos.Add(new Aldeano(propietario) { Posicion = new Point { X = Posicion.X + 1, Y = Posicion.Y } });
            propietario.PoblacionActual++;
        }
    }
}