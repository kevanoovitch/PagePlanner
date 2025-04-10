namespace PageCounter.Classes
{
    public class Program
    {
        public static int Main()
        {
            // Take input

            Console.WriteLine(
                "Hello Welcome to the page counter please enter the total lenght of the book!"
            );

            InputHandler userInputHandle = new();

            string? userInput = Console.ReadLine();

            if (userInput != null)
            {
                userInputHandle.HandleInput(userInput);
            }
            else
            {
                Console.WriteLine("No input received!");
            }

            // output the result
            return 0;
        }
    }
}
