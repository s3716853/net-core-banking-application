using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCBABackend.Models;
public class Login
{
    [Key]
    [StringLength(8, MinimumLength = 8)]
    [RegularExpression("^\\d+")] //Only allow digit
    public string LoginID { get; set; }

    [ForeignKey(nameof(Customer))]
    public string CustomerID { get; set; }

    public string PasswordHash { get; set; }
}
