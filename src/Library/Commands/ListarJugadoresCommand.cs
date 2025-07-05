using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;
using System.Linq;

namespace Ucu.Poo.DiscordBot.Commands;

public class ListarJugadoresCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("listarjugadores")]
    [Summary("Lista los Jugadores existentes en la Partida Actual.")]
    public async Task ExecuteAsync()
    {
        var jugadores = _fachada.ObtenerJugadores();
        if (jugadores == null || jugadores.Count == 0)
        {
            await ReplyAsync("No hay jugadores en la partida.");
        }
        else
        {
            string lista = string.Join(", ", jugadores.Select(j => $"{j.Nombre} ({j.Civilizacion.Nombre})"));
            await ReplyAsync($"Jugadores: {lista}");
        }
    }
}