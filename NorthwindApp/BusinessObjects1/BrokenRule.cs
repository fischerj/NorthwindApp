using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class BrokenRule
    {
        #region Private Members
        private string _Rule;
        #endregion

        #region Public Properties
        public string Rule
        {
            get
            {
                return _Rule;
            }
        }
        #endregion

        #region Private Methods 

        #endregion

        #region Public Methods 

        #endregion

        #region Public Events

        #endregion

        #region Public Event Handlers 

        #endregion

        #region Construction
        public BrokenRule(string Rule)
        {
            _Rule = Rule;
        }
        #endregion

    }
}
