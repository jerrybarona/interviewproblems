using System;
using System.Collections.Generic;

namespace InterviewProblems.LeetCode
{
    public class P0973KclosestPointsToOrigin
    {
        public int[][] KClosest(int[][] points, int K)
        {
            var heap = new MaxHeap(K);
            foreach (var point in points)
            {
                if (heap.Count < K) heap.Add(new Node(point));
                else
                {
                    var dist = point[0] * point[0] + point[1] * point[1];
                    if (dist >= heap.Peek().Dist) continue;
                    heap.Pop();
                    heap.Add(new Node(point));
                }
            }

            var result = new Stack<int[]>(K);
            while (heap.Count > 0) result.Push(heap.Pop().Point);

            return result.ToArray();
        }

        class MaxHeap
        {
            private readonly Node[] _elements;
            private int _size;

            public MaxHeap(int size) => _elements = new Node[size];

            public int Count => _size;

            private int LeftChildIndex(int idx) => 2 * idx + 1;
            private int RightChildIndex(int idx) => 2 * idx + 2;
            private int ParentIndex(int idx) => (idx - 1) / 2;

            private bool HasLeftChild(int idx) => LeftChildIndex(idx) < _size;
            private bool HasRightChild(int idx) => RightChildIndex(idx) < _size;
            private bool IsRoot(int idx) => idx == 0;

            private Node LeftChild(int idx) => _elements[LeftChildIndex(idx)];
            private Node RightChild(int idx) => _elements[RightChildIndex(idx)];
            private Node Parent(int idx) => _elements[ParentIndex(idx)];


            public Node Peek()
            {
                if (_size == 0) throw new IndexOutOfRangeException();
                return _elements[0];
            }

            public Node Pop()
            {
                if (_size == 0) throw new IndexOutOfRangeException();
                var result = _elements[0];
                _elements[0] = _elements[--_size];
                BubbleDown();
                return result;
            }

            public void Add(Node node)
            {
                if (_size == _elements.Length) throw new IndexOutOfRangeException();
                _elements[_size++] = node;
                BubbleUp();
            }

            private void BubbleDown()
            {
                for (var idx = 0; HasLeftChild(idx);)
                {
                    var greaterIdx = HasRightChild(idx) && RightChild(idx).Dist > LeftChild(idx).Dist
                        ? RightChildIndex(idx)
                        : LeftChildIndex(idx);

                    if (_elements[idx].Dist >= _elements[greaterIdx].Dist) break;
                    (_elements[idx], _elements[greaterIdx]) = (_elements[greaterIdx], _elements[idx]);
                    idx = greaterIdx;
                }
            }

            private void BubbleUp()
            {
                for (var idx = _size - 1; !IsRoot(idx) && _elements[idx].Dist > Parent(idx).Dist; )
                {
                    var parentIdx = ParentIndex(idx);
                    (_elements[idx], _elements[parentIdx]) = (_elements[parentIdx], _elements[idx]);
                    idx = parentIdx;
                }
            }
        }

        class Node
        {
            public int Dist { get; }
            public int[] Point { get; }

            public Node(int[] point)
            {
                Point = point;
                Dist = point[0] * point[0] + point[1] * point[1];
            }
        }
    }
}
