using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class ConstruirEdificioCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("construiredificio")]
    [Summary("Construye un edificio en la posicion indicada. Sintaxis: construiredificio <nombreJugador> <tipoEdificio> <x> <y>")]
    public async Task ExecuteAsync(string nombreJugador, string tipoEdificio, int x, int y)
    {
        _fachada.ConstruirEdificio(nombreJugador, tipoEdificio, new Point(x, y));
        await ReplyAsync($"Edificio '{tipoEdificio}' contruido en ({x}, {y}) para el jugador '{nombreJugador}'");
    }
}