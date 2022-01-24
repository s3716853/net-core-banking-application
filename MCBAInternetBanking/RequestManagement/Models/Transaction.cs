using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MCBABackend.Models.Validators;
using MCBABackend.Utilities;

namespace MCBABackend.Models;
public class Transaction
{
    [Key]
    public int TransactionID { get; set; }

    public TransactionType TransactionType { get; set; }

    [ForeignKey(nameof(Account))]
    public string OriginAccountNumber { get; set; }

    [JsonIgnore]
    public virtual Account? Origin { get; set; }

    [ForeignKey(nameof(Account))]
    public string? DestinationAccountNumber { get; set; }

    [JsonIgnore]
    public virtual Account? Destination { get; set; }

    [Column(TypeName = "money")]
    [DataType(DataType.Currency)]
    [DecimalGreaterThanZero]
    public decimal Amount { get; set; }
    
    [StringLength(30, MinimumLength = 1)]
    public string? Comment { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime TransactionTimeUtc { get; set; }
}
