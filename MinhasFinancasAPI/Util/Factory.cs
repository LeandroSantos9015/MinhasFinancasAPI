using MinhasFinancasAPI.Util.Enumerator;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data.SqlClient;

namespace MinhasFinancasAPI.Util
{
    public static class FactoryConnectionDb
    {
        public static DbConnection FactoryConnection(this IConfiguration configuration, EConnection connection)
        {
            switch (connection)
            {
                case EConnection.MySql:
                    return new MySqlConnection(configuration.GetConnectionString("MySQL"));

                case EConnection.SqlServer:
                    return new SqlConnection(configuration.GetConnectionString("SQLServer"));

                default:
                    return null;
            }
        }
    }
}