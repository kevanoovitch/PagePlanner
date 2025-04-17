using PageCounter.Data;
using Spectre.Console;

namespace PageCounter.UI
{
    public struct Settings
    {
        public bool UsePages;
        public bool UseLocations;
        public bool CustomDeadline;
    }

    public class InteractiveMenu(UserInputParams sharedParams)
    {
        private Settings _mySettings;

        private UserInputParams _userParams = sharedParams;

        public void InteractiveMeny()
        {
            // Ask for the user's favorite fruits
            var selected = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title("Set parameters [green][/]?")
                    .PageSize(10)
                    .InstructionsText(
                        "[grey](Press [blue]<space>[/] to toggle a setting, "
                            + "[green]<enter>[/] to accept)[/]"
                    )
                    .AddChoices(
                        new[]
                        {
                            "Using Pages",
                            "Using Locations",
                            "Use a custom deadline (Not end of the month)",
                        }
                    )
            );

            _mySettings = new Settings
            {
                UsePages = selected.Contains("Using Pages"),
                UseLocations = selected.Contains("Using Locations"),
                CustomDeadline = selected.Contains("Use a custom deadline (Not end of the month)"),
            };

            if (!VerifySettings())
            {
                // if not valid promt again
                this.InteractiveMeny();
                return;
            }

            this.SetSettings();
        }

        private bool VerifySettings()
        {
            // if loc and pages are true bad stuff
            // if verification is ok return true

            if (_mySettings.UsePages && _mySettings.UseLocations)
            {
                return false;
            }
            return true;
        }

        private void SetSettings()
        {
            //based on the menu selection set on userConfig
            if (_mySettings.UseLocations)
            {
                _userParams.IsLoc = true;
            }
            else
            {
                _userParams.IsLoc = false;
            }
            // if end date is checked use end of month else ask
            if (_mySettings.CustomDeadline)
            {
                // the box is checked
                _userParams.UseEndOfMonth = false;
            }
            else
            {
                //the box was left unchecked
                _userParams.UseEndOfMonth = true;
            }
        }
    }
}
