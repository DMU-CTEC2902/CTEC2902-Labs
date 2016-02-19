using System.Web;
using System.Web.Mvc;

namespace Week23_ModelsAndDatabases
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
