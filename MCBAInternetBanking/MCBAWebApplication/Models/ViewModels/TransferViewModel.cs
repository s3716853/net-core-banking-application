using System.ComponentModel.DataAnnotations;
using MCBACommon.Models.Validators;

namespace MCBAWebApplication.Models.ViewModels;
public class TransferViewModel
{
    [DataType(DataType.Currency)]
    [DecimalGreaterThanZero]
    public decimal Amount { get; set;}

    [Display(Name = "Origin Account Number")]
    [StringLength(4, MinimumLength = 4)]
    [RegularExpression("^\\d+", ErrorMessage = "Account Number only accepts digits")]
    public string OriginAccount { get; set; }

    [Display(Name = "Destination Account Number")]
    [StringLength(4, MinimumLength = 4)]
    [RegularExpression("^\\d+", ErrorMessage = "Account Number only accepts digits")]
    public string DestinationAccount { get; set; }

    [StringLength(30, MinimumLength = 1)]
    [Display(Name = "Comment")]
    public string? Comment { get; set; }
}
