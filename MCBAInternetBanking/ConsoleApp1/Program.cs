// See https://aka.ms/new-console-template for more information

using MCBABackend.Managers;
using MCBABackend.Services;
using MCBAConsole.Menu;

namespace MCBAConsole;
public class Program
{
    private static void Main()
    {
        PrepareManagers();
        if (DatabaseManager.RetrieveCustomers().Count == 0)
        {
            DataInitialiseService.RetrieveAndSave("https://coreteaching01.csit.rmit.edu.au/~e103884/wdt/services/customers/");
        }

        new MainMenu().Run();
        Console.WriteLine("Program Ending");
    }

    private static void PrepareManagers()
    {
        string connectionString =
            "server=rmit.australiaeast.cloudapp.azure.com;Encrypt=False;Uid=s3716853_a1;Password=abc123";
        DatabaseManager._transactionManager = new TransactionManager(connectionString);
        DatabaseManager._accountManager = new AccountManager(connectionString);
        DatabaseManager._customerManager = new CustomerManager(connectionString);
        DatabaseManager._loginManager = new LoginManager(connectionString);
    }
}