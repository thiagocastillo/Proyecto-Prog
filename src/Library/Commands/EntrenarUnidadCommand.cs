using Discord.Commands;
using System.Threading.Tasks;
using Library.Domain;
using System.Collections.Generic;

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

        try
        {
            int tiempoEntrenamiento = tipoUnidad.ToLower() switch
            {
                "aldeano" => 10,
                "guerrerojaguar" => 15,
                "arquerocompuesto" => 12,
                "ratha" => 10,
                "infanteria" => 10,
                "arquero" => 15,
                "caballeria" => 20,
                _ => throw new ArgumentException("Tipo de unidad no válido.")
            };

            _fachada.EntrenarUnidad(nombreJugador, tipoUnidad, new Point(x.Value, y.Value));

            await ReplyAsync($"Entrenamiento de {tipoUnidad} en proceso en ({x}, {y}) para el jugador '{nombreJugador}', tiempo de demora {tiempoEntrenamiento} segundos.");

            Dictionary<string, int> recursos = _fachada.ObtenerRecursosJugador(nombreJugador);
            int umbralAlerta = 50;
            List<string> alertas = new();

            foreach (KeyValuePair<string, int> recurso in recursos)
            {
                if (recurso.Value <= umbralAlerta)
                    alertas.Add($"¡Atención! Te quedan solo {recurso.Value} de {recurso.Key}.");
            }

            if (alertas.Count > 0)
                await ReplyAsync(string.Join("\n", alertas));

            // Lanza la tarea en segundo plano para avisar cuando termine el entrenamiento
            _ = Task.Run(async () =>
            {
                await Task.Delay(tiempoEntrenamiento * 1000);
                await ReplyAsync($"La unidad {tipoUnidad} en ({x}, {y}) ha sido entrenada para el jugador {nombreJugador}.");
            });
        }
        catch (System.Exception ex)
        {
            await ReplyAsync($"Error: {ex.Message}");
        }
    }
}