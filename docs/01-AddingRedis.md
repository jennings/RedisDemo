Adding Redis
=============

In production, you'd probably run Redis as a Windows service (or on Linux). For
this app, we're just going to run it as an app.

## Starting a Redis server

The NuGet package `redis-64` is an easy way to get it on our computer. We can
add it thorugh the Package Manager Console in Visual Studio:

    PM> Install-Package redis-64
    Installing 'Redis-64 2.8.17'.
    Successfully installed 'Redis-64 2.8.17'.


That was way easier than SQL Server.

To launch Redis, double-click the `start-redis.cmd` batch file in the root of
this repository. This is just a simple batch file which launches the Redis
server with the default configuration file.


## Connecting to Redis

To read from Redis, we'll use the excellent [StackExchange.Redis][seredis]
library.

    PM> Install-Package StackExchange.Redis
    ...
    Successfully added 'StackExchange.Redis 1.0.371' to RedisDemoApp.

And we'll start up a `ConnectionMultiplexer` in our `Application_Start`. This
is similar to a `SqlConnection`, but we can keep it around while our app runs
and it'll take care of the connection to Redis for us.


## Reading and writing

When the app starts, we'll store the current date/time in the "_appStarted_"
key. Then we'll display it in our hero unit:

    // In Global.asax.cs
    var redisConfiguration = ConfigurationManager.ConnectionStrings["Redis"].ConnectionString;
    MvcApplication.Redis = ConnectionMultiplexer.Connect(redisConfiguration);

    // In HomeController.cs
    IDatabase database = MvcApplication.Redis.GetDatabase();
    string value = database.StringGet("appStarted");

Taa-dah! The date is displayed on our welcome page!

**Notice these cool features:**

* Refreshing the page doesn't change the time.

* But, stopping IIS Express then relaunching it does change the value (because
  it runs `Application_Start` again).

* Stop the Redis server with Ctrl-C (don't use the X to close the window).
  Reload the page, notice the exception you get. Now relaunch Redis and reload
  again. The previous value of _appStarted_ is still available, and
  StackExchange.Redis dealt with reconnecting to the database for you.


[seredis]: https://github.com/StackExchange/StackExchange.Redis
