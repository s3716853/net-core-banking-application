namespace MCBABackend.Contexts;

public sealed class UserContext
{
    private static UserContext _instance;

    public string Username { get; set; } = "";
    public string Password { get; set; } = "";

    private UserContext() { }

    public static UserContext GetInstance()
    { 
        if (_instance == null)
        {
            _instance = new UserContext();
        }
        return _instance;
    }
}
