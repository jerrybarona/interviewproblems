using System;

namespace InterviewProblems.Facebook
{
    public class P024GenerateRandomMaxIndex
    {
        // https://leetcode.com/discuss/interview-question/451431/Facebook-or-Onsite-or-Generate-random-max-index
        private readonly Random _random = new Random();

        public void RandomMaxIndexTest()
        {
            var arr = new[] { 11, 30, 2, 30, 30, 30, 6, 2, 62, 62 };
            var idx = 5;
            Console.WriteLine($"arr: [{string.Join(',', arr)}]\nIndex: {idx}");
            for (var i = 0; i < 50; ++i) Console.Write($"{RandomMaxIndex(arr, 5)}, ");
        }

        public int RandomMaxIndex(int[] arr, int idx)
        {
            var count = 0;
            var max = int.MinValue;
            var ans = -1;

            for (var i = 0; i <= idx; ++i)
            {
                if (arr[i] > max)
                {
                    count = 1;
                    max = arr[i];
                    ans = i;
                }
                else if (arr[i] == max)
                {
                    ++count;
                    ans = _random.Next(0, count) == 0 ? i : ans;
                }
            }

            return ans;
        }
    }
}
