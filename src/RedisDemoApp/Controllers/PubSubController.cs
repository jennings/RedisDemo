using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using RedisDemoApp.Hubs;
using StackExchange.Redis;

namespace RedisDemoApp.Controllers
{
    public class PubSubController : Controller
    {
        const string CHANNEL = "myChannel";
        ISubscriber subscriber = MvcApplication.Redis.GetSubscriber();

        public ActionResult Index()
        {
            return View();
        }

        public void Publish(string message)
        {
            subscriber.Publish(CHANNEL, message);
        }

        public void Subscribe()
        {
            subscriber.Subscribe(CHANNEL, Receive);

            hubContext.Clients.All.Receive("Subscribed");
        }

        public void Unsubscribe()
        {
            subscriber.Unsubscribe(CHANNEL);

            hubContext.Clients.All.Receive("Unsubscribed");
        }

        public static void Receive(RedisChannel channel, RedisValue message)
        {
            hubContext.Clients.All.Receive("FROM REDIS: " + message);
        }

        static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
    }
}