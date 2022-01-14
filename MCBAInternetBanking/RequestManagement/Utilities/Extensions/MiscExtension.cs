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
}

