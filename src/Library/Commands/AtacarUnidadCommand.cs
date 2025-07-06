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
<<<<<<< HEAD
        if (string.IsNullOrWhiteSpace(nombreJugadorAtacante) ||
            idUnidadAtacante == null ||
            string.IsNullOrWhiteSpace(nombreJugadorObjetivo) ||
            idUnidadObjetivo == null)
        {
            await ReplyAsync("Faltan argumentos en comando, recordar sintaxis: atacarunidad <nombreJugadorAtacante> <idUnidadAtacante> <nombreJugadorObjetivo> <idUnidadObjetivo>");
            return;
        }

        string resultado = _fachada.AtacarUnidad(
            nombreJugadorAtacante,
            idUnidadAtacante.Value,
            nombreJugadorObjetivo,
            idUnidadObjetivo.Value);

        await ReplyAsync(resultado);
=======
        try
        {
            string resultado = _fachada.AtacarUnidad(nombreJugadorAtacante, idUnidadAtacante, nombreJugadorObjetivo, idUnidadObjetivo);
            await ReplyAsync(resultado);
        }
        catch (Exception ex)
        {
            await ReplyAsync(ex.Message);
        }
>>>>>>> a2322a90539cdade8ea7fe520d133588925952d2
    }
}