using System.Collections;
using System.Configuration;
using System.Data.SqlClient;

namespace CombinationOfConcerns
{
    public static class DatabaseWrapper
    {
        public static ArrayList GetManyResults(string sql)
        {
            var connString = (string)new AppSettingsReader().GetValue("ConnectionString", typeof(string));
            var results = new ArrayList();
            var connection = new SqlConnection(connString);
            var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var result = "";
                for (var index = 0; index < reader.FieldCount; index++)
                {
                    result += reader[index] + ",";
                }
                results.Add(result);
            }
            return results;
        }

        public static string GetSingleResult(string sql)
        {
            var connString = (string)new AppSettingsReader().GetValue("ConnectionString", typeof(string));
            var results = "";
            var connection = new SqlConnection(connString);
            var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                results += reader[0] + ",";
            }
            return results.Substring(0, results.Length - 1);
        }
    }
}
