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

    [Display(Name = "Transaction Type")]
    public TransactionType TransactionType { get; set; }

    [Display(Name = "Origin Account")]
    [ForeignKey(nameof(Account))]
    public string OriginAccountNumber { get; set; }

    [JsonIgnore]
    public virtual Account? Origin { get; set; }

    [Display(Name = "Destination Account")]
    [ForeignKey(nameof(Account))]
    public string? DestinationAccountNumber { get; set; }

    [JsonIgnore]
    public virtual Account? Destination { get; set; }

    [Column(TypeName = "money")]
    [DataType(DataType.Currency)]
    [DecimalGreaterThanZero]
    [Display(Name = "Amount")]
    public decimal Amount { get; set; }
    
    [StringLength(30, MinimumLength = 1)]
    [Display(Name = "Comment")]
    public string? Comment { get; set; }
    
    [DataType(DataType.DateTime)]
    [Display(Name = "Transaction Time")]
    [DisplayFormat()]
    public DateTime TransactionTimeUtc { get; set; }
}
