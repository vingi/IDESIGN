using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCBase
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "NewsList", // 路由名称
                "News/List/{newstype}/{id}", // 带有参数的 URL
                new { controller = "News", action = "List", newstype = UrlParameter.Optional, id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "News", // 路由名称
                "News/{action}/{id}", // 带有参数的 URL
                new { controller = "News", action = "List", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "DesignerDetail", // 路由名称
                "Designer/Index/{id}", // 带有参数的 URL
                new { controller = "Designer", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}/{designertype}", // 带有参数的 URL
                new { controller = "Designer", action = "List", designertype = UrlParameter.Optional }
            );

            routes.MapRoute(
                "DesignerList", // 路由名称
                "Designer/List/{designertype}/{id}", // 带有参数的 URL
                new { controller = "Designer", action = "List", designertype = UrlParameter.Optional, id = UrlParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // 默认情况下对 Entity Framework 使用 LocalDB
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            log4net.Config.XmlConfigurator.Configure();
        }
    }
}