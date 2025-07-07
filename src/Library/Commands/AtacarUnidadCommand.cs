using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class AtacarUnidadCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("atacarunidad")]
    [Summary("Ataca unidad del enemigo elegida por el jugador. Sintaxis: atacarunidad <nombreJugadorAtacante> <idUnidadAtacante> <nombreJugadorObjetivo> <idUnidadObjetivo>")]
    public async Task ExecuteAsync(
                                    string nombreJugadorAtacante = null,
                                    int? idUnidadAtacante = null,
                                    string nombreJugadorObjetivo = null,
                                    int? idUnidadObjetivo = null)
    {
        if (string.IsNullOrWhiteSpace(nombreJugadorAtacante) ||
                                    idUnidadAtacante == null ||
                                    string.IsNullOrWhiteSpace(nombreJugadorObjetivo) ||
                                    idUnidadObjetivo == null)
        {
            await ReplyAsync("Faltan argumentos en comando, recordar sintaxis: atacarunidad <nombreJugadorAtacante> <idUnidadAtacante> <nombreJugadorObjetivo> <idUnidadObjetivo>");
            return;
        }

        string resultado = _fachada.AtacarUnidad(nombreJugadorAtacante,
                                                idUnidadAtacante.Value,
                                                nombreJugadorObjetivo,
                                                idUnidadObjetivo.Value);

        await ReplyAsync(resultado);
    }
}