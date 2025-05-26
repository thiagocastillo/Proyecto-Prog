namespace DefaultNamespace;

public class ArqueroCompuesto : Arquero, IUnidadMilitar // Implementa la interfaz aunque herede de una que ya lo hace
{
    public ArqueroCompuesto(Jugador propietario) : base(propietario)
    {
        Ataque += 2; // La bonificación ya se aplica en el constructor base de Arquero
    }
}