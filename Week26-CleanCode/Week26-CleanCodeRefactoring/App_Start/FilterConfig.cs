using System.Web;
using System.Web.Mvc;

namespace Week26_CleanCodeRefactoring
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
