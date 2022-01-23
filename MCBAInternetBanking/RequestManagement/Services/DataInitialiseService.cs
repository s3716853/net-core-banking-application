using MCBABackend.Contexts;
using MCBABackend.Models;
using MCBABackend.Models.Dto;
using MCBABackend.Utilities.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
}
