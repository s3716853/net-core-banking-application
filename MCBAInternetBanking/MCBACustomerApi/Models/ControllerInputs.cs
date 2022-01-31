using MCBABackend.Utilities;

namespace MCBACustomerApi.Models;

public class ControllerInputs
{
    // Post / Put requests that require multiple parameters must wrap them in an outer class like this
    public class DepositWithdrawInput
    {
        public string accountNumber { get; set; }
        public string comment { get; set; }
        public decimal amount { get; set; }
    }
    public class TransferInput
    {
        public string originAccountNumber { get; set; }
        public string destinationAccountNumber { get; set; }
        public string comment { get; set; }
        public decimal amount { get; set; }
    }

    public class LoginUpdateInput
    {
        public string customerID { get; set; }
        public string passwordNew { get; set; }
        public string passwordOld { get; set; }
    }

    public class CustomerUpdateInput
    {
        public string CustomerID { get; set; }
        public string Name { get; set; }
        public string? TFN { get; set; }
        public string? Address { get; set; }
        public string? Suburb { get; set; }
        public States? State { get; set; }
        public string? PostCode { get; set; }
        public string? Mobile { get; set; }
    }

    public class BillPayCreateInput
    {
        public decimal Amount { get; set; }
        public string Account { get; set; }
        public int Payee { get; set; }
        public DateTime ScheduleTimeUtc { get; set; }
        public Period Period { get; set; }
    }
}

