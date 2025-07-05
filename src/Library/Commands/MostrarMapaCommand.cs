using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class MostrarMapaCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("mostrarmapa")]
    [Summary("Muestra el Mapa de la Partida actual.")]
    public async Task ExecuteAsync()
    {
        string mapa = _fachada.MostrarMapa();
        await ReplyAsync(mapa);
    }
}