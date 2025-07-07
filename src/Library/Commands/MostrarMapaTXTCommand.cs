using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class MostrarMapaTXTCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("mostrarmapaTXT")]
    [Summary("Muestra el Mapa de la Partida actual.")]
    public async Task ExecuteAsync()
    {
        try
        {
            string mapa = _fachada.MostrarMapaTXT();
            await ReplyAsync(mapa);
        }
        catch (Exception ex)
        {
            await ReplyAsync($"Error al mostrar el mapa: {ex.Message}");
        }
    }
}