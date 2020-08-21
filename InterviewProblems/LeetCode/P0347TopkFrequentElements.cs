using System;
using InterviewProblems.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0347TopkFrequentElements
    {
        public void TopKFrequentTest()
        {
            var nums = new[] { 10, 10, 10, 20, 20, 30 };
            var k = 2;
            Console.WriteLine($"[{string.Join(',', TopKfrequent2(nums, k))}]");

            nums = new[] { 5, -3, 9, 1, 7, 7, 9, 10, 2, 2, 10, 10, 3, -1, 3, 7, -9, -1, 3, 3 };
            k = 3;
            Console.WriteLine($"[{string.Join(',', TopKfrequent2(nums, k))}]");
        }

        public IList<int> TopKFrequent(int[] nums, int k)
        {
            var heap = new Heap<HeapNode, int>(k, (a, b) => a < b);
            var nodes = nums
                .Aggregate(new Dictionary<int, int>(nums.Length),
                                    (dict, num) =>
                                    {
                                        if (!dict.ContainsKey(num)) dict.Add(num, 0);
                                        ++dict[num];
                                        return dict;
                                    })
                .Select(item => new HeapNode(item.Value, item.Key));

            foreach (var node in nodes)
            {
                if (heap.Count < k) heap.Add(node);
                else if (node.Val > heap.Peek().Val)
                {
                    heap.Pop();
                    heap.Add(node);
                }
            }

            var result = new List<int>(k);
            while (heap.Count > 0) result.Add(heap.Pop().Num);

            return result;
        }

        private int[] TopKfrequent2(int[] nums, int k)
        {
            var map = nums.Aggregate(new Dictionary<int, int>(), (dict, num) =>
            {
                if (!dict.ContainsKey(num)) dict.Add(num, 0);
                dict[num]++;
                return dict;
            }).Select(kvp => (freq: kvp.Value, val: kvp.Key)).ToList();

            if (map.Count == k) return map.Select(x => x.val).ToArray();

            var i = Sort(0, map.Count - 1);
            for (var (s, e) = (0, map.Count - 1); s <= e && i != k; i = Sort(s, e))
            {
                if (i == k) break;
                if (i < k) s = i;
                else e = i - 1;
            }

            return map.Take(i).Select(x => x.val).ToArray();

            int Sort(int start, int end)
            {
                var idx = start;
                var p = map[idx].freq;
                (map[start], map[end]) = (map[end], map[start]);
                for (var j = start; j < end; ++j)
                {
                    if (map[j].freq > p)
                    {
                        (map[idx], map[j]) = (map[j], map[idx]);
                        ++idx;
                    }
                }
                (map[end], map[idx]) = (map[idx], map[end]);
                return idx;
            }
        }

        public class HeapNode : IHeapNode<int>
        {
            public int Val { get; }
            public int Num { get; }

            public HeapNode(int val, int num)
            {
                Val = val;
                Num = num;
            }
        }

    }
}
