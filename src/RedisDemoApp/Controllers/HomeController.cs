using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackExchange.Redis;

namespace RedisDemoApp.Controllers
{
    public class HomeController : Controller
    {
        IDatabase database = MvcApplication.Redis.GetDatabase();

        public ActionResult Index()
        {
            string value = database.StringGet("appStarted");
            ViewBag.AppStarted = DateTime.Parse(value);
            return View();
        }
    }
}