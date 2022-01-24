using System.ComponentModel.DataAnnotations;

namespace MCBAWebApplication.Models.ViewModels;
public class LoginViewModel
{
    [Display(Name = "Login Id")]
    [StringLength(8, MinimumLength = 8)]
    [RegularExpression("^\\d+", ErrorMessage = "Only digits allowed")]
    public string LoginId { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }
}
