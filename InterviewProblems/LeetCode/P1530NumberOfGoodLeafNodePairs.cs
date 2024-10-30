using System;

using InterviewProblems.Utilities;

namespace InterviewProblems.LeetCode
{
    public class P1530NumberOfGoodLeafNodePairs : ITestable
    {
        public void RunTest()
        {
            foreach (var (root, distance) in new (TreeNode, int)[]
            {
                (TreeNodeUtilities.GetTree("55,48,null,71,null,80,null,19,null,43"), 4),
                //(TreeNodeUtilities.GetTree("1,2,3,4,5,6,7,8"), 2),
                //(TreeNodeUtilities.GetTree("1,2,3,4,5,6"), 4)
            })
            {
                Console.WriteLine(CountPairs(root, distance));
            }
        }

        public int CountPairs(TreeNode root, int distance)
        {
            int result = 0;

            Count(root);

            return result;

            int[] Count(TreeNode node)
            {
                if (node.left == null && node.right == null)
                {
                    return new int[] { 1 };
                }

                var left = node.left == null ? null : Count(node.left);
                var right = node.right == null ? null : Count(node.right);

                int[] arr;
                if (left == null || right == null)
                {
                    var leaves = left ?? right;
                    arr = new int[1 + Math.Min(distance, leaves.Length)];
                    for (var i = 0; i < Math.Min(arr.Length - 1, leaves.Length); ++i)
                    {
                        arr[i + 1] = leaves[i];
                    }
                }
                else
                {
                    for (var i = 0; i < left.Length; ++i)
                    {
                        for (var j = 0; j < right.Length; ++j)
                        {
                            if (i + j + 2 > distance) break;

                            result += (left[i] * right[j]);
                        }
                    }

                    arr = new int[1 + Math.Min(distance, Math.Max(left.Length, right.Length))];
                    for (var i = 0; i < Math.Min(left.Length, arr.Length) && i < arr.Length - 1; ++i)
                    {
                        arr[i + 1] += left[i];
                    }

                    for (var i = 0; i < Math.Min(right.Length, arr.Length) && i < arr.Length - 1; ++i)
                    {
                        arr[i + 1] += right[i];
                    }
                }

                return arr;
            }
        }
    }
}
