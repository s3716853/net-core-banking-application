using MCBABackend.Contexts;
using MCBABackend.Models;
using MCBABackend.Models.Dto;
using MCBABackend.Utilities.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace MCBABackend.Services;
public static class DataInitialiseService
{
    public static void RetrieveAndSave(IServiceProvider serviceProvider, string url)
    {
        List<Customer> customers = new List<Customer>();

        Retrieve(url)?.ForEach(customerDto =>
        {
            customers.Add(customerDto.ToCustomer());
        });

        customers.ForEach(customer =>
        {
            Console.WriteLine(customer.AsString());
        });

        using var dbContext = serviceProvider.GetRequiredService<McbaContext>();

        if (dbContext.Customer.Any()) return;

        Console.WriteLine("nothing in db");
    }
    private static List<CustomerDto>? Retrieve(string url)
    {
        using var client = new HttpClient();
        string jsonResponse = client.GetStringAsync(url).Result;

        // Convert JSON into objects.
        List<CustomerDto>? customers = JsonConvert.DeserializeObject<List<CustomerDto>>(jsonResponse, new JsonSerializerSettings
        {
            // EXAMPLE 26/09/1999 01:02:03 PM
            DateFormatString = "dd/MM/yyyy hh:mm:ss tt"
        });

        // customers?.ForEach((customer) =>
        // {
        //     // Going to fields in objects related to Customer and filling fields which are not (explicitly)
        //     // returned by the web service
        //     customer.Login.CustomerID = customer.CustomerID;
        //
        //     customer.Accounts.ForEach(account =>
        //     {
        //         account.CustomerID = customer.CustomerID;
        //         account.Transactions.ForEach(transaction =>
        //         {
        //             // All initial Transactions are deposit
        //             transaction.TransactionType = TransactionType.Deposit;
        //             // account.Balance += transaction.Amount;
        //
        //             transaction.OriginAccountNumber = account.AccountNumber;
        //
        //         });
        //     });
        // });
        return customers;
    }
}
