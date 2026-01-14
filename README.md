efext
=====

[![Build](https://github.com/mteinum/efext/actions/workflows/build.yml/badge.svg)](https://github.com/mteinum/efext/actions/workflows/build.yml)

Entity Framework Extensions

.NET library with utility extensions for between operations.

### Installation

Install the package from nuget

https://nuget.org/packages/efext/

and add the using directive

```c#
using EfExt;
```
### GreaterThan

```c#
public static IQueryable<TSource> GreaterThan<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, string>> keySelector,
            string value)
```

Returns a subset where a given string column is greater than the given argument

### GreaterThanOrEqual

Same as GreaterThan but inclusive

### LessThan

Opposite of GreaterThan

### LessThanOrEqual

Opposite of GreaterThanOrEqual

### Between

Between is the composite of GreaterThanOrEqual and LessThanOrEqual

There are two overloads for the Between method

1. Find rows where a single column is between two strings

```c#
var subset = ctx.Numbers.Between(i => i.Number, "40401002", "40401004");
```

alternative

```c#
var subset = ctx.Numbers.Between("40401002", i => i.Number, "40401004");
```

2. Find rows where a string is between two columns

```c#
var plan = ctx.NumberPlans.Between(
                    r => r.LowerNumber,
                    r => r.UpperNumber,
                    "40410003");
```

alternative

```c#
var plan = ctx.NumberPlans.Between(
                    r => r.LowerNumber,
                    "40410003",
                    r => r.UpperNumber);
```

Linq
----

### Recursive

```c#
        public static IEnumerable<T> Recursive<T>(
            this T node, Func<T, IEnumerable<T>> selector)
```

This method can be used for traversing a tree structure. It will extend the node type, and the provided
selector is used for finding the children, that must be of the same type as node (T).

#### Example

```c#
        [Test]
        public void NodeWithoutChildren()
        {
            var noChildren = _tree.Recursive(n => n.Children)
                                  .Where(n => n.Children.Empty());

            Assert.AreEqual(4, noChildren.Count());
        }
```

How to sort a id/parent object by it's hierarchy

```c#
[Test]
public void WithLookupFunction()
{
	var items = new[]
	{
		new Item(2, 1),
		new Item(3, 1),
		new Item(4, 2),
		new Item(1, 1),
		new Item(5, 1),
		new Item(6, 4)
	};

	var root = items.Single(x => x.Id == 1);

	var sortedByTree = root.Recursive(items.ChildSelector).ToList();

	var expected = new[] { 1, 2, 4, 6, 3, 5 };

	Assert.IsTrue(expected.SequenceEqual(sortedByTree.Select(i => i.Id)));
}

public class Item
{
	public int Id;
	public int Parent;

	public Item() { }
	public Item(int id, int parent)
	{
		Id = id;
		Parent = parent;
	}
}

public static class ItemExt
{
	public static IEnumerable<Item> ChildSelector(
		this IEnumerable<Item> source,
		Item item)
	{
		return source.Where(i => i.Parent == item.Id && i.Parent != i.Id);
	}
}
```

See the testproject for more examples.

https://twitter.com/mteinum
