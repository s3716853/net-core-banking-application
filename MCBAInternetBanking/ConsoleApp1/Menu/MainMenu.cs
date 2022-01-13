using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCBAConsole.Menu;

internal class MainMenu : IConsoleMenu
{
    private readonly string _menu;
    private enum MenuOption
    {
        Deposit = 1,
        Withdraw = 2,
        Transfer = 3, 
        Logout = 4,
        Exit = 5
    }

    private readonly Dictionary<MenuOption, IConsoleMenu> _menus = new();

    public MainMenu() 
    { 
        _menu = CreateMenuString();

        _menus.Add(MenuOption.Deposit, new LoginMenu());
        _menus.Add(MenuOption.Withdraw, new LoginMenu());
        _menus.Add(MenuOption.Transfer, new LoginMenu());
        _menus.Add(MenuOption.Logout, new LoginMenu());
    }

    public void Run()
    {
        // Run login on startup
        _menus[MenuOption.Logout].Run();

        bool programRunning = true;
        while (programRunning)
        {
            Console.WriteLine($"--- {UserContext.GetInstance().Username} ---");
            MenuOption selectedOption = LoopUntilAllowedInput();
            if (selectedOption == MenuOption.Exit) {
                programRunning = false;
            }
            else
            {
                if (selectedOption != MenuOption.Logout)
                {
                    Console.WriteLine($"--- {UserContext.GetInstance().Username} ---");
                }
                _menus[selectedOption].Run();
            }
        }

    }

    private static string CreateMenuString()
    {
        StringBuilder menuOptionsStringBuilder = new StringBuilder();
        string[] menuOptions = new[]
            {
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

    private MenuOption LoopUntilAllowedInput()
    {
        bool menuRunning = true;
        MenuOption selectedOption = 0;
        while (menuRunning)
        {
            string menuInput = MenuInput();
            if (Enum.TryParse<MenuOption>(menuInput, out selectedOption))
            {
                menuRunning = false;
            }
            else
            {
                Console.WriteLine("Please Enter a Number From 1-5");
            }
        }

        return selectedOption;
    }

    private string MenuInput()
    {
        Console.Write(_menu);
        ConsoleKeyInfo inputKey = Console.ReadKey();
        // Ensuring menu has new line after input
        Console.WriteLine("");
        return inputKey.KeyChar.ToString();
    }
}

