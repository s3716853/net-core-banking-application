using System.Data;
using Microsoft.Data.SqlClient;

namespace MCBABackend.Utilities.Extensions;
public static class MiscExtension
{
    // Taken from the lectures (Week 3), thanks for the method!
    // Contacts DB and retrieves tables based on command
    public static DataTable GetDataTable(this SqlCommand command)
    {
        using var adapter = new SqlDataAdapter(command);

        var table = new DataTable();
        adapter.Fill(table);

        return table;
    }

    // Taken from the lectures (Week 3), thanks for the method!
    // Returns either the original object, BDNull if null as the database cannot handle C# null
    public static object GetObjectOrDbNull(this object value) => value ?? DBNull.Value;

    // Checks the decimal meets the business rules for a deposit
    // RETURNS - String representing error message. Null if no issue;
    public static string? MeetsDepositRules(this decimal instance)
    {
        if (instance < 0)
        {
            return "Deposit must be greator than 0";
        }

        return null;
    }

    /// <summary>
    /// Returns the string converted to a TransactionType
    /// </summary>
    /// <param name="typeString">String to convert</param>
    /// <returns>Returns null if the string does not represent TransactionType, otherwise the matching TransactionType</returns>
    public static TransactionType? ToNullableTransactionType(this string typeString)
    {
        switch (typeString)
        {
            case "D":
                return TransactionType.Deposit;
            case "T":
                return TransactionType.Transfer;
            case "W":
                return TransactionType.Withdraw;
            case "S":
                return TransactionType.Service;
            case "B":
                return TransactionType.BillPay;
            default:
                return null;
        }
    }

    /// <summary>
    /// Like ToNullableTransactionType but instead of returning null the call defaults to returning TransactionType.Deposit
    /// and has no way of telling if the string actually converted to it.
    ///
    /// Used where you can guarentee the string is an TransactionType and you cant have nullable
    /// </summary>
    /// <param name="typeString">String to convert</param>
    /// <returns>Returns TransactionType.Deposit if the string does not represent TransactionType, otherwise the matching TransactionType</returns>
    public static TransactionType ToTransactionType(this string typeString)
    {
        return typeString.ToNullableTransactionType() ?? TransactionType.Deposit;
    }

    /// <summary>
    /// Returns the string converted to a AccountType
    /// </summary>
    /// <param name="typeString">String to convert</param>
    /// <returns>Returns null if the string does not represent AccountType, otherwise the matching AccountType</returns>
    public static AccountType? ToNullableAccountType(this string typeString)
    {
        switch (typeString)
        {
            case "S":
                return AccountType.Savings;
            case "C":
                return AccountType.Checking;
            default:
                return null;
        }
    }

    /// <summary>
    /// Like ToNullableAccountType but instead of returning null the call defaults to returning AccountType.Checking
    /// and has no way of telling if the string actually converted to it.
    ///
    /// Used where you can guarentee the string is an accountype and you cant have nullable
    /// </summary>
    /// <param name="typeString">String to convert</param>
    /// <returns>Returns AccountType.Checking if the string does not represent AccountType, otherwise the matching AccountType</returns>
    public static AccountType ToAccountType(this string typeString)
    {
        return typeString.ToNullableAccountType() ?? AccountType.Checking;
    }

}

