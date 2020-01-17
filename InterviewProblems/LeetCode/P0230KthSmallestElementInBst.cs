using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P0230KthSmallestElementInBst
    {
        public int KthSmallest(TreeNode root, int k)
        {
            var tree = new Tree(root);
            return tree.KthSmallest(k);
        }

        class Tree
        {
            private Node _root;

            public Tree(TreeNode root)
            {
                Clone(root, _root);

                void Clone(TreeNode source, Node cloned)
                {
                    if (source == null) return;
                    cloned = new Node(source);
                    Clone(source.left, cloned.Left);
                    Clone(source.right, cloned.Right);
                    cloned.Count = 1 + (cloned.Left?.Count ?? 0) + (cloned.Right?.Count ?? 0);
                    //return cloned;
                    //Console.Write($"{target.Val}-{target.Count}, ");
                }
            }

            public int KthSmallest(int num)
            {
                return Kth(num, _root);

                int Kth(int k, Node node)
                {
                    if (node == null) return -1;

                    if (k <= node.Left.Count) return Kth(k, node.Left);
                    if (k == node.Left.Count + 1) return node.Val;
                    return Kth(k - node.Left.Count - 1, node.Right);
                }
            }
        }

        class Node
        {
            public int Val { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Count { get; set; }

            public Node(int val)
            {
                Val = val;
            }

            public Node(TreeNode node)
            {
                Val = node.val;
            }
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
