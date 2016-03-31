using System.Web;
using System.Web.Mvc;

namespace Week27_DependencyInjection
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
