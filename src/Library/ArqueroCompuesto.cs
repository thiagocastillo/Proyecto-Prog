namespace Library;

// Clase que representa la unidad especial Arquero Compuesto, hereda de Arquero
public class ArqueroCompuesto : Arquero, IUnidadMilitar
{
    public double Velocidad { get; protected set; } = 1.3; // Velocidad ligeramente mayor que el Arquero normal
    // Constructor: inicializa la unidad y aplica bonificaciones especiales
    public ArqueroCompuesto(Jugador propietario) : base(propietario)
    {
        // Ajusta los valores base para el Arquero Compuesto
        this.Ataque = 12;   // Ataque mayor que el Arquero normal
        this.Defensa = 5;   // Defensa mayor que el Arquero normal

        // Si la civilización es Armenios y la unidad especial es Arquero Compuesto, aplica bonificación extra
        if (propietario.Civilizacion.Nombre == "Armenios" && propietario.Civilizacion.UnidadEspecial == "Arquero Compuesto")
        {
            this.Ataque += 2; // Bonificación adicional de ataque
        }
    }

    // El resto de métodos (CalcularDaño, Mover, AtacarUnidad, AtacarEdificio) se heredan de Arquero
}