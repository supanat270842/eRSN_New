using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace eRSN_New.Services.Helpers
{
    public class EnvironmentHelper
    {
        public static string MachineName(string IP)
        {
            string result = string.Empty;

            try
            {
                IPAddress myIP = IPAddress.Parse(IP);
                IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
                List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
                result = compName.First();
            } // end try:

            catch (Exception)
            {
                result = IP;
            } // end catch: Exception

            return result;
        } // end string: MachineName

        public static string GetHostName(string IP)
        {
            string result = string.Empty;

            try
            {
                IPAddress myIP = IPAddress.Parse(IP);
                IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
                List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
                result = GetIPHost.HostName.ToString();
            } // end try

            catch (Exception)
            {
                result = IP;
            } // end catch: Exception

            return result;
        } // end string: GetHostName
    } // end class: EnvironmentHelper
} // end namespace: eRSN_New.Services.Helpers