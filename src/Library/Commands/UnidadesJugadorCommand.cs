using System.Text;
using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;
using System.Collections.Generic;

public class UnidadesJugadorCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("unidadesjugador")]
    [Summary("Lista las unidades del jugador. Sintaxis: unidadesjugador <nombreJugador>")]
    public async Task ExecuteAsync(string nombreJugador = null)
    {
        if (string.IsNullOrWhiteSpace(nombreJugador))
        {
            await ReplyAsync("Faltan argumentos en comando, recordar sintaxis: unidadesjugador <nombreJugador>");
            return;
        }

        try
        {
            List<IUnidad> unidades = _fachada.ObtenerUnidadesJugador(nombreJugador);

            if (unidades == null || unidades.Count == 0)
            {
                await ReplyAsync("El jugador no existe o no tiene unidades.");
                return;
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < unidades.Count; i++)
            {
                sb.AppendLine($"{i}: {unidades[i].GetType().Name}");
            }

            await ReplyAsync(sb.ToString().TrimEnd());
        }
        catch (System.Exception ex)
        {
            await ReplyAsync($"Error: {ex.Message}");
        }
    }
}