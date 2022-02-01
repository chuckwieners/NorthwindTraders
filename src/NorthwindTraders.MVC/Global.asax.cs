using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NorthwindTraders.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //for DI/IOC
            UnityConfig.RegisterComponents();
            //calling the unity regristration method so that all the registrations
            //can be configured.

            //with unity config configured and repository registrar implemented
            //you should  now be able to run the app and access the categories 
            //(controller/view)
        }
    }
}
