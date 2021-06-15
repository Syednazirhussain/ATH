using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace ATH
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());

            filters.Add(new CustomAuthenticationFilter());
        }
    }

    public class CustomAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {

            if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) &&
                !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {

                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {

            bool firstCond = filterContext.Result == null;
            bool secondCond = filterContext.Result is HttpUnauthorizedResult;

            if (firstCond || secondCond)
            {

                if (filterContext.HttpContext.Request.Path.Contains("Admin"))
                {
                    filterContext.Result = new RedirectResult("~/Admin/Login/Index");
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/");
                }
            }
        }
    }


}
