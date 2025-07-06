using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class EntrenarUnidadCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("entrenarunidad")]
    [Summary("Entrena una unidad para un jugador. Sintaxis: entrenarunidad <nombreJugador> <tipoUnidad> <x> <y>")]
    public async Task ExecuteAsync(
        string nombreJugador = null,
        string tipoUnidad = null,
        int? x = null,
        int? y = null)
    {
        if (string.IsNullOrWhiteSpace(nombreJugador) ||
            string.IsNullOrWhiteSpace(tipoUnidad) ||
            x == null ||
            y == null)
        {
            await ReplyAsync("Faltan argumentos en comando, recordar sintaxis: entrenarunidad <nombreJugador> <tipoUnidad> <x> <y>");
            return;
        }
        Dictionary<string, int> recursos = _fachada.ObtenerRecursosJugador(nombreJugador);
        int umbralAlerta = 50;
        List<string> alertas = new();

        foreach (var recurso in recursos)
        {
            if (recurso.Value <= umbralAlerta)
                alertas.Add($"¡Atención! Te quedan solo {recurso.Value} de {recurso.Key}.");
        }

        if (alertas.Count > 0)
            await ReplyAsync(string.Join("\n", alertas));
        _fachada.EntrenarUnidad(nombreJugador, tipoUnidad, new Point(x.Value, y.Value));
        await ReplyAsync($"Unidad '{tipoUnidad}' entrenada en ({x}, {y}) para el jugador '{nombreJugador}'");
    }
}