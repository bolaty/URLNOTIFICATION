using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrlNotificationCinetPayPayIn
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Intouchtontinemobile",
               url: "Intouchtontinemobile/{action}/{id}",
               defaults: new { controller = "Intouchtontinemobile", action = "Index" }
            );
            routes.MapRoute(
              name: "ZenithMobileIntouch",
              url: "ZenithMobileIntouch/{action}/{id}",
              defaults: new { controller = "ZenithMobileIntouch", action = "Index" }
            );
            routes.MapRoute(
             name: "ZenithMobileCinetPay",
             url: "ZenithMobileCinetPay/{action}/{id}",
             defaults: new { controller = "ZenithMobileCinetPay", action = "Index" }
           );
            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }

        );

        }
    }
}
