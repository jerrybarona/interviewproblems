using System.Collections.Generic;

using InterviewProblems.Utilities;

namespace InterviewProblems.Facebook
{
    internal class P038SinkZerosInBinaryTree : ITestable
    {
        // https://leetcode.com/discuss/interview-question/5677840/Meta-or-E5E6-or-Virtual-Onsite-or-Seattle

        /*
                  1
              2          0
          0     5     0     0
        8   9 10 11 12 13 14 15
         
         
         */

        public void RunTest()
        {
            foreach (var str in new string[]
            {
                "1,2,0,0,5,0,0,8,9,10,11,12,13,14,15"
            })
            {
                var tree = TreeNodeUtilities.GetTree(str);
                TreeNodeUtilities.PrintTree(tree);
                Sink(tree);
                TreeNodeUtilities.PrintTree(tree);
            }
        }

        private void Sink(TreeNode root)
        {
            var stack = new Stack<TreeNode>();
            GetZeros(root);
            Replace(root);

            void Replace(TreeNode node)
            {
                if (node == null || stack.Count == 0) return;
                Replace(node.left);
                Replace(node.right);

                while (stack.Count > 0 && stack.Peek().val != 0) stack.Pop();
                if (stack.Count > 0 && node.val != 0)
                {
                    (node.val, stack.Pop().val) = (0, node.val);
                }
            }

            void GetZeros(TreeNode node)
            {
                if (node == null) return;
                if (node.val == 0) stack.Push(node);
                GetZeros(node.left);
                GetZeros(node.right);
            }
        }
    }
}
