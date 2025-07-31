using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EGramPanchayat.App_Code
{
    public class UserAuthorrizeSession : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isValidUser;
            if (httpContext.Session["bid"] == null)
            {
                isValidUser = false;
            }
            else
            {
                isValidUser = true;
            }
            return isValidUser;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filtercontext)
        {
            filtercontext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Action = "Login", Controller = "General" }));
        }
    }
   
}