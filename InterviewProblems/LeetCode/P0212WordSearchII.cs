using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0212WordSearchII
    {
        public IList<string> FindWords(char[][] board, string[] words)
        {
            var memo = board.Select(row => row.Select(cell => false).ToArray()).ToArray();
            var trie = CreateTrie();
            var result = new List<string>();

            for (var i = 0; i < board.Length; ++i)
            {
                for (var j = 0; j < board[0].Length; ++j)
                {
                    if (trie.Children.Any()) Search(i, j, trie);
                }
            }

            return result;

            void Search(int r, int c, Node node)
            {
                if (node.Searched != null)
                {
                    result.Add(node.Searched);
                    RemoveFromTrie(node.Searched);
                    return;
                }
                if (r < 0 || c < 0 || r >= board.Length || c >= board[0].Length ||
                    memo[r][c] || !node.Children.ContainsKey(board[r][c])) return;

                var child = node.Children[board[r][c]];
                memo[r][c] = true;
                Search(r + 1, c, child);
                Search(r, c + 1, child);
                Search(r - 1, c, child);
                Search(r, c - 1, child);
                memo[r][c] = false;
            }

            Node CreateTrie()
            {
                var t = new Node();
                foreach (var w in words) Insert(0, w, t);
                return t;

                void Insert(int idx, string word, Node node)
                {
                    if (idx == word.Length) node.Searched = word;
                    else
                    {
                        if (!node.Children.ContainsKey(word[idx])) node.Children.Add(word[idx], new Node());
                        Insert(idx + 1, word, node.Children[word[idx]]);
                    }
                }
            }

            void RemoveFromTrie(string word)
            {
                Remove(0, trie);

                void Remove(int idx, Node node)
                {
                    if (idx == word.Length) node.Searched = null;
                    else
                    {
                        Remove(idx + 1, node.Children[word[idx]]);
                        if (node.Children[word[idx]].Children.Count == 0) node.Children.Remove(word[idx]);
                    }
                }
            }
        }

        public class Node
        {
            public string Searched { get; set; } = null;
            public Dictionary<char, Node> Children { get; } = new Dictionary<char, Node>(26);
        }
    }
}
