// @mteinum

using System.Linq;
using NUnit.Framework;

namespace EfExt.Tests
{
    [TestFixture]
    public class ExtFixture
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
    }
}
