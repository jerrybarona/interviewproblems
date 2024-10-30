using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblems.LeetCode
{
    public class P0642DesignSearchAutocompleteSystem : ITestable
    {
        // #tiktok

        public void RunTest()
        {
            var acs = new AutocompleteSystem(new[] { "abc", "abbc", "a" }, new[] { 3, 3, 3 });
            foreach (var c in new[] {'b', 'c', '#', 'b', 'c', '#', 'a', 'b', 'c', '#', 'a', 'b', 'c', '#' })
            {
                Console.WriteLine($"['{string.Join("','", acs.Input(c))}']");
            }
        }

        public class AutocompleteSystem
        {
            private readonly Trie _trie;
            private readonly TrieSearcher _searcher;
            private readonly PriorityQueue<string, (string, int)> _pq;

            public AutocompleteSystem(string[] sentences, int[] times)
            {
                _trie = new Trie();
                for (var i = 0; i < sentences.Length; ++i)
                {
                    _trie.Add(sentences[i], times[i]);
                }

                _searcher = _trie.GetSearcher();
                _pq = new PriorityQueue<string, (string, int)>(Comparer<(string, int)>.Create((a, b) =>
                {
                    var (aword, atimes) = a;
                    var (bword, btimes) = b;

                    if (atimes == btimes) return bword.CompareTo(aword);
                    return atimes - btimes;
                }));
            }

            public IList<string> Input(char c)
            {
                _pq.Clear();

                if (c == '#')
                {
                    _trie.Add(_searcher.Current(), 1);
                    _searcher.Reset();
                    return new List<string>();
                }

                var wordTimes = _searcher.GetWords(c);
                if (wordTimes == null) return new List<string>();
                foreach (var kvp in wordTimes)
                {
                    var word = kvp.Key;
                    var times = kvp.Value;
                    _pq.Enqueue(word, (word, times));

                    if (_pq.Count > 3) _pq.Dequeue();
                }

                var result = new string[_pq.Count];
                for (var i = result.Length - 1; i >= 0; --i)
                {
                    result[i] = _pq.Dequeue();
                }

                return result.ToList();
            }

            class Trie
            {
                private readonly Node _root = new Node();

                public void Add(string word, int times)
                {
                    var node = _root;

                    foreach (var c in word)
                    {
                        int idx = char.IsLetter(c) ? c - 'a' : 26;
                        if (node.Children[idx] == null) node.Children[idx] = new Node();
                        node = node.Children[idx];
                        if (!node.WordTimes.ContainsKey(word)) node.WordTimes.Add(word, 0);
                        node.WordTimes[word] += times;
                    }
                }

                public TrieSearcher GetSearcher()
                {
                    return new TrieSearcher(_root);
                }
            }

            class TrieSearcher
            {
                private readonly Node _root;
                private readonly StringBuilder _sb;
                private Node _node;

                public TrieSearcher(Node root)
                {
                    _root = root;
                    _node = root;
                    _sb = new StringBuilder();
                }

                public Dictionary<string, int> GetWords(char c)
                {
                    _sb.Append(c);
                    if (_node == null) return null;

                    int idx = char.IsLetter(c) ? c - 'a' : 26;
                    var words = _node.Children[idx] == null ? null : _node.Children[idx].WordTimes;
                    _node = _node.Children[idx];

                    return words;
                }

                public void Reset()
                {
                    _node = _root;
                    _sb.Length = 0;
                }

                public string Current()
                {
                    return _sb.ToString();
                }
            }

            class Node
            {
                public Dictionary<string, int> WordTimes { get; } = new Dictionary<string, int>();
                public Node[] Children { get; } = new Node[27];
            }
        }
    }
}
