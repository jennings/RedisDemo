using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackExchange.Redis;

namespace RedisDemoApp.Controllers
{
    public class SetsController : Controller
    {
        const string KEY = "clubMembers";
        IDatabase database = MvcApplication.Redis.GetDatabase();

        public ActionResult Index()
        {
            RedisValue[] members = database.SetMembers(KEY);

            ViewBag.ClubMembers = members.Select(x => (string)x).ToList();
            return View();
        }

        public ActionResult Join(string value)
        {
            database.SetAdd(KEY, value);
            return RedirectToAction("Index");
        }

        public ActionResult Leave(string value)
        {
            database.SetRemove(KEY, value);
            return RedirectToAction("Index");
        }
    }
}