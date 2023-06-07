using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eRSN_New.Filter
{
    public class Auth : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["USER_NAME"] == null)
            {
                if (HttpContext.Current.Request.Url != null)
                {
                    HttpContext.Current.Session["Redirect"] = HttpContext.Current.Request.Url;
                } // end if: url is not null
                string loginpath = "~/Login/Index";
                filterContext.Result = new RedirectResult(loginpath);
            } // end if: USER_NAME is null
        } // end override void: OnActionExecuting
    } // end class: Auth
} // end namespace: eRSN_New.Filter