using System;
using System.Linq;

namespace InterviewProblems.Oracle
{
    public class P007SortCharsInStringByFrequency
    {
        public void SortCharsInStringByFrequencyTest()
        {
            var str = "free";
            Console.WriteLine($"{str} -> {SortCharsInStringByFrequency(str)}");
        }

        public string SortCharsInStringByFrequency(string str)
        {
            var map = str.Aggregate(new int[26], (arr, chr) =>
            {
                ++arr[chr - 'a'];
                return arr;
            });

            return string.Concat(map
                .Select((val, idx) => (val, idx))
                .Where(x => x.val > 0)
                .OrderByDescending(y => y.val)
                .SelectMany(z => Enumerable.Repeat((char)(z.idx + 'a'), z.val)));
        }
    }
}
