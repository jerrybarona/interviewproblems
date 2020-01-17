using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P0895MaximumFrequencyStack
    {
        private readonly Dictionary<int, MaxFreqHeapNode> _dict;
        private readonly MaxFreqHeap _heap;
        private int _timestamp;

        public P0895MaximumFrequencyStack()
        {
            _dict = new Dictionary<int, MaxFreqHeapNode>();
            _heap = new MaxFreqHeap(10000);
        }

        public void Push(int x)
        {
            if (!_dict.ContainsKey(x))
            {
                var node = new MaxFreqHeapNode(x, _timestamp);
                _heap.Add(node);
                _dict.Add(x, node);
            }
            else
            {
                var node = _dict[x];
                node.PushOccurrence(_timestamp);
                _heap.BubbleUp(node.Index);
            }

            ++_timestamp;
        }

        public int Pop()
        {
            var node = _heap.Peek();
            var result = node.Number;
            node.RemoveClosestToTop();
            if (node.Freq == 0)
            {
                _heap.Pop();
                _dict.Remove(result);
            }
            else _heap.BubbleDown(0);

            return result;
        }
    }

    public class MaxFreqHeap
    {
        private readonly MaxFreqHeapNode[] _elements;
        private int _size;

        public MaxFreqHeap(int size)
        {
            _elements = new MaxFreqHeapNode[size];
        }

        private int LeftChildIndex(int idx) => 2 * idx + 1;
        private int RightChildIndex(int idx) => 2 * idx + 2;
        private int ParentIndex(int idx) => (idx - 1) / 2;

        private bool HasLeftChild(int idx) => LeftChildIndex(idx) < _size;
        private bool HasRightChild(int idx) => RightChildIndex(idx) < _size;
        private bool IsRoot(int idx) => idx == 0;

        private MaxFreqHeapNode LeftChild(int idx) => _elements[LeftChildIndex(idx)];
        private MaxFreqHeapNode RightChild(int idx) => _elements[RightChildIndex(idx)];
        private MaxFreqHeapNode Parent(int idx) => _elements[ParentIndex(idx)];


        public int Count => _size;

        public MaxFreqHeapNode Peek()
        {
            if (_size == 0) throw new IndexOutOfRangeException();
            return _elements[0];
        }

        public MaxFreqHeapNode Pop()
        {
            if (_size == 0) throw new IndexOutOfRangeException();
            var result = _elements[0];
            _elements[0] = _elements[--_size];
            BubbleDown(0);

            return result;
        }

        public void Add(MaxFreqHeapNode node)
        {
            if (_size == _elements.Length) throw new IndexOutOfRangeException();
            _elements[_size] = node;
            BubbleUp(_size++);
        }

        public void BubbleDown(int idx)
        {
            _elements[idx].Index = idx;

            while (HasLeftChild(idx))
            {
                var greaterIdx = HasRightChild(idx) && Compare(RightChild(idx), LeftChild(idx)) ? RightChildIndex(idx) : LeftChildIndex(idx);
                if (Compare(_elements[idx], _elements[greaterIdx])) break;

                (_elements[idx].Index, _elements[greaterIdx].Index) = (_elements[greaterIdx].Index, _elements[idx].Index);
                (_elements[idx], _elements[greaterIdx]) = (_elements[greaterIdx], _elements[idx]);

                idx = greaterIdx;
            }
        }

        public void BubbleUp(int idx)
        {
            _elements[idx].Index = idx;

            while (!IsRoot(idx) && Compare(_elements[idx], Parent(idx)))
            {
                var parentIdx = ParentIndex(idx);
                (_elements[idx].Index, _elements[parentIdx].Index) = (_elements[parentIdx].Index, _elements[idx].Index);
                (_elements[idx], _elements[parentIdx]) = (_elements[parentIdx], _elements[idx]);

                idx = parentIdx;
            }
        }

        private bool Compare(MaxFreqHeapNode a, MaxFreqHeapNode b)
        {
            if (a.Freq != b.Freq) return a.Freq > b.Freq;
            return a.ClosestToTop() > b.ClosestToTop();
        }
    }

    public class MaxFreqHeapNode
    {
        private readonly Stack<int> _timestamps;
        public int Number { get; }
        public int Index { get; set; } = -1;
        

        public MaxFreqHeapNode(int number, int timestamp)
        {
            Number = number;
            _timestamps = new Stack<int>(new[] { timestamp });
        }

        public int Freq => _timestamps.Count;
        public int ClosestToTop() => _timestamps.Peek();
        public int RemoveClosestToTop() => _timestamps.Pop();
        public void PushOccurrence(int timestamp) => _timestamps.Push(timestamp);
    }
}
