using Library.Services;

namespace Program
{
    internal static class Program
    {
        private static void Main()
        {
            BotLoader.LoadAsync().GetAwaiter().GetResult();
        }
    }
}