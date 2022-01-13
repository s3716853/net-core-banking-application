using System;
using System.Collections.Generic;
using System.Data;
using MCBABackend.Models;
using MCBABackend.Utilities.Extensions;
using Microsoft.Data.SqlClient;

namespace MCBABackend.Managers
{
    public class TransactionManager
    {
        private readonly string _connectionString;

        public TransactionManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Insert(Transaction transaction)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "insert into [Transaction] (TransactionType, AccountNumber, DestinationAccountNumber, Amount, Comment, TransactionTimeUtc) values (@transactionType, @accountNumber, @destinationAccountNumber, @amount, @comment, @transactionTimeUtc)";
            command.Parameters.AddWithValue("transactionType", transaction.TransactionType);
            command.Parameters.AddWithValue("accountNumber", transaction.AccountNumber);
            command.Parameters.AddWithValue("destinationAccountNumber", transaction.DestinationAccountNumber.GetObjectOrDbNull());
            command.Parameters.AddWithValue("amount", transaction.Amount);
            command.Parameters.AddWithValue("comment", transaction.Comment.GetObjectOrDbNull());
            command.Parameters.AddWithValue("transactionTimeUtc", transaction.TransactionTimeUtc);

            command.ExecuteNonQuery();
        }
    }
}
