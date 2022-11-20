using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.model
{
	public class Utility
	{
		public class SqlDbHelper
		{
			private string connString;
			/// <summary>
			/// 
			/// </summary>
			/// <param name="keyOfConnString">app.config裡的值,例如'default'</param>
			public SqlDbHelper(string keyOfConnString)
			{
				connString = System.Configuration.ConfigurationManager
										.ConnectionStrings[keyOfConnString]
										.ConnectionString;
			}

			public void ExecuteNonQuery(string sql, SqlParameter[] parameters)
			{
				using (var conn = new SqlConnection(connString))
				{
					SqlCommand command = new SqlCommand(sql, conn);
					conn.Open();

					command.Parameters.AddRange(parameters);
					command.ExecuteNonQuery();
				}
			}

			public DataTable Select(string sql, SqlParameter[] parameters)
			{
				using (var conn = new SqlConnection(connString))
				{
					var command = new SqlCommand(sql, conn);
					if (parameters != null)
					{
						command.Parameters.AddRange(parameters);
					}
					SqlDataAdapter adapter = new SqlDataAdapter(command);

					DataSet dataSet = new DataSet();
					adapter.Fill(dataSet, "dummy");

					return dataSet.Tables["dummy"];
				}
			}
			public class SqlParameterBuilder
			{
				private List<SqlParameter> parameters = new List<SqlParameter>();

				public SqlParameterBuilder AddNVarchar(string name,
														int length,
														string value)
				{
					var param = new SqlParameter(name,
												SqlDbType.NVarChar,
												length)
					{ Value = value };
					parameters.Add(param);

					return this;
				}

				public SqlParameterBuilder AddNInt(string name, int value)
				{
					var param = new SqlParameter(name, SqlDbType.Int) { Value = value };
					parameters.Add(param);

					return this;
				}

				public SqlParameterBuilder AddNDateTime(string name, DateTime value)
				{
					var param = new SqlParameter(name, SqlDbType.DateTime) { Value = value };
					parameters.Add(param);

					return this;
				}

				public SqlParameterBuilder AddNBool(string name, bool value)
				{
					var param = new SqlParameter(name, SqlDbType.Bit) { Value = value };
					parameters.Add(param);

					return this;
				}

				public SqlParameter[] Build()
				{
					return parameters.ToArray();
				}
			}
		}
	}
}
