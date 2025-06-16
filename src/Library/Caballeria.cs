namespace Library;

    public class Caballeria : IUnidadMilitar
    {
        public Jugador Propietario { get; private set; }
        public int Ataque { get; private set; } = 12;
        public int Defensa { get; private set; } = 7;
        public double Velocidad { get; private set; } = 1.5;

        public int Salud { get; set; }
        public Point Posicion { get; set; }
        
        public int TiempoDeCreacion { get; private set; } = 10;
        
        public Caballeria(Jugador propietario)
        {
            Propietario = propietario;
        }
        
        public double CalcularDaño(IUnidad objetivo)
        {
            double dañoBase = this.Ataque - objetivo.Defensa;
            if (objetivo is Arquero)
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

        public string AtacarUnidad(IUnidad objetivo)
        {
            int ataqueFinal = Ataque;
            int daño = ataqueFinal - objetivo.Defensa;
            
            
            if (objetivo is Arquero)
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
        public string AtacarEdificio(IEdificio objetivo)
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
