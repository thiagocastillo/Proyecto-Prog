namespace Library;

public class Infanteria : IUnidadMilitar
{
    public Jugador Propietario { get; private set; }
    public int Ataque { get; private set; } = 10;
    public int Defensa { get; private set; } = 5;
    
    public double Velocidad { get; private set; } = 1.0;
    
    public int Salud { get; set; } = 100;
    public Point Posicion { get; set; }

    public int TiempoDeCreacion { get; private set; } = 10;
    
    public Infanteria(Jugador propietario)
    {
        Propietario = propietario;
        if (propietario.Civilizacion.Nombre == "Armenios")
        {
            // Podríamos manejar la bonificación de vida de otra manera con interfaces si fuera necesario
        }
    }

    public double CalcularDaño(IUnidad objetivo)
    {
        double dañoBase = this.Ataque - objetivo.Defensa;
        if (objetivo is Caballeria)
        {
            dañoBase += 2;
        }

        if (dañoBase < 0)
        {
            dañoBase = 0;
        }

        return dañoBase;
    }
    public bool Mover(Point destino, Mapa mapa)
    {
        try
        {
            if(mapa == null)
            {
                throw new ArgumentNullException(nameof(mapa), "El mapa no puede ser nulo.");
            }
            
            if (destino == null)
            {
                throw new ArgumentNullException(nameof(destino), "El destino no puede ser nulo.");
            }
            
            if (destino.X < 0 || destino.X >= mapa.Ancho || destino.Y < 0 || destino.Y >= mapa.Alto)
            {
                throw new ArgumentOutOfRangeException("Destino fuera de los límites del mapa.");
                return false;
            }

            Posicion = destino;
            return true;
        }
        catch(Exception e)
        {
            throw new InvalidOperationException("No se pudo mover la unidad.", e);
            return false;
        }
    }

    public string AtacarU(IUnidad objetivo)
    {
        if (objetivo == null)
        {
            throw new ArgumentNullException(nameof(objetivo), "El objetivo no puede ser nulo.");
        }
        
        int ataqueFinal = Ataque;
        int daño = ataqueFinal - objetivo.Defensa;
        
        if (objetivo is Infanteria && Propietario.Civilizacion.Nombre == "Aztecas" && Propietario.Civilizacion.UnidadEspecial == "Guerrero Jaguar")
        {
            ataqueFinal += 3;
        }
        
        daño = Math.Max(daño, 0);
        objetivo.Salud -= daño;   
        
        string info = $"{GetType().Name} atacó a {objetivo.GetType().Name} e hizo {daño} de daño.";
        info += $" {objetivo.GetType().Name} tiene {Math.Max(0, objetivo.Salud)} de salud restante.";


        if (objetivo is Caballeria)
        {
            ataqueFinal += 2;
        }
        
        if (objetivo.Salud <= 0)
        {
            objetivo.Propietario.Unidades.Remove(objetivo);
            info += $" {objetivo.GetType().Name} fue destruido.";
        }
        return info;
    }
   
    public string AtacarE(IEdificio objetivo)
    {
        int daño = Ataque;
        objetivo.Vida -= daño;
        
        string info = $"{GetType().Name} atacó el edificio {objetivo.GetType().Name} causando {daño} de daño.";
        info += $" Vida restante del edificio: {Math.Max(0, objetivo.Vida)}.";

        if (objetivo.Vida <= 0)
        {
            objetivo.Propietario.Edificios.Remove(objetivo);
            info += $" El edificio fue destruido.";
        }
        return info;
    }
}