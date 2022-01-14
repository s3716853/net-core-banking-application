using System.Text;
using MCBABackend.Models;
using MCBABackend.Utilities.Extensions;

namespace MCBAConsole.Menu;

public abstract class ConsoleMenu : IConsoleMenu
{
    public abstract void Run();

    // Loops until a value that can be parsed as the supplied enum is entered
    protected static TEnum LoopUntilAllowedInput<TEnum>(string menu, string error) where TEnum : struct
    {
        bool menuRunning = true;
        TEnum selectedOption = default;
        while (menuRunning)
        {
            string menuInput = MenuInput(menu);
            if (Enum.TryParse(menuInput, out selectedOption))
            {
                menuRunning = false;
            }
            else
            {
                Console.WriteLine(error);
            }
        }

        return selectedOption;
    }

    // Loops until a character is entered which is included in the allowedValues array
    protected static char LoopUntilAllowedInput(string menu, string error, char[] allowedValues)
    {
        bool menuRunning = true;
        char selectedOption = default;
        while (menuRunning)
        {
            char menuInput = MenuInputAsChar(menu);
            if (allowedValues.Contains(menuInput))
            {
                selectedOption = menuInput;
                menuRunning = false;
            }
            else
            {
                Console.WriteLine(error);
            }
        }

        return selectedOption;
    }

    protected static string? GetUserInputLine(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }

    protected static Account AccountSelectionMenu(List<Account> customerAccounts)
    {
        char[] allowedMenuInputs = new char[customerAccounts.Count];

        string[] menuArray = new[]
        {
            "Which account? ",
        };
        string[] accounts = customerAccounts.Select((account, index) =>
        {
            // adding an allowed menu input for each account
            allowedMenuInputs[index] = char.Parse($"{index + 1}");
            return $"[{index + 1}] {account.AccountNumber} (balance={account.Balance} type={account.AccountType})";
        }).ToArray();

        string menu = new StringBuilder().AppendArray(menuArray.Concat(accounts).ToArray()).ToString();

        char input = LoopUntilAllowedInput(menu, $"Please only enter a number from 1 to {customerAccounts.Count}",
            allowedMenuInputs);

        return customerAccounts[int.Parse(input.ToString()) - 1];
    }
    protected decimal DecimalInputMenu(string menuString)
    {
        bool menuRunning = true;
        decimal amount = 0;
        while (menuRunning)
        {
            string? input = GetUserInputLine(menuString);
            if (input == null)
            {
                Console.WriteLine("Please enter an amount");
            }
            else
            {
                if (decimal.TryParse(input, out decimal parsedInput))
                {
                    amount = parsedInput;
                    menuRunning = false;
                }
            }
        }

        return amount;
    }

    private static string MenuInput(string menu)
    {
        return MenuInputAsChar(menu).ToString();
    }

    private static char MenuInputAsChar(string menu)
    {
        Console.Write(menu);
        ConsoleKeyInfo inputKey = Console.ReadKey();
        // Ensuring menu has new line after input
        Console.WriteLine("");
        return inputKey.KeyChar;
    }
}