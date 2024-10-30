using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.LeetCode
{
    public class P0140WordBreakII : ITestable
    {
        public void RunTest()
        {
            foreach (var (s, wordDict) in new (string, IList<string>)[]
            {
                ("catsanddog", new [] {"cat","cats","and","sand","dog"}), // ["cats and dog","cat sand dog"]
                //("pineapplepenapple", new [] {"apple","pen","applepen","pine","pineapple"}),
            })
            {
                Console.WriteLine("[" + string.Join(", ", WordBreak3(s, wordDict)) + "]");
            }
        }

        private IList<string> WordBreak3(string s, IList<string> wordDict)
        {
            var wset = new HashSet<string>(wordDict);
            var memo = new List<string>[s.Length];

            var ans = Dfs(0);

            return ans;

            List<string> Dfs(int idx)
            {
                if (idx == s.Length)
                {
                    return new List<string> { string.Empty };
                }

                if (memo[idx] != null)
                {
                    return memo[idx];
                }

                var result = new List<string>();
                var sb = new StringBuilder();
                for (var i = idx; i < s.Length; ++i)
                {
                    var str = s[idx..(i + 1)];
                    if (wset.Contains(str))
                    {
                        sb.Append(str);
                        var next = Dfs(i + 1);
                        foreach (var n in next)
                        {
                            sb.Append(' ');
                            sb.Append(n);
                            result.Add(sb.ToString());
                            sb.Length -= n.Length + 1;
                        }

                        sb.Length = 0;
                    }
                }

                memo[idx] = result;
                return result;
            }
        }

        public IList<string> WordBreak2(string s, IList<string> wordDict)
        {
            var trie = new Trie(wordDict);
            var sb = new StringBuilder();
            var result = new List<string>();

            Dfs(0);

            return result;

            void Dfs(int idx)
            {
                if (idx == s.Length)
                {
                    result.Add(sb.ToString());
                    return;
                }

                var searcher = trie.GetSearcher();
                var i = idx;
                for (; i < s.Length; ++i)
                {
                    searcher.SetCurr(s[i]);
                    if (!searcher.IsPrefix()) break;

                    if (searcher.Word() != null)
                    {
                        //Console.WriteLine(searcher.Word());
                        bool firstWord = sb.Length == 0;
                        if (!firstWord) sb.Append(' ');
                        sb.Append(searcher.Word());

                        Dfs(i + 1);

                        if (firstWord) sb.Length = 0;
                        else sb.Length -= searcher.Word().Length + 1;
                    }

                    searcher.SetNext();
                }
            }
        }

        public IList<string> WordBreak(string s, IList<string> wordDict)
        {
            var maxLen = wordDict.Select(x => x.Length).Max();
            var map = wordDict
                .Select((val, idx) => (val, idx))
                .ToDictionary(x => x.val, x => x.idx);

            var result = new List<List<int>>();
            
            return CanBreak(0, new List<int>()) ? result
                .Select(list => String.Join(" ", list.Select(y => wordDict[y]))).ToList() : new List<string>();

            bool CanBreak(int idx, List<int> comb)
            {
                if (idx == s.Length)
                {
                    result.Add(new List<int>(comb));
                    return true;
                }

                var canBreak = false;
                for (var i = idx; i < s.Length && i < idx + maxLen; ++i)
                {
                    var str = s.Substring(idx, i - idx + 1);
                    if (map.ContainsKey(str))
                    {
                        comb.Add(map[str]);
                        if (CanBreak(i + 1, comb)) canBreak = true;
                        comb.RemoveAt(comb.Count - 1);
                    }
                }
                return canBreak;
            }
        }


        class Trie
        {
            private readonly Node _root = new Node();

            public Trie(IList<string> strs)
            {
                foreach (var str in strs) Add(str);
            }

            private void Add(string str)
            {
                var node = _root;
                foreach (var c in str)
                {
                    var idx = c - 'a';
                    if (node.Children[idx] == null) node.Children[idx] = new Node();

                    node = node.Children[idx];
                }

                node.Word = str;
            }

            public Searcher GetSearcher()
            {
                return new Searcher(_root);
            }

            internal class Searcher
            {
                private Node _node;
                private char _curr;
                public Searcher(Node node)
                {
                    _node = node;
                }

                public void SetCurr(char c)
                {
                    _curr = c;
                }

                public bool IsPrefix()
                {
                    return _node.Children[_curr - 'a'] != null;
                }

                public string Word()
                {
                    return _node.Children[_curr - 'a'].Word;
                }

                public void SetNext()
                {
                    _node = _node.Children[_curr - 'a'];
                }
            }
        }

        class Node
        {
            public string Word { get; set; }
            public Node[] Children { get; } = new Node[26];
        }
    }
}
