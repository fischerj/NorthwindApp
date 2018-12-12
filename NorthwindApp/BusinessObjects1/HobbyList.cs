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
	public class HobbyList
	{
		#region Private Members
		private BindingList<Hobby> _List;

		#endregion

		#region Public Properties
		public BindingList<Hobby> List
		{
			get { return _List; }
		}
		#endregion

		#region Private Methods 

		#endregion

		#region Public Methods 
		public HobbyList GetByStudentId(Guid studentid)
		{

			//Name the connection string same one in the Datasystems.config Like "PhoneType"
			Database database = new Database("Hobby");
			database.Command.Parameters.Clear();
			database.Command.CommandType = System.Data.CommandType.StoredProcedure;
			database.Command.CommandText = "Hobby_GetbyStudentId";
			database.Command.Parameters.Add("@StudentId", SqlDbType.UniqueIdentifier).Value = studentid;
			DataTable dt = database.ExecuteQuery();
			//Creating a student objects in every row from each row.
			foreach (DataRow dr in dt.Rows)
			{
				Hobby hobby = new Hobby();
				hobby.Initialize(dr);
				hobby.initiatlizeBusinessData(dr);
				hobby.isNew = false;
				hobby.isDirty = false;
				_List.Add(hobby);
			}
			return this;
		}
		public HobbyList Save()
		{
			foreach (Hobby hobby in _List)
			{
				if (hobby.IsSavable() == true)
				{
					hobby.Save();
				}

			}
			return this;
		}
		public bool IsSavable()
		{
			bool result = false;

			foreach (Hobby hobby in _List)
			{
				if (hobby.IsSavable() == true)
				{
					hobby.Save();
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
		public HobbyList()

		{
			_List = new BindingList<Hobby>();
		}

		#endregion
	}
}
