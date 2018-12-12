using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseHelper;
using System.Data;

namespace BusinessObjects
{
	public class StudentHobby : HeaderData
	{
		#region Private Members
		private Guid _StudentId = Guid.Empty;
		private Guid _HobbyTypeId = Guid.Empty;
		private String _Hobby = string.Empty;

		#endregion

		#region Public Properties
		public Guid StudentId
		{
			get { return _StudentId; }
			set
			{
				if (_StudentId != value)
				{
					_StudentId = value;
					base.isDirty = true;
				}
			}
		}
		public Guid HobbyTypeId
		{
			get { return _HobbyTypeId; }
			set
			{
				if (_HobbyTypeId != value)
				{
					_HobbyTypeId = value;
					base.isDirty = true;
				}
			}
		}
		public String Hobby
		{
			get { return _Hobby; }
			set
			{
				if (_Hobby != value)
				{
					_Hobby = value;
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
				database.Command.CommandText = "tblstudenthobby_insert";
				database.Command.Parameters.Add("@StudentId", SqlDbType.UniqueIdentifier).Value = _StudentId;
				database.Command.Parameters.Add("@HobbyTypeId", SqlDbType.UniqueIdentifier).Value = _HobbyTypeId;
				database.Command.Parameters.Add("@Hobby", SqlDbType.VarChar).Value = _Hobby;

				//PROVIDED the empty buckets for the output paramaters
				base.Initialize(database, Guid.Empty);
				//Do the work insert the data
				database.ExecuteBQueryWithTransaction();
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
				database.Command.CommandText = "tblstudenthobby_update";
				database.Command.Parameters.Add("@StudentId", SqlDbType.UniqueIdentifier).Value = _StudentId;
				database.Command.Parameters.Add("@HobbyTypeId", SqlDbType.UniqueIdentifier).Value = _HobbyTypeId;
				database.Command.Parameters.Add("@Hobby", SqlDbType.VarChar).Value = _Hobby;

				//PROVIDED the empty buckets for the output paramaters
				base.Initialize(database, base.Id);
				//Do the work insert the data
				database.ExecuteBQueryWithTransaction();
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
				database.Command.CommandText = "tblStudentHobby_Delete";


				//PROVIDED the empty buckets for the output paramaters
				base.Initialize(database, Guid.Empty);
				//Do the work insert the data
				database.ExecuteBQueryWithTransaction();
				base.Initialize(database.Command);
			}
			catch (Exception e)
			{
				result = false;
				throw;
			}
			return result;
		}



		#endregion

		#region Public Methods 
		public StudentHobby GetById(Guid id)
		{
			//Goes out to the database from the guid
			Database database = new Database("Student");
			DataTable dt = new DataTable();
			database.Command.CommandType = CommandType.StoredProcedure;
			database.Command.CommandText = "tblStudentHobby_GetbyId";
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
			_StudentId = (Guid)dr["StudentId"];
			_HobbyTypeId = (Guid)dr["HobbyTypeId"];
			_Hobby = dr["Hobby"].ToString();
		}

		public bool IsSavable()
		{
			bool result = false;
			if (base.isDirty == true && IsValid() == true)
			{
				result = true;
			}

			return result;
		}


		public StudentHobby Save(Database database, Guid parentId)
		{
			_StudentId = parentId;
			bool result = true;

			if (base.isNew == true && IsSavable() == true)
				result = Insert(database);

			else if (base.Deleted == true && base.isDirty == true)

				result = Delete(database);

			else if (base.isNew == false && IsValid() == true)
				result = Update(database);
			if (result == true)
			{
				base.isNew = false;
				base.isDirty = false;
			}
			return this;
		}

		private bool IsValid()
		{
			bool result = true;

			if (_Hobby != null && _Hobby.Trim() == string.Empty)
			{
				result = false;
			}
			return result;
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
