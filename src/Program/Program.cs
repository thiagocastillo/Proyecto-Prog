using Discord.Commands;
using Library.Domain;
using Library.Services;

namespace Program;

internal static class Program
{
    /// <summary>
    /// Punto de entrada al programa.
    /// </summary>
    private static void Main()
    {
        Console.WriteLine("Bienvenido al juego de estrategia en tiempo real. Ejecute el comando '!ayuda' para ver la lista de comandos disponibles.");

        Bot();
    }
  
    private static void Bot()
    {
        BotLoader.LoadAsync().GetAwaiter().GetResult();
    }
}
     
    
