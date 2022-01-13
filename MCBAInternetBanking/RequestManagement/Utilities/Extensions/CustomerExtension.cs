using System.Text;
using RequestManagement.Models;

namespace RequestManagement.Utilities.Extensions
{
    public static class CustomerExtension
    {
        // Cannot override ToString with extension methods
        // Chose to use an Extension method instead of implementing an overrided ToString
        // in the model classes as to keep them purely as data classes
        public static string AsString(this Customer customer)
        {
            string accountString = "";
            customer.Accounts.ForEach(account =>
            {
                accountString += account.AsString();
            });
            
            string[] customerString = new string[]
            {
                $"CustomerID={customer.CustomerID}",
                $"Name={customer.Name}",
                $"Address={customer.Address}",
                $"City={customer.City}",
                $"PostCode={customer.PostCode}",
                "-Login-",
                $"{customer.Login.AsString().TrimEnd()}",
                "-Accounts-",
                accountString
            };
            return new StringBuilder().AppendArray(customerString).ToString();
        }
    }
}
