using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace first_project.Models.Data
{
	public class DapperDBContext
	{
		private readonly IConfiguration _configuration;
		private readonly string connectionString;

		public DapperDBContext( IConfiguration configuration)
		{
			this._configuration = configuration;
			this.connectionString = this._configuration.GetConnectionString("DefaultConnection");
		}
		public IDbConnection CreateConnection() => new SqlConnection(connectionString);
	}
}

