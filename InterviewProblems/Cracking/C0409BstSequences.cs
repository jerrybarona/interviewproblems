using System;
using System.Collections.Generic;
using System.Linq;

using InterviewProblems.Utilities;

namespace InterviewProblems.Cracking
{
    public class C0409BstSequences : ITestable
    {
        public void RunTest()
        {
            foreach (var tree in new TreeNode[]
            {
                TreeNodeUtilities.GetTree("2,1,3"),
                TreeNodeUtilities.GetTree("50,20,60,10,25,null,70,5,15,null,null,65,80"),
            })
            {
                Console.WriteLine("\nTest BST:\n");
                TreeNodeUtilities.PrintTree(tree);
                var result = BstSequences(tree);
                Console.WriteLine($"\n\nBst sequences:\n{string.Join("\n", result.Select(x => "\t[" + string.Join(',', x) + "]"))}");
            }
        }

        private IList<LinkedList<int>> BstSequences(TreeNode root)
        {
            if (root == null) return new List<LinkedList<int>>(new[] { new LinkedList<int>() });
            var left = BstSequences(root.left);
            var right = BstSequences(root.right);

            return Weave(left, right, root);
        }

        private IList<LinkedList<int>> Weave(IList<LinkedList<int>> sequences1, IList<LinkedList<int>> sequences2, TreeNode node)
        {
            var sequences = new List<LinkedList<int>>();
            foreach (var s1 in sequences1)
            {
                foreach (var s2 in sequences2)
                {
                    WeaveSequences(s1, s2, new LinkedList<int>(new[] { node.val }));
                }
            }

            return sequences;

            void WeaveSequences(LinkedList<int> sequence1, LinkedList<int> sequence2, LinkedList<int> prefix)
            {
                if (sequence1.Count == 0 || sequence2.Count == 0)
                {
                    var s = sequence1.Count == 0 ? sequence2 : sequence1;
                    var result = new LinkedList<int>(prefix.AsEnumerable().Concat(s.AsEnumerable()));
                    sequences.Add(result);
                    return;
                }

                var node1 = sequence1.First;
                sequence1.RemoveFirst();
                prefix.AddLast(node1.Value);
                WeaveSequences(sequence1, sequence2, prefix);
                prefix.RemoveLast();
                sequence1.AddFirst(node1);

                var node2 = sequence2.First;
                sequence2.RemoveFirst();
                prefix.AddLast(node2.Value);
                WeaveSequences(sequence1, sequence2, prefix);
                prefix.RemoveLast();
                sequence2.AddFirst(node2);                
            }
        }
    }
}
