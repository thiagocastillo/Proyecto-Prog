using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;
using Library.Domain;

namespace Library.Services
{
    public class Modulomotor : ModuleBase<SocketCommandContext>
    {
        private static readonly Motor motor = new Motor();

        [Command("")]
        public async Task Procesar([Remainder] string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                await ReplyAsync("Debes ingresar un comando. Escribe 'ayuda' para ver los comandos disponibles.");
                return;
            }

            var partes = input.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
            var comando = partes[0].ToLower();
            var argumentos = new List<string>(partes.Length > 1 ? partes[1..] : System.Array.Empty<string>());

            var resultado = motor.ProcesarComando(comando, argumentos);

            if (resultado.Length > 1900)
                resultado = resultado.Substring(0, 1900) + "...";

            await ReplyAsync(resultado);
        }
    }
}