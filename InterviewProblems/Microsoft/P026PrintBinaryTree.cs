using System;
using System.Collections.Generic;

namespace InterviewProblems.Microsoft
{
    public class P026PrintBinaryTree
    {
        // https://leetcode.com/discuss/interview-question/438544/Microsoft-or-Onsite-or-Print-Binary-Tree

        public void PrintBinaryTreeTest()
        {
            var root = new Node(1);
            root.Left = new Node(2);
            root.Right = new Node(3);
            root.Left.Left = new Node(4);
            root.Left.Right = new Node(5);
            root.Right.Left = new Node(6);
            root.Right.Right = new Node(7);
            root.Left.Left.Left = new Node(8);
            root.Left.Left.Right = new Node(9);
            root.Left.Right.Left = new Node(1);
            root.Left.Right.Right = new Node(2);
            root.Right.Left.Left = new Node(3);
            root.Right.Left.Right = new Node(4);
            root.Right.Right.Left = new Node(5);
            root.Right.Right.Right = new Node(6);

            PrintBinaryTree(root);
        }

        public void PrintBinaryTree(Node root)
        {
            var levels = new List<List<(int val, int x)>>();
            Traverse(root, 0, 0);
            var coords = new Dictionary<(int, int),int>();

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


            void Traverse(Node node, int level, int x)
            {
                if (node == null) return;
                if (level == levels.Count) levels.Add(new List<(int val, int x)>());
                levels[level].Add((node.Value, x));

                Traverse(node.Left, level + 1, (x << 1) + 0);
                Traverse(node.Right, level + 1, (x << 1) + 1);
            }
        }

        public class Node
        {
            public int Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(int value)
            {
                Value = value;
            }
        }
    }
}
