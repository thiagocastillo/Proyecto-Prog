namespace Library;

public class Mapa
{
    public int Ancho { get; private set; }
    public int Alto { get; private set; }

    public Mapa(int ancho, int alto)
    {
        Ancho = 100;
        Alto = 100;
    }
    public string MostrarMapa(List<Jugador> jugadores)
    {
        char[,] grid = new char[Alto, Ancho];
        // Inicializa el mapa vacío
        for (int y = 0; y < Alto; y++)
        for (int x = 0; x < Ancho; x++)
            grid[y, x] = '.';

        foreach (var jugador in jugadores)
        {
            foreach (var edificio in jugador.Edificios)
            {
                if (edificio is CentroCivico)
                    grid[edificio.Posicion.Y, edificio.Posicion.X] = 'C';
                else if (edificio is Granja)
                    grid[edificio.Posicion.Y, edificio.Posicion.X] = 'G';
                else
                    grid[edificio.Posicion.Y, edificio.Posicion.X] = 'X';
            }
            foreach (var unidad in jugador.Unidades)
            {
                if (unidad is Aldeano)
                    grid[unidad.Posicion.Y, unidad.Posicion.X] = 'A';
                else
                    grid[unidad.Posicion.Y, unidad.Posicion.X] = 'M';
            }
        }

        // Construye el string del mapa
        var sb = new System.Text.StringBuilder();
        for (int y = 0; y < Alto; y++)
        {
            for (int x = 0; x < Ancho; x++)
                sb.Append(grid[y, x]);
            sb.AppendLine();
        }
        return sb.ToString();
    }
}