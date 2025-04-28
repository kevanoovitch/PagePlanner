using System.IO;
using System.Text;
using PageCounter.Data;
using Spectre.Console;

namespace PageCounter.Handlers
{
    public class OutputHandler(CalculatedPagePlan sharedResult, UserInputParams sharedParams)
    {
        public CalculatedPagePlan result = sharedResult;

        public UserInputParams userParams = sharedParams;

        public void WriteToFile()
        {
            string fileName = "BookPlan.txt";
            File.WriteAllText(fileName, " "); //empty file

            foreach (var kvp in result.ResultPlan)
            {
                //Console.WriteLine($"{kvp.Key}: {kvp.Value} \n");
                //         File.AppendAllText("BookPlan.txt", "Something");

                string line = $"{kvp.Key:yyyy-MM-dd}: page {kvp.Value}\n";
                File.AppendAllText(fileName, line);
            }
            AnsiConsole.Markup($"[underline red]Wrote plan to {fileName}[/]\n");
        }

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
                sb.AppendLine($"{kvp.Key:yyyy-MM-dd}: page {kvp.Value} ");
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
