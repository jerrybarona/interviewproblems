using System;
using System.Collections.Generic;
using System.Linq;
using InterviewProblems.Utilities;

namespace InterviewProblems.Facebook
{
    public class P002SinkZeroesInBinaryTree
    {
        // https://leetcode.com/discuss/interview-question/506608/Sink-zeroes-in-a-Binary-Tree

        public void SinkZeroesTest()
        {
            var root = TreeNodeUtilities.GetTree("100,null,101,102,1,null,2,103,104,105,3,4,null,106,5,107,null,null,108,6,null,7,null,null,null,109,8,9,null,110,null,111,10,11,null,112,113,12,null,null,null,114,13,14,15");
            TreeNodeUtilities.PrintTree(root);
            Console.WriteLine();
            Console.WriteLine(TreeNodeUtilities.GetSequence(root));
            SinkZeroes2(root);
            Console.WriteLine();
            Console.WriteLine("Sinking...");
            TreeNodeUtilities.PrintTree(root);
            Console.WriteLine();
            Console.WriteLine(TreeNodeUtilities.GetSequence(root));
        }

        public void SinkZeroes2(TreeNode root)
        {
            if (root == null) return;
            SinkZeroes2(root.left);
            SinkZeroes2(root.right);

            if (root.val >= 100) Sink(root);

            void Sink(TreeNode node)
            {
                if (node == null) return;
                var child = node.left != null && node.left.val < 100 ? node.left : node.right != null && node.right.val < 100 ? node.right : null;
                if (child == null) return;
                (node.val, child.val) = (child.val, node.val);
                Sink(child);
            }
        }

        public void SinkZeroes(TreeNode root)
        {
            if (root == null) return;
            var zeros = new HashSet<TreeNode>();

            IsZeroWellPlaced(root);
            Traverse(root);

            bool IsZeroWellPlaced(TreeNode node)
            {
                if (node == null) return true;
                var left = IsZeroWellPlaced(node.left);
                var right = IsZeroWellPlaced(node.right);

                if (node.val >= 100)
                {
                    if (left && right) return true;
                    zeros.Add(node);
                }
                return false;
            }

            void Traverse(TreeNode node)
            {
                //if (stack.Count == 0) return;
                if (node.left != null) Traverse(node.left);
                if (node.right != null) Traverse(node.right);
                if (node.val >= 100 || zeros.Count == 0)
                {
                    if (zeros.Count == 0) zeros.Remove(node);
                    return;
                }
                var popped = zeros.First();
                zeros.Remove(popped);
                (popped.val, node.val) = (node.val, popped.val);
            }
        }
    }
}
