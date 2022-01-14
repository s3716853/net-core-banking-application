using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using MCBABackend.Models;
using MCBABackend.Services;
using MCBABackend.Utilities.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace MCBAConsole.Menu;

public class DepositWithdrawMenu : ConsoleMenu
{
    private readonly string _menuAmount;
    private readonly bool _depositMode;

    public DepositWithdrawMenu(bool depositMode)
    {
        _depositMode = depositMode;
        _menuAmount = CreateMenuString(depositMode ? "Deposit" : "Withdraw");
    }
    public override void Run()
    {
        bool running = true;
        while (running)
        {
            decimal depositAmount = AmountMenu();
            List<Account> customerAccounts = AccountService.RetrieveAccounts();
            Account accountTo = AccountSelectionMenu(customerAccounts);
            string? comment = GetUserInputLine("Enter a comment? (Press enter for no comment): ");
            if (_depositMode)
            {
                DepositWithdrawService.Deposit(accountTo, depositAmount, comment);
                running = false;
            }
            else
            {
                try
                {
                    DepositWithdrawService.Withdraw(accountTo, depositAmount, comment);
                    running = false;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

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

    private static string CreateMenuString(string title)
    {
        return new StringBuilder().AppendArray(new string[]
        {
            $"={title}=",
            "Amount: ",

        }).ToString().TrimEnd();
    } 
}