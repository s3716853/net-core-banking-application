using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestManagement.Model
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostCode { get; set; }
        public List<Account> Accounts { get; set; }
        public Login Login { get; set; }
    }
}