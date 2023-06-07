using eRSN_New.Models;
using eRSN_New.Services;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace eRSN_New.Services.Helpers
{
    public class LoginHelper
    {
        public ResultCheckADModels CheckAuthorize(string USER, string PASS, string DOMAIN = "asia")
        {
            string result = string.Empty;
            // Check AD
            ActiveDirectoryService AD = new ActiveDirectoryService();
            ResultCheckADModels result_check_ad = AD.CheckLoginActiveDirectory(USER, PASS, DOMAIN);

            if (result_check_ad.Success.Equals(false)) { throw new Exception(result_check_ad.Message); }

            return result_check_ad;
        } // end ResultCheckADModels: CheckAuthorize
    } // end class: LoginHelper
} // end namespace: eRSN_New.Services.Helpers