using System;

namespace InterviewProblems.Facebook
{
    public class P013PeekOrValleyProblem
    {
        // https://leetcode.com/discuss/interview-question/473801/Facebook-or-Phone-Screen-or-Peak-or-Valley-Problem

        public void FindPeekOrValleyTest()
        {
            var arr = new[] { 3, 2, 1, 0, 1 };
            Console.WriteLine($"[{string.Join(',', arr)}] ===> {FindPeekOrValley(arr)}");

            arr = new[] { 4, 5, 6, 7, 8, 9, 8, 7 };
            Console.WriteLine($"[{string.Join(',', arr)}] ===> {FindPeekOrValley(arr)}");
        }

        public int FindPeekOrValley(int[] arr)
        {
            for (var (start, end, mid) = (1, arr.Length - 2, 1 + (arr.Length - 3) / 2); start <= end; mid = start + (end - start) / 2)
            {
                if (IsPeekOrValley(mid)) return mid;
                if (mid - start == Math.Abs(arr[mid] - arr[start])) start = mid + 1;
                else end = mid - 1;
            }

            return int.MaxValue;

            bool IsPeekOrValley(int i) =>
                arr[i] > arr[i - 1] && arr[i] > arr[i + 1] || arr[i] < arr[i - 1] && arr[i] < arr[i + 1];

        }
    }
}
