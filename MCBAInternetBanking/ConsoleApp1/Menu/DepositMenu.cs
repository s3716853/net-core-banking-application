using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using MCBABackend.Models;
using MCBABackend.Services;
using MCBABackend.Utilities.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace MCBAConsole.Menu;

public class DepositMenu : ConsoleMenu
{
    private readonly string _menuAmount;

    public DepositMenu()
    {
        _menuAmount = CreateMenuString();
    }
    public override void Run()
    {
        decimal depositAmount = AmountMenu();
        List<Account> customerAccounts = DepositService.RetrieveAccounts();
        Account accountTo = AccountMenu(depositAmount, customerAccounts);
        string? comment = GetUserInputLine("Enter a comment? (Press enter for no comment): ");
        DepositService.Deposit(accountTo, depositAmount, comment);
    }

    private decimal AmountMenu()
    {
        bool menuRunning = true;
        decimal amount = 0;
        while (menuRunning)
        {
            string? depositInput = GetUserInputLine(_menuAmount);
            if (depositInput == null)
            {
                Console.WriteLine("Please enter an amount");
            }
            else
            {
                if (decimal.TryParse(depositInput, out decimal deposit))
                {
                    string errorMessage = deposit.MeetsDepositRules();
                    if (errorMessage != null)
                    {
                        Console.WriteLine(errorMessage);
                    }
                    else
                    {
                        amount = deposit;
                        menuRunning = false;
                    }
                }
            }
        }

        return amount;
    }

    private static Account AccountMenu(decimal depositAmount, List<Account> customerAccounts)
    {
        char[] allowedMenuInputs = new char[customerAccounts.Count];

        string[] menuArray = new[]
        {
            $"Depositing {depositAmount}",
            "Which account will you deposit to?",
        };
        string[] accounts = customerAccounts.Select((account, index) =>
        {
            // adding an allowed menu input for each account
            allowedMenuInputs[index] = char.Parse($"{index + 1}");
            return $"[{index + 1}] {account.AccountNumber} (type={account.AccountType})";
        }).ToArray();

        string menu = new StringBuilder().AppendArray(menuArray.Concat(accounts).ToArray()).ToString();

        char input = LoopUntilAllowedInput(menu, $"Please only enter a number from 1 to {customerAccounts.Count}",
            allowedMenuInputs);

        return customerAccounts[int.Parse(input.ToString())-1];
    }

    private static string CreateMenuString()
    {
        return new StringBuilder().AppendArray(new string[]
        {
            "=Deposit=",
            "Deposit Amount: ",

        }).ToString().TrimEnd();
    } 
}