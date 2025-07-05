using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class AgregarJugadorCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = new JuegoFachada();

    [Command("agregarjugador")]
    [Summary("Agrega un jugador a la partida.")]
    public async Task ExecuteAsync(string nombreJugador = null, string civilizacion = null)
    {
        if (string.IsNullOrWhiteSpace(nombreJugador) || string.IsNullOrWhiteSpace(civilizacion))
        {
            await ReplyAsync("Faltan argumentos en comando, recordar sintaxis: agregarjugador <nombreJugador> <civilización>");
            return;
        }

        _fachada.AgregarJugadorAPartida(nombreJugador, civilizacion.ToLower());
        await ReplyAsync($"Jugador '{nombreJugador}' agregado con civilización '{civilizacion}'.");
    }
}