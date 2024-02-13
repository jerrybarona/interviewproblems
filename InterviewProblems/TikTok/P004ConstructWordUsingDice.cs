using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.TikTok
{
    public class P004ConstructWordUsingDice : ITestable
    {
        // https://leetcode.com/discuss/interview-question/267985/
        // https://leetcode.com/discuss/interview-question/3798965/TikTok-or-Remote-or-N-dices-(6-sides-of-char)-and-dictionary-of-M-words
        
        public void RunTest()
        {
            foreach (var (word, dice) in new (string, string[])[]
            {
                ("hello", new[]{"alcdef","abcdef","abchef","abcdof","abclef"}),
                ("hello", new[]{"abcdef","abcdef","abcdef","abcdef","abcdef"}),
                ("aaaa", new[]{"aaaaaa","bbbbbb","abcdef","abcdef"}),
                ("abcdef", new[]{"bcwxyz","uvwxyz","adwxyz","cvwxyz","cdwxyz","fvwxyz"})
            })
            {
                Console.WriteLine(CanBeConstructed(word, dice));
            }
        }

        // Using Ford-Fulkerson (Edmonds Karp) algorithm for max flow
        public bool CanBeConstructed(string word, string[] dice)
        {
            var n = dice.Length;
            var len = 2 * n + 2;
            var graph = Enumerable.Repeat(0, len).Select(x => new int[len]).ToArray();
            var diceMap = Enumerable.Range(0, dice.Length).Aggregate(new List<int>[26], (arr, idx) =>
            {
                var die = dice[idx];
                foreach (var side in die)
                {
                    var i = side - 'a';
                    if (arr[i] == null) arr[i] = new List<int>();
                    arr[i].Add(idx);
                }

                return arr;
            });

            var sourceIdx = 2 * n;
            var sinkIdx = sourceIdx + 1;

            for (var i = 0; i < n; ++i)
            {
                graph[sourceIdx][i] = 1;
                graph[n + i][sinkIdx] = 1;
                var letterIdx = word[i] - 'a';
                if (diceMap[letterIdx] == null) continue;
                foreach (var dieIdx in diceMap[letterIdx])
                {
                    graph[dieIdx][n + i] = 1;
                }
            }

            var result = 0;
            for (var pathFlow = Bfs(); pathFlow > 0; pathFlow = Bfs())
            {
                result += pathFlow;
            }

            Console.WriteLine($"Max flow: {result}");
            return result == word.Length;

            int Bfs()
            {
                var seen = new bool[len];
                seen[sourceIdx] = true;
                var parent = Enumerable.Repeat(-1, len).ToArray();
                bool foundAugPath = false;

                for (var queue = new Queue<int>(new[] { sourceIdx }); queue.Count > 0; )
                {
                    var x = queue.Dequeue();

                    for (var i = 0; i < len; ++i)
                    {
                        if (i == x || graph[x][i] == 0 || seen[i])
                        {
                            continue;                            
                        }

                        parent[i] = x;
                        if (i == sinkIdx)
                        {
                            foundAugPath = true;
                            break;
                        }

                        seen[i] = true;
                        queue.Enqueue(i);
                    }

                    if (foundAugPath) break;
                }

                if (!foundAugPath) return 0;
                
                var min = int.MaxValue;
                for (var idx = sinkIdx; idx != sourceIdx;)
                {
                    var p = parent[idx];
                    min = Math.Min(min, graph[p][idx]);
                    idx = p;
                }

                for (var idx = sinkIdx; idx != sourceIdx;)
                {
                    var p = parent[idx];
                    graph[p][idx] -= min;
                    graph[idx][p] += min;

                    idx = p;
                }

                return min;
            }
        }

        // Using backtracking
        public bool CanBeConstructed2(string word, string[] dice)
        {
            //int wordMap = word.Aggregate(0, (res, c) => res | 1 << (c - 'a'));
            var diceMap = Enumerable.Range(0, dice.Length).Aggregate(new List<int>[26], (arr, idx) =>
            {
                var die = dice[idx];
                foreach (var side in die)
                {
                    var i = side - 'a';
                    if (arr[i] == null) arr[i] = new List<int>();
                    arr[i].Add(idx);
                }

                return arr;
            });

            var used = new HashSet<int>();
            return Dfs();


            bool Dfs(int idx = 0)
            {
                if (idx == word.Length) return true;

                var letter = word[idx] - 'a';
                var matchingDice = diceMap[letter];
                
                if (matchingDice == null) return false;
                
                foreach (var matchingDie in matchingDice)
                {
                    if (used.Contains(matchingDie)) continue;

                    used.Add(matchingDie);
                    if (Dfs(idx + 1)) return true;
                    used.Remove(matchingDie);
                }

                return false;
            }
        }


    }
}
