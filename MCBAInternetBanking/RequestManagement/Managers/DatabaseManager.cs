using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCBABackend.Models;
using MCBABackend.Utilities.Extensions;
using Microsoft.Data.SqlClient;

namespace MCBABackend.Managers
{
    public class DatabaseManager
    {
        private readonly string _connectionString;
        private readonly CustomerManager _customerManager;
        private readonly LoginManager _loginManager;
        private readonly AccountManager _accountManager;
        private readonly TransactionManager _transactionManager;

        public DatabaseManager(string connectionString)
        {
            _connectionString = connectionString;
            _customerManager = new CustomerManager(connectionString);
            _loginManager = new LoginManager(connectionString);
            _accountManager = new AccountManager(connectionString);
            _transactionManager = new TransactionManager(connectionString);
        }

        public void InitFromWebApiResponse(List<Customer>? customers)
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

        public List<Customer> RetrieveCustomers()
        {
            return _customerManager.RetrieveCustomers();
        }
    }
}
