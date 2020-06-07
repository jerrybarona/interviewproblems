using System;
using System.Linq;
using InterviewProblems.Utilities;

namespace InterviewProblems.Facebook
{
    public class P014SwapNodesInPairsInDoublyLinkedList
    {
        // https://leetcode.com/discuss/interview-question/435407/Facebook-or-Phone-or-Construct-a-Binary-Tree-and-Swap-Nodes-in-Pairs

        public void SwapNodesTest()
        {
            var list = ListNodeUtilities.GetDoublyLinkedList(new[] { 1, 2, 3, 4 });
            Console.WriteLine(
                $@"Given:  [{string.Join(',', list.AsEnumerable())}]
OutPut: [{string.Join(',', SwapNodes(list).AsEnumerable())}]
");

            list = ListNodeUtilities.GetDoublyLinkedList(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 });
            Console.WriteLine(
                $@"Given:  [{string.Join(',', list.AsEnumerable())}]
OutPut: [{string.Join(',', SwapNodes(list).AsEnumerable())}]
");
        }

        public DoublyLinkedNode SwapNodes(DoublyLinkedNode head)
        {
            if (head == null) return null;

            var newHead = head.next;
            var node = head;
            var slow = head.next;

            while (node != null && slow != null)
            {
                var fast = slow.next;

                slow.prev = node.prev;
                if (node.prev != null) node.prev.next = slow;
                slow.next = node;
                node.prev = slow;

                node.next = fast;
                if (fast != null) fast.prev = node;

                node = fast;
                slow = node?.next;
            }

            return newHead;
        }
    }
}
