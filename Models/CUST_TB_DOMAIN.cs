using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;

namespace eRSN_New.Models
{
    public class CUST_TB_DOMAIN
    {
        public string name { get; set; }
    } // end class: CUST_TB_DOMAIN

    public class CUST_TB_DOMAIN_Repository
    {
        public List<CUST_TB_DOMAIN> getDomainName()
        {
            List<CUST_TB_DOMAIN> result = new List<CUST_TB_DOMAIN>();

            string[] data = ConfigurationManager.AppSettings["Domain"].Split(',');

            foreach (string item in data)
            {
                CUST_TB_DOMAIN md = new CUST_TB_DOMAIN();
                md.name = item;

                result.Add(md);
            } // end foreach: data

            return result;
        } // end List<CUST_TB_DOMAIN>: getDomainName
    } // end class: CUST_TB_DOMIAN_Repository
} // end namespace: eRSN_New.Models

