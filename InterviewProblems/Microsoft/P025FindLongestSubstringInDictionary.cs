using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace InterviewProblems.Microsoft
{
    public class P025FindLongestSubstringInDictionary
    {
        // https://leetcode.com/discuss/interview-question/447101/MicroSoft-Interview-%3A-Find-longest-SubString-in-the-dictionary

        public void FindLongestSubstringInDictionaryTest()
        {
            var input = "toestate";
            var dict = new[] { "et", "eto", "eteot", "es", "state" };

            Console.WriteLine("input: " + input);
            Console.WriteLine("list: " + string.Join(',', dict));
            Console.WriteLine("Result:");
            Console.WriteLine(string.Join(',', FindLongestSubstringInDictionary(input, dict)));
        }

        public List<string> FindLongestSubstringInDictionary(string input, string[] dict) =>
            new Trie(dict).GetLongestSubstrings(input);

        class Trie
        {
            private readonly Node _root = new Node();

            public Trie(string[] words)
            {
                foreach (var word in words) AddWord(word);
            }

            public List<string> GetLongestSubstrings(string input)
            {
                var result = new HashSet<string>();
                for (var i = 0; i < input.Length; ++i) FindSubstrings(i, _root);

                return result.ToList();

                void FindSubstrings(int idx, Node node)
                {
                    if (node.Word != null)
                    {
                        if (result.Count == 0 || node.Word.Length == result.First().Length) result.Add(node.Word);
                        else if (node.Word.Length > result.First().Length)
                        {
                            result.Clear();
                            result.Add(node.Word);
                        }
                    }
                    if (idx == input.Length) return;
                    if (node.Children.ContainsKey(input[idx])) FindSubstrings(idx + 1, node.Children[input[idx]]);                    
                }
            }

            private void AddWord(string word)
            {
                Add(0, _root);

                void Add(int idx, Node node)
                {
                    if (idx == word.Length) node.Word = word;
                    else
                    {
                        if (!node.Children.ContainsKey(word[idx])) node.Children.Add(word[idx], new Node());
                        Add(idx + 1, node.Children[word[idx]]);
                    }
                }
            }
        }

        class Node
        {
            public string Word { get; set; }
            public Dictionary<char, Node> Children { get; } = new Dictionary<char, Node>(26);
        }
    }
}
