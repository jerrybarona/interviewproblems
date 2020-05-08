using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Facebook
{
    public class P001KthMinAcrossMultipleLists
    {
        // https://leetcode.com/discuss/interview-experience/454890/Facebook-or-Android-SWE-or-NYC-or-Nov-2019-Reject

        public void KthSmallestTest()
        {
            var l = new IList<int>[] { new[] { 1, 2, 3 }, new[] { 4, 5, 7 }, new[] { 0, 6, 8, 9 } };
            Console.WriteLine(KthSmallest(l, 1));
        }

        public int KthSmallest(IList<IList<int>> lists, int k)
        {
            var linked = lists.Select(x => new LinkedList<int>(x)).ToList();
            var heap = new MinHeap(lists.Count, linked.Select(l => l.First));

            for (var i = 0; i < k-1; ++i)
            {
                var n = heap.Pop();
                n = n.Next;
                if (n != null) heap.Add(n);
            }

            return heap.Peek().Value;
        }

        public class MinHeap
        {
            private int _size;
            private readonly LinkedListNode<int>[] _elements;


            public MinHeap(int capacity)
            {
                _elements = new LinkedListNode<int>[capacity];
            }

            public MinHeap(int capacity, IEnumerable<LinkedListNode<int>> nodes)
            {
                _elements = new LinkedListNode<int>[capacity];
                foreach (var node in nodes) Add(node);
            }

            private int LeftChildIndex(int idx) => 2 * idx + 1;
            private int RightChildIndex(int idx) => 2 * idx + 2;
            private int ParentIndex(int idx) => (idx - 1) / 2;

            private bool HasLeftChild(int idx) => LeftChildIndex(idx) < _size;
            private bool HasRightChild(int idx) => RightChildIndex(idx) < _size;
            private bool IsRoot(int idx) => idx == 0;

            private LinkedListNode<int> LeftChild(int idx) => _elements[LeftChildIndex(idx)];
            private LinkedListNode<int> RightChild(int idx) => _elements[RightChildIndex(idx)];
            private LinkedListNode<int> Parent(int idx) => _elements[ParentIndex(idx)];

            public LinkedListNode<int> Peek()
            {
                if (_size == 0) throw new ArgumentOutOfRangeException();
                return _elements[0];
            }

            public LinkedListNode<int> Pop()
            {
                if (_size == 0) throw new ArgumentOutOfRangeException();
                var result = _elements[0];

                _elements[0] = _elements[--_size];
                BubbleDown();
                return result;
            }

            public void Add(LinkedListNode<int> element)
            {
                if (_size == _elements.Length) throw new ArgumentOutOfRangeException();

                _elements[_size++] = element;
                BubbleUp();
            }

            private void BubbleUp()
            {
                for (var idx = _size - 1; !IsRoot(idx) && _elements[idx].Value < Parent(idx).Value;)
                {
                    var parentIdx = ParentIndex(idx);
                    (_elements[idx], _elements[parentIdx]) = (_elements[parentIdx], _elements[idx]);
                    idx = parentIdx;
                }
            }

            private void BubbleDown()
            {
                for (var idx = 0; HasLeftChild(idx);)
                {
                    var smallestIdx = HasRightChild(idx) && RightChild(idx).Value < LeftChild(idx).Value
                        ? RightChildIndex(idx)
                        : LeftChildIndex(idx);

                    if (_elements[idx].Value <= _elements[smallestIdx].Value) return;
                    (_elements[idx], _elements[smallestIdx]) = (_elements[smallestIdx], _elements[idx]);
                    idx = smallestIdx;
                }
            }
        }
    }
}
