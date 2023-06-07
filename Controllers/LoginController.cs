using eRSN_New.Models;
using eRSN_New.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eRSN_New.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        } // end ActionResult: Index

        public JsonResult getDomainName()
        {
            try
            {
                bool result = false;
                string msg = string.Empty;
                List<CUST_TB_DOMAIN> List = new List<CUST_TB_DOMAIN>();

                CUST_TB_DOMAIN_Repository repo = new CUST_TB_DOMAIN_Repository();

                List = repo.getDomainName();

                if (List.Count > 0)
                {
                    result = true;
                    msg = "Success";
                }
                else
                {
                    result = false;
                    msg = "Error not found any data!";
                } // end if: check data is not null

                return Json(new { result = result, message = msg, list = List });
            } // end try:

            catch (Exception ex)
            {
                return Json(new { result = false, message = ex });
            }
        } // end JsonResult: getDomainName

        public JsonResult CheckLogin(string USER, string PASS, string DOMAIN)
        {
            try
            {
                bool result = false;
                string Message = string.Empty;

                LoginHelper Helper = new LoginHelper();
                ResultCheckADModels check_ad = Helper.CheckAuthorize(USER.ToUpper(), PASS, DOMAIN);

                string full_name = check_ad.FULL_NAME;
                string user_name = check_ad.USER_NAME;

                if (!string.IsNullOrEmpty(user_name))
                {
                    Session["FULL_NAME"] = full_name;
                    Session["USER_NAME"] = user_name;
                    result = true;
                    Message = "Successfully.";
                }
                else
                {
                    Session["FULL_NAME"] = null;
                    Session["USER_NAME"] = null;
                    result = false;
                    Message = "You don't have permission to access.";
                } // end if: EN IS NOT NULL

                return Json(new { Result = result, Message = Message });
            } // end try:

            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            } // end catch: Exception ex
        } // end JsonResult: CheckLogin

        public ActionResult Logout()
        {
            Session["FULL_NAME"] = null;
            Session["USER_NAME"] = null;

            return RedirectToAction("Index", "Login");
        } // end ActionResult: Logout
    } // end class: LoginController : Controller
} // end namespace: eRSN_New.Controllers