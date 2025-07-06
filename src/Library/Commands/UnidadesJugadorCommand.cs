using System.Text;
using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class UnidadesJugadorCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("unidadesjugador")]
    [Summary("Lista las unidades del jugador. Sintaxis: unidadesjugador <nombreJugador>")]
    public async Task ExecuteAsync(string nombreJugador)
    {
        try
        {
            List<IUnidad> unidades = _fachada.ObtenerUnidadesJugador(nombreJugador);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < unidades.Count; i++)
            {
                sb.AppendLine($"{i}: {unidades[i].GetType().Name}");
            }

            await ReplyAsync(sb.ToString().TrimEnd());
        }
        catch (Exception ex)
        {
            await ReplyAsync($"Error al listar unidades: {ex.Message}");
        }
    }
}