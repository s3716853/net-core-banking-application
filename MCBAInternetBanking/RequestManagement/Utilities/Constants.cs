namespace MCBABackend.Utilities;
public enum TransactionType
{
    Deposit = 'D',
    Transfer = 'T',
    Withdraw = 'W'
}

public class Constants
{
    public static decimal WithdrawTransactionFee = new decimal(0.05);
    public static decimal TransferTransactionFee = new decimal(0.05);
    public static string WithdrawFeeComment = "Standard withdraw fee";
}