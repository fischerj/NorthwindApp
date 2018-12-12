using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseHelper;
using System.Data;

namespace BusinessObjects
{
    public class Customer : HeaderData
    {
        #region Private Members
        private string _COMPANYNAME = string.Empty, _ADDRESS = string.Empty;

        #endregion

        #region Public Properties
        public string CompanyName
        {
            get { return _COMPANYNAME; }
            set
            {
                if (_COMPANYNAME != value)
                {
                    _COMPANYNAME = value;
                    base.isDirty = true;
                }
            }
        }
        public string Address
        {
            get { return _ADDRESS; }
            set
            {
                if (_ADDRESS != value)
                {
                    _ADDRESS = value;
                    base.isDirty = true;
                }
            }
        }
        #endregion

        #region Private Methods 
        private bool Insert(Database database)
        {
            bool result = true;
            try
            {
                database.Command.Parameters.Clear();
                database.Command.CommandType = System.Data.CommandType.StoredProcedure;
                database.Command.CommandText = "tblCustomer_INSERT";
                database.Command.Parameters.Add("@COMPANYNAME", SqlDbType.VarChar).Value = _COMPANYNAME;
                database.Command.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = _ADDRESS;

                base.Initialize(database, Guid.Empty);
                database.ExecuteEQuery();
                base.Initialize(database.Command);
            }
            catch (Exception e)
            {
                result = false;
                throw e;
            }
            return result;
        }
        private bool Update(Database database)
        {
            bool result = true;
            try
            {
                database.Command.Parameters.Clear();
                database.Command.CommandType = System.Data.CommandType.StoredProcedure;
                database.Command.CommandText = "tblCustomer_UPDATE";
                database.Command.Parameters.Add("@COMPANYNAME", SqlDbType.VarChar).Value = _COMPANYNAME;
                database.Command.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = _ADDRESS;

                base.Initialize(database, base.id);
                database.ExecuteEQuery();
                base.Initialize(database.Command);
            }
            catch (Exception e)
            {
                result = false;
                throw e;
            }
            return result;
        }
        private bool Delete(StudentDB database)
        {
            //refactor to fit DELETE stored procedure
            bool result = true;
            try
            {
                database.Command.Parameters.Clear();
                database.Command.CommandType = System.Data.CommandType.StoredProcedure;
                database.Command.CommandText = "tblCustomer_DELETE";

                base.Initialize(database, base.id);
                database.ExecuteEQuery();
                base.Initialize(database.Command);
            }
            catch (Exception e)
            {
                result = false;
                throw e;
            }
            return result;
        }
        private bool IsValid()
        {
            bool result = true;
            if (_COMPANYNAME != null && _COMPANYNAME.Trim() == string.Empty)
                result = false;
            if (_ADDRESS != null && _ADDRESS.Trim() == string.Empty)
                result = false;
            return result;
        }

        #endregion

        #region Public Methods 
        public Customer GetByID(Guid id)
        {
            //refactor to fit DELETE stored procedure
            StudentDB database = new StudentDB("Student");
            DataTable dt = new DataTable();
            //may not need try|catch here
            //try
            //{
            database.Command.Parameters.Clear();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblCustomer_GETBYID";
            database.Command.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = id;
            dt = database.ExecuteQuery();
            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                base.Initialize(dr);
                InitializeBusinessData(dr);
                base.isNew = false;
                base.isDirty = false;
            }
            return this;
            //}
            //catch (Exception e)
            //{
            //    result = false;
            //    throw;
            //}
        }
        public void InitializeBusinessData(DataRow dr)
        {
            _COMPANYNAME = dr["COMPANYNAME"].ToString();
            _ADDRESS = dr["ADDRESS"].ToString();

        }
        public bool IsSaveable()
        {
            bool result = false;
            if (base.isDirty == true & IsValid() == true)
                result = true;
            return result;
        }
        public Customer Save()
        {
            bool result = true;
            StudentDB database = new StudentDB("Student");
            if (base.isNew == true & IsSaveable() == true)
                result = Insert(database);
            else if (base.deleted == true && base.isDirty == true)
                result = Delete(database);
            else if (base.isNew == false && IsValid() == true)
                result = Update(database);
            if (result == true)
            {
                base.isNew = false;
                base.isDirty = false;
            }
            return this;
        }
        #endregion

        #region Public Events

        #endregion

        #region Public Event Handlers 

        #endregion

        #region Construction

        #endregion
    }
}
