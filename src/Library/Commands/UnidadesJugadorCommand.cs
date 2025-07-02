using Discord.Commands;

namespace Ucu.Poo.DiscordBot.Commands;

public class UnidadesJugadorCommand
{
    [Command("unidadesjugador")]
    [Summary("Muestra las unidades del jugador actual.")]
    public async Task ExecuteAsync()
    {
        // Aquí se llamaría a la lógica para obtener las unidades del jugador actual.
        // Por ejemplo, podrías usar una fachada o un servicio que maneje el estado del juego.
        
        // Simulación de respuesta
        await Task.CompletedTask; // Reemplazar con la lógica real
    }
}