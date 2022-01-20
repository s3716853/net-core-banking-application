using System;
using System.Collections.Generic;
using System.Data;
using MCBABackend.Managers.Interfaces;
using MCBABackend.Models;
using MCBABackend.Utilities.Extensions;
using Microsoft.Data.SqlClient;

namespace MCBABackend.Managers;
public class AccountManager : IAccountManager
{
    private readonly string _connectionString;

    public AccountManager(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Insert(Account account)
    {
        // using SqlConnection connection = new SqlConnection(_connectionString);
        // connection.Open();
        //
        // using var command = connection.CreateCommand();
        // command.CommandText = "insert into Account (AccountNumber, AccountType, CustomerID, Balance) values (@accountNumber, @accountType, @customerID, @balance)";
        // command.Parameters.AddWithValue("accountNumber", account.AccountNumber);
        // command.Parameters.AddWithValue("accountType", account.AccountType);
        // command.Parameters.AddWithValue("customerID", account.CustomerID);
        // command.Parameters.AddWithValue("balance", account.Balance);
        //
        // command.ExecuteNonQuery();
    }

    public List<Account> RetrieveUserAccounts(string customerId)
    {
        // using var connection = new SqlConnection(_connectionString);
        // using var command = connection.CreateCommand();
        // command.CommandText = @"SELECT * FROM Account 
        //     LEFT JOIN [Transaction] 
        //     ON Account.AccountNumber = [Transaction].[AccountNumber]
        //     WHERE Account.CustomerId = @customerId";
        // command.Parameters.AddWithValue("customerId", customerId);
        //
        // Dictionary<int, Account> accountToId = new Dictionary<int, Account>();
        //
        // foreach (DataRow dataRow in command.GetDataTable().Select())
        // {
        //     Account account;
        //     // Each row in the table could have the information for a previous account
        //     // As one account can have many transactions
        //     // and trhe SQL command returns the Account joined with each Transations as a row
        //     if (!accountToId.TryGetValue(dataRow.Field<int>(nameof(Account.AccountNumber)), out account))
        //     {
        //         account = new Account()
        //         {
        //             AccountNumber = dataRow.Field<int>(nameof(Account.AccountNumber)),
        //             AccountType = char.Parse(dataRow.Field<string?>(nameof(Account.AccountType))),
        //             Balance = dataRow.Field<decimal>(nameof(Account.Balance)),
        //             CustomerID = dataRow.Field<int>(nameof(Account.CustomerID)),
        //             Transactions = new List<Transaction>()
        //         };
        //         accountToId.Add(dataRow.Field<int>(nameof(Account.AccountNumber)), account);
        //     }
        //
        //     // Some Account can have no transactions due to SQL left join
        //     if (dataRow.Field<int?>(nameof(Transaction.TransactionID)) != null)
        //     {
        //         account.Transactions.Add(new Transaction()
        //             {
        //                 TransactionID = dataRow.Field<int>(nameof(Transaction.TransactionID)),
        //                 TransactionType = char.Parse(dataRow.Field<string?>(nameof(Transaction.TransactionType))),
        //                 AccountNumber = dataRow.Field<int>(nameof(Transaction.AccountNumber)),
        //                 DestinationAccountNumber = dataRow.Field<int?>(nameof(Transaction.DestinationAccountNumber)),
        //                 Amount = dataRow.Field<decimal>(nameof(Transaction.Amount)),
        //                 Comment = dataRow.Field<string?>(nameof(Transaction.Comment)),
        //                 TransactionTimeUtc = dataRow.Field<DateTime>(nameof(Transaction.TransactionTimeUtc)),
        //             });
        //     }
        // }
        //
        // return accountToId.Values.ToList();
        return new List<Account>();
    }

    public void Update(Account account)
    {
        // using SqlConnection connection = new SqlConnection(_connectionString);
        // connection.Open();
        //
        // using var command = connection.CreateCommand();
        // // I'm assuming that I won't ever need to update an Account to change the AccountNumber, CustomerId
        // command.CommandText = @"
        //     UPDATE Account
        //     SET AccountType = @accountType, Balance = @balance
        //     WHERE AccountNumber = @accountNumber";
        // command.Parameters.AddWithValue("accountType", account.AccountType);
        // command.Parameters.AddWithValue("balance", account.Balance);
        // command.Parameters.AddWithValue("accountNumber", account.AccountNumber);
        //
        // command.ExecuteNonQuery();
    }

    public Account? RetrieveAccount(string accountNumber)
    {
        return null;
        // using var connection = new SqlConnection(_connectionString);
        // using var command = connection.CreateCommand();
        // command.CommandText =
        //     @"SELECT * FROM Account 
        //     LEFT JOIN [Transaction] 
        //     ON Account.AccountNumber = [Transaction].[AccountNumber]
        //     WHERE Account.AccountNumber = @accountNumber;";
        // command.Parameters.AddWithValue("accountNumber", accountNumber);
        //
        // Account? account = null;
        //
        // foreach (DataRow dataRow in command.GetDataTable().Select())
        // {
        //     if (account == null)
        //     {
        //         account = new Account()
        //         {
        //             AccountNumber = dataRow.Field<int>(nameof(Account.AccountNumber)),
        //             AccountType = char.Parse(dataRow.Field<string?>(nameof(Account.AccountType))),
        //             Balance = dataRow.Field<decimal>(nameof(Account.Balance)),
        //             CustomerID = dataRow.Field<int>(nameof(Account.CustomerID)),
        //             Transactions = new List<Transaction>()
        //         };
        //     }
        //
        //     if (dataRow.Field<int?>(nameof(Transaction.TransactionID)) != null)
        //     {
        //         account.Transactions.Add(new Transaction()
        //         {
        //             TransactionType = char.Parse(dataRow.Field<string?>(nameof(Transaction.TransactionType))),
        //             AccountNumber = dataRow.Field<int>(nameof(Transaction.AccountNumber)),
        //             DestinationAccountNumber = dataRow.Field<int?>(nameof(Transaction.DestinationAccountNumber)),
        //             Amount = dataRow.Field<decimal>(nameof(Transaction.Amount)),
        //             Comment = dataRow.Field<string?>(nameof(Transaction.Comment)),
        //             TransactionTimeUtc = dataRow.Field<DateTime>(nameof(Transaction.TransactionTimeUtc)),
        //         });
        //     }
        // }
        //
        // return account;
    }
}
