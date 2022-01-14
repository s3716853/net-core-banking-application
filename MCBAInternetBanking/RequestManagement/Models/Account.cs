namespace MCBABackend.Models;
public class Account
{
    public int AccountNumber { get; set; }
    public char AccountType { get; set; }
    public decimal Balance { get; set; }
    public int CustomerID { get; set; }
    public List<Transaction> Transactions { get; set; }
}
