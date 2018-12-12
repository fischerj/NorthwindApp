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
    public class AddressTypeList
    {


        #region Private Members
        private BindingList<AddressType> _List;
        #endregion

        #region Public Properties
        public BindingList<AddressType> List
        {
            get { return _List; }
        }
        #endregion

        #region Private Methods 

        #endregion

        #region Public Methods 
        public AddressTypeList GetAll()
        {
            StudentDB database = new StudentDB("Student");
            database.Command.Parameters.Clear();
            database.Command.CommandType = System.Data.CommandType.StoredProcedure;
            database.Command.CommandText = "tblAddressType_GETALL";
            DataTable dt = database.ExecuteQuery();
            foreach (DataRow dr in dt.Rows)
            {
                AddressType addresstype = new AddressType();
                addresstype.Initialize(dr);
                addresstype.InitializeBusinessData(dr);
                addresstype.isNew = false;
                addresstype.isDirty = false;
                _List.Add(addresstype);
            }
            return this;
        }
        public AddressTypeList Save()
        {
            foreach (AddressType addresstype in _List)
            {
                if (addresstype.IsSaveable() == true)
                {
                    addresstype.Save();
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
        public AddressTypeList()
        {
            _List = new BindingList<AddressType>();
        }
        #endregion
    }
}
