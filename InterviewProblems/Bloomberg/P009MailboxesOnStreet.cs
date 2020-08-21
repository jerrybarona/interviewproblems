using System;
using System.Linq;

namespace InterviewProblems.Bloomberg
{
    public class P009MailboxesOnStreet
    {
        // https://leetcode.com/discuss/interview-question/539629/Bloomberg-new-grad-SWE-on-site-question

        public void MailBoxesTest()
        {
            var houses = new[] { 5, 10, 15, 20 };
            var k = 3;
            Console.WriteLine($"\nHouses: [{string.Join(',', houses)}], k = {k}");
            Console.WriteLine($"Output: {MailBoxes(houses, k)}");

            houses = new[] { 6,7,8,12 };
            k = 2;
            Console.WriteLine($"\nHouses: [{string.Join(',', houses)}], k = {k}");
            Console.WriteLine($"Output: {MailBoxes(houses, k)}");

            houses = new[] { 3,5,6,8 };
            k = 1;
            Console.WriteLine($"\nHouses: [{string.Join(',', houses)}], k = {k}");
            Console.WriteLine($"Output: {MailBoxes(houses, k)}");

            houses = new[] { 1,4,8,10,20 };
            k = 3;
            Console.WriteLine($"\nHouses: [{string.Join(',', houses)}], k = {k}");
            Console.WriteLine($"Output: {MailBoxes(houses, k)}");
        }

        public int MailBoxes(int[] houses, int k)
        {
            if (k >= houses.Length) return 0;

            Array.Sort(houses);
            var len = houses.Length;

            var placed = new bool[len];
            var memo = Enumerable.Repeat(0, len).Select(x => Enumerable.Repeat(-1, k + 1).ToArray())
                .ToArray();

            return MinDist(0, k);

            int MinDist(int idx, int m)
            {
                if (m == 1) return DistOne();
                if (idx >= len) return 1000000007;
                if (memo[idx][m] > -1) return memo[idx][m];

                var result = 1000000007;
                for (var i = idx; i < len; ++i)
                {
                    placed[i] = true;
                    result = Math.Min(result, MinDist(i + 1, m - 1));
                    placed[i] = false;
                }

                memo[idx][m] = result;
                return result;
            }

            int DistOne()
            {
                var left = Enumerable.Range(0, len).Where(x => !placed[x]).Select(y => houses[y]).ToList();
                var median = left.Count % 2 == 1
                    ? left[left.Count / 2]
                    : (left[left.Count/2] + left[left.Count/2 - 1]) / 2;

                return left.Sum(x => Math.Abs(x - median));
            }
        }
    }
}
