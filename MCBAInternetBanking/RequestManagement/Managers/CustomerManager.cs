using System;
using System.Collections.Generic;
using System.Data;
using MCBABackend.Managers.Interfaces;
using MCBABackend.Models;
using MCBABackend.Utilities.Extensions;
using Microsoft.Data.SqlClient;

namespace MCBABackend.Managers;
public class CustomerManager : ICustomerManager
{
    private readonly string _connectionString;

    public CustomerManager(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Insert(Customer customer)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "insert into Customer (CustomerID, Name, Address, City, PostCode) values (@customerID, @name, @address, @city, @postCode)";
        command.Parameters.AddWithValue("customerID", customer.CustomerID);
        command.Parameters.AddWithValue("name", customer.Name);
        command.Parameters.AddWithValue("address", customer.Address.GetObjectOrDbNull());
        command.Parameters.AddWithValue("city", customer.City.GetObjectOrDbNull());
        command.Parameters.AddWithValue("postCode", customer.PostCode.GetObjectOrDbNull());

        command.ExecuteNonQuery();
    }

    public List<Customer> RetrieveCustomers()
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        SqlCommand command = connection.CreateCommand();
        command.CommandText = "select * from Customer";

        List<Customer> Customers = command.GetDataTable().Select().Select(dataRow => new Customer()
        {
            CustomerID = dataRow.Field<int>(nameof(Customer.CustomerID)),
            #pragma warning disable CS8601 // CS8601 = Possible null reference assignment. Cannot be Null in DB
            Name = dataRow.Field<string>(nameof(Customer.Name)),
            Address = dataRow.Field<string>(nameof(Customer.Address)),
            City = dataRow.Field<string>(nameof(Customer.City)),
            PostCode = dataRow.Field<string>(nameof(Customer.PostCode)),
        }).ToList();

        return Customers;
    }
}
