using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class AtacarUnidadCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("atacarunidad")]
    [Summary("Ataca unidad del enemigo elegida por el jugador. Sintaxis: atacarunidad <nombreJugadorAtacante> <idUnidadAtacante> <nombreJugadorObjetivo> <idUnidadObjetivo>")]

    public async Task ExecuteAsync(string nombreJugadorAtacante, int idUnidadAtacante, string nombreJugadorObjetivo, int idUnidadObjetivo)
    {
        string resultado = _fachada.AtacarUnidad(nombreJugadorAtacante, idUnidadAtacante, nombreJugadorObjetivo, idUnidadObjetivo);
        await ReplyAsync(resultado);
    }
}