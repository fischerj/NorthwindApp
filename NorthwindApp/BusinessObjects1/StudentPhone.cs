using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseHelper;
using System.Data;

namespace BusinessObjects
{
    public class StudentPhone : HeaderData
    {
        #region Private Members
        private string _Number = string.Empty;
        private Guid _StudentID = Guid.Empty;
        private Guid _PhoneTypeID = Guid.Empty;
        private BrokenRuleList _BrokenRules = null;


        #endregion

        #region Public Properties
        public string Number
        {
            get { return _Number; }
            set
            {
                if (_Number != value)
                {
                    _Number = value;
                    base.isDirty = true;
                }
            }
        }
        public Guid StudentID
        {
            get { return _StudentID; }
            set
            {
                if (_StudentID != value)
                {
                    _StudentID = value;
                    base.isDirty = true;
                }
            }
        }
        public Guid PhoneTypeID
        {
            get { return _PhoneTypeID; }
            set
            {
                if (_PhoneTypeID != value)
                {
                    _PhoneTypeID = value;
                    base.isDirty = true;
                }
            }
        }
        public BrokenRuleList BrokenRules
        {
            get
            {
                return _BrokenRules;
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
                database.Command.CommandText = "tblStudentPhone_INSERT";
                database.Command.Parameters.Add("@NUMBER", SqlDbType.VarChar).Value = _Number;
                database.Command.Parameters.Add("@STUDENTID", SqlDbType.UniqueIdentifier).Value = _StudentID;
                database.Command.Parameters.Add("@PHONETYPEID", SqlDbType.UniqueIdentifier).Value = _PhoneTypeID;

                base.Initialize(database, Guid.Empty);
                database.ExecuteEQueryWithTransaction();
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
                database.Command.CommandText = "tblStudentPhone_UPDATE";
                database.Command.Parameters.Add("@NUMBER", SqlDbType.VarChar).Value = _Number;
                database.Command.Parameters.Add("@STUDENTID", SqlDbType.UniqueIdentifier).Value = _StudentID;
                database.Command.Parameters.Add("@PHONETYPEID", SqlDbType.UniqueIdentifier).Value = _PhoneTypeID;

                base.Initialize(database, base.id);
                database.ExecuteEQueryWithTransaction();
                base.Initialize(database.Command);
            }
            catch (Exception e)
            {
                result = false;
                throw;
            }
            return result;
        }
        private bool Delete(Database database)
        {
            //refactor to fit DELETE stored procedure
            bool result = true;
            try
            {
                database.Command.Parameters.Clear();
                database.Command.CommandType = System.Data.CommandType.StoredProcedure;
                database.Command.CommandText = "tblStudentPhone_DELETE";

                base.Initialize(database, base.id);
                database.ExecuteEQueryWithTransaction();
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
            _BrokenRules = new BrokenRuleList();
            bool result = true;
            if (_Number != null && _Number.Trim() == string.Empty)
            {
                result = false;
                _BrokenRules.Rules.Add(new BrokenRule("ENTER PHONE NUMBER"));
            }

            if (_PhoneTypeID == Guid.Empty)
            {
                result = false;
                _BrokenRules.Rules.Add(new BrokenRule("SELECT PHONE TYPE"));
            }
            return result;
        }

        #endregion

        #region Public Methods 
        public StudentPhone GetByID(Guid id)
        {
            //refactor to fit DELETE stored procedure
            Database database = new Database("Student");
            DataTable dt = new DataTable();
            //may not need try|catch here
            //try
            //{
            database.Command.Parameters.Clear();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblStudentPhone_GETBYID";
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
            _Number = dr["NUMBER"].ToString();
            _StudentID = (Guid)dr["STUDENTID"];
            _PhoneTypeID = (Guid)dr["PHONETYPEID"];
        }

        public StudentPhone Save(Database database, Guid parentid)
        {
            bool result = true;
            _StudentID = parentid;
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
        public bool IsSaveable()
        {
            bool result = false;
            if (base.isDirty == true & IsValid() == true)
                result = true;
            return result;
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
