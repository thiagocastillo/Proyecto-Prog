using System.Drawing;

namespace Library;

// Implementa la interfaz IEdificio
public class Casa : IEdificio
{ 
    // Jugador propietario de la casa
    public Jugador Propietario { get; private set; }
    // Posición de la casa en el mapa
    public Point Posicion { get; set; }
    // Aumento de población máxima que otorga la casa
    public const int AumentoPoblacion = 5;
    // Vida actual de la casa
    public int Vida { get; set; }

    // Límite máximo de aldeanos que puede albergar la casa
    private int max_aldeanos = 20;
    // Límite máximo de unidades militares que puede albergar la casa
    private int max_unidadMilitar = 30;
    
    // Cantidad actual de aldeanos en la casa
    public int CantidadAldeanos { get; set; }
    // Cantidad actual de unidades militares en la casa
    public int CantidadUnidadesMilitar { get; set; }
    
    // Objeto que gestiona el tiempo de construcción de la casa
    private TiempoConstruccion tiempoconstruccion;

    // Tiempo total de construcción en segundos
    public int TiempoConstruccionTotal => tiempoconstruccion.TiempoTotalSegundos;
    // Tiempo restante de construcción en segundos
    public int TiempoConstruccionRestante => tiempoconstruccion.TiempoRestanteSegundos;
    // Indica si la casa ya está construida
    public bool EstaConstruido => tiempoconstruccion.EstaCompleta;
    
    // Constructor: inicializa la casa, su vida y el tiempo de construcción (15 segundos)
    public Casa(Jugador propietario)
    {
        Propietario = propietario;
        Vida = 10000;
        tiempoconstruccion = new TiempoConstruccion(15); // 15 segundos para construir
        propietario.AumentarPoblacionMaxima(AumentoPoblacion);
    }

    // Verifica si hay espacio para más aldeanos
    public bool CantidadAldeano()
    {
        return CantidadAldeanos <= max_aldeanos;
    }

    // Verifica si hay espacio para más unidades militares
    public bool CantidadUnidadMilitar()
    {
        return CantidadUnidadesMilitar <= max_unidadMilitar;
    }
}