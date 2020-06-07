using System;
using System.Collections.Generic;
using System.Text;
using InterviewProblems.Utilities;

namespace InterviewProblems.Microsoft
{
    public class P035SerializeNaryTree
    {
        // https://leetcode.com/discuss/interview-question/562206/Microsoft-Phone-screen-or-SDE-or-April-2020

        public void SerializeNaryTreeTest()
        {
            var root = TreeNodeUtilities.GetTree("1,2,3,4,5,6,7");
            Console.WriteLine(SerializeNaryTree(root));
        }

        public string SerializeNaryTree(TreeNode root)
        {
            if (root == null) return string.Empty;

            return Serialize(root);

            string Serialize(TreeNode node)
            {
                var result = new StringBuilder(node.val.ToString());
                if (node.left != null || node.right != null)
                {
                    var children = new List<string>();
                    if (node.left != null) children.Add(Serialize(node.left));
                    if (node.right != null) children.Add(Serialize(node.right));
                    result.Append($"({string.Join(',', children)})");
                }

                return $"({result})";
            }
        }
    }
}
