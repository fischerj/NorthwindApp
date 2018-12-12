using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DatabaseHelper;
using System.Data;


namespace BusinessObjects
{
	public class StudentHobbyList
	{
		#region Private Members
		private BindingList<StudentHobby> _List;

		#endregion

		#region Public Properties
		public BindingList<StudentHobby> List
		{
			get { return _List; }
		}
		#endregion

		#region Private Methods 

		#endregion

		#region Public Methods 
		public StudentHobbyList GetbyStudentId(Guid studentId)
		{

			//Name the connection string same one in the Datasystems.config Like "PhoneType"
			Database database = new Database("Student");
			database.Command.Parameters.Clear();
			database.Command.CommandType = System.Data.CommandType.StoredProcedure;
			database.Command.CommandText = "tblStudentHobby_GetByStudentId";
			database.Command.Parameters.Add("@StudentId", SqlDbType.UniqueIdentifier).Value = studentId;

			DataTable dt = database.ExecuteQuery();
			//Creating a student objects in every row from each row.
			foreach (DataRow dr in dt.Rows)
			{
				StudentHobby hobby = new StudentHobby();
				hobby.Initialize(dr);
				hobby.initiatlizeBusinessData(dr);
				hobby.isNew = false;
				hobby.isDirty = false;
				_List.Add(hobby);
			}
			return this;
		}
		public StudentHobbyList Save(Database database, Guid parentId)
		{
			foreach (StudentHobby hobby in _List)
			{
				if (hobby.IsSavable() == true)
				{
					hobby.Save(database, parentId);
				}

			}
			return this;
		}
		public bool IsSavable()
		{
			bool result = false;

			foreach (StudentHobby sp in _List)
			{
				if (sp.IsSavable() == true)
				{
					result = true;
					break;
				}
			}

			return result;
		}

		#endregion

		#region Public Events

		#endregion

		#region Public Event Handlers 

		#endregion

		#region Construction
		public StudentHobbyList()

		{
			_List = new BindingList<StudentHobby>();
		}
		#endregion
	}

}