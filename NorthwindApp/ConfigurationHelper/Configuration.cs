using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Configuration;

namespace ConfigurationHelper
{
    public class Configuration

    {
        public static string GetConnectionString(string connName)
        {
            string strConnection = string.Empty;
            strConnection = ConfigurationManager.ConnectionStrings[connName].ConnectionString;
            return strConnection;
        }

    }
}
