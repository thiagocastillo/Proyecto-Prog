using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

public class MoverUnidadCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = new JuegoFachada();

    [Command("moverunidad")]
    [Summary("Mueve una unidad a una nueva posicion. Sintaxis: moverUnidad <nombreJugador> <idUnidad> <x> <y>")]
    public async Task ExecuteAsync(string tipoUnidad, int idUnidad, int x, int y)
    {
        try
        {
            _fachada.MoverUnidad(tipoUnidad, idUnidad, new Point(x, y));
            await ReplyAsync($"Unidad '{tipoUnidad}' se movio a la posicion ({x}, {y}) ");
        }
        catch (Exception ex)
        {
            await ReplyAsync($"Error al mover unidad: {ex:Message}");
        }
    }
}