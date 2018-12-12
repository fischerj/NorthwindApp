using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseHelper;
using System.Data;
using PhotoHelper;
using System.IO;

namespace BusinessObjects
{
    public class Shipper : HeaderData
    {
        #region Private Members
        private string _CompanyName = string.Empty;
        private string _Phone = string.Empty;
        private byte[] _Image = null;
        private string _fullPathfileName = string.Empty;
        private string _relativeFilePath = string.Empty;
        #endregion

        #region Public Properties
        public string CompanyName
        {
            get { return _CompanyName; }
            set
            {
                if (_CompanyName != value)
                {
                    _CompanyName = value;
                    base.isDirty = true;
                }
            }
        }

        public string Phone
        {
            get { return _Phone; }
            set
            {
                if (_Phone != value)
                {
                    _Phone = value;
                    base.isDirty = true;
                }
            }
        }

        public string RelativeFilePath
        {
            get { return _relativeFilePath; }
            set { _relativeFilePath = value; }
        }
        public string FullPathfileName
        {
            get { return _fullPathfileName; }
            set { _fullPathfileName = value; }
        }

        public byte[] Image
        {
            get { return _Image; }
            set
            {
                if (_Image != value)
                {
                    _Image = value;
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
                database.Command.CommandText = "Shipper_INSERT";
                database.Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = _CompanyName;
                database.Command.Parameters.Add("@Phone", SqlDbType.VarChar).Value = _Phone;
                database.Command.Parameters.Add("@Image", SqlDbType.Image).Value = Photo.ImageToByteArray(_fullPathfileName);


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
                database.Command.CommandText = "Shipper_UPDATE";
                database.Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = _CompanyName;
                database.Command.Parameters.Add("@Phone", SqlDbType.VarChar).Value = _Phone;
                database.Command.Parameters.Add("@Image", SqlDbType.Image).Value = Photo.ImageToByteArray(_fullPathfileName);

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
                database.Command.CommandText = "Shipper_DELETE";

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
            if (_CompanyName != null && _CompanyName.Trim() == string.Empty)
            {
                result = false;
            }


            return result;
        }

        #endregion

        #region Public Methods 
        public Shipper GetByID(Guid id)
        {
            //refactor to fit DELETE stored procedure
            Database database = new Database("Student");
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
        }
        public void InitializeBusinessData(DataRow dr)
        {
            _CompanyName = dr["CompanyName"].ToString();
            _Phone = dr["Phone"].ToString();
            _Image = (byte[])dr["image"];
            Photo.ByteArrayToFile(_Image, Path.Combine(_fullPathfileName, string.Format("{0}.jpg", Id)));

        }
        public bool IsSaveable()
        {
            bool result = false;
            if (base.isDirty == true & IsValid() == true)
                result = true;
            return result;
        }
        public Shipper Save()
        {
            bool result = true;
            Database database = new Database("Student");
            if (base.isNew == true & IsSaveable() == true)
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
        #endregion

        #region Public Events

        #endregion

        #region Public Event Handlers 

        #endregion

        #region Construction

        #endregion
    }
}
