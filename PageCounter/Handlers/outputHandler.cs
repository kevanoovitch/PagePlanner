using System.Text;
using PageCounter.Data;
using Spectre.Console;

namespace PageCounter.Handlers
{
    public class OutputHandler(CalculatedPagePlan sharedResult)
    {
        public CalculatedPagePlan result = sharedResult;

        public void PrintResult()
        {
            var sb = new StringBuilder();

            foreach (var kvp in result.ResultPlan)
            {
                //Console.WriteLine($"{kvp.Key}: {kvp.Value} \n");
                sb.AppendLine($"{kvp.Key:yyyy-MM-dd}: {kvp.Value} pages");
            }
            var panel = new Panel(sb.ToString())
                .Header("📚 Reading Plan")
                .Border(BoxBorder.Rounded);

            AnsiConsole.Write(panel);
        }
    }
}
