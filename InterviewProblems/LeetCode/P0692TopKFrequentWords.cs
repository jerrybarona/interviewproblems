using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0692TopKFrequentWords
    {
        public void TopKFrequentTest()
        {
            var words = new[] { "i", "love", "leetcode", "i", "love", "coding" };
            var k = 2;
            Console.WriteLine($"[{string.Join(',', TopKFrequent(words, k))}]");

            var words2 = new[] { "the", "day", "is", "sunny", "the", "the", "the", "sunny", "is", "is" };
            var k2 = 4;
            Console.WriteLine($"[{string.Join(',', TopKFrequent(words2, k2))}]");
        }

        public IList<string> TopKFrequent(string[] words, int k)
        {
            var map = words.Aggregate(new Dictionary<string, int>(), (dict, word) =>
            {
                if (!dict.ContainsKey(word)) dict.Add(word, 0);
                ++dict[word];
                return dict;
            });

            var heap = new MinHeap<Node>(k);
            foreach (var node in map.Select(item => new Node(item.Value, item.Key)))
            {
                if (heap.Count == k && heap.Peek().CompareTo(node) < 0) heap.Pop();
                if (heap.Count < k) heap.Add(node);
            }

            return heap.ToEnumerable().Reverse().Select(n => n.Word).ToList();
        }

        class Node : IComparable<Node>
        {
            public int Frequency { get; }
            public string Word { get; }

            public Node(int frequency, string word) =>
                (Frequency, Word) = (frequency, word);

            public int CompareTo(Node otherNode)
            {
                if (Frequency != otherNode.Frequency) return Frequency - otherNode.Frequency;
                return string.Compare(otherNode.Word, Word, StringComparison.Ordinal);
            }
        }

        class MinHeap<T> : IEnumerable<T> where T : IComparable<T>
        {
            private readonly T[] _elements;
            private int _size;

            public MinHeap(int k)
            {
                _elements = new T[k];
            }

            private int LeftChildIndex(int idx) => 2 * idx + 1;
            private int RightChildIndex(int idx) => 2 * idx + 2;
            private int ParentIndex(int idx) => (idx - 1) / 2;

            private bool HasLeftChild(int idx) => LeftChildIndex(idx) < _size;
            private bool HasRightChild(int idx) => RightChildIndex(idx) < _size;
            private bool IsRoot(int idx) => idx == 0;

            private T LeftChild(int idx) => _elements[LeftChildIndex(idx)];
            private T RightChild(int idx) => _elements[RightChildIndex(idx)];
            private T Parent(int idx) => _elements[ParentIndex(idx)];

            public int Count => _size;

            public IEnumerable<T> ToEnumerable()
            {
                while (_size > 0) yield return Pop();
            }

            public T Peek()
            {
                if (_size == 0) throw new IndexOutOfRangeException();
                return _elements[0];
            }

            public T Pop()
            {
                if (_size == 0) throw new IndexOutOfRangeException();
                var result = _elements[0];
                _elements[0] = _elements[--_size];
                BubbleDown();
                return result;
            }

            public void Add(T element)
            {
                if (_size == _elements.Length) throw new IndexOutOfRangeException();
                _elements[_size++] = element;
                BubbleUp();
            }

            private void BubbleDown()
            {
                for (var idx = 0; HasLeftChild(idx);)
                {
                    var smallestIdx = HasRightChild(idx) && RightChild(idx).CompareTo(LeftChild(idx)) < 0
                        ? RightChildIndex(idx)
                        : LeftChildIndex(idx);

                    if (_elements[idx].CompareTo(_elements[smallestIdx]) <= 0) break;
                    (_elements[idx], _elements[smallestIdx]) = (_elements[smallestIdx], _elements[idx]);
                    idx = smallestIdx;
                }
            }

            private void BubbleUp()
            {
                for (var idx = _size - 1; !IsRoot(idx) && _elements[idx].CompareTo(Parent(idx)) < 0;)
                {
                    var parentIdx = ParentIndex(idx);
                    (_elements[idx], _elements[parentIdx]) = (_elements[parentIdx], _elements[idx]);
                    idx = parentIdx;
                }
            }

            public IEnumerator<T> GetEnumerator()
            {
                while (_size > 0) yield return Pop();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
