Scalar Operations
==================

Redis is a _data structure server_, which means it has a boatload of handy data
structures it can work with natively:

* Text
* Numbers
* Lists
* Sets
* Sorted sets
* Hashes (dictionaries)
* HyperLogLog (which is just plain fun to say)


## Text

(Redis refers to all scalars as "strings", so I will say "text" when I mean
what C# would call a string)

The _appStarted_ key stored as text the date when our app started. We are
storing a simple text representation of the `DateTime`, since Redis doesn't
natively store dates. We parse it back into a DateTime when we read the value.
In addition to [GET][get] and [SET][set] (which StackExchange.Redis calls
`StringGet()` and `StringSet()`), there are many string commands we can use,
such as:

* `StringGetRange()` ([GETRANGE][getrange]) - Gets a substring
* `StringSetRange()` ([SETRANGE][setrange]) - Changes a substring of an existing key
* `StringAppend()` ([APPEND][append]) - Append text onto the end of a value


## Numbers

If you store a string in Redis that looks like an integer, you can treat it
like an integer. Let's make a page counter like every good website from the
90's:


    long counter = database.StringIncrement("welcomePageCount");

Every time we load the page, we [increment][incr] the _welcomePageCount_ key
and return the new value. We get back a long because Redis store 64-bit
integers.

Look at what's cool about this!

* We don't have to create the key on the first page load; if the key doesn't
  exist, Redis just starts value of 0.

* We don't have to query-change-update; a single command increments/creates the
  existing value and gives us the new value. Incrementing an integer is an
  atomic operation, so even if you have many browsers hitting the page
  simultaneously, there is no race condition and no locking.

Other handy commands for working with numbers:

* `StringDecrement()` ([DECR][decr]) - Decrement a value
* `StringIncrementBy()` ([INCRBY][incrby]) - Increment a value by a specified amount
* `StringIncrementBy()` ([INCRBYFLOAT][incrbyfloat]) - Increment a floating-point value by a specified amount


[string-cmd]: http://redis.io/commands#string
[get]: http://redis.io/commands/get
[set]: http://redis.io/commands/set
[getrange]: http://redis.io/commands/getrange
[setrange]: http://redis.io/commands/setrange
[append]: http://redis.io/commands/append
[incr]: http://redis.io/commands/incr
[decr]: http://redis.io/commands/decr
[incrby]: http://redis.io/commands/incrby
[incrbyfloat]: http://redis.io/commands/incrbyfloat
