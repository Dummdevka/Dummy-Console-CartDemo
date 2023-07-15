using System;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CartDemo.DAL
{
	public class DatabaseConnector
	{
		private readonly IConfiguration __config;

		public DatabaseConnector(IConfiguration config)
		{
			__config = config;
		}

		public SqlConnection getConnection() {
			SqlConnection connection = new SqlConnection(__config.GetConnectionString("Default"));

			return connection;
		}
	}
}

