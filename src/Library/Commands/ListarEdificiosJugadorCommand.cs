using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;
using System.Collections.Generic;

public class ListarEdificiosJugadorCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("listaredificiosjugador")]
    [Summary("Lista los edificios del jugador actual.")]
    public async Task ExecuteAsync(string nombreJugador)
    {
        try
        {
            List<IEdificio> edificios = _fachada.ObtenerEdificiosJugador(nombreJugador);

            if (edificios == null || edificios.Count == 0)
            {
                await ReplyAsync("No tienes edificios.");
                return;
            }

            List<string> lista = new();
            
            for (int i = 0; i < edificios.Count; i++)
            {
                lista.Add($"{i}: {edificios[i].GetType().Name}");
            }
            await ReplyAsync($"Tus edificios:\n{string.Join("\n", lista)}");
        }
        catch (System.Exception ex)
        {
            await ReplyAsync($"Error: {ex.Message}");
        }
    }
}