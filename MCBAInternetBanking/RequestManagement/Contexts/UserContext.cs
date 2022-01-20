namespace MCBABackend.Contexts;

public sealed class UserContext
{
    private static UserContext _instance;

    public string LoginId { get; set; } = "";
    public string CustomerId { get; set; } = "";

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
