using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseHelper;
using System.Data;

namespace BusinessObjects
{
    public class EmailType : HeaderData
    {
        #region Private Members
        private string _TYPE = string.Empty;

        #endregion

        #region Public Properties
        public string Type
        {
            get { return _TYPE; }
            set
            {
                if (_TYPE != value)
                {
                    _TYPE = value;
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
                database.Command.CommandText = "tblEmailType_INSERT";
                database.Command.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = _TYPE;

                base.Initialize(database, Guid.Empty);
                database.ExecuteEQuery();
                base.Initialize(database.Command);
            }
            catch (Exception e)
            {
                result = false;
                throw;
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
                database.Command.CommandText = "tblEmailType_UPDATE";
                database.Command.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = _TYPE;

                base.Initialize(database, base.id);
                database.ExecuteEQuery();
                base.Initialize(database.Command);
            }
            catch (Exception e)
            {
                result = false;
                throw;
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
                database.Command.CommandText = "tblEmailType_DELETE";

                base.Initialize(database, base.id);
                database.ExecuteEQuery();
                base.Initialize(database.Command);
            }
            catch (Exception e)
            {
                result = false;
                throw;
            }
            return result;
        }
        private bool IsValid()
        {
            bool result = true;
            if (_TYPE != null && _TYPE.Trim() == string.Empty)
                result = false;
            return result;
        }

        #endregion

        #region Public Methods 
        public EmailType GetByID(Guid id)
        {
            //refactor to fit DELETE stored procedure
            Database database = new Database("Student");
            DataTable dt = new DataTable();
            //may not need try|catch here
            //try
            //{
            database.Command.Parameters.Clear();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblEmailType_GETBYID";
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
            _TYPE = dr["TYPE"].ToString();
        }
        public bool IsSaveable()
        {
            bool result = false;
            if (base.isDirty == true & IsValid() == true)
                result = true;
            return result;
        }
        public EmailType Save()
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
