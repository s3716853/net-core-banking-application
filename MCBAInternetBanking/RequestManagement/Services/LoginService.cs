using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCBABackend.Contexts;
using MCBABackend.Models;
using SimpleHashing;

namespace MCBABackend.Services;

public static class LoginService
{
    public static bool Login(string userId, string password)
    {
        return false;
        // Login? login = DatabaseManager.RetrieveLogin(userId);
        // if (login != null)
        // {
        //     bool verified = PBKDF2.Verify(login.PasswordHash, password);
        //     if (verified)
        //     {
        //         UserContext userContext = UserContext.GetInstance();
        //         userContext.CustomerId = login.CustomerID;
        //         userContext.LoginId = login.LoginID;
        //     }
        //     return verified;
        // }
        // else
        // {
        //     return false;
        // }
    }
}
