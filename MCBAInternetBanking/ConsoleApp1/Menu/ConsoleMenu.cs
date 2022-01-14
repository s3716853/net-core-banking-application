namespace MCBAConsole.Menu;

public abstract class ConsoleMenu : IConsoleMenu
{
    public abstract void Run();

    private enum MenuOption
    {
        Deposit = 1,
        Withdraw = 2,
        Transfer = 3,
        Logout = 4,
        Exit = 5
    }
    public static TEnum LoopUntilAllowedInput<TEnum>(string menu, string error) where TEnum : struct
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

    private static string MenuInput(string menu)
    {
        Console.Write(menu);
        ConsoleKeyInfo inputKey = Console.ReadKey();
        // Ensuring menu has new line after input
        Console.WriteLine("");
        return inputKey.KeyChar.ToString();
    }
}