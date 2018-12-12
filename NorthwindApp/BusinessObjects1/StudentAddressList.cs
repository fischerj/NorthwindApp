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
    public class StudentAddressList
    {


        #region Private Members
        private BindingList<StudentAddress> _List;
        #endregion

        #region Public Properties
        public BindingList<StudentAddress> List
        {
            get { return _List; }
        }
        #endregion

        #region Private Methods 

        #endregion

        #region Public Methods 
        public StudentAddressList GetByStudentID(Guid id)
        {
            Database database = new Database("Student");
            database.Command.Parameters.Clear();
            database.Command.Parameters.Add("@STUDENTID", SqlDbType.UniqueIdentifier).Value = id;
            database.Command.CommandType = System.Data.CommandType.StoredProcedure;
            database.Command.CommandText = "tblStudentAddress_GETBYSTUDENTID";
            DataTable dt = database.ExecuteQuery();
            foreach (DataRow dr in dt.Rows)
            {
                StudentAddress studentaddress = new StudentAddress();
                studentaddress.Initialize(dr);
                studentaddress.InitializeBusinessData(dr);
                studentaddress.isNew = false;
                studentaddress.isDirty = false;
                _List.Add(studentaddress);
            }
            return this;
        }
        public StudentAddressList Save(Database database, Guid parentid)
        {
            foreach (StudentAddress studentaddress in _List)
            {
                if (studentaddress.IsSaveable() == true)
                {
                    studentaddress.Save(database, parentid);
                }
            }
            return this;
        }
        public bool IsSaveable()
        {
            bool result = false;
            foreach (StudentAddress sa in _List)
            {
                if (sa.IsSaveable() == true)
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
        public StudentAddressList()
        {
            _List = new BindingList<StudentAddress>();
        }
        #endregion
    }
}
