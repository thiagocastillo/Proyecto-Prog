using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class MoverUnidadesCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = new JuegoFachada();

    [Command("moverunidades")]
    [Summary("Mueve multiples unidades a una posicion. Sintaxis: moverunidades <nombreJugador> <x> <y> <id1> <id2>")]
    public async Task ExecuteAsync(string nombreJugador, int x, int y, [Remainder] string idsTexto)
    {
        
        string[] partes = idsTexto.Split(' ');
        List<int> ids = new List<int>();
        foreach (string parte in partes)
        {
            if (!string.IsNullOrWhiteSpace(parte))
            {
                ids.Add(int.Parse(parte));
            }
        }
        var resultados = _fachada.MoverUnidades(nombreJugador, ids, new Point(x, y));
        await ReplyAsync(string.Join("\n", resultados));
        }
    }
