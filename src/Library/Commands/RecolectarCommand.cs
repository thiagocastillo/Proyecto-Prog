using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class RecolectarCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("recolectar")]
    [Summary("Ordena a un aldeano recolectar en una posicion. Sintaxis: recolectar <nombreJugador> <idAldeano> <x> <y>")]
    public async Task ExecuteAsync(
        string nombreJugador = null,
        int? idAldeano = null,
        int? x = null,
        int? y = null)
    {
        if (string.IsNullOrWhiteSpace(nombreJugador) ||
            idAldeano == null ||
            x == null ||
            y == null)
        {
            await ReplyAsync("Faltan argumentos en comando, recordar sintaxis: recolectar <nombreJugador> <idAldeano> <x> <y>");
            return;
        }

        try
        {
            _fachada.OrdenarRecolectar(nombreJugador, idAldeano.Value, x.Value, y.Value);
            await ReplyAsync($"Aldeano {idAldeano} del jugador '{nombreJugador}' recolectando en ({x}, {y})");
        }
        catch (System.Exception ex)
        {
            await ReplyAsync($"Error al recolectar: {ex.Message}");
        }
    }
}