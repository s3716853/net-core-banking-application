namespace MCBACommon.Models.Dto;
public class AccountDto
{
    public string AccountNumber { get; set; }

    public string AccountType { get; set; }

    public string CustomerID { get; set; }
    
    public List<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
}
