using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.Utilities
{
    public class Heap<T,U> where T : IHeapNode<U>
    {
        private readonly T[] _elements;
        private readonly Func<U,U, bool> _func;

        public Heap(int size, Func<U,U, bool> func)
        {
            _elements = new T[size];
            _func = func;
        }

        private int LeftChildIndex(int idx) => 2 * idx + 1;
        private int RightChildIndex(int idx) => 2 * idx + 2;
        private int ParentIndex(int idx) => (idx - 1) / 2;

        private bool HasLeftChild(int idx) => LeftChildIndex(idx) < Count;
        private bool HasRightChild(int idx) => RightChildIndex(idx) < Count;
        private bool IsRoot(int idx) => idx == 0;

        private T LeftChild(int idx) => _elements[LeftChildIndex(idx)];
        private T RightChild(int idx) => _elements[RightChildIndex(idx)];
        private T Parent(int idx) => _elements[ParentIndex(idx)];

        public int Count { get; private set; }

        public T Peek()
        {
            if (Count == 0) throw new IndexOutOfRangeException();
            return _elements[0];
        }

        public T Pop()
        {
            if (Count == 0) throw new IndexOutOfRangeException();
            var result = _elements[0];
            _elements[0] = _elements[Count - 1];
            --Count;
            BubbleDown();
            return result;
        }

        public void Add(T element)
        {
            if (Count == _elements.Length) throw new IndexOutOfRangeException();
            _elements[Count] = element;
            ++Count;
            BubbleUp();
        }

        private void BubbleDown()
        {            
            for (var idx = 0; HasLeftChild(idx); )
            {
                var nextIdx = HasRightChild(idx) && _func(RightChild(idx).Val, LeftChild(idx).Val) ? RightChildIndex(idx) : LeftChildIndex(idx);
                if (_func(_elements[idx].Val, _elements[nextIdx].Val)) break;
                (_elements[idx], _elements[nextIdx]) = (_elements[nextIdx], _elements[idx]);
                idx = nextIdx;
            }
        }

        private void BubbleUp()
        {
            for (var idx = Count - 1; !IsRoot(idx) && _func(_elements[idx].Val, Parent(idx).Val); )
            {
                var parentIdx = ParentIndex(idx);
                (_elements[idx], _elements[parentIdx]) = (_elements[parentIdx], _elements[idx]);
                idx = parentIdx;
            }
        }
    }

    public interface IHeapNode<U>
    {
        U Val { get; }
    }
}
