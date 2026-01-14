using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace EfExt.Tests
{
    [TestFixture]
    public class RecursiveTreeExtFixture
    {
        class Node
        {
            public Node(int id)
            {
                Id = id;
            }

            public int Id { get; private set; }
            public readonly List<Node> Children = new List<Node>();

            public Node Add(Node node)
            {
                Children.Add(node);
                return this;
            }
        }

        private Node _tree;

        [OneTimeSetUp]
        public void SetUp()
        {
            _tree = new Node(1)
                        .Add(new Node(2))
                        .Add(new Node(3)
                            .Add(new Node(4))
                            .Add(new Node(5)))
                        .Add(new Node(6));
        }

        [Test]
        public void CorrectSequence()
        {
            var secuence = _tree.Recursive(n => n.Children)
                                .Select(n => n.Id);

            var expected = new[] { 1, 2, 3, 4, 5, 6 };

            ClassicAssert.IsTrue(expected.SequenceEqual(secuence));
        }

        [Test]
        public void NodeWithoutChildren()
        {
            var noChildren = _tree.Recursive(n => n.Children)
                                  .Where(n => n.Children.Empty());

            ClassicAssert.AreEqual(4, noChildren.Count());
        }


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

            ClassicAssert.IsTrue(expected.SequenceEqual(sortedByTree.Select(i => i.Id)));
        }
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
}
