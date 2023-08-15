
using System.Collections.Generic;
// using System.Data;
// using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Npgsql;
using System.Data.SqlClient;

namespace gatewayapi.Services
{
    public class pgQuery
    {

        public static IEnumerable<T> Qry<T>(string sql, string conStr)
        {

            using (SqlConnection con = new SqlConnection(conStr))
            {
                return con.Query<T>(sql).ToList();
            }
        }
    }
}