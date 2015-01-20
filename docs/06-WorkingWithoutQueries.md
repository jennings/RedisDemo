Working without queries
========================

Redis is a non-relational data store, and lacks much of the querying capability
of SQL databases.

For example, in a user database, a SQL database will deal with:

1. Storing all the fields that describe a particular user
2. Providing a unique, incrementing userId with each new user
3. Allow looking up users by username rather than userId

Item 1 is easily done with [Redis hashes][hashes-cmd]. But, how do we deal
without auto-incrementing ID fields and lookups by arbitrary fields?

Well, auto-incrementing IDs is easily done with a single key that stores an
integer. Each time we need a new user, we [INCR][incr] the key and get back a
new number. We use this as the userId.

    INCR users:nextUserId
    > 34

We'll store this user's data under the key "_users:data:34_":

    INCR users:data:34  username frederick  age 50

When a user signs in, we have "frederick", and we somehow need to find his
userId.

We need to create an "index" to help us look up userIds by username. So let's
make a hash that stores that:

    HSET users:userToId frederick 34

So if we have the username "frederick", we can use "`HGET users:userToId
frederick`" to get back his userId.


## Creating a User

This is the set of Redis commands to create a new user:

    INCR  users:nextNumber
    > 35
    HMSET users:data:35  username joe  age 40
    HSET  users:idToUser 35 joe
    HSET  users:userToId joe 35

[hashes-cmd]: http://redis.io/commands#hash
[incr]: http://redis.io/commands/incr
