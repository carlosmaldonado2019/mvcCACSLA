using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mvcCACSLA.Models;
using System.Web.Mvc;

namespace mvcCACSLA.Security
{
    public class AuthorizeRolesAttribute: AuthorizeAttribute
    {
        private readonly string[] userAssignedRoles;
        public AuthorizeRolesAttribute(params string[] roles)
        {
            this.userAssignedRoles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            using (DB_9F6318_fcacacslav2Entities1 db = new DB_9F6318_fcacacslav2Entities1())
            {
                UserManager UM = new UserManager();
                foreach (var roles in userAssignedRoles)
                {
                    authorize = UM.IsUserInRole(httpContext.User.Identity.Name, roles);
                    if (authorize)
                        return authorize;
                }
            }
            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Home/NoAutorizado");
        }

    }
}