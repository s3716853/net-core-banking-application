using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MCBABackend.Utilities;

namespace MCBABackend.Models;
public class Account
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Account Number")]
    [StringLength(4, MinimumLength = 4)]
    [RegularExpression("^\\d+", ErrorMessage = "Account Number only accepts digits")] //Only allow digit
    // Changed from initial db design of this being an int since its never actually used in any calculations
    public string AccountNumber { get; set; }
    
    [Display(Name = "Account Type")]
    public AccountType AccountType { get; set; }
    
    // [Column(TypeName = "money")]
    // [DataType(DataType.Currency)]
    // public decimal Balance { get; set; }
    
    [ForeignKey(nameof(Customer))]
    public int CustomerID { get; set; }
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
}
