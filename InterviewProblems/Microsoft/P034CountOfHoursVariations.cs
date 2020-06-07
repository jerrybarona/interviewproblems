using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Microsoft
{
    public class P034CountOfHoursVariations
    {
        // https://leetcode.com/discuss/interview-question/645626/Microsoft-or-OA-2020-or-Count-Of-Hours-Variations

        public void CountVariationsTest()
        {
            var nums = new[] { 1, 0, 0, 2 };
            Console.WriteLine($"\nInput: [{string.Join(',', nums)}]");
            var (result, times) = CountVariations(nums);
            Console.WriteLine($"Ouput: {result} => {string.Join(' ', times)}");

            nums = new[] { 2, 1, 2, 1 };
            Console.WriteLine($"\nInput: [{string.Join(',', nums)}]");
            (result, times) = CountVariations(nums);
            Console.WriteLine($"Ouput: {result} => {string.Join(' ', times)}");

            nums = new[] { 1, 4, 8, 2 };
            Console.WriteLine($"\nInput: [{string.Join(',', nums)}]");
            (result, times) = CountVariations(nums);
            Console.WriteLine($"Ouput: {result} => {string.Join(' ', times)}");

            nums = new[] { 4, 4, 4, 4 };
            Console.WriteLine($"\nInput: [{string.Join(',', nums)}]");
            (result, times) = CountVariations(nums);
            Console.WriteLine($"Ouput: {result} => {string.Join(' ', times)}");
        }

        public (int count, List<string> times) CountVariations(int[] nums)
        {
            Array.Sort(nums);
            var map = nums.Aggregate(new Dictionary<int, int>(), (dict, num) =>
            {
                if (!dict.ContainsKey(num)) dict.Add(num, 0);
                ++dict[num];
                return dict;
            });
            var time = new int[4];
            var times = new List<string>();
            var result = 0;
            Permute();

            return (result, times);

            void Permute(int idx = 0)
            {
                if (idx == 4)
                {
                    ++result;
                    times.Add($"({time[0]}{time[1]}:{time[2]}{time[3]})");
                    return;
                }

                foreach (var key in map.Keys.ToList())
                {
                    if (map[key] > 0)
                    {
                        var hour = 10 * time[0] + time[1];
                        if (idx == 0 && key <= 2 ||
                            idx == 1 && time[0] <= 1 ||
                            idx == 1 && time[0] == 2 && key <= 4 ||
                            idx == 2 && hour == 24 && key == 0 ||
                            idx == 2 && hour != 24 && key <= 5 ||
                            idx == 3 && hour == 24 && key == 0 ||
                            idx == 3 && hour != 24)
                        {
                            --map[key];
                            time[idx] = key;
                            Permute(idx+1);
                            ++map[key];
                        }
                    }
                }
            }
        }
    }
}
