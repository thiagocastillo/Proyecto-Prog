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
<<<<<<< HEAD

   
=======
    
   /* private static void DemoFacade()
    {
        Console.WriteLine(Facade.Instance.AddTrainerToWaitingList("player"));
        Console.WriteLine(Facade.Instance.AddTrainerToWaitingList("opponent"));
        Console.WriteLine(Facade.Instance.GetAllTrainersWaiting());
        Console.WriteLine(Facade.Instance.StartBattle("player", "opponent"));
        Console.WriteLine(Facade.Instance.GetAllTrainersWaiting());
    }*/
>>>>>>> f78aa9b869a28216a17766ea99e5357496e6b148

    private static void Bot()
    {
        BotLoader.LoadAsync().GetAwaiter().GetResult();
    }
}
     
    
