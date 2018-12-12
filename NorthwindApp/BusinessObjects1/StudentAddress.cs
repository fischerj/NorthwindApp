using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseHelper;
using System.Data;

namespace BusinessObjects
{
    public class StudentAddress : HeaderData
    {
        #region Private Members
        private string _Address = string.Empty;
        private string _City = string.Empty;
        private string _State = string.Empty;
        private string _Zipcode = string.Empty;
        private Guid _StudentID = Guid.Empty;
        private Guid _AddressTypeID = Guid.Empty;
        private BrokenRuleList _BrokenRules = null;

        #endregion

        #region Public Properties
        public string Address
        {
            get { return _Address; }
            set
            {
                if (_Address != value)
                {
                    _Address = value;
                    base.isDirty = true;
                }
            }
        }
        public string City
        {
            get { return _City; }
            set
            {
                if (_City != value)
                {
                    _City = value;
                    base.isDirty = true;
                }
            }
        }
        public string State
        {
            get { return _State; }
            set
            {
                if (_State != value)
                {
                    _State = value;
                    base.isDirty = true;
                }
            }
        }
        public string Zipcode
        {
            get { return _Zipcode; }
            set
            {
                if (_Zipcode != value)
                {
                    _Zipcode = value;
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
        public Guid AddressTypeID
        {
            get { return _AddressTypeID; }
            set
            {
                if (_AddressTypeID != value)
                {
                    _AddressTypeID = value;
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
                database.Command.CommandText = "tblStudentAddress_INSERT";
                database.Command.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = _Address;
                database.Command.Parameters.Add("@CITY", SqlDbType.VarChar).Value = _City;
                database.Command.Parameters.Add("@STATE", SqlDbType.VarChar).Value = _State;
                database.Command.Parameters.Add("@ZIPCODE", SqlDbType.VarChar).Value = _Zipcode;
                database.Command.Parameters.Add("@STUDENTID", SqlDbType.UniqueIdentifier).Value = _StudentID;
                database.Command.Parameters.Add("@ADDRESSTYPEID", SqlDbType.UniqueIdentifier).Value = _AddressTypeID;

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
                database.Command.CommandText = "tblStudentAddress_UPDATE";
                database.Command.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = _Address;
                database.Command.Parameters.Add("@CITY", SqlDbType.VarChar).Value = _City;
                database.Command.Parameters.Add("@STATE", SqlDbType.VarChar).Value = _State;
                database.Command.Parameters.Add("@ZIPCODE", SqlDbType.VarChar).Value = _Zipcode;
                database.Command.Parameters.Add("@STUDENTID", SqlDbType.UniqueIdentifier).Value = _StudentID;
                database.Command.Parameters.Add("@ADDRESSTYPEID", SqlDbType.UniqueIdentifier).Value = _AddressTypeID;

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
                database.Command.CommandText = "tblStudentAddress_DELETE";

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
            if (_Address != null && _Address.Trim() == string.Empty)
            {
                result = false;
                _BrokenRules.Rules.Add(new BrokenRule("ENTER STREET ADDRESS"));
            }
            if (_City != null && _City.Trim() == string.Empty)
            {
                result = false;
                _BrokenRules.Rules.Add(new BrokenRule("ENTER CITY"));
            }
            if (_State != null && _State.Trim() == string.Empty)
            {
                result = false;
                _BrokenRules.Rules.Add(new BrokenRule("ENTER STATE"));
            }
            if (_Zipcode != null && _Zipcode.Trim() == string.Empty)
            {
                result = false;
                _BrokenRules.Rules.Add(new BrokenRule("ENTER ZIPCODE"));
            }

            if (_AddressTypeID == Guid.Empty)
            {
                result = false;
                _BrokenRules.Rules.Add(new BrokenRule("SELECT ADDRESS TYPE"));
            }
            return result;
        }

        #endregion

        #region Public Methods 
        public StudentAddress GetByID(Guid id)
        {
            //refactor to fit DELETE stored procedure
            Database database = new Database("Student");
            DataTable dt = new DataTable();
            //may not need try|catch here
            //try
            //{
            database.Command.Parameters.Clear();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblStudentAddress_GETBYID";
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
            _Address = dr["ADDRESS"].ToString();
            _City = dr["CITY"].ToString();
            _State = dr["STATE"].ToString();
            _Zipcode = dr["ZIPCODE"].ToString();
            _StudentID = (Guid)dr["STUDENTID"];
            _AddressTypeID = (Guid)dr["ADDRESSTYPEID"];
        }

        public StudentAddress Save(Database database, Guid parentid)
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
