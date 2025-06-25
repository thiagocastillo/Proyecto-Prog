namespace Library.Domain;
using System.IO;
using System.Diagnostics;

// Clase que representa el mapa del juego
public class Mapa
{
    // Ancho del mapa (número de columnas)
    public int Ancho { get; private set; } = 100;
    // Alto del mapa (número de filas)
    public int Alto { get; private set; } = 100;
    // Lista de recursos naturales presentes en el mapa
    public List<RecursoNatural> Recursos { get; set; } = new List<RecursoNatural>();
    // Generador de números aleatorios para posiciones y recursos
    private static Random random = new Random();

    // Constructor: genera recursos aleatorios al crear el mapa
    public Mapa()
    {
        GenerarRecursosAleatorios(50, 200);
    }

    // Devuelve todas las unidades en una coordenada específica
    public List<IUnidad> ObtenerUnidadesEn(Point coordenada, List<Jugador> jugadores)
    {
        var unidades = new List<IUnidad>();
        
        foreach (var jugador in jugadores)
        {
            if (jugador?.Unidades == null) continue;
            unidades.AddRange(jugador.Unidades.Where(u => u.Posicion.Equals(coordenada)));
        }
        return unidades;
    }

    // Devuelve todos los edificios en una coordenada específica
    public List<IEdificio> ObtenerEdificiosEn(Point coordenada, List<Jugador> jugadores)
    {
        var edificios = new List<IEdificio>();
        foreach (var jugador in jugadores)
        {
            if (jugador?.Edificios == null) continue;
            edificios.AddRange(jugador.Edificios.Where(e => e.Posicion.Equals(coordenada)));
        }
        return edificios;
    }

    // Genera recursos naturales aleatorios en el mapa
    public void GenerarRecursosAleatorios(int min, int max)
    {
        Recursos.Clear();
        int cantidad = random.Next(min, max + 1);
        var posicionesOcupadas = new HashSet<(int, int)>();

        // Asegura que haya al menos 5 árboles
        for (int i = 0; i < 5; i++)
        {
            int x, y;
            
            do
            {
                x = random.Next(0, Ancho);
                y = random.Next(0, Alto);
            } 
            while (!posicionesOcupadas.Add((x, y)));

            int vidaBase = random.Next(80, 151);
            Recursos.Add(new Arbol(vidaBase, new Point(x, y)));
        }

        // Genera el resto de los recursos aleatoriamente
        for (int i = 5; i < cantidad; i++)
        {
            int x, y;
            do
            {
                x = random.Next(0, Ancho);
                y = random.Next(0, Alto);
            } 
            while (!posicionesOcupadas.Add((x, y)));

            int vidaBase = random.Next(80, 151);
            Point ubicacion = new Point(x, y);

            int tipo = random.Next(0, 3);
            
            RecursoNatural recurso = tipo switch
            {
                0 => new Arbol(vidaBase, ubicacion),
                1 => new Piedra(vidaBase, ubicacion),
                _ => new Oro(vidaBase, ubicacion)
            };
            Recursos.Add(recurso);
        }
    }

    // Muestra el mapa en un archivo de texto, representando recursos, unidades y edificios
    public string MostrarMapa(List<Jugador> jugadores)
    {
        // Matriz de caracteres para representar el mapa
        char[,] grid = new char[Alto, Ancho];

        // Inicializa el mapa vacío con puntos
        for (int y = 0; y < Alto; y++)
            for (int x = 0; x < Ancho; x++)
                grid[y, x] = '.';

        // Coloca los recursos en el mapa
        foreach (var recurso in Recursos)
        {
            if (recurso?.Ubicacion == null) continue;
            int x = recurso.Ubicacion.X;
            int y = recurso.Ubicacion.Y;
            
            if (x >= 0 && x < Ancho && y >= 0 && y < Alto)
            {
                char simbolo = recurso switch
                {
                    Arbol => 'T',
                    Piedra => '#',
                    Oro => '$',
                    _ => 'R'
                };
                grid[y, x] = simbolo;
            }
        }

        // Coloca los edificios y unidades de cada jugador en el mapa
        foreach (var jugador in jugadores)
        {
            if (jugador == null) continue;

            // Edificios
            if (jugador.Edificios != null)
            {
                foreach (var edificio in jugador.Edificios)
                {
                    if (edificio?.Posicion == null) continue;
                    int x = edificio.Posicion.X;
                    int y = edificio.Posicion.Y;
                    
                    if (x >= 0 && x < Ancho && y >= 0 && y < Alto)
                    {
                        grid[y, x] = edificio switch
                        {
                            CentroCivico => 'C',
                            Granja => 'G',
                            _ => 'X'
                        };
                    }
                }
            }

            // Unidades
            if (jugador.Unidades != null)
            {
                foreach (var unidad in jugador.Unidades)
                {
                    if (unidad?.Posicion == null) continue;
                    int x = unidad.Posicion.X;
                    int y = unidad.Posicion.Y;
                    
                    if (x >= 0 && x < Ancho && y >= 0 && y < Alto)
                    {
                        grid[y, x] = unidad switch
                        {
                            Aldeano => 'A',
                            Arquero => 'Æ',
                            Caballeria => '©',
                            Ratha => '®',
                            GuerreroJaguar => 'J',
                            Infanteria => 'I',
                            _ => '?'
                        };
                    }
                }
            }
        }

        // Construye el string del mapa con coordenadas
        var sb = new System.Text.StringBuilder();

        // Encabezado de columnas
        sb.Append("    ");
        
        for (int x = 0; x < Ancho; x++)
            sb.Append(x.ToString("D2") + " ");
        sb.AppendLine();

        // Filas del mapa
        for (int y = 0; y < Alto; y++)
        {
            sb.Append(y.ToString("D2") + "  ");
            for (int x = 0; x < Ancho; x++)
                sb.Append(grid[y, x] + "  ");
            sb.AppendLine();
        }

        // Guarda el mapa en un archivo de texto y lo abre con el Bloc de notas
        string ruta = "mapa.txt";
        File.WriteAllText(ruta, sb.ToString());
        Process.Start(new ProcessStartInfo("notepad.exe", ruta) { UseShellExecute = true });

        return "Abriendo Mapa en el Bloc de notas...";
    }
}