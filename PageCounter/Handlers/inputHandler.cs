using System.Globalization;
using System.Text.RegularExpressions;
using PageCounter.Data;
using PageCounter.UI;

namespace PageCounter.Handlers // eller valfritt namespace
{
    public class InputHandler
    {
        private UserInputParams _results = new();

        public UserInputParams GetUserParams()
        {
            return _results;
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

        private void SetDeadlineEndOfMonth()
        {
            // Use end of month
            this._results.DtEndDate = GetLastDateOfMonth();
            return;
        }

        private void AskDeadline()
        {
            Console.WriteLine("Please input a date for deadline now YYYY-MM-DD");
            string? userInputDate = Console.ReadLine();
            if (userInputDate == null)
            {
                Console.WriteLine("Input was Null and can't be!");
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

        private void AskBookLength()
        {
            Console.WriteLine("Input the number of pages");
            string? userInputNumber = Console.ReadLine();
            //check it's != null
            while (userInputNumber == null || !userInputNumber.All(char.IsDigit))
            {
                Console.Error.WriteLine("Input can not be null");
                Console.Error.WriteLine("Re enter");

                userInputNumber = Console.ReadLine();
            }
            // Convert to int
            int pagesNumber;
            int.TryParse(userInputNumber, out pagesNumber);

            // set the value
            _results.BookLength = pagesNumber;
            return;
        }

        public void HandleInput()
        {
            InteractiveMenu menu = new(_results);

            menu.InteractiveMeny();
            AskBookLength();

            if (_results.UseEndOfMonth)
            {
                AskDeadline();
            }
            else
            {
                SetDeadlineEndOfMonth();
            }

            DebugPrintParams();
        }

        public void DebugPrintParams()
        {
            Console.WriteLine("Current interpret values: \n");
            Console.WriteLine("Uses location: {0}", _results.IsLoc);
            Console.WriteLine("Deadline: {0}", _results.DtEndDate);
            Console.WriteLine("Lenght of book: {0}", _results.BookLength);
        }
    }
}
