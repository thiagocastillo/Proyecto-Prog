using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;
using System;

public class CargarPartidaCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("cargarpartida")]
    [Summary("Carga una partida guardada previamente.")]
    public async Task ExecuteAsync(string nombreArchivo = null)
    {
        if (string.IsNullOrWhiteSpace(nombreArchivo))
        {
            await ReplyAsync("Debe indicar el nombre de la partida a cargar. Ejemplo: cargarpartida mi_partida");
            return;
        }

        try
        {
            _fachada.CargarPartida(nombreArchivo);
            await ReplyAsync($"📂 La partida '{nombreArchivo}' fue cargada correctamente.");
        }
        catch (Exception ex)
        {
            await ReplyAsync($"❌ Error al cargar la partida: {ex.Message}");
        }
    }
}