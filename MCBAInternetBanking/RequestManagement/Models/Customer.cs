using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MCBABackend.Utilities;

namespace MCBABackend.Models;
public class Customer
{
    // Changed from initial db design of this being an int since its never actually used in any calculations
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    [StringLength(4, MinimumLength = 4)]
    [RegularExpression("^\\d+", ErrorMessage = "CustomerID only accepts digits")] //Only allow digit
    public string CustomerID { get; set; }

    [StringLength(50, MinimumLength = 1)] // Don't want to allow ""
    public string Name { get; set; } 

    [MaxLength(11)]
    public string? TFN { get; set; }

    [StringLength(50, MinimumLength = 1)]
    public string? Address { get; set; }

    [StringLength(40, MinimumLength = 1)]
    public string? Suburb { get; set; }

    public States? State { get; set; }

    [StringLength(4, MinimumLength = 4)]
    [RegularExpression("^\\d+", ErrorMessage = "Post Code only accepts digits")]
    [DataType(DataType.PostalCode)]
    public string? PostCode { get; set; }

    [DataType(DataType.PhoneNumber)]
    [StringLength(12, MinimumLength = 12)]
    [RegularExpression("^04\\d{2} \\d{3} \\d{3}$", ErrorMessage = "Must follow 04XX XXX XXX format")]
    public string? Mobile { get; set; }

    public virtual List<Account> Accounts { get; set; } = new List<Account>();
    
    public virtual Login Login { get; set; }
}
