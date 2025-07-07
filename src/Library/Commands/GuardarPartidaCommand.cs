using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;
using System;

public class GuardarPartidaCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("guardarpartida")]
    [Summary("Guarda el estado actual de la partida.")]
    public async Task ExecuteAsync(string nombreArchivo = null)
    {
        if (string.IsNullOrWhiteSpace(nombreArchivo))
        {
            await ReplyAsync("Debe especificar un nombre para la partida. Ejemplo: guardarpartida mi_partida");
            return;
        }

        try
        {
            _fachada.GuardarPartida(nombreArchivo);
            await ReplyAsync($"✅ La partida se guardó correctamente como '{nombreArchivo}.json'.");
        }
        catch (Exception ex)
        {
            await ReplyAsync($"❌ Error al guardar la partida: {ex.Message}");
        }
    }
}