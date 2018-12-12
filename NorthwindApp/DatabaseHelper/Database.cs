using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using ConfigurationHelper;

namespace DatabaseHelper
{
    public class Database

	{
		#region Private Members
		private SqlConnection _cn;
		private SqlCommand _cmd;
		private SqlDataAdapter _da;
		private DataTable _dt;

		private string _connectionName = string.Empty;
		#endregion

		#region Public Properties
		public SqlCommand Command
		{
			get
			{
				return _cmd;
			}
		}
		#endregion

		#region Private Methods 

		#endregion

		#region Public Methods 
		public SqlCommand ExecuteBQuery() //ExecuteNonQuery
		{
			//Set the connection string
			_cn.ConnectionString = 
				Configuration.GetConnectionString(_connectionName);
			//Tell the command object about the open connection
			_cmd.Connection = _cn;
			_cn.Open();


			_cmd.ExecuteNonQuery();
			_cn.Close();
			return _cmd;
		}
		public void Rollback()
		{
			_cmd.Transaction.Rollback();
			_cn.Close();
		}
		public void EndTransaction()
		{
			_cmd.Transaction.Commit();
			_cn.Close();
		}
		public SqlCommand ExecuteBQueryWithTransaction()
		{
			_cmd.ExecuteNonQuery();
			return _cmd;
		}
		public void BeginTransaction()
		{
			_cn.ConnectionString = 
				Configuration.GetConnectionString(_connectionName);
			_cmd.Connection = _cn;
			_cn.Open();
			_cmd.Transaction = _cn.BeginTransaction();

		}
		public DataTable ExecuteQuery()
		{
			_cn.ConnectionString =
				Configuration.GetConnectionString(_connectionName);
			_cmd.Connection = _cn;
			_cn.Open();
			_da = new SqlDataAdapter();
			_dt = new DataTable();
			_da.SelectCommand = _cmd;
			_da.Fill(_dt);
			return _dt;
		}
		#endregion

		#region Public Events

		#endregion

		#region Public Event Handlers 

		#endregion

		#region Construction
		public Database(string strConnectionName)
		{
			_connectionName = strConnectionName;
			_cn = new SqlConnection();
			_cmd = new SqlCommand();
		}
		#endregion
	}
}
