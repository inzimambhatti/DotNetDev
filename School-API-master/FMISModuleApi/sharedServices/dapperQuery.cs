using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Options;
using System.IO;
using FMISModuleApi.Configuration;
using System.Data.SqlClient;

namespace FMISModuleApi.Services
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

        public static IEnumerable<T> QryResult<T>(string sql, IOptions<conStr> conStr)
        {
            using (SqlConnection con = new SqlConnection(conStr.Value.dbCon))
            {
                return con.Query<T>(sql).ToList();
            }
        }
        public static IEnumerable<int> CRUDQry(string query, DynamicParameters parameters, IOptions<conStr> conStr)
        {
            using (SqlConnection con = new SqlConnection(conStr.Value.dbCon))
            {
                var rowAffected = con.Execute(query, parameters, commandType: CommandType.Text);

                yield return rowAffected;
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

        public static string saveImageFile(string regPath, string name, string binData, string ext)
        {
            string path = regPath; //Path
            //Check if directory exist
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            string imageName = name + "." + ext;

            //set the image path
            string imgPath = Path.Combine(path, imageName);

            byte[] imageBytes = Convert.FromBase64String(binData);

            System.IO.File.WriteAllBytes(imgPath, imageBytes);

            return "Ok";
        }
        
    }
}