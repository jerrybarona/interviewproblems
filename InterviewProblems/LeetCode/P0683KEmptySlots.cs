using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0683KEmptySlots
    {
        public void KEmptySlotsTest()
        {
            var bulbs = new[] { 3, 9, 2, 8, 1, 6, 10, 5, 4, 7 };
            var k = 1;
            Console.WriteLine(KEmptySlots(bulbs,k));
        }

        public int KEmptySlots(int[] bulbs, int K)
        {
            var days = bulbs.Select((bulb, day0) => (bulb, day0)).Aggregate(new int[bulbs.Length + 1], (arr, x) =>
            {
                arr[x.bulb] = x.day0 + 1;
                return arr;
            });

            var window = new LinkedList<int>();

            for (var i = 0; i < K; ++i)
            {
                while (window.Count > 0 && days[2 + i] <= days[window.Last.Value]) window.RemoveLast();
                window.AddLast(2 + i);
            }

            var left = 1;
            var right = left + K + 1;
            var result = int.MaxValue;

            for (; right < days.Length; ++left, ++right)
            {
                if (window.Count > 0 && left == window.First.Value) window.RemoveFirst();
                var min = window.Count > 0 ? days[window.First.Value] : int.MaxValue;
                if (days[left] < min && days[right] < min) result = Math.Min(result, Math.Max(days[left], days[right]));
                while (window.Count > 0 && days[right] <= days[window.Last.Value]) window.RemoveLast();
                window.AddLast(right);
            }

            return result == int.MaxValue ? -1 : result;
        }
    }
}
