using System.Text;
using MCBABackend.Models;
using MCBABackend.Models.Dto;
using SimpleHashing;

namespace MCBABackend.Utilities.Extensions;
public static class LoginExtension
{
    // Cannot override ToString with extension methods
    // Chose to use an Extension method instead of implementing an overrided ToString
    // in the model classes as to keep them purely as data classes
    public static string AsString(this Login login)
    {
        return new StringBuilder().AppendArray(new string[]
        {
            $"  CustomerID={login.CustomerID}",
            $"  LoginID={login.LoginID}",
            $"  PasswordHash={login.PasswordHash}"
        }).ToString();
    }

    public static bool Verify(this Login login, string password)
    {
        return PBKDF2.Verify(login.PasswordHash, password);
    }

    public static Login ToLogin(this LoginDto dto, string customerId)
    {
        return new Login()
        {
            LoginID = dto.LoginID,
            PasswordHash = dto.PasswordHash,
            CustomerID = customerId
        };
    }
}
