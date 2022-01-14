using MCBABackend.Managers;
using MCBABackend.Models;
using MCBABackend.Utilities;
using Newtonsoft.Json;

namespace MCBABackend.Services;
public static class DataInitialiseService
{
    public static void RetrieveAndSave(string url)
    {
        List<Customer>? customers = Retrieve(url);
        DatabaseManager.InitFromWebApiResponse(customers);
    }
    private static List<Customer>? Retrieve(string url)
    {
        using var client = new HttpClient();
        string jsonResponse = client.GetStringAsync(url).Result;

        // Convert JSON into objects.
        List<Customer>? customers = JsonConvert.DeserializeObject<List<Customer>>(jsonResponse, new JsonSerializerSettings
        {
            // EXAMPLE 26/09/1999 01:02:03 PM
            DateFormatString = "dd/MM/yyyy hh:mm:ss tt"
        });

        customers?.ForEach((customer) =>
        {
            // Going to fields in objects related to Customer and filling fields which are not (explicitly)
            // returned by the web service
            customer.Login.CustomerID = customer.CustomerID;

            customer.Accounts.ForEach(account =>
            {
                account.CustomerID = customer.CustomerID;
                account.Transactions.ForEach(transaction =>
                {
                    // All initial Transactions are deposit
                    transaction.TransactionType = (char) TransactionType.Deposit;
                    account.Balance += transaction.Amount;

                    transaction.AccountNumber = account.AccountNumber;

                });
            });
        });
        return customers;
    }
}
