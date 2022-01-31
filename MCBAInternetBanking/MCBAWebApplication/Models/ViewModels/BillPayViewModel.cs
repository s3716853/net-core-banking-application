using System.ComponentModel.DataAnnotations;
using MCBABackend.Models;
using MCBABackend.Models.Validators;
using MCBABackend.Utilities;

namespace MCBAWebApplication.Models.ViewModels;
public class BillPayViewModel
{
    [DataType(DataType.Currency)]
    [DecimalGreaterThanZero]
    public decimal Amount { get; set;}

    [Display(Name = "Account Number")]
    [StringLength(4, MinimumLength = 4)]
    [RegularExpression("^\\d+", ErrorMessage = "Account Number only accepts digits")]
    public string Account { get; set; }

    public int Payee { get; set; }

    public DateTime ScheduleTimeUtc { get; set; }

    public Period Period { get; set; }
}
