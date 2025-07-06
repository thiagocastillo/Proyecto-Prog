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
        try
        {
            string mapa = _fachada.MostrarMapa();
            await ReplyAsync(mapa);
        }
        catch (Exception ex)
        {
            await ReplyAsync($"Error al mostrar el mapa: {ex.Message}");
        }
    }
}