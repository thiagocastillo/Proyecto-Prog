
using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class MoverUnidadCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("moverunidad")]
    [Summary("Mueve una unidad a una nueva posicion. Sintaxis: moverunidad <nombreJugador> <idUnidad> <x> <y>")]
    public async Task ExecuteAsync(
        string nombreJugador = null,
        int? idUnidad = null,
        int? x = null,
        int? y = null)
    {
        if (string.IsNullOrWhiteSpace(nombreJugador) ||
            idUnidad == null ||
            x == null ||
            y == null)
        {
            await ReplyAsync("Faltan argumentos en comando, recordar sintaxis: moverunidad <nombreJugador> <idUnidad> <x> <y>");
            return;
        }

        try
        {
            _fachada.MoverUnidad(nombreJugador, idUnidad.Value, new Point(x.Value, y.Value));
            await ReplyAsync($"Unidad movida a la posición ({x}, {y}) para el jugador '{nombreJugador}'.");
        }
        catch (System.Exception ex)
        {
            await ReplyAsync($"Error al mover unidad: {ex.Message}");
        }
    }
}