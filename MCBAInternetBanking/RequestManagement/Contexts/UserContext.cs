namespace MCBABackend.Contexts;

public sealed class UserContext
{
    private static UserContext _instance;

    public string LoginId { get; set; } = "";
    public int CustomerId { get; set; } = 0;

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
