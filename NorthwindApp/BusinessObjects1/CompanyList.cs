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
	public class CompanyList
	{
		#region Private Members
		private BindingList<Company> _List;

		#endregion

		#region Public Properties
		public BindingList<Company> List
		{
			get { return _List; }
		}
		#endregion

		#region Private Methods 

		#endregion

		#region Public Methods 
		public CompanyList GetAll()
		{


			Database database = new Database("Company");
			database.Command.Parameters.Clear();
			database.Command.CommandType = System.Data.CommandType.StoredProcedure;
			database.Command.CommandText = "Company_GetAll";
			DataTable dt = database.ExecuteQuery();
			//Creating a student objects in every row from each row.
			foreach (DataRow dr in dt.Rows)
			{
				Company company = new Company();
				company.Initialize(dr);
				company.initiatlizeBusinessData(dr);
				company.isNew = false;
				company.isDirty = false;
				_List.Add(company);
			}
			return this;
		}
		public CompanyList Save()
		{
			foreach (Company company in _List)
			{
				if (company.IsSavable() == true)
				{
					company.Save();
				}

			}
			return this;
		}

		#endregion

		#region Public Events

		#endregion

		#region Public Event Handlers 

		#endregion

		#region Construction
		public CompanyList()

		{
			_List = new BindingList<Company>();
		}

		#endregion

	}
}
