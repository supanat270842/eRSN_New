using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace eRSN_New.Services
{
    public class DatabaseConnectionService
    {
        public static string eRSNConnection = ConfigurationManager.AppSettings["eRSNConnection"].ToString();
    } // end class: DatabaseConnectionService
} // end namespace: eRSN_New.Services