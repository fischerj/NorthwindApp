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
        private Guid _id;
        private int _version;
        private DateTime _lastUpdated;
        private bool _deleted;

        private bool _isNew = true;
        private bool _isDirty = false;
        #endregion

        #region Public Properties
        public Guid id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int version
        {
            get { return _version; }
            set { _version = value; }
        }
        public DateTime lastUpdated
        {
            get { return _lastUpdated; }
            set { _lastUpdated = value; }
        }
        public bool deleted
        {
            get { return _deleted; }
            set
            {
                isDirty = true;
                _deleted = value;
            }
        }
        public bool isNew
        {
            get { return _isNew; }
            set { _isNew = value; }
        }
        public bool isDirty
        {
            get { return _isDirty; }
            set { _isDirty = value; }
        }
        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        public void Initialize(Database database, Guid id)
        {
            SqlParameter parm = new SqlParameter();
            parm.ParameterName = "@ID";
            parm.Direction = ParameterDirection.InputOutput;
            parm.SqlDbType = SqlDbType.UniqueIdentifier;
            parm.Value = id;
            database.Command.Parameters.Add(parm);

            parm = new SqlParameter();
            parm.ParameterName = "@VERSION";
            parm.Direction = ParameterDirection.Output;
            parm.SqlDbType = SqlDbType.Int;
            parm.Value = 0;
            database.Command.Parameters.Add(parm);

            parm = new SqlParameter();
            parm.ParameterName = "@LASTUPDATED";
            parm.Direction = ParameterDirection.Output;
            parm.SqlDbType = SqlDbType.DateTime;
            parm.Value = DateTime.MaxValue;
            database.Command.Parameters.Add(parm);

            parm = new SqlParameter();
            parm.ParameterName = "@DELETED";
            parm.Direction = ParameterDirection.Output;
            parm.SqlDbType = SqlDbType.Bit;
            parm.Value = false;
            database.Command.Parameters.Add(parm);
        }
        public void Initialize(SqlCommand cmd)
        {
            _id = (Guid)cmd.Parameters["@ID"].Value;
            _version = (int)cmd.Parameters["@VERSION"].Value;
            _lastUpdated = (DateTime)cmd.Parameters["@LASTUPDATED"].Value;
            _deleted = (bool)cmd.Parameters["@DELETED"].Value;
        }
        public void Initialize(DataRow dr)
        {
            _id = (Guid)dr["ID"];
            _version = (int)dr["VERSION"];
            _lastUpdated = (DateTime)dr["LASTUPDATED"];
            _deleted = (bool)dr["DELETED"];
        }
        #endregion

        #region Public Events
        #endregion

        #region Construction
        #endregion
    }
}
