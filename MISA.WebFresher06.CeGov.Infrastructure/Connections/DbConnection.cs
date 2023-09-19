using MySqlConnector;
using System.Data;
using System.Data.Common;

namespace MISA.WebFresher06.CeGov.Infrastructure
{
    public class DbConnection
    {
        private static readonly string ConnectionString = "server=localhost;uid=root;pwd=dodinhhuu0302@;database=misa.cegov_mf1716_ddvang;";
        private static IDbConnection Connection;
        public static IDbConnection GetConnection()
        {
            if (Connection == null || Connection.State != ConnectionState.Open)
            {
                Connection = new MySqlConnection(ConnectionString);
                Connection.Open();
            }

            return Connection;
        }

    }
}
