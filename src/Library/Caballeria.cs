namespace Library;


    public class Caballeria : IUnidadMilitar
    {
        public Jugador Propietario { get; private set; }
        public int Ataque { get; private set; } = 12;
        public int Defensa { get; private set; } = 7;
        public double Velocidad { get; private set; } = 1.5;
        public Point Posicion { get; set; }

        public Caballeria(Jugador propietario)
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
            int daño = Ataque - objetivo.Defensa;
            // Registrar daño
        }
    }
