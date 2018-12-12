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
    public class CustomerList
    {


        #region Private Members
        private BindingList<Customer> _List;
        #endregion

        #region Public Properties
        public BindingList<Customer> List
        {
            get { return _List; }
        }
        #endregion

        #region Private Methods 

        #endregion

        #region Public Methods 
        public CustomerList GetAll()
        {
            StudentDB database = new StudentDB("Student");
            database.Command.Parameters.Clear();
            database.Command.CommandType = System.Data.CommandType.StoredProcedure;
            database.Command.CommandText = "tblCustomer_GETALL";
            DataTable dt = database.ExecuteQuery();
            foreach (DataRow dr in dt.Rows)
            {
                Customer customer = new Customer();
                customer.Initialize(dr);
                customer.InitializeBusinessData(dr);
                customer.isNew = false;
                customer.isDirty = false;
                _List.Add(customer);
            }
            return this;
        }
        public CustomerList Save()
        {
            foreach (Customer customer in _List)
            {
                if (customer.IsSaveable() == true)
                {
                    customer.Save();
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
        public CustomerList()
        {
            _List = new BindingList<Customer>();
        }
        #endregion

    }
}
