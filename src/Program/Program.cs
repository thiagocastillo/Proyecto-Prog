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
        Bot();
    }

    

    private static void Bot()
    {
        BotLoader.LoadAsync().GetAwaiter().GetResult();
    }
}
     
    
