// See https://aka.ms/new-console-template for more information

using System.Text;

Console.Write("Enter Login ID: ");
string? loginIdInput = Console.ReadLine();
Console.Write("Enter Password: ");
string? passwordInput = Console.ReadLine();

StringBuilder menuOptionsStringBuilder = new StringBuilder();
string[] menuOptions = new[]
    {
        "",
        $"--- {loginIdInput} ---",
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
Console.Write(menuOptionsStringBuilder.ToString().TrimEnd());

ConsoleKeyInfo selectedMenuOption = Console.ReadKey();