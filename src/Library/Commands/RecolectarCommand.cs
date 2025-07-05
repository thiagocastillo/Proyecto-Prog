using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class RecolectarCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("recolectar")]
    [Summary("Ordena a un aldeano recolectar en una posicion. Sintaxis: recolectar <nombreJugador> <idAldeano> <x> <y>")]
    public async Task ExecuteAsync(string nombreJugador, int idAldeano, int x, int y)
    {
        try
        {
            _fachada.OrdenarRecolectar(nombreJugador, idAldeano, x, y);
            await ReplyAsync(($"Aldeano {idAldeano} del jugador '{nombreJugador}' recolectando en ({x}, {y})"));
        }
        catch (Exception ex)
        {
            await ReplyAsync($"Error al recolectar: {ex.Message}");
        }
    }
}