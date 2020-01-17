using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Google
{
    public class P010CompareStrings
    {
        public int[] CompareStrings(string a, string b)
        {
            var freqsA = new int[11];
            var smallest = (char)('z' + 1);
            var freq = 0;
            for (var (i, len) = (0, a.Length); i <= len; ++i)
            {
                if (i < len && char.IsLower(a[i]))
                {
                    if (a[i] == smallest) ++freq;
                    else if (a[i] < smallest)
                    {
                        smallest = a[i];
                        freq = 1;
                    }
                }
                else if (freq > 0)
                {
                    ++freqsA[freq];
                    freq = 0;
                    smallest = (char)('z' + 1);
                }
            }

            smallest = (char)('z' + 1);
            freq = 0;

            var result = new List<int>();
            for (var (i, len) = (0, b.Length); i <= len; ++i)
            {
                if (i < len && char.IsLower(b[i]))
                {
                    if (b[i] == smallest) ++freq;
                    else if (b[i] < smallest)
                    {
                        smallest = b[i];
                        freq = 1;
                    }
                }
                else if (freq > 0)
                {
                    result.Add(freqsA.Select((val, idx) => (val, idx)).Where(x => x.idx > 0 && x.idx < freq)
                        .Sum(y => y.val));
                    freq = 0;
                    smallest = (char)('z' + 1);
                }
            }

            return result.ToArray();
        }
    }
}
