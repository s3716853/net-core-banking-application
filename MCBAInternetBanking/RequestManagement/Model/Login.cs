using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestManagement.Model
{
    public class Login
    {
        public int CustomerID { get; set; }
        public string LoginID { get; set; }
        public string PasswordHash { get; set; }
    }
}
