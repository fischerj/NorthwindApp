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
    public class ShipperList
    {
        #region Private Members
        private BindingList<Shipper> _List;
        private string _mapPath = string.Empty;
        #endregion

        #region Public Properties
        public BindingList<Shipper> List
        {
            get { return _List; }
        }

        public string MapPath
        {
            get { return _mapPath; }
            set { _mapPath = value; }
        }
        #endregion

        #region Private Methods 

        #endregion

        #region Public Methods 
        public ShipperList GetAll()
        {
            Database database = new Database("Northwind");
            database.Command.Parameters.Clear();
            database.Command.CommandType = System.Data.CommandType.StoredProcedure;
            database.Command.CommandText = "Shipper_GETALL";
            DataTable dt = database.ExecuteQuery();
            foreach (DataRow dr in dt.Rows)
            {
                Shipper sh = new Shipper();
                sh.Initialize(dr);
                sh.FullPathfileName = MapPath;
                sh.RelativeFilePath = String.Format("~/files/{0}.jpg", sh.Id);
                sh.InitializeBusinessData(dr);
                sh.isNew = false;
                sh.isDirty = false;
                _List.Add(sh);
            }
            return this;
        }
        public ShipperList Save()
        {
            foreach (Shipper sh in _List)
            {
                if (sh.IsSaveable() == true)
                {
                    sh.Save();
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
        public ShipperList()
        {
            _List = new BindingList<Shipper>();
        }
        #endregion
    }
}
