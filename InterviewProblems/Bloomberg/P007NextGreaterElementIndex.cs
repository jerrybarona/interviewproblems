using System;
using System.Linq;

namespace InterviewProblems.Bloomberg
{
    public class P007NextGreaterElementIndex
    {
        // https://leetcode.com/discuss/interview-question/688722/Bloomberg-or-Phone

        public void GreaterElementsTest()
        {
            var arr = new[] { 21, 5, 6, 56, 88, 52 };
            Console.WriteLine($"\nInput: [{string.Join(',', arr)}]");
            Console.WriteLine($"Output: [{string.Join(',', GreaterElements(arr))}]");
        }

        public int[] GreaterElements(int[] arr)
        {
            var result = Enumerable.Repeat(-1, arr.Length).ToArray();
            var maxIdx = -1;

            foreach (var (_, idx) in arr.Select((val, idx) => (val, idx)).OrderByDescending(x => x.val).ThenBy(y => y.idx))
            {
                if (maxIdx == -1)
                {
                    maxIdx = idx;
                    continue;
                }

                if (idx < maxIdx) result[idx] = maxIdx;
                else maxIdx = idx;
            }

            return result;
        }
    }
}
