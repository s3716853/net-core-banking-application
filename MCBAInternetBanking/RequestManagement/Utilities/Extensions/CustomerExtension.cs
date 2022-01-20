﻿using System.Text;
using MCBABackend.Models;

namespace MCBABackend.Utilities.Extensions;
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
            "-Login-",
            $"{customer.Login.AsString().TrimEnd()}",
            "-Accounts-",
            accountString
        };
        return new StringBuilder().AppendArray(customerString).ToString();
    }
}
