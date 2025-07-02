using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class AyudaUnidadesCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = new JuegoFachada();

    [Command("ayudaunidades")]
    [Summary("Muestra la ayuda del juego.")]
    public async Task ExecuteAsync()
    {
        Ayuda.ObtenerComandos();
        await ReplyAsync(@"Unidades disponibles y sus costos:
        - Aldeano: 50 Alimento
        - Infanteria: 60 Alimento
        - Arquero: 70 Madera
        - Caballeria: 80 Alimento, 60 Madera
        - GuerreroJaguar: 60 Alimento
        - ArqueroCompuesto: 70 Madera
        - Ratha: 80 Alimento, 60 Madera");
    }
}