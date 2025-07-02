using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

public class RecolectarCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = new JuegoFachada();

    [Command("recolectar")]
    [Summary("Ordena a un aldeano recolectar en una posicion. Sintaxis: recolectar <nombreJugador> <idAldeano> <x> <y>")]
    public async Task ExecuteAsync(string nombreJugador, int idAldeano, int x, int y)
    {
        _fachada.OrdenarRecolectar(nombreJugador, idAldeano, x, y);
        await ReplyAsync(($"Aldeano {idAldeano} del jugador '{nombreJugador}' recolectando en ({x}, {y})"));
    }
}