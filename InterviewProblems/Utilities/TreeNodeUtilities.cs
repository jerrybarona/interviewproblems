using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace InterviewProblems.Utilities
{
    public static class TreeNodeUtilities
    {
        public static TreeNode GetTree(string sequence)
        {
            var elements = sequence.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (elements.Length == 0 || elements[0] == "null") return null;
            var root = new TreeNode(int.Parse(elements[0]));
            var queue = new Queue<TreeNode>(new[] { root });
            TreeNode node = null;
            for (var (i, left) = (1, true); i < elements.Length; ++i, left = !left)
            {
                var child = GetNode(elements[i]);
                if (left)
                {
                    if (queue.Count == 0) break;
                    node = queue.Dequeue();
                    node.left = child;
                }
                else
                {
                    node.right = child;
                }
                if (child != null) queue.Enqueue(child);
            }

            return root;

            TreeNode GetNode(string str) => int.TryParse(str, out int val) ? new TreeNode(val) : null;
        }

        public static string GetSequence(TreeNode root)
        {
            var result = new List<string>();
            var queue = new Queue<TreeNode>(new[] { root });

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                result.Add(node == null ? "null" : node.val.ToString());
                if (node != null)
                {
                    queue.Enqueue(node.left);
                    queue.Enqueue(node.right);
                }
            }

            return string.Join(',', result);
        }

        public static void PrintTree(TreeNode root)
        {
            var levels = new List<List<(int val, int x)>>();
            Traverse(root, 0, 0);
            var coords = new Dictionary<(int, int), int>();

            var offset = 1;
            var step = 2;
            var maxj = 0;
            for (var i = levels.Count - 1; i >= 0; --i)
            {
                foreach (var item in levels[i])
                {
                    var j = offset + (item.x) * step;
                    coords.Add((i, j), item.val);
                    maxj = Math.Max(maxj, j);
                }

                offset = step;
                step *= 2;
            }

            for (var i = 0; i < levels.Count; ++i)
            {
                for (var j = 0; j <= maxj; ++j)
                {
                    if (coords.ContainsKey((i, j))) Console.Write(coords[(i, j)]);
                    else Console.Write(' ');
                }
                Console.Write("\n");
            }


            void Traverse(TreeNode node, int level, int x)
            {
                if (node == null) return;
                if (level == levels.Count) levels.Add(new List<(int val, int x)>());
                levels[level].Add((node.val, x));

                Traverse(node.left, level + 1, (x << 1) + 0);
                Traverse(node.right, level + 1, (x << 1) + 1);
            }
        }
    }

    [DebuggerDisplay("val = {val}")]
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
    }
}
