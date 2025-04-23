using System.Web;
using System.Web.Mvc;

namespace Examen_3_ServWeb_Natillera
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
