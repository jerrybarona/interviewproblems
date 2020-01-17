using InterviewProblems.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0347TopkFrequentElements
    {
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
