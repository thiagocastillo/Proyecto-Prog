
using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;
using System.Collections.Generic;

public class MoverUnidadesCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("moverunidades")]
    [Summary("Mueve multiples unidades a una posicion. Sintaxis: moverunidades <nombreJugador> <x> <y> <id1> <id2> ...")]
    public async Task ExecuteAsync(
        string nombreJugador = null,
        int? x = null,
        int? y = null,
        [Remainder] string idsTexto = null)
    {
<<<<<<< HEAD
        if (string.IsNullOrWhiteSpace(nombreJugador) ||
            x == null ||
            y == null ||
            string.IsNullOrWhiteSpace(idsTexto))
        {
            await ReplyAsync("Faltan argumentos en comando, recordar sintaxis: moverunidades <nombreJugador> <x> <y> <id1> <id2> ...");
            return;
        }

        try
        {
            string[] partes = idsTexto.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
            List<int> ids = new();
            foreach (string parte in partes)
            {
                if (int.TryParse(parte, out int id))
                    ids.Add(id);
                else
                {
                    await ReplyAsync($"ID de unidad inválido: {parte}");
                    return;
                }
            }

            if (ids.Count == 0)
            {
                await ReplyAsync("Debes especificar al menos un ID de unidad.");
                return;
            }

            List<string> resultados = _fachada.MoverUnidades(nombreJugador, ids, new Point(x.Value, y.Value));
            await ReplyAsync(string.Join("\n", resultados));
        }
        catch (System.Exception ex)
=======
        try
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
            List<string> resultados = _fachada.MoverUnidades(nombreJugador, ids, new Point(x, y));
            await ReplyAsync(string.Join("\n", resultados));
        }
        catch (Exception ex)
>>>>>>> a2322a90539cdade8ea7fe520d133588925952d2
        {
            await ReplyAsync($"Error al mover unidades: {ex.Message}");
        }
    }
}