using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Microsoft
{
    class P022CrosswordSuggestions
    {
        // https://leetcode.com/discuss/interview-experience/478747/Microsoft-or-SDE1-or-Seattle-or-Jan-2020-Reject

        public List<string> CrosswordSuggestions(string input, string[] options)
        {
            var trie = new Trie(options.Where(w => w.Length == input.Length));
            return trie.GetSuggestions(input);
        }

        public void CrosswordSuggestionsTest()
        {
            var options = new[] { "angel", "apple", "angle", "actor" };
            var inputs = new[] { "a__l_","a___r", "a____" };

            foreach (var input in inputs)
            {
                var result = CrosswordSuggestions(input, options);
                Console.WriteLine(string.Join(',', result));
            }
        }

        class Trie
        {
            private readonly Node _trie = new Node();

            public Trie(IEnumerable<string> words)
            {
                foreach (var word in words) Add(word);
            }

            private void Add(string word)
            {
                AddWord(0, _trie);

                void AddWord(int idx, Node node)
                {
                    if (idx == word.Length) node.Word = word;
                    else
                    {
                        if (!node.Children.ContainsKey(word[idx])) node.Children.Add(word[idx], new Node());
                        AddWord(idx+1, node.Children[word[idx]]);
                    }
                }
            }

            public List<string> GetSuggestions(string word)
            {
                var result = new List<string>();
                Suggest(0, _trie);
                return result;

                void Suggest(int idx, Node node)
                {
                    if (idx == word.Length) result.Add(node.Word);
                    else
                    {
                        if (word[idx] == '_')
                        {
                            foreach (var child in node.Children) Suggest(idx + 1, child.Value);
                        }
                        else if (node.Children.ContainsKey(word[idx])) Suggest(idx + 1, node.Children[word[idx]]);
                    }
                }
            }
        }

        private class Node
        {
            public string Word { get; set; }
            public Dictionary<char, Node> Children { get; } = new Dictionary<char, Node>(26);
        }
    }
}
