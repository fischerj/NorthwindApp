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
    public class StudentEmailList
    {


        #region Private Members
        private BindingList<StudentEmail> _List;
        #endregion

        #region Public Properties
        public BindingList<StudentEmail> List
        {
            get { return _List; }
        }
        #endregion

        #region Private Methods 

        #endregion

        #region Public Methods 
        public StudentEmailList GetByStudentID(Guid id)
        {
            StudentDB database = new StudentDB("Student");
            database.Command.Parameters.Clear();
            database.Command.Parameters.Add("@STUDENTID", SqlDbType.UniqueIdentifier).Value = id;
            database.Command.CommandType = System.Data.CommandType.StoredProcedure;
            database.Command.CommandText = "tblStudentEmail_GETBYSTUDENTID";
            DataTable dt = database.ExecuteQuery();
            foreach (DataRow dr in dt.Rows)
            {
                StudentEmail studentemail = new StudentEmail();
                studentemail.Initialize(dr);
                studentemail.InitializeBusinessData(dr);
                studentemail.isNew = false;
                studentemail.isDirty = false;
                _List.Add(studentemail);
            }
            return this;
        }
        public StudentEmailList Save(Database database, Guid parentid)
        {
            foreach (StudentEmail studentemail in _List)
            {
                if (studentemail.IsSaveable() == true)
                {
                    studentemail.Save(database, parentid);
                }
            }
            return this;
        }
        public bool IsSaveable()
        {
            bool result = false;
            foreach (StudentEmail se in _List)
            {
                if (se.IsSaveable() == true)
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
        public StudentEmailList()
        {
            _List = new BindingList<StudentEmail>();
        }
        #endregion
    }
}
