using System.ComponentModel.DataAnnotations;
using MCBABackend.Models;
using MCBABackend.Models.Validators;

namespace MCBAWebApplication.Models.ViewModels;
public class DepositViewModel
{
    [DataType(DataType.Currency)]
    [DecimalGreaterThanZero]
    public decimal Amount { get; set;}

    [Display(Name = "Account Number")]
    [StringLength(4, MinimumLength = 4)]
    [RegularExpression("^\\d+", ErrorMessage = "Account Number only accepts digits")]
    public string Account { get; set; }
}
