Lists
=====

A Redis list is similar to a `List<T>()` in C#. [List commands][list-cmd]
include:

* [LPUSH][lpush] - Prepend a value to a list (push it on the left)
* [RPUSH][rpush] - Append a value to a list (push it on the right)
* [LPOP][lpop] - Pop the first element of a list
* [RPOP][rpop] - Pop the last element of a list
* [LSET][lset] - Set a particular index of a list to a value

Because you can push/pop from either the left or right ends of a list, you can
also use them as queues (FIFO) or stacks (LIFO). You might have two processes:

* A producer who uses LPUSH to push work items into a list
* A consumer who uses RPOP to get and remove the oldest work item

ListsController shows this pattern. Click "Lists Demo" in the header to see it
in action.

    // ListsController.cs
    // Viewing the list
    database.ListRange(KEY, 0, -1);

    // Adding a work item
    long newLength = database.ListLeftPush(KEY, value);

    // Completing a work item
    string value = database.ListRightPop(KEY);


[list-cmd]: http://redis.io/commands#list
[lpush]: http://redis.io/commands/lpush
[rpush]: http://redis.io/commands/rpush
[lpop]: http://redis.io/commands/lpop
[rpop]: http://redis.io/commands/rpop
[lset]: http://redis.io/commands/lset
