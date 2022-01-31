using MCBABackend.Contexts;
using MCBABackend.Models;
using MCBABackend.Models.Dto;
using MCBABackend.Utilities;
using MCBABackend.Utilities.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace MCBABackend.Services;
public static class DataInitialiseService
{
    public static void Start(IServiceProvider serviceProvider, string url)
    {
        using McbaContext mcbaContext = serviceProvider.GetRequiredService<McbaContext>();

        if (mcbaContext.Customer.Any()) return; // We have a database so no need to prefill!
        Console.WriteLine("Database empty, running prefill commands");

        List<Customer> customers = new List<Customer>();

        Retrieve(url)?.ForEach(customerDto =>
        {
            customers.Add(customerDto.ToCustomer());
        });

        mcbaContext.Payee.AddRange(PayeeSeedData());
        mcbaContext.Customer.AddRange(customers);
        mcbaContext.SaveChanges();
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

        return customers;
    }

    private static List<Payee> PayeeSeedData()
    {
        List<Payee> payees = new List<Payee>();

        payees.Add(new Payee()
        {
            Name = "Telstra",
            Address = "123 Telstra Street",
            Mobile = "0459 015 570",
            PostCode = "3223",
            State = States.Vic,
            Suburb = "Bundoora"
        });

        payees.Add(new Payee()
        {
            Name = "Optus",
            Address = "123 Optus Street",
            Mobile = "0459 015 570",
            PostCode = "3223",
            State = States.Vic,
            Suburb = "Watsonia"
        });

        payees.Add(new Payee()
        {
            Name = "MCBA",
            Address = "123 MCBA Street",
            Mobile = "0459 015 570",
            PostCode = "3223",
            State = States.Tas,
            Suburb = "???"
        });

        return payees;
    }
}
