namespace ASM1.Utility;


// This class is used to display menu
public class Menu
{
    public List<string> MenuItems { get; set; }
    public string Title { get; set; }

    public Menu(string title, List<string> menuItems)
    {
        Title = title;
        MenuItems = menuItems;
    }

    public void Display()
    {
        // Print new line
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(Title);
        Console.ResetColor();
        for (int i = 0; i < MenuItems.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {MenuItems[i]}");
        }
    }

    public int GetChoice()
    {
        int choice;
        while (true)
        {
            Console.Write("Enter your choice: ");
            try
            {
                choice = int.Parse(s: Console.ReadLine() ?? "0");
                if (choice > 0 && choice <= MenuItems.Count)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
        return choice;
    }

}

public class Input
{
    public static string GetString(string message)
    {
        Console.Write(message);
        return Console.ReadLine() ?? "";
    }

    public static int GetInt(string message)
    {
        int result;
        while (true)
        {
            Console.Write(message);
            try
            {
                result = int.Parse(s: Console.ReadLine() ?? "0");
                break;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
        return result;
    }

    // Get input double
    public static double GetDouble(string message)
    {
        double result;
        while (true)
        {
            Console.Write(message);
            try
            {
                result = double.Parse(s: Console.ReadLine() ?? "0");
                break;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
        return result;
    }
    // Get input address
    public static string GetAddress(string message)
    {
        Console.WriteLine(message);
        string street = Input.GetString("Enter street: ");
        string ward = Input.GetString("Enter ward: ");
        string district = Input.GetString("Enter district: ");
        string city = Input.GetString("Enter city: ");
        return $"{street}, {ward}, {district}, {city}";
    }
    // AskContinue
    public static bool AskContinue(string message)
    {
        while (true)
        {
            Console.Write(message);
            string choice = Console.ReadLine() ?? "";
            if (choice.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (choice.Equals("n", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
    }
}