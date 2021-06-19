using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;


namespace ATH.Common
{
    public class TrackExecutionTime : ActionFilterAttribute, IExceptionFilter
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string message = "\n" + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            message += " -> " + filterContext.ActionDescriptor.ActionName + " -> OnActionExecuting \t-";
            message += DateTime.Now.ToString()+ "\n";

            LogExecutionTime(message);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string message = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            message += " -> " + filterContext.ActionDescriptor.ActionName + " -> OnActionExecuted \t-";
            message += DateTime.Now.ToString() + "\n";

            LogExecutionTime(message);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            string message = filterContext.RouteData.Values["controller"].ToString();
            message += " -> " + filterContext.RouteData.Values["action"].ToString() + " -> OnResultExecuting \t-";
            message += DateTime.Now.ToString() + "\n";

            LogExecutionTime(message);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            string message = filterContext.RouteData.Values["controller"].ToString();
            message += " -> " + filterContext.RouteData.Values["action"].ToString() + " -> OnResultExecuted \t-";
            message += DateTime.Now.ToString() + "\n";

            LogExecutionTime(message);
            LogExecutionTime("---------------------------------------------------");
        }

        public void OnException(ExceptionContext filterContext)
        {
            string message = filterContext.RouteData.Values["controller"].ToString();
            message += " -> " + filterContext.RouteData.Values["action"].ToString() + " -> "+ filterContext.Exception.Message +" \t-";
            message += DateTime.Now.ToString() + "\n";

            LogExecutionTime(message);
            LogExecutionTime("---------------------------------------------------");
        }

        private void LogExecutionTime (string data)
        {
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/Data/Data.txt"), data);
        }

    }
}