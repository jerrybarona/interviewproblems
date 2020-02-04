using System;
using System.Collections.Generic;

namespace InterviewProblems.Google
{
    public class P015KthAncestorInNaryTree
    {
        // https://leetcode.com/discuss/interview-question/487749/Google-or-Phone-or-Kth-ancestor-of-a-node-in-an-N-ary-tree

        public void KthAncestorInNaryTreeTest()
        {
            var root = new Node(1)
            {
                Children = { new Node(4) { Children = { new Node(3) } }, new Node(5) }
            };

            Console.WriteLine($"getAncestor(3,2) => {KthAncestorInNaryTree(root, 3, 2)}");
            Console.WriteLine($"getAncestor(5,3) => {KthAncestorInNaryTree(root, 5, 3)}");
            Console.WriteLine($"getAncestor(5,1) => {KthAncestorInNaryTree(root, 5, 1)}");
        }


        public int KthAncestorInNaryTree(Node root, int n, int k)
        {
            var queue = GetAncestry(root);
            for (; queue.Count > 0 && k > 0; --k, queue.Dequeue()) { }

            return queue.Count > 0 ? queue.Dequeue() : -1;

            Queue<int> GetAncestry(Node node)
            {
                if (node == null) return null;
                if (node.Val == n) return new Queue<int>(new[] { node.Val });
                foreach (var child in node.Children)
                {
                    var ancestry = GetAncestry(child);
                    if (ancestry != null)
                    {
                        ancestry.Enqueue(node.Val);
                        return ancestry;
                    }
                }

                return null;
            }
        }

        public class Node
        {
            public int Val { get; }
            public List<Node> Children { get; } = new List<Node>();

            public Node(int val)
            {
                Val = val;
            }
        }
    }
}
