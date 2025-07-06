using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;

public class ConstruirEdificioCommand : ModuleBase<SocketCommandContext>
{
    private readonly JuegoFachada _fachada = JuegoFachada.Instancia;

    [Command("construiredificio")]
    [Summary("Construye un edificio en la posicion indicada. Sintaxis: construiredificio <nombreJugador> <tipoEdificio> <x> <y>")]
    public async Task ConstruirEdificioAsync(
        string nombreJugador = null,
        string tipoEdificio = null,
        int? x = null,
        int? y = null)
    {
        if (string.IsNullOrWhiteSpace(nombreJugador) ||
            string.IsNullOrWhiteSpace(tipoEdificio) ||
            x == null ||
            y == null)
        {
            await ReplyAsync("Faltan argumentos en comando, recordar sintaxis: construiredificio <nombreJugador> <tipoEdificio> <x> <y>");
            return;
        }

        try
        {
            int tiempoConstruccion = tipoEdificio.ToLower() switch
            {
                "casa" => 15,
                "cuartel" => 30,
                "molino" => 20,
                _ => 10
            };

            _fachada.ConstruirEdificio(
                nombreJugador,
                tipoEdificio,
                new Library.Domain.Point(x.Value, y.Value)
            );

            await ReplyAsync($"Construcción de {tipoEdificio} en proceso en ({x}, {y}) para el jugador '{nombreJugador}', tiempo de demora {tiempoConstruccion} segundos.");
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
            // Lanza la tarea en segundo plano
            _ = Task.Run(async () =>
            {
                await Task.Delay(tiempoConstruccion * 1000);

                await ReplyAsync($"El edificio {tipoEdificio} en ({x}, {y}) ha sido construido para el jugador {nombreJugador}.");

               
            });
        }
        catch (Exception ex)
        {
            await ReplyAsync($"Error: {ex.Message}");
        }
    }
}