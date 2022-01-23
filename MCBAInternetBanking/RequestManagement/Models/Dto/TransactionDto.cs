using MCBABackend.Utilities;

namespace MCBABackend.Models.Dto;
public class TransactionDto
{
    public TransactionType TransactionType = TransactionType.Deposit; // All loaded in transactions are deposit
    public decimal Amount { get; set; }
    public string? Comment { get; set; }
    public DateTime TransactionTimeUtc { get; set; }
}
