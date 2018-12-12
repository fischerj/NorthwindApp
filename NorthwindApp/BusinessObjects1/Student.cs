using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseHelper;
using System.Data;

namespace BusinessObjects
{
    public class Student : HeaderData
    {
        #region Private Members
        private string _FNAME = string.Empty;
        private string _LNAME = string.Empty;
        private StudentPhoneList _Phones = null;
        private StudentEmailList _Emails = null;
        private StudentAddressList _Addresses = null;
        //TRY CONSTRUCTING AS A PUBLIC PROPERTY THAT CAN BE PASSED TO BROKENRULELIST
        private BrokenRuleList _BrokenRules = null;


        #endregion

        #region Public Properties
        public string FName
        {
            get { return _FNAME; }
            set
            {
                if (_FNAME != value)
                {
                    _FNAME = value;
                    base.isDirty = true;
                }
            }
        }
        public string LName
        {
            get { return _LNAME; }
            set
            {
                if (_LNAME != value)
                {
                    _LNAME = value;
                    base.isDirty = true;
                }
            }
        }
        public StudentPhoneList Phones
        {
            get
            {
                if (_Phones == null)
                {
                    _Phones = new StudentPhoneList();
                    _Phones = _Phones.GetByStudentID(base.id);
                }
                return _Phones;
            }
        }
        public StudentEmailList Emails
        {
            get
            {
                if (_Emails == null)
                {
                    _Emails = new StudentEmailList();
                    _Emails = _Emails.GetByStudentID(base.id);
                }
                return _Emails;
            }
        }
        public StudentAddressList Addresses
        {
            get
            {
                if (_Addresses == null)
                {
                    _Addresses = new StudentAddressList();
                    _Addresses = _Addresses.GetByStudentID(base.id);
                }
                return _Addresses;
            }
        }
        public BrokenRuleList BrokenRules
        {
            get
            {
                //_BrokenRules = new BrokenRuleList();
                foreach (StudentPhone sp in Phones.List)
                {
                    if (sp.BrokenRules != null && sp.BrokenRules.Rules.Count > 0)
                    {
                        foreach (BrokenRule br in sp.BrokenRules.Rules)
                        {
                            _BrokenRules.Rules.Add(new BrokenRule(br.Rule));
                        }
                    }
                }


                foreach (StudentEmail se in Emails.List)
                {
                    if (se.BrokenRules != null && se.BrokenRules.Rules.Count > 0)
                    {
                        foreach (BrokenRule br in se.BrokenRules.Rules)
                        {
                            _BrokenRules.Rules.Add(new BrokenRule(br.Rule));
                        }
                    }
                }

                foreach (StudentAddress sa in Addresses.List)
                {
                    if (sa.BrokenRules != null && sa.BrokenRules.Rules.Count > 0)
                    {
                        foreach (BrokenRule br in sa.BrokenRules.Rules)
                        {
                            _BrokenRules.Rules.Add(new BrokenRule(br.Rule));
                        }
                    }
                }
                return _BrokenRules;
            }
        }
        #endregion

        #region Private Methods 
        private bool Insert(StudentDB database)
        {
            bool result = true;
            try
            {
                database.Command.Parameters.Clear();
                database.Command.CommandType = System.Data.CommandType.StoredProcedure;
                database.Command.CommandText = "tblStudent_INSERT";
                database.Command.Parameters.Add("@FNAME", SqlDbType.VarChar).Value = _FNAME;
                database.Command.Parameters.Add("@LNAME", SqlDbType.VarChar).Value = _LNAME;

                base.Initialize(database, Guid.Empty);
                database.ExecuteEQueryWithTransaction();
                base.Initialize(database.Command);
            }
            catch (Exception e)
            {
                result = false;
                throw e;
            }
            return result;
        }
        private bool Update(StudentDB database)
        {
            bool result = true;
            try
            {
                database.Command.Parameters.Clear();
                database.Command.CommandType = System.Data.CommandType.StoredProcedure;
                database.Command.CommandText = "tblStudent_UPDATE";
                database.Command.Parameters.Add("@FNAME", SqlDbType.VarChar).Value = _FNAME;
                database.Command.Parameters.Add("@LNAME", SqlDbType.VarChar).Value = _LNAME;

                base.Initialize(database, base.id);
                database.ExecuteEQueryWithTransaction();
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
                database.Command.CommandText = "tblStudent_DELETE";

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
            _BrokenRules = new BrokenRuleList();
            bool result = true;
            if (_FNAME != null && _FNAME.Trim() == string.Empty)
            {
                result = false;
                _BrokenRules.Rules.Add(new BrokenRule("ENTER FIRST NAME"));
            }

            if (_LNAME != null && _LNAME.Trim() == string.Empty)
            {
                result = false;
                _BrokenRules.Rules.Add(new BrokenRule("ENTER LAST NAME"));
            }
            return result;
        }

        #endregion

        #region Public Methods 
        public bool Register ( string Email, string lastName)
        {
            bool result = true;
            try
            {
                StudentDB database = new StudentDB("Student");
                DataTable dt = new DataTable();
                database.Command.Parameters.Clear();
                database.Command.CommandType = System.Data.CommandType.StoredProcedure;
                database.Command.CommandText = "tblStudent_REGISTER";
                database.Command.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = Email;
                database.Command.Parameters.Add("@LNAME", SqlDbType.VarChar).Value = lastName;

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
        public Student Login(string firstName, string lastName)
        {
            StudentDB database = new StudentDB("Student");
            DataTable dt = new DataTable();
            database.Command.Parameters.Clear();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblStudent_Login";
            database.Command.Parameters.Add("@FNAME", SqlDbType.VarChar).Value = firstName;
            database.Command.Parameters.Add("@LNAME", SqlDbType.VarChar).Value = lastName;

            dt = database.ExecuteQuery();
            if (dt != null && dt.Rows.Count == 1)
            {
                {
                    DataRow dr = dt.Rows[0];
                    base.Initialize(dr);
                    InitializeBusinessData(dr);
                    base.isNew = false;
                    base.isDirty = false;
                }
                return this;
            }
            else
            {
                return null;
            }
        }
        public Student GetByID(Guid id)
        {
            //refactor to fit DELETE stored procedure
            StudentDB database = new StudentDB("Student");
            DataTable dt = new DataTable();
            //may not need try|catch here
            //try
            //{
            database.Command.Parameters.Clear();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblStudent_GETBYID";
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
            _FNAME = dr["FNAME"].ToString();
            _LNAME = dr["LNAME"].ToString();

        }
        public bool IsSaveable()
        {
            bool result = false;
            if ((base.isDirty == true & IsValid() == true) || (_Phones != null && _Phones.IsSaveable() == true) || (_Emails != null && _Emails.IsSaveable() == true) || (_Addresses != null && _Addresses.IsSaveable() == true))
                result = true;
            return result;
        }
        public Student Save()
        {
            bool result = true;
            StudentDB database = new StudentDB("Student");
            //OPEN CONNECTION BEGIN TRANSACTION
            database.BeginTransaction();

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

            //SAVE CHILDREN
            if (result == true && _Phones != null && _Phones.IsSaveable() == true)
            {
                _Phones.Save(database, base.id);
            }
            if (result == true && _Emails != null && _Emails.IsSaveable() == true)
            {
                _Emails.Save(database, base.id);
            }
            if (result == true && _Addresses != null && _Addresses.IsSaveable() == true)
            {
                _Addresses.Save(database, base.id);
            }
            //ALL DATA IS COMMITTED
            if (result == true)
            {
                database.EndTransaction();
            }
            else
            {
                database.Rollback();
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
