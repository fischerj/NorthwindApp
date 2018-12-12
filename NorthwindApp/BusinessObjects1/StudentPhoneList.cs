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
    public class StudentPhoneList
    {


        #region Private Members
        private BindingList<StudentPhone> _List;
        #endregion

        #region Public Properties
        public BindingList<StudentPhone> List
        {
            get { return _List; }
        }
        #endregion

        #region Private Methods 

        #endregion

        #region Public Methods 
        public StudentPhoneList GetByStudentID(Guid id)
        {
            Database database = new Database("Student");
            database.Command.Parameters.Clear();
            database.Command.Parameters.Add("@STUDENTID", SqlDbType.UniqueIdentifier).Value = id;
            database.Command.CommandType = System.Data.CommandType.StoredProcedure;
            database.Command.CommandText = "tblStudentPhone_GETBYSTUDENTID";
            DataTable dt = database.ExecuteQuery();
            foreach (DataRow dr in dt.Rows)
            {
                StudentPhone studentphone = new StudentPhone();
                studentphone.Initialize(dr);
                studentphone.InitializeBusinessData(dr);
                studentphone.isNew = false;
                studentphone.isDirty = false;
                _List.Add(studentphone);
            }
            return this;
        }
        public StudentPhoneList Save(Database database, Guid parentid)
        {
            foreach (StudentPhone studentphone in _List)
            {
                if (studentphone.IsSaveable() == true)
                {
                    studentphone.Save(database, parentid);
                }
            }
            return this;
        }
        public bool IsSaveable()
        {
            bool result = false;
            foreach (StudentPhone sp in _List)
            {
                if (sp.IsSaveable() == true)
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
        public StudentPhoneList()
        {
            _List = new BindingList<StudentPhone>();
        }
        #endregion
    }
}
