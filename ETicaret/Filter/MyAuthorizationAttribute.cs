using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.Filter
{
    public class MyAuthorizationAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public int ActionMemberType { get; private set; }
        public MyAuthorizationAttribute()
        {

        }
        public MyAuthorizationAttribute(int _memberType)
        {
            this.ActionMemberType = _memberType;
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var member = (DB.Members)HttpContext.Current.Session["LogonUser"];
            if (member == null)
            {
                filterContext.Result = new RedirectResult("/i/Index");
            }
            else
            {
                var memberType = (int)member.MemberType;
                //
                if (memberType < ActionMemberType)
                {
                    filterContext.Result = new RedirectResult("/i/Index");
                }             
            }
        }
    }
}