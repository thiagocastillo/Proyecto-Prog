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

    public bool Mover(Point destino, Mapa mapa) // Metodo para mover al aldeano a un destino valido dentro del mapa
    {
        if (destino == null)
        {
            throw new ArgumentNullException(nameof(destino), "El destino no puede ser nulo.");
        }

        if (destino.X < 0 || destino.X >= mapa.Ancho || destino.Y < 0 || destino.Y >= mapa.Alto) // Verifica que el destino este dentro de los limites del mapa
        {
            return false; // Movimiento invalido
        }

        Posicion = destino; // Actualiza la posicion
        return true;
    }

    public double CalcularDaño(IUnidad objetivo)
    {
        return 0; // Los aldeanos no pueden hacer daño
    }

    public string AtacarEdificio(IEdificio objetivo)
    {
        return "Los aldeanos no atacan edificios.";
    }

    public string AtacarUnidad(IUnidad objetivo)
    {
        return "Los aldeanos no atacan unidades.";
    }

    public void Recolectar(RecursoNatural recurso, IAlmacenamiento almacenCercano = null) // Metodo para recolectar recursos naturales y depositarlos
    {
        if (recurso.EstaAgotado) // Verifica que el recurso no este agotado
            throw new InvalidOperationException("El recurso está agotado.");

        double cantidadRecolectada = recurso.TasaRecoleccion;
        if (Propietario.Civilizacion.Nombre == "Aztecas") // Si la civilizacion es Azteca, el aldeano recolecta 3 unidades extras
            cantidadRecolectada += 3;

        int extraido = recurso.Recolectar(cantidadRecolectada);

        IAlmacenamiento almacenMasCercano = almacenCercano; // Busca el almacen compatible mas cercano para depositar el recurso
        double distanciaMinima = double.MaxValue;

        foreach (var edificio in Propietario.Edificios)
        { 
            if (edificio is IAlmacenamiento almacen)
            {
                if (EsCompatible(almacen, recurso.Nombre))
                {
                    double distancia = CalcularDistancia(this.Posicion, almacen.Posicion); // Calcula la distancia entre el aldeano y el almacen
                    if (distancia < distanciaMinima)
                    {
                        distanciaMinima = distancia;
                        almacenMasCercano = almacen;
                    }
                }
            }
        }

        if (almacenMasCercano == null) // Si no se encontro almacen compatible lanza excepcion
            throw new InvalidOperationException(
                "No existe un edificio de almacenamiento compatible para recolectar este recurso.");

        if (!Propietario.Recursos.ContainsKey(recurso.Nombre)) // Si el propietario aun no tiene el recurso, lo inicializa
            Propietario.Recursos[recurso.Nombre] = 0;

        Propietario.Recursos[recurso.Nombre] += extraido; // Se suma la cantidad recolectad a los recursos del propietario
    }

    private bool EsCompatible(IAlmacenamiento almacen, string nombre) // Verifica si un almacen es compatible con el tipo de recurso
    {
        return (nombre == "Madera" && (almacen is DepositoMadera || almacen is CentroCivico)) ||
               (nombre == "Alimento" && (almacen is Granja || almacen is Molino)) ||
               (nombre == "Oro" && almacen is DepositoOro) ||
               (nombre == "Piedra" && almacen is DepositoPiedra);
    }

    private double CalcularDistancia(Point a, Point b) // Calcula la distancia entre 2 puntos del mapa
    {
        int dx = a.X - b.X;
        int dy = a.Y - b.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}