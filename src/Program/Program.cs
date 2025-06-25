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
        //DemoFacade();
        Bot();
    }

   /* private static void DemoFacade()
    {
        Console.WriteLine(Facade.Instance.AddTrainerToWaitingList("player"));
        Console.WriteLine(Facade.Instance.AddTrainerToWaitingList("opponent"));
        Console.WriteLine(Facade.Instance.GetAllTrainersWaiting());
        Console.WriteLine(Facade.Instance.StartBattle("player", "opponent"));
        Console.WriteLine(Facade.Instance.GetAllTrainersWaiting());
    }*/

    private static void Bot()
    {
        BotLoader.LoadAsync().GetAwaiter().GetResult();
    }
}
       /* var motor = new Motor();
        Console.WriteLine("Bienvenido. Escriba 'Ayuda' para ver los comandos disponibles.");
        */
 
        /*while (true)
        {
            Console.Write("\nComando: ");
            var entrada = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(entrada))
                continue;

            var partes = entrada.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var comando = partes[0].ToLower();
            var argumentos = new List<string>(partes.Length > 1 ? partes[1..] : Array.Empty<string>());

            var resultado = motor.ProcesarComando(comando, argumentos);
            Console.WriteLine(resultado);

            if (comando == "salir" || comando == "exit" || resultado.Contains("ganó la partida"))
                break;
        }*/

    
