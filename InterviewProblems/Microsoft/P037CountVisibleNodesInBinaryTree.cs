using System;
using InterviewProblems.Utilities;

namespace InterviewProblems.Microsoft
{
    public class P037CountVisibleNodesInBinaryTree
    {
        // https://leetcode.com/discuss/interview-question/546703/Microsoft-or-OA-2020-or-Count-Visible-Nodes-in-Binary-Tree

        public void CountVisibleNodesTest()
        {
            var str = "5,3,10,20,21,1,null";
            Console.WriteLine($"\nInput: [{string.Join(',', str)}]");
            Console.WriteLine($"Output: {CountVisibleNodes(TreeNodeUtilities.GetTree(str))}");

            str = "-10,null,-15,null,-1";
            Console.WriteLine($"\nInput: [{string.Join(',', str)}]");
            Console.WriteLine($"Output: {CountVisibleNodes(TreeNodeUtilities.GetTree(str))}");
        }

        public int CountVisibleNodes(TreeNode root)
        {
            return Count(root, int.MinValue);

            int Count(TreeNode node, int max)
            {
                if (node == null) return 0;
                var result = node.val > max ? 1 : 0;
                max = Math.Max(max, node.val);
                return result + Count(node.left, max) + Count(node.right, max);
            }
        }
    }
}
