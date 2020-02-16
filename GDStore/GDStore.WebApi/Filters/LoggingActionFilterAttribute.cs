using System;
using System.Text;
using System.Web.Mvc;
using log4net;
using WebGrease.Css.Extensions;

namespace GDStore.WebApi.Filters
{
    public class LoggingActionFilterAttribute : ActionFilterAttribute
    {
        private readonly ILog log = LogManager.GetLogger(typeof(LoggingActionFilterAttribute));
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log(filterContext);

            base.OnActionExecuting(filterContext);
        }

        private void Log(ActionExecutingContext filterContext)
        {
            var controllerName = filterContext.RouteData.Values["controller"];
            var actionName = filterContext.RouteData.Values["action"];

            var parameters = new StringBuilder(string.Empty);

            filterContext.ActionParameters.ForEach(parameter => parameters.Append($"{parameter.Key}={parameter.Value}, "));

            var message = $"{controllerName}Controller - {actionName} handler called with URL parameters: {parameters}";

            try
            {
                log.Info(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}