using System.Collections;
using System.Collections.Generic;

namespace InterviewProblems.Utilities
{
    public static class ListNodeUtilities
    {
        public static DoublyLinkedNode GetDoublyLinkedList(int[] arr)
        {
            if (arr.Length == 0) return null;
            
            var head = new DoublyLinkedNode(arr[0]);

            for (var (i, prev) = (1, head); i < arr.Length; ++i, prev = prev?.next as DoublyLinkedNode)
            {
                var node = new DoublyLinkedNode(arr[i]);
                if (prev != null)
                {
                    prev.next = node;
                    node.prev = prev;
                }
            }

            return head;
        }

        public static ListNode GetSinglyLinkedList(int[] arr)
        {
            if (arr.Length == 0) return null;

            var head = new ListNode(arr[0]);

            for (var (i, prev) = (1, head); i < arr.Length; ++i, prev = prev?.next)
            {
                var node = new ListNode(arr[i]);
                if (prev != null) prev.next = node;
            }

            return head;
        }
    }

    public class DoublyLinkedNode : IEnumerable<int>
    {
        public int val { get; set; }
        public DoublyLinkedNode next { get; set; }
        public DoublyLinkedNode prev { get; set; }

        public DoublyLinkedNode(int val)
        {
            this.val = val;
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            var node = next;
            yield return val;
            while (node != null)
            {
                yield return node.val;
                node = node.next;
            }
        }

        public IEnumerator GetEnumerator()
        {
            var node = next;
            yield return val;
            while (node != null)
            {
                yield return node.val;
                node = node.next;
            }
        }
    }

    public class ListNode : IEnumerable<int>
    {
        public int val { get; set; }
        public virtual ListNode next { get; set; }
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            var node = next;
            yield return val;
            while (node != null)
            {
                yield return node.val;
                node = node.next;
            }
        }

        public IEnumerator GetEnumerator()
        {
            var node = next;
            yield return val;
            while (node != null)
            {
                yield return node.val;
                node = node.next;
            }
        }
    }
}
