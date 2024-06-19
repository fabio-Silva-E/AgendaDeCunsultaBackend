using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_api.Configurations
{
    public class SQLServer
    {
        public static string getConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["consultorio"].ConnectionString;
        }
    }
}