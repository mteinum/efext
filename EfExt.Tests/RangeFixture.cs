using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace EfExt.Tests
{
    [TestFixture]
    public class RangeFixture
    {
        [Test]
        public void GetRangesSingle()
        {
            var ranges = Range.Parse("80085364").ToList();

            ClassicAssert.AreEqual(1, ranges.Count);
            ClassicAssert.AreEqual("80085364", ranges[0].From);
            ClassicAssert.AreEqual("80085364", ranges[0].To);
        }

        [Test]
        public void GetRangesTwoSingle()
        {
            var ranges = Range.Parse("80085364,80085365").ToList();

            ClassicAssert.AreEqual(2, ranges.Count);
            ClassicAssert.AreEqual("80085364", ranges[0].From);
            ClassicAssert.AreEqual("80085364", ranges[0].To);

            ClassicAssert.AreEqual("80085365", ranges[1].From);
            ClassicAssert.AreEqual("80085365", ranges[1].To);
        }

        [Test]
        public void GetRangesSingleRange()
        {
            var ranges = Range.Parse("80085364,80085365-80085366").ToList();

            ClassicAssert.AreEqual(2, ranges.Count);
            ClassicAssert.AreEqual("80085364", ranges[0].From);
            ClassicAssert.AreEqual("80085364", ranges[0].To);

            ClassicAssert.AreEqual("80085365", ranges[1].From);
            ClassicAssert.AreEqual("80085366", ranges[1].To);
        }

        [Test]
        public void Compress0()
        {
            var r = new Range { From = "40401000", To = "40401000" };
            ClassicAssert.AreEqual("40401000", r.Compress());
            ClassicAssert.AreEqual(1, r.Numbers.Count());
            ClassicAssert.AreEqual("40401000", r.Numbers.First());
        }

        [Test]
        public void Compress10()
        {
            var r = new Range { From = "40401000", To = "40401009" };
            ClassicAssert.AreEqual("40401000-9", r.Compress());
        }

        [Test]
        public void Compress50()
        {
            var r = new Range { From = "40401000", To = "40401049" };
            ClassicAssert.AreEqual("40401000-49", r.Compress());
        }

        [Test]
        public void Compress1000()
        {
            var r = new Range { From = "40401000", To = "40401999" };
            ClassicAssert.AreEqual("40401000-999", r.Compress());
        }

        [Test]
        public void CompressDifferentRange()
        {
            var r = new Range { From = "40000000", To = "50000000" };
            ClassicAssert.AreEqual("40000000-50000000", r.Compress());
        }

        [Test]
        public void ParseCompressed100()
        {
            var r = Range.Parse("40401000-99").First();

            ClassicAssert.AreEqual("40401000", r.From);
            ClassicAssert.AreEqual("40401099", r.To);

            ClassicAssert.AreEqual(100, r.Numbers.Count());
            ClassicAssert.AreEqual("40401000", r.Numbers.First());
            ClassicAssert.AreEqual("40401099", r.Numbers.Last());
        }
    }
}
