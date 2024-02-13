using System;
using System.Collections.Generic;

namespace InterviewProblems.Facebook
{
    public class P034MaxNumberOfNephewGifts : ITestable
    {
        // https://leetcode.com/discuss/interview-question/356960
        public void RunTest()
        {
            foreach (var (day, cost) in new(int[], int[])[]
            {
                (new []{ 3, 5, 6 }, new []{ 2, 4, 3 }),
                (new []{ 1, 3, 5, 6, 7 }, new []{ 2, 2, 3, 2, 2 }),
                (new []{ 1, 50, 51, 52 }, new []{ 1, 40, 20, 20 })
            })
            {
                Console.WriteLine(MaxNumberOfNephewGifts(day, cost));
            }
        }
        
        public int MaxNumberOfNephewGifts(int[] day, int[] cost)
        {
            return MaxNum();

            int MaxNum(int idx = 0, int spent = 0)
            {
                if (idx == day.Length) return 0;
                int money = day[idx] - spent;

                bool canAffordToday = money >= cost[idx];
                var gifts = 0;

                if (canAffordToday)
                {
                    gifts = MaxNum(idx + 1, spent + cost[idx]) + 1;
                }

                return Math.Max(gifts, MaxNum(idx + 1, spent));
            }            
        }
    }
}
