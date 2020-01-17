using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.LeetCode
{
    public class P0127WordLadder2
    {
        public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
        {
            var ladders = new List<IList<string>>();
            var wordListSet = wordList.ToHashSet();
            if (!wordListSet.Contains(endWord)) return ladders;
            var graph = new Dictionary<string,List<string>>();
            var wordMappings = GetWordMappings();
            var beginQueue = new HashSet<string>(new[] { beginWord });
            var endQueue = new HashSet<string>(new[] { endWord });

            while (beginQueue.Count > 0 && endQueue.Count > 0)
            {
                var foundEndWord = beginQueue.Count <= endQueue.Count ? Bfs(true) : Bfs(false);
                if (foundEndWord) break;
            }

            var seen = new HashSet<string>(new[] { beginWord });
            
            var ladder = new List<string>(new [] { beginWord });
            Dfs(beginWord);

            return ladders;

            void Dfs(string word)
            {
                if (string.Equals(word, endWord))
                {
                    ladders.Add(new List<string>(ladder));
                    return;
                }
                
                if (graph.ContainsKey(word))
                {
                    foreach (var nextWord in graph[word])
                    {
                        if (!seen.Contains(nextWord))
                        {
                            seen.Add(nextWord);
                            ladder.Add(nextWord);
                            Dfs(nextWord);
                            ladder.RemoveAt(ladder.Count - 1);
                            seen.Remove(nextWord);
                        }
                    }
                }
            }

            bool Bfs(bool isForward)
            {
                var queue = isForward ? beginQueue : endQueue;
                var otherQueue = queue == beginQueue ? endQueue : beginQueue;
                wordListSet.RemoveWhere(queue.Contains);
                var nextQueue = new HashSet<string>();
                var foundEnd = false;
                foreach (var word in queue)
                {
                    foreach (var key in GetWordKeys(word).Where(k => wordMappings.ContainsKey(k)))
                    {
                        foreach (var nextWord in wordMappings[key])
                        {
                            if (otherQueue.Contains(nextWord)) foundEnd = true;
                            if (wordListSet.Contains(nextWord))
                            {
                                if (isForward)
                                {
                                    if (!graph.ContainsKey(word)) graph.Add(word, new List<string>());
                                    graph[word].Add(nextWord);
                                }
                                else
                                {
                                    if (!graph.ContainsKey(nextWord)) graph.Add(nextWord, new List<string>());
                                    graph[nextWord].Add(word);
                                }

                                nextQueue.Add(nextWord);
                            }
                        }
                    }
                }

                if (isForward) beginQueue = nextQueue;
                else endQueue = nextQueue;

                return foundEnd;
            }

            Dictionary<string, List<string>> GetWordMappings()
            {
                var dict = new Dictionary<string,List<string>>();

                foreach (var word in wordList)
                {
                    foreach (var key in GetWordKeys(word))
                    {
                        if (!dict.ContainsKey(key)) dict.Add(key, new List<string>());
                        dict[key].Add(word);
                    }
                }

                return dict;
            }

            IEnumerable<string> GetWordKeys(string word) =>
                word.Select((chr, i) => $"{word.Substring(0, i)}_{word.Substring(i + 1)}");
        }
    }
}
