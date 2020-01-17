using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P0431EncodeNaryTreeToBinaryTree
    {
        public TreeNode encode(Node root)
        {
            if (root == null) return new TreeNode(int.MinValue) { right = new TreeNode(int.MinValue) };
            var node = new TreeNode(root.val);
            if (root.children == null || root.children.Count == 0)
            {
                node.right = root.children == null ? new TreeNode(int.MinValue) : new TreeNode(0);
                return node;
            }

            node.right = new TreeNode(root.children.Count);
            var runner = node;
            foreach (var child in root.children)
            {                
                node.left = new TreeNode(child.val);
                node.left.right = encode(child);
                node = node.left;
            }
            return runner;
        }

        // Decodes your binary tree to an n-ary tree.
        public Node decode(TreeNode root)
        {
            if (root.val == int.MinValue) return null;
            var node = new Node();
            node.val = root.val;

            if (root.right.val == int.MinValue) node.children = null;
            else if (root.right.val == 0) node.children = new List<Node>();
            else
            {
                node.children = new List<Node>(root.right.val);
                var runner = root.left;
                while (runner != null)
                {
                    node.children.Add(decode(runner.right));
                    runner = runner.left;
                }
            }

            return node;
        }

        public class Node
        {
            public int val;
            public IList<Node> children;

            public Node() { }
            public Node(int _val, IList<Node> _children)
            {
                val = _val;
                children = _children;
            }
        }

        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }
    }
}
