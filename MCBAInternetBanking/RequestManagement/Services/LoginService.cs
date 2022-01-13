using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCBABackend.Managers;
using MCBABackend.Models;
using SimpleHashing;

namespace MCBABackend.Services;

public static class LoginService
{
    public static bool Verify(string userId, string password)
    {
        Login? login = DatabaseManager.RetrieveLogin(userId);
        if (login != null)
        {
            return PBKDF2.Verify(login.PasswordHash, password);
        }
        else
        {
            return false;
        }
    }
}
