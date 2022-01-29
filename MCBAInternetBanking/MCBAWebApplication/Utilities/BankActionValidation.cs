using MCBABackend.Models;
using MCBABackend.Utilities;
using MCBABackend.Utilities.Extensions;
using MCBAWebApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MCBAWebApplication.Utilities
{
    internal static class BankActionValidation
    {
        public static void Deposit(ModelStateDictionary modelState, Account account, decimal amount)
        {
            // TODO
        }

        public static void Withdraw(ModelStateDictionary modelState, Account account, decimal amount)
        {
            if (account.Balance() - amount < Constants.MinBalances[account.AccountType])
            {
                modelState.AddModelError("", $"{account.AccountType} accounts cannot go below ${Constants.MinBalances[account.AccountType]}");
            }
        }

        public static void Transfer(ModelStateDictionary modelState, Account accountOrigin, decimal amount, string customerId)
        {
            if (accountOrigin.CustomerID != customerId)
            {
                modelState.AddModelError("", "You can only transfer from an account you own");
            }

            if (accountOrigin.Balance() - amount < Constants.MinBalances[accountOrigin.AccountType])
            {
                modelState.AddModelError("", $"{accountOrigin.AccountType} accounts cannot go below ${Constants.MinBalances[accountOrigin.AccountType]}");
            }
        }

    }
}
