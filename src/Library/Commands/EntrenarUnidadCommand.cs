using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class EntrenarUnidadCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("entrenarunidad")]
    [Summary("Entrena una unidad para un jugador. Sintaxis: entrenarunidad <nombreJugador> <tipoUnidad> <x> <y>")]
    public async Task ExecuteAsync(string nombreJugador, string tipoUnidad, int x, int y)
    {
        if (string.IsNullOrWhiteSpace(nombreJugador) || string.IsNullOrWhiteSpace(tipoUnidad))
        {
            await ReplyAsync("Faltan argumentos en comando, recordar sintaxis: entrenarunidad <nombreJugador> <tipoUnidad> <x> <y>");
            return;
        }

        try
        {
            _fachada.EntrenarUnidad(nombreJugador, tipoUnidad, new Point(x, y));
            await ReplyAsync($"Unidad '{tipoUnidad}' entrenada en ({x}, {y}) para el jugador '{nombreJugador}'");
        }
        catch (Exception ex)
        {
            await ReplyAsync(ex.Message);
        }
    }
}