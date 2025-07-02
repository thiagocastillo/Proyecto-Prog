using Discord.Commands;
using System.Threading.Tasks;

public class ComandoNoReconocidoCommand : ModuleBase<SocketCommandContext>
{
    [Command("comandodesconocido")]
    [Summary("Responde cuando el comando no es reconocido.")]
    public async Task ExecuteAsync()
    {
        await ReplyAsync("Comando no reconocido.");
    }
}