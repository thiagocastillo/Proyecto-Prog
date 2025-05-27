namespace Library;

public class Aldeano : IUnidad, IRecolector
{
    public Jugador Propietario { get; private set; }
    public int Ataque { get; private set; } = 0;
    public int Defensa { get; private set; } = 0;
    public double Velocidad { get; private set; } = 1.0;
    public Point Posicion { get; set; }

    public Aldeano(Jugador propietario)
    {
        Propietario = propietario;
    }

    public void Mover(Point destino)
    {
        Posicion = destino;
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

  
   public void Recolectar(Recurso.TipoRecurso tipoRecurso, IAlmacenamiento? almacenCercano = null)
   {
       int cantidadRecolectada = 10;
       if (Propietario.Civilizacion.Nombre == "Aztecas")
       {
           cantidadRecolectada += 3;
       }

       // Buscar el edificio de almacenamiento compatible más cercano
       IAlmacenamiento? almacenMasCercano = null;
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

       // Simular traslado y depósito de recursos
       if (!Propietario.Recursos.ContainsKey(tipoRecurso))
       {
           Propietario.Recursos[tipoRecurso] = 0;
       }

       Propietario.Recursos[tipoRecurso] += cantidadRecolectada;
   }


   private double CalcularDistancia(Point a, Point b)
    {
        int dx = a.X - b.X;
        int dy = a.Y - b.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    private bool EsCompatible(IAlmacenamiento almacen, Recurso.TipoRecurso tipo)
    {
        return (tipo == Recurso.TipoRecurso.Madera && almacen is DepositoMadera) ||
               (tipo == Recurso.TipoRecurso.Alimento && (almacen is Granja || almacen is Molino)) ||
               (tipo == Recurso.TipoRecurso.Oro && almacen is DepositoOro) ||
               (tipo == Recurso.TipoRecurso.Piedra && almacen is DepositoPiedra);
    }
}