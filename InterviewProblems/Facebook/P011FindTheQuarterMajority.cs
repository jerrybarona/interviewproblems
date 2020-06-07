using System;
using System.Linq;

namespace InterviewProblems.Facebook
{
    public class P011FindTheQuarterMajority
    {
        // https://leetcode.com/discuss/interview-question/627483/Facebook-or-Phone-or-Find-the-Quarter-Majority

        public void FindTheQuarterMajorityTest()
        {
            var arr = new[] { 1, 1, 2, 2, 3, 3, 3, 4, 5, 6, 7 };
            Console.WriteLine($"Input: [{string.Join(',', arr)}]; Output: {FindTheQuarterMajority(arr)}");

            arr = new[] { 1, 1, 2, 2, 3, 3, 4, 5, 7, 7, 7 };
            Console.WriteLine($"Input: [{string.Join(',', arr)}]; Output: {FindTheQuarterMajority(arr)}");

            arr = new[] { 1, 1, 2, 3 };
            Console.WriteLine($"Input: [{string.Join(',', arr)}]; Output: {FindTheQuarterMajority(arr)}");
        }

        public int FindTheQuarterMajority(int[] arr)
        {
            var winsize = arr.Length / 4 + 1;
            return Enumerable.Range(0, 4)
                .Where(z => z*winsize < arr.Length)
                .Select(x => FirstOccurrence(arr[x * winsize]))
                .Where(y => arr[y] == arr[y + winsize - 1])
                .Select(a => arr[a])
                .DefaultIfEmpty(-1)
                .First();

            int FirstOccurrence(int target)
            {
                var result = -1;
                for (var (start, end, mid) = (0, arr.Length - 1, (arr.Length - 1) / 2);
                     start <= end;
                     mid = start + (end - start) / 2)
                {
                    if (target > arr[mid]) start = mid + 1;
                    else
                    {
                        if (target == arr[mid]) result = mid;
                        end = mid - 1;
                    }
                }

                return result;
            }
        }
    }
}
