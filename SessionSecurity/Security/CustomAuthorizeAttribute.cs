﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SessionSecurity.Models;

namespace SessionSecurity.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (string.IsNullOrEmpty(SessionPersister.Username))
            {
                filterContext.Result =
                    new RedirectToRouteResult(new RouteValueDictionary(new {Controller = "Account", action = "Index"}));
            }
            else
            {
                var accountModel = new AccountModel();
                var customPrincipal = new CustomPrincipal(accountModel.Find(SessionPersister.Username));
                if (!customPrincipal.IsInRole(Roles))
                {
                    filterContext.Result =
                        new RedirectToRouteResult(
                            new RouteValueDictionary(new {controller = "AccessDenied", action = "Index"}));
                }
            }
        }
    }
}