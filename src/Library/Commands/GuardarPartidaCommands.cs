using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;
using System;

public class GuardarPartidaCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("guardarpartida")]
    [Summary("Guarda la partida actual en la ruta especificada.")]
    public async Task ExecuteAsync(string rutaArchivo = "partida.json")
    {
        try
        {
            _fachada.GuardarPartida(rutaArchivo);
            await ReplyAsync($"Partida guardada en `{rutaArchivo}`.");
        }
        catch (Exception ex)
        {
            await ReplyAsync($"Error al guardar la partida: {ex.Message}");
        }
    }
}

