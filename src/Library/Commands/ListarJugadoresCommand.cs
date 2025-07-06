using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;
using System.Linq;

public class ListarJugadoresCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("listarjugadores")]
    [Summary("Lista los Jugadores existentes en la Partida Actual.")]
    public async Task ExecuteAsync()
    {
        try
        {

            List<Jugador> jugadores = _fachada.ObtenerJugadores();
            if (jugadores == null || jugadores.Count == 0)
            {
                await ReplyAsync("No hay jugadores en la partida.");
            }
            else
            {
                string lista = string.Join(", ", jugadores.Select(j => $"{j.Nombre} ({j.Civilizacion.Nombre})"));
                await ReplyAsync($"Jugadores: {lista}");
            }
        }
        catch (Exception ex)
        {
            await ReplyAsync($"Error al listar jugadores: {ex.Message}");
        }
    }
}