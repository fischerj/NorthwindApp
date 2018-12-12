using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseHelper;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
namespace BusinessObjects
{
    public class HeaderData
    {

        #region Private Members
        private Guid _Id;
        private int _Version;
        private DateTime _LastUpdated;
        private bool _Deleted;

        private bool _isNew = true;
        private bool _isDirty = false;

        #endregion

        #region Public Properties
        public Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }

        }
        public int Version
        {
            get
            {
                return _Version;
            }
            set
            {
                _Version = value;
            }
        }
        public DateTime LastUpdated
        {
            get
            {
                return _LastUpdated;
            }
            set
            {
                _LastUpdated = value;
            }

        }
        public bool Deleted
        {
            get
            {
                return _Deleted;
            }
            set
            {
                _isDirty = true;
                _Deleted = value;
            }
        }
        public bool isDirty
        {
            get
            {
                return _isDirty;
            }
            set
            {
                _isDirty = value;
            }
        }
        public bool isNew
        {
            get
            {
                return _isNew;
            }
            set
            {
                _isNew = value;
            }
        }



        #endregion

        #region Private Methods 

        #endregion

        #region Public Methods 
        public void Initialize(Database database, Guid id)
        {
            SqlParameter parm = new SqlParameter();
            parm.ParameterName = "@Id";
            parm.Direction = ParameterDirection.InputOutput;
            parm.SqlDbType = SqlDbType.UniqueIdentifier;
            parm.Value = id;
            database.Command.Parameters.Add(parm);

            parm = new SqlParameter();
            parm.ParameterName = "@Version";
            parm.Direction = ParameterDirection.Output;
            parm.SqlDbType = SqlDbType.Int;
            parm.Value = 0;
            database.Command.Parameters.Add(parm);

            parm = new SqlParameter();
            parm.ParameterName = "@LastUpdated";
            parm.Direction = ParameterDirection.Output;
            parm.SqlDbType = SqlDbType.DateTime;
            parm.Value = DateTime.MaxValue;
            database.Command.Parameters.Add(parm);

            parm = new SqlParameter();
            parm.ParameterName = "@Deleted";
            parm.Direction = ParameterDirection.Output;
            parm.SqlDbType = SqlDbType.Bit;
            parm.Value = false;
            database.Command.Parameters.Add(parm);

        }

        public void Initialize(SqlCommand cmd)
        {
            _Id = (Guid)cmd.Parameters["@Id"].Value;
            _Version = (int)cmd.Parameters["@Version"].Value;
            _LastUpdated = (DateTime)cmd.Parameters["@LastUpdated"].Value;
            _Deleted = (bool)cmd.Parameters["@Deleted"].Value;
        }

        public void Initialize(DataRow dr)
        {
            _Id = (Guid)dr["Id"];
            _Version = (int)dr["Version"];
            _LastUpdated = Convert.ToDateTime(dr["LastUpdated"]);
            _Deleted = (bool)dr["Deleted"];
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

