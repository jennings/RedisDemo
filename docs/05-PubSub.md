Pub/Sub
========

Redis also has a [publish/subscribe system][pubsub]. Some Redis clients can publish
messages and other clients and subscribe to those messages, and Redis handles
delivery.

The basic [Pub/Sub commands][pubsub-cmd]:

* [PUBLISH][publish] - Publish a message to a channel
* [SUBSCRIBE][subscribe] - Subscribe to a channel

Any client connected to the Redis server can subscribe or publish to a channel.

Channels are identified with keys (strings), but they're totally separate from
keys used for data storage. You can store an integer with the key "frederick"
and a channel "frederick" and they don't interfere with each other in any way.

    // PubSubController.cs

    // Subscribing to a channel
    ISubscriber = MvcApplication.Redis.GetSubscriber();
    subscriber.Subscribe(CHANNEL, ReceiverCallback);

    // Publishing to a channel
    subscriber.Publish(CHANNEL, message);

Channels are not reliable delivery mechanisms. If no clients are subscribed to
a channel, messages will not be delivered or saved. If you need reliable
delivery, you can create this by storing the messages in a list, then using
pub/sub to notify consumers that work is available.

Publishers add work to the queue then notify any consumers:

    database.ListLeftPush("workItems", "Some work to do");
    subscriber.Publish("newWorkItem", 1);

Consumers listen for notifications, and check once at startup:

    subscriber.Subscribe("newWorkItem", (ch, val) => ConsumeWork());
    ConsumeWork();

    void ConsumeWork() {
        while (true) {
            var item = database.ListRightPop("workItems");
            if (item.IsNull) break;
            DoWork(item);
        }
    }


[pubsub]: http://redis.io/topics/pubsub
[pubsub-cmd]: http://redis.io/commands#pubsub
[publish]: http://redis.io/commands/publish
[subscribe]: http://redis.io/commands/subscribe
