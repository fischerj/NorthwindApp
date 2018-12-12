using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseHelper;
using System.Data;

namespace BusinessObjects
{
		public class Hobby: HeaderData
		{

			#region Private Members
			private string _Name = string.Empty;

			#endregion

			#region Public Properties
			public string Name
			{
				get { return _Name; }
				set
				{
					if (_Name != value)
					{
						_Name = value;
						base.isDirty = true;
					}
				}
			}
			#endregion

			#region Private Methods 
			private bool Insert(Database database)
			{
				bool result = true;
				try
				{
					database.Command.Parameters.Clear();
					database.Command.CommandType = System.Data.CommandType.StoredProcedure;
					database.Command.CommandText = "hobby_insert";
					database.Command.Parameters.Add("@Name", SqlDbType.VarChar).Value = _Name;

					//PROVIDED the empty buckets for the output paramaters
					base.Initialize(database, Guid.Empty);
					//Do the work insert the data
					database.ExecuteBQuery();
					base.Initialize(database.Command);
				}
				catch (Exception e)
				{
					result = false;
					throw;
				}
				return result;
			}
			private bool Update(Database database)
			{
				bool result = true;
				try
				{
					database.Command.Parameters.Clear();
					database.Command.CommandType = System.Data.CommandType.StoredProcedure;
					database.Command.CommandText = "hobby_update";
					database.Command.Parameters.Add("@Name", SqlDbType.VarChar).Value = _Name;

					//PROVIDED the empty buckets for the output paramaters
					base.Initialize(database, base.Id);
					//Do the work insert the data
					database.ExecuteBQuery();
					base.Initialize(database.Command);
				}
				catch (Exception e)
				{
					result = false;
					throw;
				}
				return result;
			}

			private bool Delete(Database database)
			{
				bool result = true;
				try
				{
					database.Command.Parameters.Clear();
					database.Command.CommandType = System.Data.CommandType.StoredProcedure;
					database.Command.CommandText = "Hobby_Delete";


					//PROVIDED the empty buckets for the output paramaters
					base.Initialize(database, Guid.Empty);
					//Do the work insert the data
					database.ExecuteBQuery();
					base.Initialize(database.Command);
				}
				catch (Exception e)
				{
					result = false;
					throw;
				}
				return result;
			}

			private bool IsValid()
			{
				bool result = true;

				if (_Name != null && _Name.Trim() == string.Empty)
				{
					result = false;
				}

				return result;
			}


			#endregion

			#region Public Methods 
			public Hobby GetById(Guid id)
			{
				//Goes out to the database from the guid
				Database database = new Database("Hobby");
				DataTable dt = new DataTable();
				database.Command.CommandType = CommandType.StoredProcedure;
				database.Command.CommandText = "Hobby_GetbyId";
				database.Command.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = id;
				dt = database.ExecuteQuery();
				if (dt != null && dt.Rows.Count == 1)
				{
					DataRow dr = dt.Rows[0];
					base.Initialize(dr);
					initiatlizeBusinessData(dr);
					base.isNew = false;
					base.isDirty = false;
				}
				return this;
			}

			public void initiatlizeBusinessData(DataRow dr)
			{
				_Name = dr["Name"].ToString();
			}

			public bool IsSavable()
			{
				bool result = false;
				if (base.isDirty == true & IsValid() == true)
				{
					result = true;
				}

				return result;
			}

			public Hobby Save()
			{
				bool result = true;
				//
				Database database = new Database("Hobby");
				if (base.isNew == true && IsSavable() == true)
				{
					result = Insert(database);
				}
				else if (base.Deleted == true && base.isDirty == true)
				{
					result = Delete(database);
				}
				else if (base.isNew == false && IsValid() == true)
				{
					result = Update(database);
				}

				if (result == true)
				{
					base.isNew = false;
					base.isDirty = false;
				}
				return this;
			}
			#endregion

			#region Public Events

			#endregion

			#region Public Event Handlers 

			#endregion

			#region Construction

			#endregion
		}
	}
