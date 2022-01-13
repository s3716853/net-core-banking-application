using System.Text;
using RequestManagement.Models;

namespace RequestManagement.Utilities.Extensions
{
    public static class LoginExtension
    {
        // Cannot override ToString with extension methods
        // Chose to use an Extension method instead of implementing an overrided ToString
        // in the model classes as to keep them purely as data classes
        public static string AsString(this Login login)
        {
            return new StringBuilder().AppendArray(new string[]
            {
                $"CustomerID={login.CustomerID}",
                $"LoginID={login.LoginID}",
                $"PasswordHash={login.PasswordHash}"
            }).ToString();
        }
    }
}
