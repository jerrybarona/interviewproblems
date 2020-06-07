using System;
using System.Linq;
using InterviewProblems.Utilities;

namespace InterviewProblems.LeetCode
{
    public class P0092ReverseLinkedList2
    {
        public void ReverseBetweenTest()
        {
            var list = new[] { 1, 2, 3, 4, 5 };
            var head = ListNodeUtilities.GetSinglyLinkedList(list);
            var m = 2;
            var n = 4;
            Console.WriteLine($"[{string.Join(',', list)}]");
            Console.WriteLine($"[{string.Join(',', ReverseBetween(head, m, n).ToList())}]");

        }
        public ListNode ReverseBetween(ListNode head, int m, int n)
        {
            var k = n - m;
            var node = head;
            ListNode left = null;
            for (var i = m; i > 1; --i)
            {
                left = node;
                node = node.next;
            }
            var right = node;
            ListNode prev = null;
            while (node != null && k >= 0)
            {
                var fast = node.next;
                node.next = prev;
                prev = node;
                node = fast;
                --k;
            }

            if (left != null) left.next = prev;
            right.next = node;
            return m > 1 ? head : prev;
        }
    }
}
