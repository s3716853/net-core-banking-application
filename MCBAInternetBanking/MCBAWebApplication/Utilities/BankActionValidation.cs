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
            // No implemntion needed here,  but made its own method if future validation needs to be done
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

        public static void BillPay(ModelStateDictionary modelState, Account account, decimal amount)
        {
            // BillPay should follow same rules as withdraw, but made its own method if furture validation needs to be done
            Withdraw(modelState, account, amount);
        }

    }
}
