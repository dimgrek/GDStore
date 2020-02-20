using System.Web.Mvc;
using GDStore.WebApi.Filters;

namespace GDStore.WebApi
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
