efext
=====

Entity Framework Extensions

.NET library with utility extensions for between operations.

Install the package from nuget

https://nuget.org/packages/efext/

and add the using directive

```c#
using EfExt;
```


There are two overloads for the Between method

1. Find rows where a single column is between two strings

```c#
var subset = ctx.Numbers.Between(i => i.Number, "40401002", "40401004");
```

2. Find rows where a string is between two columns

```c#
var plan = ctx.NumberPlans.Between(
                    r => r.LowerNumber,
                    r => r.UpperNumber,
                    "40410003");
```
