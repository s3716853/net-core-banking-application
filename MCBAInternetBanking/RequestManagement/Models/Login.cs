namespace RequestManagement.Models
{
    public class Login
    {
        public int CustomerID { get; set; }
        public string LoginID { get; set; }
        public string PasswordHash { get; set; }
    }
}
