namespace Library;

public class ArqueroCompuesto : Arquero, IUnidadMilitar
{
    public ArqueroCompuesto(Jugador propietario) : base(propietario)
    {
        // La bonificación de ataque para el Arquero Compuesto (armenio) ya se aplica
        // en el constructor de la clase Arquero si la civilización es la correcta.
        // No necesitamos modificar Ataque aquí directamente.
    }
}