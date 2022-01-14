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