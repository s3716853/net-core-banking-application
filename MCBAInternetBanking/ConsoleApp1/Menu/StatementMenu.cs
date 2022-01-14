using System.Text;
using MCBABackend.Models;
using MCBABackend.Services;
using MCBABackend.Utilities.Extensions;

namespace MCBAConsole.Menu;

public class StatementMenu : ConsoleMenu
{
    private readonly string _paginationMenu= "[1] Previous \n[2] Next\n[3] Exit\n";
    private enum PaginationInputs
    {
        Previous = 1,
        Next = 2,
        Exit = 3
    }
    public override void Run()
    {
        List<Account> accounts = AccountService.RetrieveAccounts();
        Account account = AccountSelectionMenu(accounts);

        if (account.Transactions.Count == 0)
        {
            Console.WriteLine($"AccountNumber={account.AccountNumber} Balance={account.Balance}");
            Console.WriteLine("No Transactions to show");
        }
        else
        {
            List<Transaction> sortedTransactions = account.Transactions.OrderByDescending(transaction => transaction.TransactionTimeUtc).ToList();
            PaginationInputs input = default;
            int currentPage = 0;
            int maxPages = Convert.ToInt32(Math.Ceiling(sortedTransactions.Count / 4.0));
            Console.WriteLine($"PAGE {currentPage + 1} of {maxPages}");
            printTransaction(currentPage, sortedTransactions, account);
            while (input != PaginationInputs.Exit)
            {
                input = LoopUntilAllowedInput<PaginationInputs>(_paginationMenu, "Only 1, 2 and 3 are allowed");
                if (input == PaginationInputs.Next)
                {
                    if (currentPage == maxPages - 1)
                    {
                        Console.WriteLine("Max Page Reached");
                    }
                    else
                    {
                        currentPage++;
                        Console.WriteLine($"PAGE {currentPage + 1} of {maxPages}");
                        printTransaction(currentPage, sortedTransactions, account);
                    }
                }
                else if (input == PaginationInputs.Previous)
                {
                    if (currentPage == 0)
                    {
                        Console.WriteLine("On First Page");
                    }
                    else
                    {
                        currentPage--;
                        Console.WriteLine($"PAGE {currentPage + 1} of {maxPages}");
                        printTransaction(currentPage, sortedTransactions, account);
                    }
                }
            }
        }
        
    }

    private void printTransaction(int page, List<Transaction> transactions, Account account)
    {
        List<string> menu = new List<string>();
        menu.Add($"AccountNumber={account.AccountNumber} Balance={account.Balance}");

        int transaction1Index = page * 4;
        int transaction2Index = (page * 4) + 1;
        int transaction3Index = (page * 4) + 2;
        int transaction4Index = (page * 4) + 3;
        if (transaction1Index < transactions.Count)
        {
            menu.Add(transactions[transaction1Index].AsString());
        }
        if (transaction2Index < transactions.Count)
        {
            menu.Add(transactions[transaction2Index].AsString());
        }
        if (transaction3Index < transactions.Count)
        {
            menu.Add(transactions[transaction3Index].AsString());
        }
        if (transaction4Index < transactions.Count)
        {
            menu.Add(transactions[transaction4Index].AsString());
        }

        String output = new StringBuilder().AppendArray(menu.ToArray()).ToString();

        Console.WriteLine(output);

    }
}