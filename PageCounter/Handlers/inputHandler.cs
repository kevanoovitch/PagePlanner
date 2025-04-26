using System.Globalization;
using System.Text.RegularExpressions;
using PageCounter.Data;
using PageCounter.UI;
using Spectre.Console;

namespace PageCounter.Handlers // eller valfritt namespace
{
    public class InputHandler
    {
        public UserInputParams userInputs = new();

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

        private void setStartDate()
        {
            if (userInputs.UseTodaysDate == true)
            {
                userInputs.DtStartDate = DateTime.Now;
                return;
            }

            //Ask for date
            AskStartDate();
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
            this.userInputs.DtEndDate = GetLastDateOfMonth();
            return;
        }

        private void AskStartDate()
        {
            var userInputDate = AnsiConsole.Prompt(
                new TextPrompt<string>("Input startdate (YYYY-MM-DD)")
            );

            // get the current datestamp
            DateTime dtUserDate = DateTime.ParseExact(
                userInputDate,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture
            );
            // valid date
            userInputs.DtStartDate = dtUserDate;
            return;
        }

        private void AskDeadline()
        {
            var userInputDate = AnsiConsole.Prompt(
                new TextPrompt<string>("Input deadline date (YYYY-MM-DD)")
            );

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
                AskDeadline();
            }
            // valid date
            userInputs.DtEndDate = dtUserDate;
            return;
        }

        private void AskBookLength()
        {
            var userInputNumber = AnsiConsole.Prompt(
                new TextPrompt<string>("How long is book (Number of pages/locations)")
            );

            // Convert to int
            int pagesNumber;
            int.TryParse(userInputNumber, out pagesNumber);

            // set the value
            userInputs.BookLength = pagesNumber;
            return;
        }

        public void HandleInput()
        {
            InteractiveMenu menu = new(userInputs);

            menu.InteractiveMeny();
            AskBookLength();

            if (userInputs.UseEndOfMonth == false)
            {
                AskDeadline();
            }
            else
            {
                SetDeadlineEndOfMonth();
            }
            setStartDate();
        }
    }
}
