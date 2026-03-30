using System;
using System.Text;

using InterviewProblems.Utilities;

namespace InterviewProblems.DoorDash
{
    internal class P005SerializeAndDeserializeBinaryTree : ITestable
    {
        public void RunTest()
        {
            foreach (var (val, limit) in new (int, int)[]
            {
                (1,7),
                (1,15),
                (1,31),
            })
            {
                var tree = BuildCompleteTree(val, limit);
                Console.WriteLine("\nOriginal tree in order:");
                Console.WriteLine(GetInOrderString(tree));
                Console.WriteLine("Serialized:");
                var serialized = SerializeBinaryTree(tree);
                Console.WriteLine(serialized);
                Console.WriteLine("Deserialized:");
                Console.WriteLine(GetInOrderString(DeserializeBinaryTree(serialized)));
            }
        }

        private string SerializeBinaryTree(TreeNode root)
        {
            var sb = new StringBuilder();
            Traverse(root);

            return sb.ToString();

            void Traverse(TreeNode node)
            {
                if (sb.Length > 0) sb.Append(',');
                
                if (node == null)
                {
                    sb.Append('#');
                    return;
                }

                sb.Append(node.val);
                Traverse(node.left);
                Traverse(node.right);
            }
        }

        private TreeNode DeserializeBinaryTree(string str)
        {
            var arr = str.Split(',');
            var idx = 0;

            return GetTree();

            TreeNode GetTree()
            {
                if (idx >= arr.Length) return null;

                var val = arr[idx];
                ++idx;

                if (val == "#")
                {
                    return null;
                }

                return new TreeNode(int.Parse(val))
                {
                    left = GetTree(),
                    right = GetTree()
                };
            }
        }

        private TreeNode BuildCompleteTree(int val, int limit)
        {
            if (val > limit) return null;
            TreeNode node = new TreeNode(val)
            {
                left = BuildCompleteTree(2 * val, limit),
                right = BuildCompleteTree(2 * val + 1, limit)
            };

            return node;
        }

        private string GetInOrderString(TreeNode root)
        {
            var sb = new StringBuilder();
            Traverse(root);

            return sb.ToString();

            void Traverse(TreeNode node)
            {
                if (node == null) return;
                if (sb.Length > 0) sb.Append(", ");
                sb.Append(node.val);
                Traverse(node.left);
                Traverse(node.right);
            }
        }
    }
}
