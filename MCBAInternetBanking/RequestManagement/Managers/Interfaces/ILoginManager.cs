using MCBABackend.Models;

namespace MCBABackend.Managers.Interfaces;

public interface ILoginManager
{
    public void Insert(Login login);
    public Login? RetrieveLogin(string loginId);
}