using System;
using InterviewProblems.Utilities;

namespace InterviewProblems.LeetCode
{
	public class P0109ConvertSortedListToBinarySearchTree
	{
        public void SortedListToBSTTest()
		{
            var input = new[] { -10, -1, 5, 64, 77, 100 };
            Console.WriteLine($"input: {string.Join(" -> ", input)}");
            var result = SortedListToBST(ListNodeUtilities.GetSinglyLinkedList(input));
            Console.WriteLine("\noutput:");
            TreeNodeUtilities.PrintTree(result);
		}

        public TreeNode SortedListToBST(ListNode head)
        {
            return ConvertToBst(0, GetLength(head) - 1);

            TreeNode ConvertToBst(int start, int end)
            {
                if (end < start) return null;
                var mid = start + (end - start) / 2;
                var left = ConvertToBst(start, mid - 1);
                var node = new TreeNode(head.val);
                head = head.next;
                var right = ConvertToBst(mid + 1, end);
                
                node.left = left;
                node.right = right;

                return node;
            }

            int GetLength(ListNode node)
			{
                var len = 0;
                for (; node != null; node = node.next) ++len;

                return len;
			}
        }
    }
}
