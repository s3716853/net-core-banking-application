namespace MCBACustomerApi.Models;

public class ControllerInputs
{
    // Post / Put requests that require multiple parameters must wrap them in an outer class like this
    public class WithdrawInput
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
}

