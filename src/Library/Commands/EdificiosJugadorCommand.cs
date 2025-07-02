using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;
using System.Collections.Generic;

public class EdificiosJugadorCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = new JuegoFachada();

    [Command("listaredificiosjugador")]
    [Summary("Lista los edificios del jugador actual.")]
    public async Task ExecuteAsync()
    {
        string jugadorId = Context.User.Id.ToString();
        // Asegúrate de que este método busque por ID, o adapta tu lógica de jugadores
        List<IEdificio> edificios = _fachada.ObtenerEdificiosJugador(jugadorId);

        if (edificios == null || edificios.Count == 0)
        {
            await ReplyAsync("No tienes edificios.");
        }
        else
        {
            List<string> lista = new List<string>();
            for (int i = 0; i < edificios.Count; i++)
            {
                lista.Add($"{i}: {edificios[i].GetType().Name}");
            }
            await ReplyAsync($"Tus edificios:\n{string.Join("\n", lista)}");
        }
    }
}