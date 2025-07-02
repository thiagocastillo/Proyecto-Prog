using Discord.Commands;
using System.Threading.Tasks;

namespace Ucu.Poo.DiscordBot.Commands;

public class EdificiosJugadorCommand : ModuleBase<SocketCommandContext>
{
    [Command("edificiosjugador")]
    [Summary("Muestra los edificios del jugador.")]
    public async Task ExecuteAsync()
    {
        // Aquí puedes agregar la lógica para obtener y mostrar los edificios del jugador.
        await ReplyAsync("Aquí se mostrarán los edificios del jugador.");
    }
}