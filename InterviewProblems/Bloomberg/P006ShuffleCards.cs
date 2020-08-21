using System;
using System.Linq;
using InterviewProblems.Utilities;

namespace InterviewProblems.Bloomberg
{
    public class P006ShuffleCards
    {
        // https://leetcode.com/discuss/interview-question/689328/Bloomberg-or-Phone-or-Shuffle-Cards

        public void ShuffleTest()
        {
            var list = new [] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
            Console.WriteLine($"\nInput: [{string.Join(',', list)}]");
            Console.Write($"Output: [{string.Join(',', Shuffle(ListNodeUtilities.GetSinglyLinkedList(list)).AsEnumerable())}]");
        }

        public ListNode Shuffle(ListNode head, int k = 10)
        {
            if (k == 0 || head?.next == null) return head;

            (ListNode prev, var slow, var fast) = (null, head, head);
            while (fast?.next != null)
            {
                fast = fast.next.next;
                prev = slow;
                slow = slow.next;
            }

            prev.next = null;
            return Shuffle(Interleave(slow, head), k - 1);

            ListNode Interleave(ListNode n1, ListNode n2)
            {
                if (n1 == null || n2 == null) return n1 ?? n2;
                var node = n1;
                n1 = n1.next;
                node.next = Interleave(n2, n1);

                return node;
            }
        }
    }
}
