using System.Text;
using MCBACommon.Models;
using MCBACommon.Models.Dto;

namespace MCBACommon.Utilities.Extensions;
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
            $"TFN={customer.TFN}",
            $"Suburb={customer.Suburb}",
            $"State={customer.State}",
            $"PostCode={customer.PostCode}",
            $"Mobile={customer.Mobile}",
            "-Accounts-",
            accountString,
            "-Login-",
            $"{customer.Login.AsString().TrimEnd()}",
        };
        return new StringBuilder().AppendArray(customerString).ToString();
    }

    public static Customer ToCustomer(this CustomerDto dto)
    {
        List<Account> accounts = new List<Account>();
        dto.Accounts.ForEach(accountDto =>
        {
            accounts.Add(accountDto.ToAccount());
        });

        return new Customer()
        {
            CustomerID = dto.CustomerID,
            Name = dto.Name,
            Address = dto.Address,
            Suburb = dto.City, // suburb renamed to city but still functionally same
            PostCode = dto.PostCode,
            Accounts = accounts,
            Login = dto.Login.ToLogin(dto.CustomerID)
        };
    }
}
