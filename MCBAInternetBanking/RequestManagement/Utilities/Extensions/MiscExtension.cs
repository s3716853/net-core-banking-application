using System.Data;
using Microsoft.Data.SqlClient;

namespace MCBABackend.Utilities.Extensions
{
    public static class MiscExtension
    {
        // Taken from the lectures (Week 3), thanks for the method!
        public static DataTable GetDataTable(this SqlCommand command)
        {
            using var adapter = new SqlDataAdapter(command);

            var table = new DataTable();
            adapter.Fill(table);

            return table;
        }
    }
}
