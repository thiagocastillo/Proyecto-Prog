namespace DefaultNamespace;

public class GuerreroJaguar : Infanteria, IUnidadMilitar // Similar al anterior
{
    public GuerreroJaguar(Jugador propietario) : base(propietario)
    {
        // La bonificación contra infantería se maneja en el método Atacar de Infanteria
    }
}