namespace Library;

public class Aldeano : IUnidad, IRecolector
{
    public Jugador Propietario { get; private set; }
    public int Ataque { get; private set; } = 0;
    public int Defensa { get; private set; } = 0;
    public double Velocidad { get; private set; } = 1.0;
    public Point Posicion { get; set; }
    
    public int TiempoDeCreacion { get; private set; } = 10;

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

    public void Atacar(IUnidad objetivo)
    {
        // Los aldeanos normalmente no atacan
    }

   /* public void Recolectar(Recurso.TipoRecurso tipoRecurso, IAlmacenamiento? almacenCercano)
    {
        int cantidadRecolectada = 10;
        if (Propietario.Civilizacion.Nombre == "Aztecas")
        {
            cantidadRecolectada += 3;
        }
        Propietario.Recursos[tipoRecurso] += cantidadRecolectada;
    }*/

  
   //public void Recolectar(Recurso.TipoRecurso tipoRecurso, IAlmacenamiento? almacenCercano = null)
   public void Recolectar(ITipoRecurso tipoRecurso, IAlmacenamiento almacenCercano = null)
   {
       int cantidadRecolectada = 10;
       if (Propietario.Civilizacion.Nombre == "Aztecas")
       {
           cantidadRecolectada += 3;
       }

       // Buscar el edificio de almacenamiento compatible más cercano
       IAlmacenamiento almacenMasCercano = null;
       double distanciaMinima = double.MaxValue;

       foreach (var edificio in Propietario.Edificios)
       {
           if (edificio is IAlmacenamiento almacen)
           {
               if (EsCompatible(almacen, tipoRecurso))
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

       // Si no hay edificio compatible, no se recolecta
       if (almacenMasCercano == null)
       {
           throw new InvalidOperationException("No existe un edificio de almacenamiento compatible para recolectar este recurso.");
       }
       
       string claveTipoRecurso = tipoRecurso.Nombre;
       
       // Simular traslado y depósito de recursos
       if (!Propietario.Recursos.ContainsKey(claveTipoRecurso))
       {
           Propietario.Recursos[claveTipoRecurso] = 0;
       }
       {
           Propietario.Recursos[claveTipoRecurso] = 0;
       }

       Propietario.Recursos[claveTipoRecurso] += cantidadRecolectada;
   }


   private double CalcularDistancia(Point a, Point b)
    {
        int dx = a.X - b.X;
        int dy = a.Y - b.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    private bool EsCompatible(IAlmacenamiento almacen, ITipoRecurso tipo)
    {
        string nombre = tipo.Nombre;

        return (nombre == "Madera" && almacen is DepositoMadera) ||
               (nombre == "Alimento" && (almacen is Granja || almacen is Molino)) ||
               (nombre == "Oro" && almacen is DepositoOro) ||
               (nombre == "Piedra" && almacen is DepositoPiedra);
    }

}