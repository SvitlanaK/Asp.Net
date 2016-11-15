using System;
using System.Collections.Generic;
using System.Linq;
using Market.Domain.Entities;
using Market.WEB.UI.Binders;

using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Market.Domain.Concrete;
using System.Data.Entity;

namespace Market.WEB.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
            Database.SetInitializer<EFDbContext>(null);
        }
    }
}
