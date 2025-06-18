namespace Library;

public class Aldeano : IUnidad, IRecolector
{
    public Jugador Propietario { get; private set; }

    public int Ataque { get; private set; } = 0;
    public int Defensa { get; private set; } = 0;

    public double Velocidad { get; private set; } = 1.0;

    public int Salud { get; set; }
    public Point Posicion { get; set; }

    public int TiempoDeCreacion { get; }

    public Aldeano(Jugador propietario)
    {
        Propietario = propietario;
    }

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

    public double CalcularDaño(IUnidad objetivo)
    {
        return 0;
    }

    public string AtacarEdificio(IEdificio objetivo)
    {
        return "Los aldeanos no atacan edificios.";
    }

    public string AtacarUnidad(IUnidad objetivo)
    {
        return "Los aldeanos no atacan unidades.";
    }

    public void RecolectarEn(Point coordenada, Mapa mapa)
    {
        // Buscar el recurso natural en la coordenada
        var recurso = mapa.Recursos.FirstOrDefault(r => r.Ubicacion.Equals(coordenada));        if (recurso == null)
            throw new InvalidOperationException("No hay recurso natural en la coordenada especificada.");

        if (recurso.EstaAgotado)
            throw new InvalidOperationException("El recurso está agotado.");

        // Mover al aldeano a la coordenada
        this.Posicion = coordenada;

        // Calcular cantidad a recolectar
        double cantidadRecolectada = recurso.TasaRecoleccion;
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
    }

    private bool EsCompatible(IAlmacenamiento almacen, string nombre)
    {
        return (nombre == "Madera" && (almacen is DepositoMadera || almacen is CentroCivico)) ||
               (nombre == "Alimento" && (almacen is Granja || almacen is Molino)) ||
               (nombre == "Oro" && almacen is DepositoOro) ||
               (nombre == "Piedra" && almacen is DepositoPiedra);
    }

    private double CalcularDistancia(Point a, Point b)
    {
        int dx = a.X - b.X;
        int dy = a.Y - b.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}