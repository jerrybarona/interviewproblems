using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0425WordSquares
    {
        public void WordSquaresTest()
        {
            var words = new[] { "area", "lead", "wall", "lady", "ball" };
            Console.WriteLine("input 1: " + string.Join(',', words));
            Console.WriteLine("Result:");
            var result = WordSquares(words);

            foreach (var square in result)
            {
                foreach (var row in square)
                {
                    Console.WriteLine(row);
                }
                Console.Write("\n");
            }

            words = new[] { "abat", "baba", "atan", "atal" };
            Console.WriteLine("input 2: " + string.Join(',', words));
            Console.WriteLine("Result:");
            result = WordSquares(words);

            foreach (var square in result)
            {
                foreach (var row in square)
                {
                    Console.WriteLine(row);
                }
                Console.Write("\n");
            }
        }

        public IList<IList<string>> WordSquares(string[] words)
        {
            var len = words[0].Length;
            var trie = new Trie(words);
            var square = Enumerable.Repeat(0, len).Select(x => new char[len]).ToArray();
            var result = new List<IList<string>>();

            MakeSquares(0);
            return result;

            void MakeSquares(int rowIdx)
            {
                if (rowIdx == len)
                {
                    result.Add(new List<string>(square.Select(row => new string(row))));
                    return;
                }

                var rowPrefix = rowIdx == 0 ? string.Empty :
                    string.Concat(Enumerable.Range(0, rowIdx)
                        .Select(x => square[rowIdx][x]));

                var candidates = trie.GetWordsForPrefix(rowPrefix);
                foreach (var candidate in candidates)
                {
                    var candidatefitsSquare = true;
                    PopulateRow(candidate, rowIdx);
                    for (var i = rowIdx; i < len; ++i)
                    {
                        var prefix = string.Concat(Enumerable.Range(0, rowIdx + 1).Select(x => square[x][i]));
                        if (!trie.PrefixExists(prefix))
                        {
                            candidatefitsSquare = false;
                            break;
                        }
                    }
                    if (!candidatefitsSquare) continue;
                    //trie.RemoveWord(candidate);
                    MakeSquares(rowIdx + 1);
                    //trie.AddWord(candidate);
                }
            }

            void PopulateRow(string word, int idx)
            {
                square[idx][idx] = word[idx];
                for (var i = idx+1; i < word.Length; ++i)
                {
                    square[i][idx] = word[i];
                    square[idx][i] = word[i];
                }
            }

        }

        class Trie
        {
            private readonly Node _root = new Node();

            public Trie(string[] words)
            {
                foreach (var word in words) AddWord(word);
            }

            public bool PrefixExists(string prefix)
            {
                return Exists(0, _root);

                bool Exists(int idx, Node node)
                {
                    if (idx == prefix.Length) return true;
                    return node.Children.ContainsKey(prefix[idx]) && Exists(idx + 1, node.Children[prefix[idx]]);
                }
            }

            public List<string> GetWordsForPrefix(string prefix)
            {
                var result = new List<string>();
                var root = GetNode(0, _root);
                GetWords(root);
                return result;

                void GetWords(Node node)
                {
                    if (node.Word != null) result.Add(node.Word);
                    else
                    {
                        foreach (var nodeChild in node.Children.Values) GetWords(nodeChild);
                    }
                }

                Node GetNode(int idx, Node node)
                {
                    if (idx == prefix.Length) return node;
                    return GetNode(idx + 1, node.Children[prefix[idx]]);
                }
            }

            public void RemoveWord(string word)
            {
                Remove(0, _root);

                void Remove(int idx, Node node)
                {
                    if (idx == word.Length) node.Word = null;
                    else
                    {
                        Remove(idx + 1, node.Children[word[idx]]);
                        if (node.Children[word[idx]].Children.Count == 0) node.Children.Remove(word[idx]);
                    }
                }
            }

            public void AddWord(string word)
            {
                Add(0, _root);

                void Add(int idx, Node node)
                {
                    if (idx == word.Length) node.Word = word;
                    else
                    {
                        if (!node.Children.ContainsKey(word[idx])) node.Children.Add(word[idx],new Node());
                        Add(idx + 1, node.Children[word[idx]]);
                    }
                }
            }
        }

        class Node
        {
            public string Word { get; set; }
            public Dictionary<char,Node> Children { get; set; } = new Dictionary<char, Node>(26);
        }
    }
}
