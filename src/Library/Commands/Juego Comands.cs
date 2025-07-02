using Discord.Commands;
using System.Collections.Generic;
using System.Linq;
using Library.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

public class JuegoCommand : ModuleBase<SocketCommandContext>
{
    // Instancia del motor (puedes inyectarla si usas DI)
    private static readonly Motor motor = new Motor();

    [Command("juego")]
    [Summary("Procesa comandos del juego. Ejemplo: !juego crearpartida")]
    public async Task ExecuteAsync([Remainder] string input)
    {
        // Separar comando y argumentos
        var partes = input.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
        if (partes.Length == 0)
        {
            await ReplyAsync("Debes ingresar un comando.");
            return;
        }

        string comando = partes[0];
        List<string> argumentos = partes.Skip(1).ToList();

        // Procesar comando con el motor
        string resultado = motor.ProcesarComando(comando, argumentos);

        // Responder en Discord
        await ReplyAsync(resultado);
    }
}