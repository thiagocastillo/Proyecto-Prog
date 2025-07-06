using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;
using System.Collections.Generic;
using System.Text;

public class ListarRecursosJugadorCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("listarrecursosjugador")]
    [Summary("Lista los recursos del jugador actual. Sintaxis: listarrecursosjugador <nombreJugador>")]
    public async Task ExecuteAsync(string nombreJugador = null)
    {
        if (string.IsNullOrWhiteSpace(nombreJugador))
        {
            await ReplyAsync("Faltan argumentos en comando, recordar sintaxis: listarrecursosjugador <nombreJugador>");
            return;
        }

        try
        {
            Dictionary<string, int> recursos = _fachada.ObtenerRecursosJugador(nombreJugador);

            if (recursos == null || recursos.Count == 0)
            {
                await ReplyAsync("El jugador no existe, cree uno usando el comando correspondiente.");
                return;
            }

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, int> r in recursos)
                sb.AppendLine($"{r.Key}: {r.Value}");

            await ReplyAsync(sb.ToString().TrimEnd());
        }
        catch (System.Exception ex)
        {
            await ReplyAsync($"Error: {ex.Message}");
        }
    }
}