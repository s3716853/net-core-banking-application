using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MCBABackend.Models.Validators;
using MCBABackend.Utilities;

namespace MCBABackend.Models;
public class BillPay
{
    [Key]
    public int BillPayId { get; set; }

    [Display(Name = "Account")]
    [ForeignKey(nameof(Account))]
    public string AccountNumber { get; set; }

    public Account Account { get; set; }

    [Display(Name = "Payee")]
    [ForeignKey(nameof(MCBABackend.Models.Payee))]
    public int PayeeId { get; set; }

    public Payee Payee { get; set; }

    [Column(TypeName = "money")]
    [DataType(DataType.Currency)]
    [DecimalGreaterThanZero]
    [Display(Name = "Amount")]
    public decimal Amount { get; set; }

    [DataType(DataType.DateTime)]
    [Display(Name = "Transfer Time")]
    [DisplayFormat()]
    public DateTime ScheduleTimeUtc { get; set; }

    public Period Period { get; set; }

    public bool Completed { get; set; }
}
