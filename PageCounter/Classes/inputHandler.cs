using System.Globalization;
using System.Text.RegularExpressions;

namespace PageCounter.Classes // eller valfritt namespace
{
    public class InputHandler
    {
        private UserInputParams _results = new();

        private void AskUnit()
        {
            Console.WriteLine("What is the unit? If pages press y otherwise presumed location");
            string? userInput = Console.ReadLine();

            if (userInput == null)
            {
                Console.WriteLine("No input received!");
                return;
            }
            if (userInput != "y")
            {
                // User choose pages
                Console.WriteLine("System: Using pages");
                this._results.IsLoc = false;
                return;
            }

            // user didn't choose pages
            Console.WriteLine("System: Using loc");
            this._results.IsLoc = false;
        }

        private static bool DidEnterDate(string userInput)
        {
            string pattern = @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$";

            if (Regex.IsMatch(userInput, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static DateTime GetLastDateOfMonth()
        {
            DateTime now = DateTime.Now;
            int year = now.Year;
            int month = now.Month;

            int lastDay = DateTime.DaysInMonth(year, month);
            DateTime lastDate = new(year, month, lastDay);

            return lastDate;
        }

        private void AskDeadline()
        {
            Console.WriteLine(
                "Deadline will be the last day of current month by default. Otherwise input a date now YYYY-MM-DD"
            );
            string? userInputDate = Console.ReadLine();
            if (userInputDate == null)
            {
                Console.WriteLine("Input was Null and can't be!");
                return;
            }

            // was input empty -> default -> return
            if (!DidEnterDate(userInputDate))
            {
                // Use end of month
                this._results.DtEndDate = GetLastDateOfMonth();
                return;
            }
            // get the current datestamp
            DateTime dtUserDate = DateTime.ParseExact(
                userInputDate,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture
            );
            DateTime dtNow = DateTime.Now;
            // Compare with the entered one so its valid
            if (dtUserDate < dtNow)
            {
                Console.Error.WriteLine("Invalid deadline: it has already passed");
                return;
            }
            // valid date
            _results.DtEndDate = dtUserDate;
            return;
        }

        private void AskBookLenght()
        {
            _results.BookLenght = 100;
            return;
        }

        public void HandleInput(string userInput)
        {
            AskUnit();

            AskDeadline();

            AskBookLenght();

            DebugPrintParams();
        }

        public void DebugPrintParams()
        {
            Console.WriteLine("Current interpret values: \n");
            Console.WriteLine("Uses location: {0}", _results.IsLoc);
            Console.WriteLine("Deadline: {0}", _results.DtEndDate);
            Console.WriteLine("Lenght of book: {0}", _results.BookLenght);
        }
    }
}
