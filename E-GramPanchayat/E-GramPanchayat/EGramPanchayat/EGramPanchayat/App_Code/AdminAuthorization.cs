using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EGramPanchayat.App_Code
{
    public class AdminAuthorization : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            bool isValidUser;
            if (httpContext.Session["aid"] == null)
            {
                isValidUser = false;
            }
            else
            {
                isValidUser = true;
            }
            return isValidUser;
        }



        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", Controller = "General" }));
        }
    }
}