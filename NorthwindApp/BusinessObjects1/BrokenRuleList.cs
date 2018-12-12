using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BusinessObjects
{
    public class BrokenRuleList
    {
        #region Private Members
        private BindingList<BrokenRule> _Rules = null;
        #endregion

        #region Public Properties
        public BindingList<BrokenRule> Rules
        {
            get
            {
                return _Rules;
            }
        }
        #endregion

        #region Private Methods 
        //BUILD BROKENRULELISTMASTER FROM STUDENT AND ALL CHILDREN
        #endregion

        #region Public Methods 
        //GET BROKENRULELIST FROM STUDENT AND ALL CHILDREN

        //RETURN BROKENRULELISTMASTER TO STUDENT
        #endregion

        #region Public Events

        #endregion

        #region Public Event Handlers 

        #endregion

        #region Construction
        public BrokenRuleList()
        {
            _Rules = new BindingList<BrokenRule>();
        }
        #endregion

    }
}
