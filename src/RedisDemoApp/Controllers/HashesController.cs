using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackExchange.Redis;

namespace RedisDemoApp.Controllers
{
    public class HashesController : Controller
    {
        const string NEXT_NUMBER = "users:nextNumber";
        const string USER_TO_ID = "users:userToId";
        const string USER_DATA_PREFIX = "users:data:";
        IDatabase database = MvcApplication.Redis.GetDatabase();

        public ActionResult Index()
        {
            var users = database.HashGetAll(USER_TO_ID);
            ViewBag.Users = users.ToDictionary();
            return View();
        }

        public JsonResult GetAge(int userId)
        {
            var age = (int)database.HashGet(USER_DATA_PREFIX + userId, "age");
            return Json(age, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(string username, int age)
        {
            // Get a new userId
            var userId = database.StringIncrement(NEXT_NUMBER);

            // Set the user fields
            var userFields = new HashEntry[] {
                new HashEntry("username", username),
                new HashEntry("age", age),
            };
            database.HashSet(USER_DATA_PREFIX + userId, userFields);

            // Create lookup indexes
            database.HashSet(USER_TO_ID, username, userId);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string userId)
        {
            var username = database.HashGet(USER_DATA_PREFIX + userId, "username");

            // Delete the user
            database.KeyDelete(USER_DATA_PREFIX + userId);

            // Remove the user from lookup indexes
            database.HashDelete(USER_TO_ID, username);

            return RedirectToAction("Index");
        }
    }
}