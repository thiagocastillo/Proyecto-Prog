namespace Library.Domain;

public class ArqueroCompuesto : Arquero, IUnidadMilitar
{
    // Nuevo: tiempo de generación asociado a la unidad
    public TiempoDeGeneracion TiempoGeneracion { get; private set; }

    // Implementación requerida por la interfaz
    public int TiempoDeCreacion => TiempoGeneracion.TiempoTotalSegundos;

    public new double Velocidad { get; protected set; } = 1.3; // Velocidad ligeramente mayor que el Arquero normal

    public ArqueroCompuesto()
    {
        
    }
    public ArqueroCompuesto(Jugador propietario) : base(propietario)
    {
        // Ajusta los valores base para el Arquero Compuesto
        this.Ataque = 400;
        this.Defensa = 5;

        // Tiempo de generación específico para Arquero Compuesto (puedes ajustar el valor)
        TiempoGeneracion = new TiempoDeGeneracion(12);

        // Bonificación extra si corresponde
        if (propietario.Civilizacion.Nombre == "Armenios" && propietario.Civilizacion.UnidadEspecial == "Arquero Compuesto")
        {
            this.Ataque += 2;
        }
    }
    // El resto de métodos se heredan de Arquero
}