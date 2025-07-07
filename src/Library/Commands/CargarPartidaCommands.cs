using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;
using System;

public class CargarPartidaCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("cargarpartida")]
    [Summary("Carga la partida guardada con el nombre especificado (sin extensión).")]
    public async Task ExecuteAsync(string nombreArchivo = "partida")
    {
        try
        {
            _fachada.CargarPartida(nombreArchivo);
            await ReplyAsync($"Partida cargada desde `{nombreArchivo}`.");
        }
        catch (Exception ex)
        {
            await ReplyAsync($"Error al cargar la partida: {ex.Message}");
        }
    }
}