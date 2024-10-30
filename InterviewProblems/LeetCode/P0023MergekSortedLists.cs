using System;
using System.Collections.Generic;
using System.Linq;

using InterviewProblems.Utilities;

namespace InterviewProblems.LeetCode
{
    internal class P0023MergekSortedLists : ITestable
    {
        public void RunTest()
        {
            foreach (var lists in new ListNode[][]
            {
                new [] { ListNodeUtilities.GetSinglyLinkedList(new[] { 1, 4, 5 } ), ListNodeUtilities.GetSinglyLinkedList(new[] { 1, 3, 4 }), ListNodeUtilities.GetSinglyLinkedList(new[] { 2, 6 }) }
            })
            {
                Console.WriteLine(string.Join(", ", MergeKLists(lists)));
            }
        }

        public ListNode MergeKLists(ListNode[] lists)
        {
            var count = lists.Length;
            IEnumerable<ListNode> reduced = lists;

            while (count > 1)
            {
                reduced = Reduce(reduced);
                count = (count + 1) / 2;
            }

            return reduced.SingleOrDefault();

            IEnumerable<ListNode> Reduce(IEnumerable<ListNode> nodes)
            {
                var list = new List<ListNode>();

                foreach (var node in nodes)
                {
                    list.Add(node);
                    if (list.Count == 2)
                    {
                        yield return Merge2(list[0], list[1]);
                        list.Clear();
                    }
                }

                if (list.Count > 0)
                {
                    yield return list[0];
                }
            }

            ListNode Merge2(ListNode n1, ListNode n2)
            {
                if (n1 == null || n2 == null) return n1 ?? n2;

                ListNode head = null;
                ListNode tail = null;

                while (n1 != null && n2 != null)
                {
                    ListNode node = n1.val <= n2.val ? n1 : n2;
                    if (node == n1) n1 = n1.next;
                    else n2 = n2.next;

                    if (head == null)
                    {
                        head = node;
                        tail = node;
                    }
                    else
                    {
                        tail.next = node;
                        tail = node;
                    }
                }

                tail.next = n1 ?? n2;
                return head;
            }
        }
    }
}
