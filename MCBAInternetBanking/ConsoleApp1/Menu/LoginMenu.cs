using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCBABackend.Contexts;
using MCBABackend.Services;

namespace MCBAConsole.Menu;

internal class LoginMenu : IConsoleMenu
{
    public LoginMenu(){}

    public void Run()
    {
        bool loggedIn = false;

        while (!loggedIn)
        {
            var (loginId, password) = DisplayMenu();

            UserContext userContext = UserContext.GetInstance();

            if (LoginService.Verify(loginId, password))
            {
                // A verified password will never have a null loginid 
                #pragma warning disable CS8601 // Possible null reference assignment.
                userContext.Username = loginId;
                userContext.Password = password;
                loggedIn = true;
            }
        }

    }

    private static (string loginId, string password) DisplayMenu()
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
        // Ensurinng there is a new line after login
        Console.WriteLine();
        return (loginId: (loginIdInput ?? ""), password: passwordBuilder.ToString());
    }
}

