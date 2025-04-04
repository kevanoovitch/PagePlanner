using System;
using System.Text.RegularExpressions;

public class InputHandler {
 
    private UserInputParams _results; 

    private void ask_unit()
    {
      Console.WriteLine("What is the unit? If pages press y otherwise presumed location");
      string user_input = Console.ReadLine();

      if (user_input != "y")
      {
        // User choose pages
        Console.WriteLine("System: Using pages");
        this._results.is_loc = false; 
        return;
      }

      // user didn't choose pages
        Console.WriteLine("System: Using loc"); 
        this._results.is_loc = false;
    }
 
    private bool didEnterDate(string userInput)
    {
      string pattern = @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$";
      
      if (Regex.IsMatch(userInput, pattern))
      {
        return true;
      }
      else return false;
    }

    private DateTime get_last_day_of_month()
    {
      DateTime now = DateTime.Now;
      int year = now.Year;
      int month = now.Month;

      int lastDay = DateTime.DaysInMonth(year, month);
      DateTime lastDate = new DateTime(year, month, lastDay);
      
      return lastDate; 
    }

    private void ask_deadline()
    { 
      Console.WriteLine("Deadline will be the last day of current month by default. Otherwise input a date now YYYY-MM-DD"); 
      string user_input_date = Console.ReadLine();
      
      
      // was input empty -> default -> return
      if(!this.didEnterDate(user_input_date))
      {
        // Use end of month 
        this._results.end_Date = get_last_day_of_month(user_input_date); 
        return;
      }
      // get the current datestamp 
      DateTime dt_user_date = DateTime.ParseExact(user_input_date)
      DateTime dt_now = DateTime.Now;  
      // Compare with the entered one so its valid
      if(dt_user_date < dt_now)
      {
        Console.Error.WriteLine("Invalid deadline: it has already passed")
        return; 
      }

    }

   public void handle_input(string user_input)
   {
      ask_unit();
      
      ask_deadline();
   }
}
