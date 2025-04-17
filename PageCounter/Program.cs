using PageCounter.Data;
using PageCounter.Handlers;
using Spectre.Console;

namespace PageCounter
{
    public class Program
    {
        public static int Main()
        {
            // Figlet / Banner
            AnsiConsole.Write(new FigletText("Page").LeftJustified().Color(Color.Teal));
            AnsiConsole.Write(new FigletText("Planner").LeftJustified().Color(Color.White));
            AnsiConsole.Write(new FigletText("App").LeftJustified().Color(Color.Teal));

            InputHandler userInputHandle = new();

            userInputHandle.HandleInput();

            PageCalculator calculator = new(userInputHandle.userInputs);

            calculator.Calculate();

            OutputHandler outputer = new OutputHandler(
                calculator.calcResults,
                userInputHandle.userInputs
            );

            outputer.PrintResult();

            return 0;
        }
    }
}
