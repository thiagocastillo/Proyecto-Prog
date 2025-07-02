using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class CrearPartidaCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = new JuegoFachada();

    [Command("crearpartida")]
    [Summary("Crea una nueva partida.")]
    public async Task ExecuteAsync()
    {
        _fachada.CrearNuevaPartida();
        await ReplyAsync("Partida creada.");
    }
}