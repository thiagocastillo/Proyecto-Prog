using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;
using System;

public class CargarPartidaCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("cargarpartida")]
    [Summary("Carga la partida desde la ruta especificada.")]
    public async Task ExecuteAsync(string rutaArchivo = "partida.json")
    {
        try
        {
            _fachada.CargarPartida(rutaArchivo);
            await ReplyAsync($"Partida cargada desde `{rutaArchivo}`.");
        }
        catch (Exception ex)
        {
            await ReplyAsync($"Error al cargar la partida: {ex.Message}");
        }
    }
}