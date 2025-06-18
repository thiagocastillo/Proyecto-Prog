namespace Library;
using System.IO;
using System.Diagnostics;

public class Mapa
{
    public int Ancho { get; private set; } = 100;
    public int Alto { get; private set; } = 100;
    public List<RecursoNatural> Recursos { get; set; } = new List<RecursoNatural>();
    private static Random random = new Random();

    public Mapa()
    {
        GenerarRecursosAleatorios(50, 200);
    }

    public void GenerarRecursosAleatorios(int min, int max)
    {
        Recursos.Clear();
        int cantidad = random.Next(min, max + 1);
        var posicionesOcupadas = new HashSet<(int, int)>();

        for (int i = 0; i < 5; i++)
        {
            int x, y;
            do
            {
                x = random.Next(0, Ancho);
                y = random.Next(0, Alto);
            } while (!posicionesOcupadas.Add((x, y)));

            int vidaBase = random.Next(80, 151);
            Recursos.Add(new Arbol(vidaBase, new Point(x, y)));
        }

        for (int i = 5; i < cantidad; i++)
        {
            int x, y;
            do
            {
                x = random.Next(0, Ancho);
                y = random.Next(0, Alto);
            } while (!posicionesOcupadas.Add((x, y)));

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


public string MostrarMapa(List<Jugador> jugadores)
{
    char[,] grid = new char[Alto, Ancho];

    for (int y = 0; y < Alto; y++)
        for (int x = 0; x < Ancho; x++)
            grid[y, x] = '.';

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

    foreach (var jugador in jugadores)
    {
        if (jugador == null) continue;

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

        if (jugador.Unidades != null)
        {
            foreach (var unidad in jugador.Unidades)
            {
                if (unidad?.Posicion == null) continue;
                int x = unidad.Posicion.X;
                int y = unidad.Posicion.Y;
                if (x >= 0 && x < Ancho && y >= 0 && y < Alto)
                {
                    grid[y, x] = unidad is Aldeano ? 'A' : 'M';
                }
            }
        }
    }

    var sb = new System.Text.StringBuilder();

    sb.Append("    ");
    for (int x = 0; x < Ancho; x++)
        sb.Append(x.ToString("D2") + " ");
    sb.AppendLine();

    for (int y = 0; y < Alto; y++)
    {
        sb.Append(y.ToString("D2") + "  ");
        for (int x = 0; x < Ancho; x++)
            sb.Append(grid[y, x] + "  ");
        sb.AppendLine();
    }

    string ruta = "mapa.txt";
    File.WriteAllText(ruta, sb.ToString());

    Process.Start(new ProcessStartInfo("notepad.exe", ruta) { UseShellExecute = true });

    return sb.ToString();
}
}
