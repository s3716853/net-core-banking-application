using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCBABackend.Managers.Interfaces;
using MCBABackend.Models;
using MCBABackend.Utilities;
using MCBABackend.Utilities.Extensions;
using Microsoft.Data.SqlClient;

namespace MCBABackend.Managers;
public static class DatabaseManager
{
    // Public setter to allow dependency injection
    // Private getter because only want services to use DatabaseManager not the rest of the Manager classes
    public static ICustomerManager _customerManager { private get; set; }
    public static ILoginManager _loginManager { private get; set; }
    public static IAccountManager _accountManager { private get; set; }
    public static ITransactionManager _transactionManager { private get; set; }

    public static void InitFromWebApiResponse(List<Customer>? customers)
    {
        customers.ForEach(customer =>
        {
            _customerManager.Insert(customer);
            _loginManager.Insert(customer.Login);
            customer.Accounts.ForEach(account =>
            {
                _accountManager.Insert(account);
                account.Transactions.ForEach(transaction =>
                {
                    _transactionManager.Insert(transaction);
                });
            });
        });
    }

    // Retrieves customers WITHOUT other tables, so no LOGIN or ACCOUNTS
    public static List<Customer> RetrieveCustomers()
    {
        return _customerManager.RetrieveCustomers();
    }

    public static Login? RetrieveLogin(string loginId)
    {
        return _loginManager.RetrieveLogin(loginId);
    }

    public static List<Account> RetrieveUserAccounts(string customerId)
    {
        return _accountManager.RetrieveUserAccounts(customerId);
    }

    public static void Deposit(Account account, decimal amount, string? comment)
    {
        // account.Balance += amount;
        // _accountManager.Update(account);
        // _transactionManager.Insert(new Transaction()
        // {
        //     TransactionType = (char) TransactionType.Deposit,
        //     AccountNumber = account.AccountNumber,
        //     Amount = amount,
        //     Comment = comment,
        //     TransactionTimeUtc = DateTime.Now.ToUniversalTime()
        // });
    }

    /// <summary>
    /// Throws ArgumentException if there is not enough money in supplied account to withdraw supplied amount
    /// </summary>
    /// 
    public static void Withdraw(Account account, decimal amount, string? comment)
    {
        // if (account.HasFreeTransactions() && account.Balance > amount + Constants.WithdrawTransactionFee || 
        //     !account.HasFreeTransactions() && account.Balance > amount)
        // {
        //     account.Balance -= amount;
        //     _transactionManager.Insert(new Transaction()
        //     {
        //         TransactionType = (char)TransactionType.Withdraw,
        //         AccountNumber = account.AccountNumber,
        //         Amount = amount,
        //         Comment = comment,
        //         TransactionTimeUtc = DateTime.Now.ToUniversalTime()
        //     });
        //     if (account.Transactions.Count >= 2)
        //     {
        //         account.Balance -= Constants.WithdrawTransactionFee;
        //         _transactionManager.Insert(new Transaction()
        //         {
        //             TransactionType = (char)TransactionType.Service,
        //             AccountNumber = account.AccountNumber,
        //             Amount = Constants.WithdrawTransactionFee,
        //             Comment = Constants.WithdrawFeeComment,
        //             TransactionTimeUtc = DateTime.Now.ToUniversalTime()
        //         });
        //     }
        //     _accountManager.Update(account);
        // }
        // else
        // {
        //     throw new ArgumentException("Cannot withdraw more than is available in account");
        // }
    }

    public static void Transfer(Account accountTo, Account accountFrom, decimal amount, string? comment)
    {
        // if (accountFrom.HasFreeTransactions() && accountFrom.Balance > amount ||
        //     !accountFrom.HasFreeTransactions() && accountFrom.Balance > amount+Constants.TransferTransactionFee)
        // {
        //     accountFrom.Balance -= amount;
        //     accountTo.Balance += amount;
        //     // Transaction for the AccountFrom account
        //     _transactionManager.Insert(new Transaction()
        //     {
        //         TransactionType = (char)TransactionType.Transfer,
        //         AccountNumber = accountFrom.AccountNumber,
        //         DestinationAccountNumber = accountTo.AccountNumber,
        //         Amount = amount,
        //         Comment = comment,
        //         TransactionTimeUtc = DateTime.Now.ToUniversalTime()
        //     });
        //     // Transaction for the AccountTo account
        //     _transactionManager.Insert(new Transaction()
        //     {
        //         TransactionType = (char)TransactionType.Transfer,
        //         AccountNumber = accountTo.AccountNumber,
        //         Amount = amount,
        //         Comment = comment,
        //         TransactionTimeUtc = DateTime.Now.ToUniversalTime()
        //     });
        //
        //     if (!accountFrom.HasFreeTransactions())
        //     {
        //         // Transaction for the transfer fee to AccountFrom
        //         accountFrom.Balance -= Constants.TransferTransactionFee;
        //         _transactionManager.Insert(new Transaction()
        //         {
        //             TransactionType = (char)TransactionType.Service,
        //             AccountNumber = accountFrom.AccountNumber,
        //             Amount = Constants.TransferTransactionFee,
        //             Comment = Constants.TransferFeeComment,
        //             TransactionTimeUtc = DateTime.Now.ToUniversalTime()
        //         });
        //     }
        //
        //     _accountManager.Update(accountFrom);
        //     _accountManager.Update(accountTo);
        // }
        // else
        // {
        //     throw new ArgumentException("Cannot transfer more than is available in account");
        // }
    }

    public static Account? RetrieveAccount(string accountNumber)
    {
        return _accountManager.RetrieveAccount(accountNumber);
    }
}
