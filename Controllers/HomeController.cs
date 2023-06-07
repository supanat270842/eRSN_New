using eRSN_New.Filter;
using eRSN_New.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace eRSN_New.Controllers
{
    [Auth]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        } // end ActionResult: Index
        
        public ActionResult Document()
        {
            return View();
        } // end ActionResult: Document

        public ActionResult Contact()
        {
            return View();
        } // end ActionResult: Contact
    } // end class: HomeController : Controller
} // end namespace: eRSN_New.Controllers