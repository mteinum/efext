using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace EfExt.Tests
{
    [TestFixture]
    public class RecursiveTreeExtFixture
    {
        class Node
        {
            public Node(int id) {
                Id = id;
            }

            public int Id { get; private set; }
            public readonly List<Node> Children = new List<Node>();

            public Node Add(Node node) {
                Children.Add(node);
                return this;
            }
        }

        private Node _tree;

        [TestFixtureSetUp]
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

            Assert.IsTrue(expected.SequenceEqual(secuence));
        }

        [Test]
        public void NodeWithoutChildren()
        {
            var noChildren = _tree.Recursive(n => n.Children)
                                  .Where(n => n.Children.Empty());

            Assert.AreEqual(4, noChildren.Count());
        }
    }
}
