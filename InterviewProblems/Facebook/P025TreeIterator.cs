using System;
using System.Collections.Generic;
using InterviewProblems.Utilities;

namespace InterviewProblems.Facebook
{
    public class P025TreeIterator
    {
        // https://leetcode.com/discuss/interview-question/371618/Facebook-or-Onsite-or-Tree-Iterator

        // Folow-up 1

        public void TreeIteratorTest()
        {
            var tree = TreeNodeUtilities.GetTree("7,3,15,null,null,9,20");
            var it = new TreeIterator(tree);
            Console.WriteLine($"hasNext() => {it.HasNext()}");
            Console.WriteLine($"next()    => {it.Next()}");
            Console.WriteLine($"next()    => {it.Next()}");
            Console.WriteLine($"next()    => {it.Next()}");
            Console.WriteLine($"next()    => {it.Next()}");
            Console.WriteLine($"hasPrev() => {it.HasPrev()}");
            Console.WriteLine($"prev()    => {it.Prev()}");
            Console.WriteLine($"hasPrev() => {it.HasPrev()}");
            Console.WriteLine($"next()    => {it.Next()}");
            Console.WriteLine($"next()    => {it.Next()}");
            Console.WriteLine($"hasNext() => {it.HasNext()}");
            Console.WriteLine($"hasPrev() => {it.HasPrev()}");
            Console.WriteLine($"prev()    => {it.Prev()}");
            Console.WriteLine($"hasNext() => {it.HasNext()}");
            Console.WriteLine($"next()    => {it.Next()}");
        }

        public class TreeIterator
        {
            private TreeNode _node;
            private readonly LinkedList<int> _list = new LinkedList<int>();
            private int? _next;
            private bool _hasPrev = true;

            public TreeIterator(TreeNode root)
            {
                _node = root;
            }

            public bool HasNext() => _node != null || _next.HasValue;

            public bool HasPrev() => _list.Count == 2 && _hasPrev;

            public int Prev()
            {
                _hasPrev = false;
                _next = _list.Last.Value;
                return _list.First.Value;
            }

            public int Next()
            {
                _hasPrev = true;
                if (_next != null)
                {
                    var val = _next.Value;
                    _next = null;
                    return val;
                }
                while (_node.left != null)
                {
                    var pred = _node.left;
                    while (pred.right != null && pred.right != _node) pred = pred.right;
                    if (pred.right == null)
                    {
                        pred.right = _node;
                        _node = _node.left;
                        continue;
                    }

                    pred.right = null;
                    break;
                }

                var result = _node.val;
                _node = _node.right;
                if (_list.Count == 2) _list.RemoveFirst();
                _list.AddLast(result);
                return result;
            }
        }
    }
}
