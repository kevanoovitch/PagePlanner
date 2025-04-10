using PageCounter.Handlers;

namespace PageCounter
{
    public class Program
    {
        public static int Main()
        {
            // Take input

            Console.WriteLine("Hello Welcome to the page counter");

            InputHandler userInputHandle = new();

            userInputHandle.HandleInput();
            return 0;
        }
    }
}
