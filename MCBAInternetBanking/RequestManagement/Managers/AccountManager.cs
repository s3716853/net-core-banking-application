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
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "insert into Account (AccountNumber, AccountType, CustomerID, Balance) values (@accountNumber, @accountType, @customerID, @balance)";
        command.Parameters.AddWithValue("accountNumber", account.AccountNumber);
        command.Parameters.AddWithValue("accountType", account.AccountType);
        command.Parameters.AddWithValue("customerID", account.CustomerID);
        command.Parameters.AddWithValue("balance", account.Balance);

        command.ExecuteNonQuery();
    }
}
