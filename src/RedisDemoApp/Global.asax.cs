using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StackExchange.Redis;

namespace RedisDemoApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static ConnectionMultiplexer Redis;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Create our Redis multiplexer, which deals with the connection to Redis for us
            var redisConfiguration = ConfigurationManager.ConnectionStrings["Redis"].ConnectionString;
            Redis = ConnectionMultiplexer.Connect(redisConfiguration);

            // Set a value we'll use later
            var db = Redis.GetDatabase();
            db.StringSet("appStarted", DateTime.Now.ToString("u"));
        }
    }
}
