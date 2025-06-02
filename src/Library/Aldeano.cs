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

    public string AtacarE(IEdificio objetivo)
    {
        return "Los aldeanos no atacan edificios.";
    }
    public string AtacarU(IUnidad objetivo)
    {
        return "Los aldeanos no atacan unidades.";
    }

  

  
   public void Recolectar(RecursoNatural recurso, IAlmacenamiento almacenCercano = null)
   {
       if (recurso.EstaAgotado())
           throw new InvalidOperationException("El recurso está agotado.");

       int cantidadRecolectada = (int)recurso.TasaRecoleccion;
       if (Propietario.Civilizacion.Nombre == "Aztecas")
           cantidadRecolectada += 3;

       int extraido = recurso.Recolectar(cantidadRecolectada);

       IAlmacenamiento almacenMasCercano = almacenCercano;
       double distanciaMinima = double.MaxValue;

       foreach (var edificio in Propietario.Edificios)
       {
           if (edificio is IAlmacenamiento almacen)
           {
               if (EsCompatible(almacen, recurso.Nombre))
               {
                   double distancia = CalcularDistancia(this.Posicion, almacen.Posicion);
                   if (distancia < distanciaMinima)
                   {
                       distanciaMinima = distancia;
                       almacenMasCercano = almacen;
                   }
               }
           }
       }

       if (almacenMasCercano == null)
           throw new InvalidOperationException("No existe un edificio de almacenamiento compatible para recolectar este recurso.");

       if (!Propietario.Recursos.ContainsKey(recurso.Nombre))
           Propietario.Recursos[recurso.Nombre] = 0;

       Propietario.Recursos[recurso.Nombre] += extraido;
   }

   private bool EsCompatible(IAlmacenamiento almacen, string nombre)
   {
       return (nombre == "Madera" && almacen is DepositoMadera) ||
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