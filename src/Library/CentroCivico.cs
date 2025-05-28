namespace Library;
// AGREGAR CANTIDAD MAXIMA DE CENTRO CIVICO POR JUGADOR == 1
public class CentroCivico : IEdificio
{
    public Jugador Propietario { get; private set; }
    public Point Posicion { get; set; }
    public int MaxAldeanos { get; private set; } = 10;
    public int Vida { get; set; }


    public CentroCivico(Jugador propietario)
    {
        Propietario = propietario;
        Vida = 100000;
        if (propietario.Civilizacion.Nombre == "Bengalíes")
        {
            propietario.Aldeanos.Add(new Aldeano(propietario) { Posicion = new Point { X = Posicion.X + 1, Y = Posicion.Y } });
            propietario.PoblacionActual++;
        }
    }
}