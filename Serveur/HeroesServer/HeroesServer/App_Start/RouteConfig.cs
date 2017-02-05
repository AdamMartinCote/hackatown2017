using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HeroesServer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Controllers with Actions
            // To handle routes like `/api/VTRouting/route`
            routes.MapRoute(
                name: "ControllerAndAction",
                url: "api/{controller}/{action}"
            );
            routes.MapRoute(
               name: "ActionApi",
               url: "api/{controller}/{action}/{id}",
               defaults: new { id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
