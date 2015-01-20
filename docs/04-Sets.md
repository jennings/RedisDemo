Sets
=====

Sets are like lists, except that set members can only be added once.

* [SADD][sadd] - Add a value to a set (adding an item that is already in the
  set has no effect)
* [SREM][srem] - Remove a value from a set
* [SMEMBERS][smembers] - Returns all members of the set

Click "Sets Demo" in the header to see sets in action. We have a very exclusive
club and we need to keep a list of all our club members. A person can't be a
member of the club twice, so a set ensures everyone is in the club at most
once.

    // SetsController.cs
    // Getting all our club members
    var members = database.SetMembers(KEY);

    // Adding a member
    database.SetAdd(KEY, value);

    // Removing a member
    database.SetRemove(KEY);


[set-cmd]: http://redis.io/commands#set
[sadd]: http://redis.io/commands/sadd
[srem]: http://redis.io/commands/srem
[smembers]: http://redis.io/commands/smembers
