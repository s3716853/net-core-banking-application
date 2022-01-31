using System.ComponentModel.DataAnnotations;
using MCBACommon.Utilities;

namespace MCBAAdminWebApplication.Models.ViewModels;
public class TransactionDateViewModel
{
    public DateTime? startDate { get; set; }

    public DateTime? endDate { get; set; }
}
