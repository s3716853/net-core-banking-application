namespace MCBACommon.Utilities;
public enum TransactionType
{
    Deposit,
    Transfer,
    Withdraw,
    Service,
    BillPay
}

public enum AccountType
{
    Savings,
    Checking
}

public enum States
{
    Vic,
    Tas,
    Wa,
    Sa,
    Nt,
    Qld,
    Nsw
}

public enum Period
{
    OneOff,
    Monthly
}

public static class Constants
{
    public static readonly decimal WithdrawTransactionFee = new decimal(0.05);
    public static readonly decimal TransferTransactionFee = new decimal(0.10);
    public static readonly string WithdrawFeeComment = "Standard withdraw fee";
    public static readonly string TransferFeeComment = "Standard transfer fee";
    public static readonly string BillPayComment = "Scheduled BillPay";
    public static readonly decimal MinSavingsOpeningBalance = new decimal(50);
    public static readonly decimal MinCheckingOpeningBalance = new decimal(500);
    public static readonly decimal MinSavingsBalance = new decimal(0);
    public static readonly decimal MinCheckingBalance = new decimal(300);
    public static readonly Dictionary<AccountType, decimal> MinBalances = new Dictionary<AccountType, decimal>()
    {
        { AccountType.Checking, MinCheckingBalance }, 
        { AccountType.Savings, MinSavingsBalance }
    };

    public static readonly int FreeTransactionAmount = 2;
}