using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;
using System;

public class AtacarEdificioCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("atacaredificio")]
    [Summary("Ordena a una unidad militar atacar un edificio enemigo. Sintaxis: atacaredificio <nombreJugadorAtacante> <idAtacante> <nombreJugadorObjetivo> <idObjetivo>")]
    public async Task ExecuteAsync(string nombreJugadorAtacante = null, int? idAtacante = null, string nombreJugadorObjetivo = null, int? idObjetivo = null)
    {
        if (string.IsNullOrWhiteSpace(nombreJugadorAtacante) ||
            string.IsNullOrWhiteSpace(nombreJugadorObjetivo) ||
            idAtacante == null ||
            idObjetivo == null)
        {
            await ReplyAsync("Faltan argumentos en comando, recordar sintaxis: atacaredificio <nombreJugadorAtacante> <idAtacante> <nombreJugadorObjetivo> <idObjetivo>");
            return;
        }

        try
        {
            string resultado = _fachada.AtacarEdificio(nombreJugadorAtacante, idAtacante.Value, nombreJugadorObjetivo, idObjetivo.Value);
            await ReplyAsync(resultado);

            if (resultado.Contains("ganó la partida"))
            {  
                await Task.Delay(2000); // Espera para asegurar que el mensaje se envíe
                Environment.Exit(0);    
            }
        }
        catch (Exception ex)
        {
            await ReplyAsync(ex.Message);
        }
    }
}