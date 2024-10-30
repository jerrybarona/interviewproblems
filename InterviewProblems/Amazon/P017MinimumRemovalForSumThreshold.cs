using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Amazon
{
    internal class P017MinimumRemovalForSumThreshold : ITestable
    {
        // https://leetcode.com/discuss/interview-question/5470838/Amazon-or-Online-Assessment-or-SDE-2-or-2024

        public void RunTest()
        {
            foreach (var (prices, k, threshold) in new(int[], int, int)[]
            {
                (new [] { 1, 8 , 4, 5}, 2, 10), // 1
                (new [] { 3,2,1,4,6,5}, 3, 14), // 1
                (new [] { 1, 1, 1, 2, 1, 1, 1, 3, 9, 10, 15, 16, 7, 6, 5 }, 3, 15)
            })
            {
                Console.WriteLine(MinRemoval(prices, k, threshold));
                Console.WriteLine(MinRemoval2(prices, k, threshold));
            }
        }

        private int MinRemoval(int[] prices, int k, int threshold)
        {
            var treeMap = new SortedDictionary<int, int>();
            var sum = 0;
            var count = 0;
            var limit = prices.Length - k + 1;
            var result = 0;

            foreach (var price in prices)
            {
                if (result == limit)
                {
                    return result;
                }

                if (price > threshold)
                {
                    ++result;
                    continue;
                }

                if (count < k)
                {
                    if (sum + price > threshold)
                    {
                        ++result;
                        continue;
                    }
                    
                    if (!treeMap.ContainsKey(price)) treeMap.Add(price, 0);
                    treeMap[price]++;
                    sum += price;
                    ++count;
                    continue;
                }

                var low = treeMap.First().Key;
                if (price <= low)
                {
                    continue;
                }

                if (sum - low + price <= threshold)
                {
                    if (--treeMap[low] == 0) treeMap.Remove(low);
                    sum -= low;

                    if (!treeMap.ContainsKey(price)) treeMap.Add(price, 0);
                    treeMap[price]++;
                    sum += price;
                    continue;
                }

                var high = treeMap.Last().Key;
                if (price < high)
                {
                    if (--treeMap[high] == 0) treeMap.Remove(high);
                    sum -= high;

                    if (!treeMap.ContainsKey(price)) treeMap.Add(price, 0);
                    treeMap[price]++;
                    sum += price;
                }

                ++result;
            }

            return result;
        }

        private int MinRemoval2(int[] prices, int k, int threshold)
        {
            if (prices.Length < k) return 0;

            Array.Sort(prices, (a, b) => b - a);

            var sum = 0;
            var result = 0;
            for (var (s, e) = (0,0); e < prices.Length; ++e)
            {
                sum += prices[e];
                if (sum > threshold)
                {
                    sum -= prices[s++];                    
                    ++result;
                    if (prices.Length - s < k) break;
                    continue;
                }

                if (e - s + 1 == k)
                {
                    break;
                }
            }

            return result;
        }
    }
}
