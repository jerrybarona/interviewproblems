using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P0987VerticalOrderTraversalOfBinaryTree
    {
        public IList<IList<int>> VerticalTraversal(TreeNode root)
        {
            var list = new List<(int val, int x, int y)>();
            Traverse2(root, 0, 0);

            return Traverse(root, 0, 0).GroupBy(a => a.x,
                (k, nodes) => (IList<int>) nodes.OrderBy(b => b.y).ThenBy(c => c.val).Select(d => d.val)).ToList();

            void Traverse2(TreeNode node, int x, int y)
            {
                if (node == null) return;
                list.Add((node.val, x, y));
                Traverse2(node.left, x - 1, y - 1);
                Traverse2(node.right, x + 1, y - 1);
            }

            IEnumerable<(int val, int x, int y)> Traverse(TreeNode node, int x, int y)
            {
                if (node != null)
                {
                    yield return (node.val, x, y);
                    foreach (var n in Traverse(node.left, x - 1, y - 1)) yield return n;
                    foreach (var n in Traverse(node.right, x + 1, y - 1)) yield return n;
                }
            }
        }
    }
}
