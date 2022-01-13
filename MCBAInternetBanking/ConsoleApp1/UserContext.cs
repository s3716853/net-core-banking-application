using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace MCBAConsole;

public sealed class UserContext
{
    private string _username = "";
    private string _password = "";
    private static UserContext _instance;

    public string Username { get { return _username; } set { _username = value; } }
    public string Password { get { return _password; } set { _password = value; } }

    private UserContext() { }

    public static UserContext GetInstance()
    { 
        if (_instance == null)
        {
            _instance = new UserContext();
        }
        return _instance;
    }

    public static bool Verify(string? username, string? password)
    {
        // TODO
        return true;
    }
}
