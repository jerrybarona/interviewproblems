using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0743NetworkDelayTime
    {
        public void NetworkDelayTimeTest()
        {
            var times = new[] { new[] { 2, 1, 1 }, new[] { 2, 3, 1 }, new[] { 3, 4, 1 } };
            Console.WriteLine(NetworkDelayTime(times, 4, 2));

            var times2 = new[]
            {
                new[] { 1, 2, 5 }, new[] { 1, 4, 9 }, new[] { 1, 5, 2 }, new[] { 2, 3, 2 }, new[] { 5, 6, 3 },
                new[] { 3, 4, 3 }, new[] { 6, 4, 2 }
            };
            Console.WriteLine(NetworkDelayTime(times2, 6, 1));
        }

        public int NetworkDelayTime(int[][] times, int N, int K)
        {
            var heap = new MinHeap(N);
            foreach (var key in Enumerable.Range(1, N)) heap.Add(key, int.MaxValue);
            heap.Decrease(K, 0);

            var graph = times.Aggregate(new Dictionary<int, List<Node>>(), (dict, time) =>
            {
                if (!dict.ContainsKey(time[0])) dict.Add(time[0], new List<Node>());
                dict[time[0]].Add(new Node(time[1], time[2]));
                return dict;
            });

            var dist = Enumerable.Repeat(int.MaxValue, N+1).ToArray();

            while (heap.Count > 0)
            {
                var curr = heap.Pop();
                var (currNode, currDist) = (curr.Key, curr.Value);
                dist[currNode] = currDist;

                if (!graph.ContainsKey(currNode)) continue;
                foreach (var neighbor in graph[currNode])
                {
                    if (!heap.ContainsKey(neighbor.Key)) continue;
                    var newDist = dist[currNode] + neighbor.Value;
                    if (newDist < heap.GetValue(neighbor.Key)) heap.Decrease(neighbor.Key, newDist);
                }
            }

            return dist.Select((val, idx) => (val, idx)).Where(x => x.idx > 0).Any(y => y.val == int.MaxValue)
                ? -1
                : dist.Select((val, idx) => (val, idx)).Where(x => x.idx > 0).Max(y => y.val);

        }
    }

        class MinHeap
        {
            private readonly Node[] _elements;
            private readonly Dictionary<int, int> _map;
            private int _size;

            public MinHeap(int capacity)
            {
                _elements = new Node[capacity];
                _map = new Dictionary<int, int>(capacity);
            }

            private int LeftChildIndex(int idx) => 2 * idx + 1;
            private int RightChildIndex(int idx) => 2 * idx + 2;
            private int ParentIndex(int idx) => (idx - 1) / 2;

            private bool HasLeftChild(int idx) => LeftChildIndex(idx) < _size;
            private bool HasRightChild(int idx) => RightChildIndex(idx) < _size;
            private bool IsRoot(int idx) => idx == 0;

            private Node LeftChild(int idx) => _elements[LeftChildIndex(idx)];
            private Node RightChild(int idx) => _elements[RightChildIndex(idx)];
            private Node Parent(int idx) => _elements[ParentIndex(idx)];

            public int Count => _size;

            public int Peek()
            {
                if (_size == 0) throw new IndexOutOfRangeException();
                return _elements[0].Value;
            }

            public Node Pop()
            {
                if (_size == 0) throw new IndexOutOfRangeException();
                _map.Remove(_elements[0].Key);
                var result = _elements[0];
                _elements[0] = _elements[--_size];
                _map[_elements[0].Key] = 0;
                BubbleDown(0);

                return result;
            }

            public bool ContainsKey(int key) => _map.ContainsKey(key);

            public int GetValue(int key) => _elements[_map[key]].Value;

            public void Add(int key, int value)
            {
                if (_size == _elements.Length) throw new IndexOutOfRangeException();
                _map.Add(key, _size);
                var node = new Node(key, value);
                _elements[_size++] = node;
                //_map[_elements[_size - 1].Key] = 0;
                BubbleUp(_size - 1);
            }

            public void Decrease(int key, int value)
            {
                _elements[_map[key]].Value = value;
                BubbleUp(_map[key]);
            }

            private void BubbleDown(int idx)
            {
                while (HasLeftChild(idx))
                {
                    var smallestIdx = HasRightChild(idx) && RightChild(idx).Value < LeftChild(idx).Value
                        ? RightChildIndex(idx)
                        : LeftChildIndex(idx);

                    if (_elements[idx].Value <= _elements[smallestIdx].Value) break;
                    (_map[_elements[idx].Key], _map[_elements[smallestIdx].Key]) = (smallestIdx, idx);
                    (_elements[idx], _elements[smallestIdx]) = (_elements[smallestIdx], _elements[idx]);
                    idx = smallestIdx;
                }
            }

            private void BubbleUp(int idx)
            {
                while (!IsRoot(idx) && _elements[idx].Value < Parent(idx).Value)
                {
                    var parentIdx = ParentIndex(idx);
                    (_map[_elements[idx].Key], _map[_elements[parentIdx].Key]) = (parentIdx, idx);
                    (_elements[idx], _elements[parentIdx]) = (_elements[parentIdx], _elements[idx]);
                    idx = parentIdx;
                }
            }
        }

        class Node
        {
            public int Key { get; }
            public int Value { get; set; }

            public Node(int key, int value)
            {
                Key = key;
                Value = value;
            }
        }
    
}
