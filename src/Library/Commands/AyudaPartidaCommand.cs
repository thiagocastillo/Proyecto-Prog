using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class AyudaPartidaCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("ayudapartida")]
    [Summary("Muestra la ayuda del juego.")]
    public async Task AyudaAsync()
    {
        string ayuda = Ayuda.AyudaPartida();
        await ReplyAsync(ayuda);
    }
}