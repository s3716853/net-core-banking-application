using System.Data;
using Microsoft.Data.SqlClient;

namespace MCBABackend.Utilities.Extensions
{
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
    }



}
