using System.Web.Mvc;
using GDStore.MVC.Filters;

namespace GDStore.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LoggingActionFilterAttribute());
        }
    }
}
