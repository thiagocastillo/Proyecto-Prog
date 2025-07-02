using Discord.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Domain;

namespace Library.Commands
{
    public class JuegoCommand : ModuleBase<SocketCommandContext>
    {
        private static readonly Motor motor = new Motor();
        private readonly JuegoFachada _fachada = new JuegoFachada();

        [Command("crearpartida")]
        [Summary("Procesa comandos del juego. Ejemplo: !juego crearpartida")]
        public async Task CrearPartidaAsync()
        {
            _fachada.CrearNuevaPartida();
            await Task.CompletedTask;
        }
    }
}