using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;
using System;
using System.Linq;

public class ListarPartidasCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("listarpartidas")]
    [Summary("Lista las partidas guardadas.")]
    public async Task ListarPartidas()
    {
        List<string> partidas = JuegoFachada.Instancia.ListarPartidas();

        if (partidas.Count == 0)
        {
            await ReplyAsync("No hay partidas guardadas disponibles.");
        }
        else
        {
            string mensaje = "Partidas disponibles:\n" + string.Join("\n", partidas.Select(p => $"📁 {p}"));
            await ReplyAsync(mensaje);
        }
    }

}