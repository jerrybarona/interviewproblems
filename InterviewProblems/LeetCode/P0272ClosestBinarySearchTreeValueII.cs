using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P0272ClosestBinarySearchTreeValueII
    {
        public IList<int> ClosestKValues(TreeNode root, double target, int k)
        {
            var heap = new MaxHeap(k, target);
            var stack = new Stack<TreeNode>();
            Closest(root);
            while (stack.Count > 0)
            {
                var n = stack.Pop();
                Closest(n);
            }

            var result = new List<int>();
            while (heap.Count > 0) result.Add(heap.Pop().val);

            return result;

            void Closest(TreeNode node)
            {
                if (node == null) return;
                if (heap.Count == k)
                {
                    if (Diff(node) > Diff(heap.Peek())) return;
                    heap.Pop();
                }
                heap.Add(node);

                var moveToLeft = target <= (double)node.val;
                if (moveToLeft)
                {

                    Closest(node.left);
                    if (node.right != null && (heap.Count == 0 || Diff(node) <= Diff(heap.Peek()))) stack.Push(node.right);
                }
                else
                {

                    Closest(node.right);
                    if (node.left != null && (heap.Count == 0 || Diff(node) <= Diff(heap.Peek()))) stack.Push(node.left);
                }
            }

            double Diff(TreeNode n) => Math.Abs((double)n.val - target);
        }


        public class MaxHeap
        {
            private readonly TreeNode[] _elements;
            private int _size;
            private double _target;

            public MaxHeap(int size, double target)
            {
                _elements = new TreeNode[size];
                _target = target;
            }

            private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
            private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
            private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

            private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
            private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _size;
            private bool IsRoot(int elementIndex) => elementIndex == 0;

            private TreeNode GetLeftChild(int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
            private TreeNode GetRightChild(int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
            private TreeNode GetParent(int elementIndex) => _elements[GetParentIndex(elementIndex)];

            private void Swap(int firstIndex, int secondIndex)
            {
                var temp = _elements[firstIndex];
                _elements[firstIndex] = _elements[secondIndex];
                _elements[secondIndex] = temp;
            }

            public bool IsEmpty()
            {
                return _size == 0;
            }

            public int Count => _size;

            public TreeNode Peek()
            {
                if (_size == 0)
                    throw new IndexOutOfRangeException();

                return _elements[0];
            }

            public TreeNode Pop()
            {
                if (_size == 0)
                    throw new IndexOutOfRangeException();

                var result = _elements[0];
                _elements[0] = _elements[_size - 1];
                _size--;

                ReCalculateDown();

                return result;
            }

            public void Add(TreeNode element)
            {
                if (_size == _elements.Length)
                    throw new IndexOutOfRangeException();

                _elements[_size] = element;
                _size++;

                ReCalculateUp();
            }

            private void ReCalculateDown()
            {
                int index = 0;
                while (HasLeftChild(index))
                {
                    var biggerIndex = GetLeftChildIndex(index);
                    var rightDiff = Math.Abs((double)GetRightChild(index).val - _target);
                    var leftDiff = Math.Abs((double)GetLeftChild(index).val - _target);
                    if (HasRightChild(index) && rightDiff > leftDiff)
                    {
                        biggerIndex = GetRightChildIndex(index);
                    }

                    var bigDiff = Math.Abs((double)_elements[biggerIndex].val - _target);
                    var elemDiff = Math.Abs((double)_elements[index].val - _target);

                    if (bigDiff < elemDiff)
                    {
                        break;
                    }

                    Swap(biggerIndex, index);
                    index = biggerIndex;
                }
            }

            private void ReCalculateUp()
            {
                var index = _size - 1;
                var elemDiff = Math.Abs((double)_elements[index].val - _target);
                var parentDiff = Math.Abs((double)GetParent(index).val - _target);

                while (!IsRoot(index) && elemDiff > parentDiff)
                {
                    var parentIndex = GetParentIndex(index);
                    Swap(parentIndex, index);
                    index = parentIndex;
                }
            }
        }
    }
}
