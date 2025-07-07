// src/Library/Domain/Mapa.cs
namespace Library.Domain;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Text;

// Clase que representa el mapa del juego
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

    public List<IUnidad> ObtenerUnidadesEn(Point coordenada, List<Jugador> jugadores)
    {
        List<IUnidad> unidades = new List<IUnidad>();
        foreach (var jugador in jugadores)
        {
            if (jugador?.Unidades == null) continue;
            unidades.AddRange(jugador.Unidades.Where(u => u.Posicion.Equals(coordenada)));
        }
        return unidades;
    }

    public List<IEdificio> ObtenerEdificiosEn(Point coordenada, List<Jugador> jugadores)
    {
        List<IEdificio> edificios = new List<IEdificio>();
        foreach (var jugador in jugadores)
        {
            if (jugador?.Edificios == null) continue;
            edificios.AddRange(jugador.Edificios.Where(e => e.Posicion.Equals(coordenada)));
        }
        return edificios;
    }

    public void GenerarRecursosAleatorios(int min, int max)
    {
        Recursos.Clear();
        int cantidad = random.Next(min, max + 1);
        var posicionesOcupadas = new HashSet<(int, int)>();

        // Al menos 5 árboles
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

        // Resto de recursos
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

    public string MostrarMapaTXT(List<Jugador> jugadores)
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

        var sb = new StringBuilder();
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

        return "Abriendo Mapa en el Bloc de notas...";
    }

    public string MostrarMapaHtml(List<Jugador> jugadores)
    {
   
        var coloresJugadores = new[] { "#FF0000", "#0000FF" };
        var colorArbol = "#228B22";
        var colorPiedra = "#A9A9A9";
        var colorOro = "#FFD700";
        var colorVacio = "#FFFFFF";

        var sb = new StringBuilder();
        sb.AppendLine("<html><head><meta charset='UTF-8'><title>Mapa</title></head><body>");
        sb.AppendLine("<table border='1' cellspacing='0' cellpadding='2' style='font-family:monospace;font-size:10px;border-collapse:collapse;'>");

        sb.Append("<tr><td></td>");
        for (int x = 0; x < Ancho; x++)
            sb.Append($"<td style='background:#eee'>{x:D2}</td>");
        sb.AppendLine("</tr>");

        for (int y = 0; y < Alto; y++)
        {
            sb.Append($"<tr><td style='background:#eee'>{y:D2}</td>");
            for (int x = 0; x < Ancho; x++)
            {
                string color = colorVacio;
                string contenido = "&nbsp;";

                var recurso = Recursos.FirstOrDefault(r => r.Ubicacion.X == x && r.Ubicacion.Y == y);
                if (recurso != null)
                {
                    if (recurso is Arbol)
                    {
                        color = colorArbol;
                        contenido = "T";
                    }
                    else if (recurso is Piedra)
                    {
                        color = colorPiedra;
                        contenido = "#";
                    }
                    else if (recurso is Oro)
                    {
                        color = colorOro;
                        contenido = "$";
                    }
                }

                for (int j = 0; j < jugadores.Count; j++)
                {
                    var jugador = jugadores[j];
                    var colorJugador = coloresJugadores[j % coloresJugadores.Length];
                    
                    var edificio = jugador.Edificios?.FirstOrDefault(e => e.Posicion.X == x && e.Posicion.Y == y);
                    if (edificio != null)
                    {
                        color = colorJugador;
                        contenido = edificio switch
                        {
                            CentroCivico => "C",
                            Granja => "G",
                            _ => "X"
                        };
                    }

                    var unidad = jugador.Unidades?.FirstOrDefault(u => u.Posicion.X == x && u.Posicion.Y == y);
                    if (unidad != null)
                    {
                        color = colorJugador;
                        contenido = unidad switch
                        {
                            Aldeano => "A",
                            Arquero => "Æ",
                            Caballeria => "©",
                            Ratha => "®",
                            GuerreroJaguar => "J",
                            Infanteria => "I",
                            _ => "?"
                        };
                    }
                }

                sb.Append($"<td style='width:12px;height:12px;background:{color};text-align:center'>{contenido}</td>");
            }
            sb.AppendLine("</tr>");
        }

        sb.AppendLine("</table></body></html>");

        string ruta = "mapa.html";
        File.WriteAllText(ruta, sb.ToString());
        Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true });

        return "Mapa abierto en el navegador.";
    }
}