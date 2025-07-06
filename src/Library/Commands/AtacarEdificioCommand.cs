using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class AtacarEdificioCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("atacaredificio")]
    [Summary("Ordena a una unidad militar atacar un edificio enemigo. Sintaxis: atacaredificio <nombreJugadorAtacante> <idAtacante> <nombreJugadorObjetivo> <idObjetivo>")]
    public async Task ExecuteAsync(string nombreJugadorAtacante, int idAtacante, string nombreJugadorObjetivo, int idObjetivo)
    {
        if (string.IsNullOrWhiteSpace(nombreJugadorAtacante) || string.IsNullOrWhiteSpace(nombreJugadorObjetivo))
        {
            await ReplyAsync("Faltan argumentos en comando, recordar sintaxis: atacaredificio <nombreJugadorAtacante> <idAtacante> <nombreJugadorObjetivo> <idObjetivo>");
            return;
        }

        try
        {
            string resultado = _fachada.AtacarEdificio(nombreJugadorAtacante, idAtacante, nombreJugadorObjetivo, idObjetivo);
            await ReplyAsync(resultado);
        }
        catch (Exception ex)
        {
            await ReplyAsync(ex.Message);
        }
    }
}