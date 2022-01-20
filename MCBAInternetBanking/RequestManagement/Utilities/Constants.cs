namespace MCBABackend.Utilities;
public enum TransactionType
{
    Deposit = 'D',
    Transfer = 'T',
    Withdraw = 'W',
    Service = 'S',
    BillPay = 'B'
}

public enum AccountType
{
    Savings = 'S',
    Checking = 'C'
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

public class Constants
{
    public static readonly decimal WithdrawTransactionFee = new decimal(0.05);
    public static readonly decimal TransferTransactionFee = new decimal(0.10);
    public static readonly string WithdrawFeeComment = "Standard withdraw fee";
    public static readonly string TransferFeeComment = "Standard transfer fee";
    public static readonly decimal MinSavingsOpeningBalance = new decimal(50);
    public static readonly decimal MinCheckingOpeningBalance = new decimal(500);
    public static readonly decimal MinSavingsBalance = new decimal(0);
    public static readonly decimal MinCheckingBalance = new decimal(300);
    public static readonly int FreeTransactionAmount = 2;
}