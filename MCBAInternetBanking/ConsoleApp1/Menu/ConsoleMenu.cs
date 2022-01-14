namespace MCBAConsole.Menu;

public abstract class ConsoleMenu : IConsoleMenu
{
    public abstract void Run();

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

    protected static string? GetUserInputLine(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }

    private static string MenuInput(string menu)
    {
        Console.Write(menu);
        ConsoleKeyInfo inputKey = Console.ReadKey();
        // Ensuring menu has new line after input
        Console.WriteLine("");
        return inputKey.KeyChar.ToString();
    }
}