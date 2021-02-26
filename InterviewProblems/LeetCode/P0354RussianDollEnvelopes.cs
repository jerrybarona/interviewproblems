using System;
using System.Linq;

namespace InterviewProblems.LeetCode
{
	public class P0354RussianDollEnvelopes
    {
        public void MaxEnvelopesTest()
		{
			var inputs = new[] { 
                new[] { new[] { 133, 6606 }, new[] { 7765, 6180 }, new[] { 732, 786 }, new[] { 9824, 548 } },
                new[] { new[] { 5, 4 }, new[] { 6, 4 }, new[] { 6, 7 }, new[] { 2, 3 } } };


            foreach (var input in inputs) Console.WriteLine(MaxEnvelopes(input));
		}

        public int MaxEnvelopes(int[][] envelopes)
        {
            Array.Sort(envelopes, (a, b) =>
            {
                var xdiff = a[0].CompareTo(b[0]);
                if (xdiff != 0) return xdiff;
                else return a[1].CompareTo(b[1]);
            });
            var memo = Enumerable.Repeat(-1, envelopes.Length).ToArray();

            return Enumerable.Range(0, envelopes.Length).Max(x => Count(x));

            int Count(int idx)
            {
                if (idx == envelopes.Length - 1) return 1;
                if (memo[idx] != -1) return memo[idx];

                memo[idx] = 0;
                for (var (i, width, height) = (idx + 1, envelopes[idx][0], envelopes[idx][1]);
                                      i < envelopes.Length; ++i)
                {
                    var (w, h) = (envelopes[i][0], envelopes[i][1]);
                    if (width < w && height < h) memo[idx] = Math.Max(memo[idx], Count(i));
                }

                return ++memo[idx];
            }
        }
    }
}
