// See https://aka.ms/new-console-template for more information

using System.Text;

namespace MCBAConsole;
public class Program
{
    private static void Main()
    {
        var (loginId, password) = Login();
        string menu = CreateMenuString(loginId);

        MenuLoop(menu);
    }

    private static int MenuLoop(string menu)
    {
        bool menuRunning = true;
        int selectedOption = 0;
        while (menuRunning)
        {
            int? menuInput = MenuInput(menu);

            if (menuInput.HasValue && (menuInput > 0 && menuInput < 6))
            {
                menuRunning = false;
                selectedOption = menuInput.Value;
            }
            else
            {
                Console.WriteLine("Please Enter a Number From 1-5");
            }
        }

        return selectedOption;
    }

    private static int? MenuInput(string menu)
    {
        Console.Write(menu);
        ConsoleKeyInfo inputKey = Console.ReadKey();
        // Ensuring menu has new line after input
        Console.WriteLine("");
        try
        {
            return int.Parse(inputKey.KeyChar.ToString());
        } catch (FormatException)
        {
            return null;
        }
    }

    // Displays the login prompt and returns the LoginId and Password entered 
    private static (string? loginId, string password) Login()
    {
        Console.Write("Enter Login ID: ");
        string? loginIdInput = Console.ReadLine();

        Console.Write("Enter Password: ");
        bool passwordInputComplete = false;
        StringBuilder passwordBuilder = new StringBuilder();
        // Hiding the password and instead displaying * for each read character other than enter
        while (!passwordInputComplete)
        {
            ConsoleKeyInfo inputKey = Console.ReadKey(true);
            if (inputKey.Key != ConsoleKey.Enter)
            {
                passwordBuilder.Append(inputKey.KeyChar);
                Console.Write("*");
            }
            else { passwordInputComplete = true; }
        }
       
        return (loginId: loginIdInput, password: passwordBuilder.ToString());
    }

    private static string CreateMenuString(string? user)
    {
        StringBuilder menuOptionsStringBuilder = new StringBuilder();
        string[] menuOptions = new[]
            {
                "",
                "",
                $"--- {user} ---",
                "[1] Deposit",
                "[2] Withdraw",
                "[3] Transfer",
                "[4] Logout",
                "[5] Exit",
                "",
                "Enter an option: "
            };
        foreach (string MenuOption in menuOptions)
        {
            menuOptionsStringBuilder.AppendLine(MenuOption);
        }

        // Using TrimEnd() to remove the new line added in above foreach loop to allow input to be on same line as text
        return menuOptionsStringBuilder.ToString().TrimEnd();
    }
}