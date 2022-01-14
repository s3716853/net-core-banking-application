using System.Text;
using MCBABackend.Models;
using MCBABackend.Services;
using MCBABackend.Utilities.Extensions;

namespace MCBAConsole.Menu;

public class TransferMenu : ConsoleMenu
{
    public override void Run()
    {
        bool loop = true;
        while (loop)
        {
            List<Account> accounts = AccountService.RetrieveAccounts();
            Account accountFrom = AccountSelectionMenu(accounts);
            Account? accountTo = null;
            decimal amount = DecimalInputMenu("How much will you be transferring?");
            bool accountExists = false;
            while (!accountExists)
            {
                string? accountToName = GetUserInputLine("Enter the Account Number for the account you wish to transfer to (Account numbers only have digits): ");

                if (int.TryParse(accountToName, out int accountNumber))
                {
                    // Can't transfer between the same account
                    if (accountNumber != accountFrom.AccountNumber)
                    {
                        accountTo = AccountService.RetrieveAccount(accountNumber);
                    }
                }
                else
                {
                    Console.WriteLine("Account numbers only have digits");
                }

                if (accountTo != null)
                {
                    accountExists = true;
                }
            }

            string? comment = GetUserInputLine("Enter a comment (Enter to leave empty)");

            // AccountTo will not be null by this point as accountExists is made true through its not being so
            string? message = TransferServices.Transfer(accountTo, accountFrom, amount, comment);
            // An error message is sent if there is a failure, otherwise null comes through for success.
            if (message == null)
            {
                loop = false;
            }
            else
            {
                Console.WriteLine(message);
            }
        }
    }

}