using System;
using System.Collections.Generic;
using System.Text;
using InterviewProblems.Utilities;

namespace InterviewProblems.Facebook
{
    public class P026TreeIterator
    {
        // https://leetcode.com/discuss/interview-question/371618/Facebook-or-Onsite-or-Tree-Iterator

        // Follow-up 2

        public class TreeIterator
        {
            private TreeNode _head;
            private TreeNode _tail;
            private TreeNode _node;

            public TreeIterator(TreeNode root)
            {
                Traverse(root);
                _head.left = null;
                _tail.right = null;
                _node = _head;

                void Traverse(TreeNode node)
                {
                    if (node == null) return;
                    Traverse(node.left);
                    if (_tail != null)
                    {
                        _tail.right = node;
                        node.left = _tail;
                        
                    }
                    else _head = node;
                    _tail = node;

                    Traverse(node.right);
                }
            }

            public bool HasPrev() => _node != _head ;
            public bool HasNext() => _node != null;

            public int Next()
            {
                var val = _node.val;
                _node = _node.right;
                return val;
            }


        }
    }
}
