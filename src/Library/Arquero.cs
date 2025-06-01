namespace Library;
public class Arquero : IUnidadMilitar
{
    public Jugador Propietario { get; private set; }
    public int Ataque { get; set; } = 8; // Añadimos 'set;'
    public int Defensa { get; private set; } = 3;
    
    public double Velocidad { get; private set; } = 1.2;

    public int Salud { get; set; }  
    public Point Posicion { get; set; }
    
    public int TiempoDeCreacion { get; private set; } = 10;
    
    public Arquero(Jugador propietario)
    {
        Propietario = propietario;
        if (propietario.Civilizacion.Nombre == "Armenios" && propietario.Civilizacion.UnidadEspecial == "Arquero Compuesto")
        {
            Ataque += 2;
        }
    }

    public double CalcularDaño(IUnidad objetivo)
    {
        double dañoBase = this.Ataque - objetivo.Defensa;
        if (objetivo is Infanteria)
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
        if (destino.X < 0 || destino.X >= mapa.Ancho || destino.Y < 0 || destino.Y >= mapa.Alto)
        {
            return false; 
        }
        Posicion = destino;
        return true;
    }

    public string AtacarU(IUnidad objetivo)
    {
        int daño = Ataque - objetivo.Defensa;
       

        if (objetivo is Infanteria)
        {
            daño += 2;
        }
        
        daño = Math.Max(daño, 0);
        objetivo.Salud -= daño;   
        
        string info = $"{GetType().Name} atacó a {objetivo.GetType().Name} e hizo {daño} de daño.";
        info += $" {objetivo.GetType().Name} tiene {Math.Max(0, objetivo.Salud)} de salud restante.";
        
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