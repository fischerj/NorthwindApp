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
    public class EmailTypeList
    {


        #region Private Members
        private BindingList<EmailType> _List;
        #endregion

        #region Public Properties
        public BindingList<EmailType> List
        {
            get { return _List; }
        }
        #endregion

        #region Private Methods 

        #endregion

        #region Public Methods 
        public EmailTypeList GetAll()
        {
            StudentDB database = new StudentDB("Student");
            database.Command.Parameters.Clear();
            database.Command.CommandType = System.Data.CommandType.StoredProcedure;
            database.Command.CommandText = "tblEmailType_GETALL";
            DataTable dt = database.ExecuteQuery();
            foreach (DataRow dr in dt.Rows)
            {
                EmailType emailtype = new EmailType();
                emailtype.Initialize(dr);
                emailtype.InitializeBusinessData(dr);
                emailtype.isNew = false;
                emailtype.isDirty = false;
                _List.Add(emailtype);
            }
            return this;
        }
        public EmailTypeList Save()
        {
            foreach (EmailType emailtype in _List)
            {
                if (emailtype.IsSaveable() == true)
                {
                    emailtype.Save();
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
        public EmailTypeList()
        {
            _List = new BindingList<EmailType>();
        }
        #endregion
    }
}
