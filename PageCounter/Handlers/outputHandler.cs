using System.Text;
using PageCounter.Data;
using Spectre.Console;

namespace PageCounter.Handlers
{
    public class OutputHandler(CalculatedPagePlan sharedResult, UserInputParams sharedParams)
    {
        public CalculatedPagePlan result = sharedResult;

        public UserInputParams userParams = sharedParams;

        private void PrintConfigSummary()
        {
            var color = "springgreen3";
            var convertedDeadline = userParams.DtEndDate.ToString("MMMM d, yyyy");

            AnsiConsole.Write(new Rule($"[{color}]Summary of configuration[/]").Centered());

            if (userParams.IsLoc)
            {
                AnsiConsole.MarkupLine($"[{color}]Book uses kindle Locations instead of pages[/]");
                AnsiConsole.MarkupLine($"[{color}]Book is {userParams.BookLength} locs long[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[{color}]Book uses pages[/]");
                AnsiConsole.MarkupLine($"[{color}]Book is {userParams.BookLength} pages long[/]");
            }
            AnsiConsole.MarkupLine($"[{color}]Set deadline is {convertedDeadline} [/]");
        }

        private void printPlan()
        {
            var sb = new StringBuilder();

            foreach (var kvp in result.ResultPlan)
            {
                //Console.WriteLine($"{kvp.Key}: {kvp.Value} \n");
                sb.AppendLine($"{kvp.Key:yyyy-MM-dd}: {kvp.Value} pages");
            }
            var panel = new Panel(sb.ToString())
                .Header("📚 Reading Plan")
                .Border(BoxBorder.Rounded)
                .BorderColor(Color.Teal);

            AnsiConsole.Write(panel);
        }

        public void PrintResult()
        {
            AnsiConsole.WriteLine();
            PrintConfigSummary();

            AnsiConsole.Write(new Rule($"[Teal]Plan[/]").Centered());

            printPlan();
        }
    }
}
