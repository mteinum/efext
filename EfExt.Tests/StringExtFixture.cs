// @mteinum

using System.Linq;
using NUnit.Framework;

namespace EfExt.Tests
{
    [TestFixture]
    public class StringExtFixture
    {
        [Test]
        public void KeyTest()
        {
            using (var ctx = new TestContext())
            {
                var subset = ctx.Numbers.Between(i => i.Number, "40401002", "40401004");

                Assert.AreEqual(3, subset.Count());
            }
        }

        [Test]
        public void KeyTestAlternative()
        {
            using (var ctx = new TestContext())
            {
                var subset = ctx.Numbers.Between("40401002", i => i.Number, "40401004");

                Assert.AreEqual(3, subset.Count());
            }
        }

        [Test]
        public void GreaterThanTest()
        {
            using (var ctx = new TestContext())
            {
                var subset = ctx.Numbers.GreaterThan(m => m.Number, "40401002");

                Assert.AreEqual(3, subset.Count());
            }
        }

        [Test]
        public void GreaterThanOrEqual()
        {
            using (var ctx = new TestContext())
            {
                var subset = ctx.Numbers.GreaterThanOrEqual(m => m.Number, "40401002");

                Assert.AreEqual(4, subset.Count());
            }
        }

        [Test]
        public void LessThan()
        {
            using (var ctx = new TestContext())
            {
                var subset = ctx.Numbers.LessThan(m => m.Number, "40401002");

                Assert.AreEqual(2, subset.Count());
            }
        }

        [Test]
        public void LessThanOrEqual()
        {
            using (var ctx = new TestContext())
            {
                var subset = ctx.Numbers.LessThanOrEqual(m => m.Number, "40401002");

                Assert.AreEqual(3, subset.Count());
            }
        }

        [Test]
        public void FaxTest()
        {
            using (var ctx = new TestContext())
            {
                var subset = ctx.Numbers.Between(i => i.Fax, "2000", "2003");

                Assert.AreEqual(4, subset.Count());
            }
        }

        [Test]
        public void PlanTest()
        {
            using (var ctx = new TestContext())
            {
                var plan = ctx.NumberPlans.Between(
                    r => r.LowerNumber,
                    r => r.UpperNumber,
                    "40410003").Single();

                Assert.AreEqual(2, plan.OperatorId);
            }
        }

        [Test]
        public void PlanTestAlternative()
        {
            using (var ctx = new TestContext())
            {
                var plan = ctx.NumberPlans.Between(
                    r => r.LowerNumber,
                    "40410003",
                    r => r.UpperNumber).Single();

                Assert.AreEqual(2, plan.OperatorId);
            }
        }

    }
}
