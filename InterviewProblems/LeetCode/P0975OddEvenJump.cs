using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P0975OddEvenJump
    {
        public void OddEvenJumpTest()
        {
            var input1 = new[] { 10, 13, 12, 14, 15 };
            var input2 = new[] { 2, 3, 1, 1, 4 };
            var input3 = new[] { 5, 1, 3, 4, 2 };

            Console.WriteLine($"Input: [{string.Join(',', input1)}] Result: {OddEvenJumps(input1)}");
            Console.WriteLine($"Input: [{string.Join(',', input2)}] Result: {OddEvenJumps(input2)}");
            Console.WriteLine($"Input: [{string.Join(',', input3)}] Result: {OddEvenJumps(input3)}");
        }

        public int OddEvenJumps(int[] A)
        {
            var asc = GetNexts(true);
            var des = GetNexts(false);

            return Enumerable.Range(0, A.Length).Count(x => CanReach(x, true));

            bool CanReach(int idx, bool isOdd)
            {
                if (idx == A.Length - 1) return true;
                if (idx == -1) return false;
                var arr = isOdd ? asc : des;
                return CanReach(arr[idx], !isOdd);
            }

            int[] GetNexts(bool isAscending)
            {
                var idxs = isAscending
                    ? A.Select((val, idx) => (val, idx)).OrderBy(x => x.val).Select(y => y.idx)
                    : A.Select((val, idx) => (val, idx)).OrderByDescending(x => x.val).Select(y => y.idx);

                var arr = Enumerable.Repeat(-1, A.Length).ToArray();
                var stack = new Stack<int>();

                foreach (var idx in idxs)
                {
                    while (stack.Count > 0 && idx > stack.Peek()) arr[stack.Pop()] = idx;
                    stack.Push(idx);
                }

                return arr;
            }
        }
    }
}
