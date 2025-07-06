using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class EntrenarUnidadCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("entrenarunidad")]
    [Summary("Entrena una unidad para un jugador. Sintaxis: entrenarunidad <nombreJugador> <tipoUnidad> <x> <y>")]
    public async Task ExecuteAsync(
        string nombreJugador = null,
        string tipoUnidad = null,
        int? x = null,
        int? y = null)
    {
<<<<<<< HEAD
        if (string.IsNullOrWhiteSpace(nombreJugador) ||
            string.IsNullOrWhiteSpace(tipoUnidad) ||
            x == null ||
            y == null)
=======
        if (string.IsNullOrWhiteSpace(nombreJugador) || string.IsNullOrWhiteSpace(tipoUnidad))
>>>>>>> a2322a90539cdade8ea7fe520d133588925952d2
        {
            await ReplyAsync("Faltan argumentos en comando, recordar sintaxis: entrenarunidad <nombreJugador> <tipoUnidad> <x> <y>");
            return;
        }

<<<<<<< HEAD
        _fachada.EntrenarUnidad(nombreJugador, tipoUnidad, new Point(x.Value, y.Value));
        await ReplyAsync($"Unidad '{tipoUnidad}' entrenada en ({x}, {y}) para el jugador '{nombreJugador}'");
=======
        try
        {
            _fachada.EntrenarUnidad(nombreJugador, tipoUnidad, new Point(x, y));
            await ReplyAsync($"Unidad '{tipoUnidad}' entrenada en ({x}, {y}) para el jugador '{nombreJugador}'");
        }
        catch (Exception ex)
        {
            await ReplyAsync(ex.Message);
        }
>>>>>>> a2322a90539cdade8ea7fe520d133588925952d2
    }
}