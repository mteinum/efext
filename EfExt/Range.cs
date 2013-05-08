using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EfExt
{
    public sealed class Range
    {
        public string From { get; set; }
        public string To { get; set; }

        public IEnumerable<string> Numbers
        {
            get
            {
                long start = Int64.Parse(From);
                long to = Int64.Parse(To);

                return Enumerable.Range(0, (int)(to - start) + 1)
                                 .Select(i => start + i)
                                 .Select(l => l.ToString(CultureInfo.InvariantCulture));
            }
        }

        public static IEnumerable<Range> Parse(string line)
        {
            return line.Split(',').Select(ParseRange);
        }

        private static Range ParseRange(string item)
        {
            var arr = item.Split('-');

            return new Range
            {
                From = arr[0],
                To = arr.Length == 2 ? Expand(arr[0], arr[1]) : arr[0]
            };
        }

        private static string Expand(string from, string to)
        {
            var numberOfCharToPrepend = from.Length - to.Length;

            if (numberOfCharToPrepend == 0)
                return to;

            return String.Format("{0}{1}", from.Substring(0, numberOfCharToPrepend), to);
        }

        public override string ToString()
        {
            return Compress();
        }

        public string Compress()
        {
            var start = FirstDifference();

            if (start == From.Length)
                return From;

            return String.Format("{0}-{1}", From, To.Substring(start));
        }

        private int FirstDifference()
        {
            int i;

            for (i = 0; i < From.Length; i++)
            {
                if (From[i] != To[i])
                    break;
            }

            return i;
        }
    }
}