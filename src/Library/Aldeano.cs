namespace Library;

public class Aldeano : IUnidad, IRecolector
{
    public Jugador Propietario { get; private set; }

    // Propiedades de combate
    public int Ataque { get; private set; } = 0;
    public int Defensa { get; private set; } = 0;

    // Velocidad de movimiento
    public double Velocidad { get; private set; } = 1.0;

    // Vida actual
    public int Salud { get; set; }
    // Posicion actual en el mapa
    public Point Posicion { get; set; }
    // Tiempo necesario para crear aldeano
    public int TiempoDeCreacion { get; }
    
    public Aldeano(Jugador propietario)
    {
        Propietario = propietario;
    }

    // Mueve al aldeano a una nueva posicion si es valida en el mapa
    public bool Mover(Point destino, Mapa mapa)
    {
        if (destino == null)
        {
            throw new ArgumentNullException(nameof(destino), "El destino no puede ser nulo.");
        }

        if (destino.X < 0 || destino.X >= mapa.Ancho || destino.Y < 0 || destino.Y >= mapa.Alto)
        {
            return false;
        }

        Posicion = destino;
        return true;
    }

    // No causa daño ya que no combate
    public double CalcularDaño(IUnidad objetivo)
    {
        return 0;
    }

    // Los aldeanos no pueden atacar
    public string AtacarEdificio(IEdificio objetivo)
    {
        return "Los aldeanos no atacan edificios.";
    }

    public string AtacarUnidad(IUnidad objetivo)
    {
        return "Los aldeanos no atacan unidades.";
    }

    // Recolecta recursos de un punto del mapa y los lleva al edificio compatible mas cercano
    public void RecolectarEn(Point coordenada, Mapa mapa)    {
        // Buscar el recurso natural en la coordenada
        var recurso = mapa.Recursos.FirstOrDefault(r => r.Ubicacion.X == coordenada.X && r.Ubicacion.Y == coordenada.Y);
        if (recurso == null)
            throw new InvalidOperationException("No hay recurso natural en la coordenada especificada.");

        if (recurso.EstaAgotado)
            throw new InvalidOperationException("El recurso está agotado.");

        // Mover al aldeano a la coordenada
        this.Posicion = coordenada;
        
       

        // Calcular cantidad a recolectar según vida restante y tasa de recolección
        double cantidadRecolectada = recurso.Cantidad * recurso.TasaRecoleccion;
        if (Propietario.Civilizacion.Nombre == "Aztecas")
            cantidadRecolectada += 3;

        int extraido = recurso.Recolectar(cantidadRecolectada);

        // Buscar el edificio de almacenamiento compatible más cercano
        IAlmacenamiento almacenMasCercano = null;
        double distanciaMinima = double.MaxValue;

        foreach (var edificio in Propietario.Edificios)
        {
            if (edificio is IAlmacenamiento almacen && EsCompatible(almacen, recurso.Nombre))
            {
                double distancia = CalcularDistancia(this.Posicion, almacen.Posicion);
                if (distancia < distanciaMinima)
                {
                    distanciaMinima = distancia;
                    almacenMasCercano = almacen;
                }
            }
        }

        if (almacenMasCercano == null)
            throw new InvalidOperationException("No existe un edificio de almacenamiento compatible para este recurso.");

        // Registrar el recurso en el almacenamiento
        if (!almacenMasCercano.Recursos.ContainsKey(recurso.Nombre))
            almacenMasCercano.Recursos[recurso.Nombre] = 0;
        almacenMasCercano.Recursos[recurso.Nombre] += extraido;
        if (recurso.EstaAgotado)
        {
            mapa.Recursos.Remove(recurso);
        }
    }
    
    // Verifica si un edificio de almacenamiento es compatible con el recurso
    private bool EsCompatible(IAlmacenamiento almacen, string nombre)
    {
        return (nombre == "Madera" && (almacen is DepositoMadera || almacen is CentroCivico)) ||
               (nombre == "Alimento" && (almacen is Granja || almacen is Molino|| almacen is CentroCivico)) ||
               (nombre == "Oro" && (almacen is DepositoOro || almacen is CentroCivico)) ||
               (nombre == "Piedra" && (almacen is DepositoPiedra || almacen is CentroCivico));
    }

    // Calcula la distancia entre dos puntos del mapa
    private double CalcularDistancia(Point a, Point b)
    {
        int dx = a.X - b.X;
        int dy = a.Y - b.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}