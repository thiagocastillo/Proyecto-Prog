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

        public TipoUnidad Tipo
        {
            get
            {
                return TipoUnidad.Caballeria;
            }
        }
        public Caballeria(Jugador propietario)
        {
            Propietario = propietario;
        }
        
        public double CalcularDaño(IUnidad objetivo)
        {
            double dañoBase = this.Ataque - objetivo.Defensa;
            if (objetivo.Tipo == TipoUnidad.Arquero)
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

        public void AtacarU(IUnidad objetivo)
        {
            int daño = Ataque - objetivo.Defensa;
            // Registrar daño
            if (objetivo is Arquero)
            {
                daño += 2;
            }
        }
        public void AtacarE(IEdificio objetivo)
        {
            int daño = Ataque - objetivo.Vida;
            // Registrar daño
        }
    }
