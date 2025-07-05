using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;
using System.Collections.Generic;

public class ListarRecursosJugadorCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("listarrecursosjugador")]
    [Summary("Lista los recursos del jugador actual.")]
    public async Task ExecuteAsync()
    {
        string jugadorId = Context.User.Id.ToString();
        var recursos = _fachada.ObtenerRecursosJugador(jugadorId); 

        if (recursos == null || recursos.Count == 0)
        {
            await ReplyAsync("No tienes recursos.");
        }
        else
        {
            string lista = string.Join(", ", recursos);
            await ReplyAsync($"Tus recursos: {lista}");
        }
    }
}