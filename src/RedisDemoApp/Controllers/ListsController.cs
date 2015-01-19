using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackExchange.Redis;

namespace RedisDemoApp.Controllers
{
    public class ListsController : Controller
    {
        const string KEY = "workItems";
        IDatabase database = MvcApplication.Redis.GetDatabase();

        public ActionResult Index()
        {
            RedisValue[] items = database.ListRange(KEY, 0, -1);
            ViewBag.WorkItems = items.Select(x => (string)x).ToList();
            return View();
        }

        public ActionResult Push(string value)
        {
            long newLength = database.ListLeftPush(KEY, value);
            return RedirectToAction("Index");
        }

        public ActionResult Pop()
        {
            string popped = database.ListRightPop(KEY);
            TempData["popped"] = popped ?? "(nil)";
            return RedirectToAction("Index");
        }
    }
}