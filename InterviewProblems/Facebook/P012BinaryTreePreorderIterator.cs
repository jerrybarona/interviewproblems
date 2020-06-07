using System;
using InterviewProblems.Utilities;

namespace InterviewProblems.Facebook
{
    public class P012BinaryTreePreorderIterator
    {
        // https://leetcode.com/discuss/interview-question/632709/Facebook-Interview-Question

        public void BinaryTreePreorderIteratorTest()
        {
            var root = TreeNodeUtilities.GetTree("1,2,3,4,5,6,7,8,9,10");
            var iterator = new PreorderIterator(root);
            while (iterator.HasNext()) Console.Write($"{iterator.Next().val}, ");
        }

        public class PreorderIterator
        {
            private TreeNode _node;

            public PreorderIterator(TreeNode root)
            {
                _node = root;
            }

            public bool HasNext() => _node != null;

            public TreeNode Next()
            {
                while (_node.left != null)
                {
                    var pred = _node.left;
                    while (pred.right != null && pred.right != _node) pred = pred.right;
                    if (pred.right == null)
                    {
                        pred.right = _node;
                        _node = _node.left;
                        return pred.right;
                    }

                    pred.right = null;
                    _node = _node.right;
                }

                var result = _node;
                _node = _node.right;
                return result;
            }
        }
    }
}
