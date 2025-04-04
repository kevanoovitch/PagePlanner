public class calculator
{
    
    bool is_loc = false; 
    

    public void get_unit()
    {
      Console.WriteLine("What is the unit? If pages press y otherwise presumed location");
      string user_input = Console.ReadLine();



      if (user_input != "y")
      {
        // User choose pages
        Console.WriteLine("System: Using pages");
        this.is_loc = false; 
        return; 
      }

      // user didn't choose pages
        Console.WriteLine("System: Using loc"); 
        this.is_loc = false;
    }


    public bool calculate_input() 
    {

      return true; 
    }
}
   
