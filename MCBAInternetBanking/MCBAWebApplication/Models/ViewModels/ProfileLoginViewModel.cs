using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MCBAWebApplication.Models.ViewModels;
public class ProfileLoginViewModel
{
    [StringLength(4, MinimumLength = 4)]
    [RegularExpression("^\\d+", ErrorMessage = "CustomerID only accepts digits")] //Only allow digit
    public string CustomerID { get; set; }

    [DisplayName("New Password")]
    public string PasswordNew { get; set; }
    
    [DisplayName("Old Password")]
    public string PasswordOld { get; set; }

    public string PasswordOldHash { get; set; }
}
