using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

public class SalirCommand: ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("salir")]
    [Alias("exit")]
    [Summary("Sale del juego.")]
    public async Task ExecuteAsync()
    {
        await ReplyAsync("Saliendo...");
        Environment.Exit(0); // Termina el proceso del bot
    }
}
