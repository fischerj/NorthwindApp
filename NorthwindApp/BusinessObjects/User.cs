using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseHelper;
using System.Data;

namespace BusinessObjects
{
    public class User : HeaderData
    {
        #region Private Members
        private string _Email = string.Empty;
        private string _Password = string.Empty;

        #endregion

        #region Public Properties
        public string Email
        {
            get { return _Email; }
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    base.isDirty = true;
                }
            }
        }

        public string Password
        {
            get { return _Password; }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
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
                database.Command.CommandText = "tblUser_INSERT";
                database.Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = _Email;
                database.Command.Parameters.Add("@Password", SqlDbType.VarChar).Value = _Password;


                base.Initialize(database, Guid.Empty);
                database.ExecuteBQuery();
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
                database.Command.CommandText = "tblUser_UPDATE";
                database.Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = _Email;
                database.Command.Parameters.Add("@Password", SqlDbType.VarChar).Value = _Password;

                base.Initialize(database, base.Id);
                database.ExecuteBQuery();
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
                database.Command.CommandText = "tblUser_DELETE";

                base.Initialize(database, base.Id);
                database.ExecuteBQuery();
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
            if (_Email != null && _Email.Trim() == string.Empty)
            {
                result = false;
            }

            if (_Password != null && _Password.Trim() == string.Empty)
            {
                result = false;
            }

            return result;
        }

        #endregion

        #region Public Methods 
        public User GetByID(Guid id)
        {
            //refactor to fit DELETE stored procedure
            Database database = new Database("Northwind");
            DataTable dt = new DataTable();
            //may not need try|catch here
            //try
            //{
            database.Command.Parameters.Clear();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblShipper_GETBYID";
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
            _Email = dr["Email"].ToString();
            _Password = dr["Password"].ToString();
        }
        public bool IsSaveable()
        {
            bool result = false;
            if (base.isDirty == true && IsValid() == true)
                result = true;
            return result;
        }
        public User Save()
        {
            bool result = true;
            Database database = new Database("Northwind");
            if (base.isNew == true && IsSaveable() == true)
                result = Insert(database);
            else if (base.Deleted == true && base.isDirty == true)
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
        public User Login(string email, string password)
        {

            User userObj = this;
            Database database = new Database("Northwind");
            DataTable dt = new DataTable();

            database.Command.Parameters.Clear();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblUser_Login";
            database.Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
            database.Command.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
            dt = database.ExecuteQuery();
            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                base.Initialize(dr);
                InitializeBusinessData(dr);
                base.isNew = false;
                base.isDirty = false;
            } else
            {
                userObj= null;
            }
            return userObj;

        }

        public User ForgotPassword(string email)
        {

            User userObj = this;
            Database database = new Database("Northwind");
            DataTable dt = new DataTable();

            database.Command.Parameters.Clear();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblUser_GETBYEMAIL";
            database.Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
            dt = database.ExecuteQuery();
            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                base.Initialize(dr);
                InitializeBusinessData(dr);
                base.isNew = false;
                base.isDirty = false;
                
            }
            else
            {
                userObj = null;
            }
            return userObj;

        }

        public User Register(string email)
        {
            User userObj = this;
            Password = CreatePassword(12);
            Email = email;
            Save();
            if (base.isNew==false && base.isDirty == false)
            {
                //SEND AN EMAIL TO REGISTERER
                EmailHelper.Email.Send(_Email, "Your new password", _Password);
            }else
            {
                userObj = null;
            }
            return userObj;
        }


        public User ChangePassword(string email, string password)
        {
            User userObj = this;
            Password = password;
            Email = email;
            Save();
            if (base.isNew == false && base.isDirty == false)
            {
                //SEND AN EMAIL TO REGISTERER
                EmailHelper.Email.Send(_Email, "Forgot your password", _Password);
            }
            else
            {
                userObj = null;
            }
            return userObj;
        }


        private string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
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
