using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class ConstruirEdificioCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("construiredificio")]
    [Summary("Construye un edificio en la posicion indicada. Sintaxis: construiredificio <nombreJugador> <tipoEdificio> <x> <y>")]
   
    public async Task ConstruirEdificioAsync(string nombreJugador, string tipoEdificio, int x, int y)
    {
        try
        {
            _fachada.ConstruirEdificio(nombreJugador, tipoEdificio, new Point(x, y));
            Dictionary<string,int> recursos = _fachada.ObtenerRecursosJugador(nombreJugador);

            string mensaje = $"Edificio '{tipoEdificio}' contruido en ({x}, {y}) para el jugador '{nombreJugador}'";

            // Umbral de alerta
            int umbralAlerta = 50;
            List<string> alertas = new();

            foreach (KeyValuePair<string, int> recurso in recursos)
            {
                if (recurso.Value <= umbralAlerta)
                    alertas.Add($"¡Atención! Te quedan solo {recurso.Value} de {recurso.Key}.");
            }

            if (alertas.Count > 0)
                mensaje += "\n" + string.Join("\n", alertas);

            await ReplyAsync(mensaje);
        }
        catch (Exception ex)
        {
            await ReplyAsync($"Error: {ex.Message}");
        }
    }
}