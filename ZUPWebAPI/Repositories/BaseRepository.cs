using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ZUPWebAPI.Repositories
{
    public class BaseRepository
    {
        protected T QueryFirstOrDefault<T>(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.QueryFirstOrDefault<T>(sql, parameters);
            }
        }

        protected List<T> Query<T>(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.Query<T>(sql, parameters).ToList();
            }
        }

        protected int Execute(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.Execute(sql, parameters);
            }
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection("Server=DBSRV\\DBSRV;Database=CLON;Trusted_Connection=True;Encrypt=no");
            //return new SqlConnection("Server=DESKTOP-QRK8HG8\\SQLEXPRESS;Database=testing;Trusted_Connection=True;Encrypt=no");
        }

    }
}
