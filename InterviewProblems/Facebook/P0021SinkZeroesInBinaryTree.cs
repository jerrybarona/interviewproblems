using System;
using System.Collections.Generic;
using InterviewProblems.Utilities;

namespace InterviewProblems.Facebook
{
    public class P0021SinkZeroesInBinaryTree
    {
        // https://leetcode.com/discuss/interview-question/506608/Sink-zeroes-in-a-Binary-Tree

        public void SinkZeroesTest()
        {
            var tree = "0,1,0,null,null,0,2,3,4,null,null";
            Console.WriteLine($"Input: [{tree}]");
            var root = TreeNodeUtilities.GetTree(tree);
            SinkZeroesInBinaryTree(root);
            Console.WriteLine($"Output: [{TreeNodeUtilities.GetSequence(root)}]");
        }

        public void SinkZeroesInBinaryTree(TreeNode root)
        {
            var stack = new Stack<int>();
            var zeroCount = 0;

            Sink(root);

            void Sink(TreeNode node)
            {
                if (node == null) return;
                if (node.val == 0) ++zeroCount;
                else stack.Push(node.val);

                Sink(node.left);
                Sink(node.right);

                if (zeroCount > 0)
                {
                    node.val = 0;
                    --zeroCount;
                }
                else node.val = stack.Pop();
            }
        }
    }
}
