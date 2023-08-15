
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Options;
using UMISModuleAPI.Configuration;
using UMISModuleAPI.Entities;
using System.Data.SqlClient;

namespace UMISModuleAPI.Services
{
    public class dapperQuery
    {
        public static IEnumerable<T> Qry<T>(string sql, IOptions<conStr> conStr)
        {
            using (SqlConnection con = new SqlConnection(conStr.Value.dbCon))
            {
                return con.Query<T>(sql);
            }
        }
        
        public static IEnumerable<string> SPReturn<T>(string procedure, T model, IOptions<conStr> conStr)
        {
            using (IDbConnection con = new SqlConnection(conStr.Value.dbCon))
            {       
                string[] response = new string[2];
                // check connection state
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var row = con.Query<string>(procedure, model, commandType: CommandType.StoredProcedure);

               //close connection 
                if (con.State == ConnectionState.Open)
                    con.Close();

                return row;
            }
        }
    }
}