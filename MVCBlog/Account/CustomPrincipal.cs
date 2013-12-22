using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using Domain.Entities;

namespace MVCBlog.Account
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public CustomPrincipal(string login)
        {
            this.Identity = new GenericIdentity(login);
        }

        public bool IsInRole(string role)
        {
            string[] roles = role.Split(',');
            foreach (var r in roles)
            {
                if (r.Equals(info.Role)) { return true; }
            }
            return false;
        }
        
        public UserInfo info { get; set; }
    }
}