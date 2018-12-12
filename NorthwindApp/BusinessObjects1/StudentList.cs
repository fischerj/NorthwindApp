using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DatabaseHelper;
using System.Data;
using BusinessObjects;


namespace BusinessObjects
{
    public class StudentList
    {

        #region Private Members
        private BindingList<Student> _List;
        private SQLHelper.Criteria _Criteria;
        private BindingList<BrokenRule> _BrokenRules;
        #endregion

        #region Public Properties
        public BindingList<Student> List
        {
            get { return _List; }
        }
        public BindingList<BrokenRule> BrokenRules
        {
            get
            {
                foreach (Student s in _List)
                {
                    foreach (BrokenRule br in s.BrokenRules.Rules)
                    {
                        _BrokenRules.Add(new BrokenRule(br.Rule));
                    }
                }
                return _BrokenRules;
            }
        }
        public string FirstName
        {
            set
            {
                if(value.Trim() != string.Empty)
                {
                    _Criteria.Fields.Add("FNAME");
                    _Criteria.Values.Add(value);
                }
            }
        }
        
        #endregion

        #region Private Methods 

        #endregion

        #region Public Methods 
        public StudentList Search()
        {
            StudentDB database = new StudentDB("Student");
            database.Command.Parameters.Clear();
            database.Command.CommandType = System.Data.CommandType.Text;
            _Criteria.TableName = "tblStudent";
            database.Command.CommandText = SQLHelper.Builder.Build(_Criteria);
            DataTable dt = database.ExecuteQuery();
            foreach(DataRow dr in dt.Rows)
            {
                Student student = new Student();
                student.Initialize(dr);
                student.InitializeBusinessData(dr);
                student.isNew = false;
                student.isDirty = false;
                _List.Add(student);
            }
            return this;
        }
        public StudentList Save()
        {
            foreach(Student student in _List)
            {
                if(student.IsSaveable()==true)
                {
                    student.Save();
                }
            }
            return this;
        }
        public bool IsSaveable()
        {
            bool result = false;
            foreach (Student student in _List)
            {
                if (student.IsSaveable() == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        #endregion

        #region Public Events

        #endregion

        #region Public Event Handlers 

        #endregion

        #region Construction
        public StudentList()
        {
            _List = new BindingList<Student>();
            _BrokenRules = new BindingList<BrokenRule>();
            _Criteria = new SQLHelper.Criteria();
        }
        #endregion

    }
}
