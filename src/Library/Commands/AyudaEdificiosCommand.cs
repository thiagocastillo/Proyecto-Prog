using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class AyudaEdificiosCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("ayudaedificios")]
    [Summary("Ver edificios disponibles y sus costos: ")]
    public async Task ExecuteAsync()
    {
        Ayuda.ObtenerComandos();
        await ReplyAsync(@"Edificios disponibles y sus costos:
        - Casa: 50 Madera
        - Cuartel: 100 Madera
        - Molino: 75 Madera
        - DepositoMadera: 60 Madera
        - DepositoOro: 60 Madera
        - DepositoPiedra: 60 Madera
        - CentroCivico: 200 Madera");
    }
}