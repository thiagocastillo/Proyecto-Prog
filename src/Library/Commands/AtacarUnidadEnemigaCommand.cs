using Discord.Commands;
using Library.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

public class AtacarUnidadEnemigaCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = new JuegoFachada();

    [Command("AtacarUnidadEnemiga")]
    [Summary("Ataca unidad del enemigo elegida por el jugador")]

    public async Task ExecuteAsync(string nombreJugador, int idUnidadAtacante, string nombreJugadorRecibeAtaque, int idUnidadRecibeAtaque)
    {
        _fachada.AtacarUnidad(nombreJugador, idUnidadAtacante, nombreJugadorRecibeAtaque, idUnidadRecibeAtaque);
        await ReplyAsync($"{idUnidadAtacante} del jugador {nombreJugador} ataca unidad {idUnidadRecibeAtaque} del jugador {nombreJugadorRecibeAtaque}");
    
    }
}