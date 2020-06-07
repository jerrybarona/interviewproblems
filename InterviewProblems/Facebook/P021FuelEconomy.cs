using System;
using System.Collections.Generic;

namespace InterviewProblems.Facebook
{
    public class P021FuelEconomy
    {
        // https://leetcode.com/discuss/interview-question/603001/Facebook-or-Onsite-or-Fuel-Economy

        public void CheapestFuelCostTest()
        {
            Console.WriteLine(CheapestFuelCost(7, 7, 10, new[] { 5, 9 }, new[] { 2, 1 }));
            Console.WriteLine(CheapestFuelCost(3, 10, 17, new[] { 2, 5, 9, 10 }, new[] { 40, 7, 15, 12 }));
        }

        // b -> starting amount of fuel
        // k -> tank capacity
        public int CheapestFuelCost(int b, int k, int d, int[] distance, int[] price)
        {
            var heap = new MinHeap();
            var fuel = b;
            var pos = 0;
            var result = 0;
            var n = distance.Length;

            for (var i = 0; i < n; ++i)
            {
                fuel -= distance[i] - pos;

                while (fuel < 0 && heap.Count > 0)
                {
                    var top = heap.Peek();
                    var quant = Math.Min(top.LeftToRefuel, -fuel);
                    result += quant * top.Price;
                    top.LeftToRefuel -= quant;
                    if (top.LeftToRefuel == 0) heap.Pop();
                    fuel += quant;
                }

                if (fuel < 0) return -1;
                heap.Add(new HeapNode(price[i], k - fuel));
                pos = distance[i];
            }

            fuel -= d - pos;
            while (fuel < 0 && heap.Count > 0)
            {
                var top = heap.Peek();
                var quant = Math.Min(top.LeftToRefuel, -fuel);
                result += quant * top.Price;
                top.LeftToRefuel -= quant;
                if (top.LeftToRefuel == 0) heap.Pop();
                fuel += quant;
            }

            if (fuel < 0) return -1;
            return result;
        }

        class MinHeap
        {
            private readonly List<HeapNode> _elements = new List<HeapNode>();

            private int LeftChildIndex(int idx) => 2 * idx + 1;
            private int RightChildIndex(int idx) => 2 * idx + 2;
            private int ParentIndex(int idx) => (idx - 1) / 2;

            private bool HasLeftChild(int idx) => LeftChildIndex(idx) < _elements.Count;
            private bool HasRightChild(int idx) => RightChildIndex(idx) < _elements.Count;
            private bool IsRoot(int idx) => idx == 0;

            private HeapNode LeftChild(int idx) => _elements[LeftChildIndex(idx)];
            private HeapNode RightChild(int idx) => _elements[RightChildIndex(idx)];
            private HeapNode Parent(int idx) => _elements[ParentIndex(idx)];

            public int Count => _elements.Count;

            public HeapNode Peek()
            {
                return _elements[0];
            }

            public HeapNode Pop()
            {
                var top = _elements[0];
                _elements[0] = _elements[_elements.Count - 1];
                _elements.RemoveAt(_elements.Count - 1);
                BubbleDown();
                return top;
            }

            public void Add(HeapNode node)
            {
                _elements.Add(node);
                BubbleUp();
            }

            private void BubbleDown()
            {
                for (var idx = 0; HasLeftChild(idx);)
                {
                    var smallestIdx = HasRightChild(idx) && RightChild(idx).Price < LeftChild(idx).Price
                        ? RightChildIndex(idx)
                        : LeftChildIndex(idx);

                    if (_elements[idx].Price <= _elements[smallestIdx].Price) return;
                    (_elements[idx], _elements[smallestIdx]) = (_elements[smallestIdx], _elements[idx]);
                    idx = smallestIdx;
                }
            }

            private void BubbleUp()
            {
                for (var idx = _elements.Count - 1; !IsRoot(idx) && _elements[idx].Price < Parent(idx).Price;)
                {
                    var parentIdx = ParentIndex(idx);
                    (_elements[idx], _elements[parentIdx]) = (_elements[parentIdx], _elements[idx]);
                    idx = parentIdx;
                }
            }
        }

        class HeapNode
        {
            public int Price { get; set; }
            public int LeftToRefuel { get; set; }

            public HeapNode(int price, int leftToRefuel)
            {
                Price = price;
                LeftToRefuel = leftToRefuel;
            }
        }
    }
}
