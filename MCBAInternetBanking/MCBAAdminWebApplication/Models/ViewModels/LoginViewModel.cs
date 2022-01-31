using System.ComponentModel.DataAnnotations;

namespace MCBAAdminWebApplication.Models.ViewModels;
public class LoginViewModel
{
    public string LoginId { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }
}
