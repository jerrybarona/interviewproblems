using System;

namespace InterviewProblems.LeetCode
{
    public class P0045JumpGame2 : ITestable
    {
        public void RunTest()
        {
            foreach (var nums in new int[][]
            {
                new []{1,1,1,1},
                //new []{ 9, 4, 6, 7, 1, 4, 9, 0, 1, 9, 9, 9, 5, 0, 7, 7, 9, 2, 4, 6, 6, 7, 6, 3, 6, 1, 8, 5, 4, 3, 3, 3, 7, 1, 0, 8, 6, 1, 0, 7, 1, 1, 9, 6, 1, 6, 6, 3, 1, 2, 1, 7, 0, 8, 0, 6, 1, 9, 1, 6, 4, 5, 9, 1, 8, 0, 1, 4 }
            })
            {
                Console.WriteLine(Jump(nums));
            }
        }

        public int Jump(int[] nums)
        {
            var len = nums.Length;
            if (len == 1) return 0;

            var result = 0;
            for (var (start, range) = (0, nums[0]); ;)
            {
                ++result;
                if (start + range >= nums.Length - 1)
                {
                    break;
                }
                var nextLen = 0;
                var count = range;
                for (var i = start + 1; count > 0; --count, ++i)
                {
                    var x = i + nums[i];
                    if (x > nextLen)
                    {
                        nextLen = x;
                    }
                }

                start += range + 1;
                range = nextLen - start;
            }

            return result;
            //var result = 0;
            //for (var start = 0; ; ++result)
            //{
            //    if (start >= nums.Length - 1) break;
            //    if (start + nums[start] >= nums.Length - 1)
            //    {
            //        ++result;
            //        break;
            //    }
            //    var nextLen = 0;
            //    var count = nums[start];
            //    for (var i = start + 1; count > 0; --count, ++i)
            //    {
            //        var x = i + nums[i];
            //        if (x > nextLen)
            //        {
            //            nextLen = x;
            //            start = i;
            //        }
            //    }
            //}

            //return result;
        }
    }
}
