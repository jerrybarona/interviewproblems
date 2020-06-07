using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P1439FindtheKthSmallestSumOfMatrixWithSortedRows
    {
        public void FindtheKthSmallestSumOfMatrixWithSortedRowsTest()
        {
            var mat = new[] { new[] { 1, 3, 11 }, new[] { 2, 4, 6 } };
            var k = 8;
            Console.WriteLine(KthSmallest(mat, k));
        }

        public int KthSmallest(int[][] mat, int k)
        {
            var (m, n) = (mat.Length, mat[0].Length);

            var arr = Enumerable.Repeat(0, m + 1).ToList();
            arr[m] = mat.Sum(row => row[0]);
            var heap = new MinHeap(new[] { arr }, (a, b) => a[a.Count - 1] < b[b.Count - 1]);

            var result = 0;
            while (heap.Count > 0 && --k > 0)
            {
                var popped = heap.Pop();
                result = popped[m];
                Console.WriteLine(result);
                for (var i = 0; i < m; ++i)
                {
                    if (i == 0 || popped[i - 1] == 0)
                    {
                        var next = new List<int>(popped);
                        if (next[i] < n - 1) ++next[i];
                        else continue;
                        next[m] = next[m] - mat[i][next[i] - 1] + mat[i][next[i]];
                        heap.Add(next);
                    }
                    else break;
                }
            }

            return heap.Count > 0 ? heap.Peek()[m] : result;
        }

        public class MinHeap
        {
            private readonly Func<List<int>, List<int>, bool> _func;
            private readonly List<List<int>> _elements = new List<List<int>>();

            public MinHeap(IEnumerable<List<int>> elements, Func<List<int>, List<int>, bool> func)
            {
                _func = func;
                foreach (var element in elements) Add(element);
            }

            private int LeftChildIndex(int idx) => 2 * idx + 1;
            private int RightChildIndex(int idx) => 2 * idx + 2;
            private int ParentIndex(int idx) => (idx - 1) / 2;

            private bool HasLeftChild(int idx) => LeftChildIndex(idx) < _elements.Count;
            private bool HasRightChild(int idx) => RightChildIndex(idx) < _elements.Count;
            private bool IsRoot(int idx) => idx == 0;

            private List<int> LeftChild(int idx) => _elements[LeftChildIndex(idx)];
            private List<int> RightChild(int idx) => _elements[RightChildIndex(idx)];
            private List<int> Parent(int idx) => _elements[ParentIndex(idx)];

            public int Count => _elements.Count;

            public List<int> Peek()
            {
                return _elements[0];
            }

            public List<int> Pop()
            {
                var result = _elements[0];
                _elements[0] = _elements[_elements.Count - 1];
                _elements.RemoveAt(_elements.Count - 1);
                BubbleDown();

                return result;
            }

            public void Add(List<int> element)
            {
                _elements.Add(element);
                BubbleUp();
            }

            public void BubbleDown()
            {
                for (var idx = 0; HasLeftChild(idx);)
                {
                    var smallestIdx = HasRightChild(idx) && _func(RightChild(idx), LeftChild(idx))
                        ? RightChildIndex(idx)
                        : LeftChildIndex(idx);

                    if (_func(_elements[idx], _elements[smallestIdx])) return;
                    (_elements[idx], _elements[smallestIdx]) = (_elements[smallestIdx], _elements[idx]);
                    idx = smallestIdx;
                }
            }

            public void BubbleUp()
            {
                for (var idx = _elements.Count - 1; !IsRoot(idx) && _func(_elements[idx], Parent(idx));)
                {
                    var parentIdx = ParentIndex(idx);
                    (_elements[idx], _elements[parentIdx]) = (_elements[parentIdx], _elements[idx]);
                    idx = parentIdx;
                }
            }
        }
    }
}
