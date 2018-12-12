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
    public class PhoneTypeList
    {


        #region Private Members
        private BindingList<PhoneType> _List;
        #endregion

        #region Public Properties
        public BindingList<PhoneType> List
        {
            get { return _List; }
        }
        #endregion

        #region Private Methods 

        #endregion

        #region Public Methods 
        public PhoneTypeList GetAll()
        {
            StudentDB database = new StudentDB("Student");
            database.Command.Parameters.Clear();
            database.Command.CommandType = System.Data.CommandType.StoredProcedure;
            database.Command.CommandText = "tblPhoneType_GETALL";
            DataTable dt = database.ExecuteQuery();
            foreach (DataRow dr in dt.Rows)
            {
                PhoneType phonetype = new PhoneType();
                phonetype.Initialize(dr);
                phonetype.InitializeBusinessData(dr);
                phonetype.isNew = false;
                phonetype.isDirty = false;
                _List.Add(phonetype);
            }
            return this;
        }
        public PhoneTypeList Save()
        {
            foreach (PhoneType phonetype in _List)
            {
                if (phonetype.IsSaveable() == true)
                {
                    phonetype.Save();
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
        public PhoneTypeList()
        {
            _List = new BindingList<PhoneType>();
        }
        #endregion
    }
}
