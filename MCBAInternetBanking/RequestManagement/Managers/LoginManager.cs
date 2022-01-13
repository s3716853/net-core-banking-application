using System;
using System.Collections.Generic;
using System.Data;
using MCBABackend.Models;
using MCBABackend.Utilities.Extensions;
using Microsoft.Data.SqlClient;

namespace MCBABackend.Managers
{
    public class LoginManager
    {
        private readonly string _connectionString;

        public LoginManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Insert(Login login)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "insert into Login (LoginID, CustomerID, PasswordHash) values (@loginID, @customerID, @passwordHash)";
            command.Parameters.AddWithValue("loginID", login.LoginID);
            command.Parameters.AddWithValue("customerID", login.CustomerID);
            command.Parameters.AddWithValue("passwordHash", login.PasswordHash);

            command.ExecuteNonQuery();
        }
    }
}

