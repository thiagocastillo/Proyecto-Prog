using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;
using System.Collections.Generic;

public class CivilizacionesCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = new JuegoFachada();

    [Command("civilizaciones")]
    [Summary("Lista las civilizaciones disponibles.")]
    public async Task ExecuteAsync()
    {
        List<string> civs = _fachada.ObtenerCivilizacionesDisponibles();
        await ReplyAsync("Civilizaciones disponibles:\n" + string.Join("\n", civs));
    }
}