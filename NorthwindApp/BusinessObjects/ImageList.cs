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
    public class ImageList
    {
        #region Private Members
        private BindingList<Images> _List;
        private string _mapPath = string.Empty;
        #endregion

        #region Public Properties


        #endregion
        public string MapPath
        {
            get { return _mapPath; }
            set { _mapPath = value; }
        }
        #region Public Properties
        public BindingList<Images> List
        {
            get { return _List; }
        }


        #endregion

        #region Private Methods 

        #endregion

        #region Public Methods 
        public ImageList GetAll()
        {
            Database database = new Database("Northwind");
            database.Command.Parameters.Clear();
            database.Command.CommandType = System.Data.CommandType.StoredProcedure;
            database.Command.CommandText = "tblImages_GetAll";
            DataTable dt = database.ExecuteQuery();
            foreach (DataRow dr in dt.Rows)
            {
                Images im = new Images();
                im.Initialize(dr);
                im.FullPathfileName = MapPath;
                im.RelativeFilePath = String.Format("~/files/{0}.jpg", im.Id) ;
                im.InitializeBusinessData(dr);
                im.isNew = false;
                im.isDirty = false;
                _List.Add(im);
            }
            return this;
        }
        public ImageList Save()
        {
            foreach (Images sh in _List)
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
        public ImageList()
        {
            _List = new BindingList<Images>();
        }
        #endregion
    }
}
