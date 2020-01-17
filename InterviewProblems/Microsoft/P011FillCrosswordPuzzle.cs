using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.Microsoft
{
    public class P011FillCrosswordPuzzle
    {
        public bool SolveCrossword(char[][] matrix, string[] words)
        {
            var m = matrix.Length;
            var n = matrix[0].Length;
            var wordLenMap = words.Aggregate(new Dictionary<int, List<string>>(), (dict, word) =>
            {
                if (!dict.ContainsKey(word.Length)) dict.Add(word.Length, new List<string>());
                dict[word.Length].Add(word);
                return dict;
            });

            var wordLengths = wordLenMap.OrderBy(x => x.Value.Count).Select(y => y.Key).ToArray();

            var matrixWordMap = new Dictionary<int, List<WordRef>>();
            for (var i = 0; i < m; i++)
            {
                for (var (start, end) = (0, 0); end <= n; ++end)
                {
                    if (end < n && matrix[i][end] == '#') continue;
                    var currLen = end - start;
                    if (currLen > 0)
                    {
                        if (!matrixWordMap.ContainsKey(currLen)) matrixWordMap.Add(currLen, new List<WordRef>());
                        matrixWordMap[currLen].Add(new WordRef(currLen, i, start, true));
                    }

                    start = end + 1;
                }
            }

            for (var i = 0; i < n; i++)
            {
                for (var (start, end) = (0, 0); end <= m; ++end)
                {
                    if (end < m && matrix[end][i] == '#') continue;
                    var currLen = end - start;
                    if (currLen > 0)
                    {
                        if (!matrixWordMap.ContainsKey(currLen)) matrixWordMap.Add(currLen, new List<WordRef>());
                        matrixWordMap[currLen].Add(new WordRef(currLen, start, i, false));
                    }

                    start = end + 1;
                }
            }

            return Solve(0);

            bool Solve(int idx)
            {
                if (idx == wordLengths.Length) return true; 

                var len = wordLengths[idx];
                if (wordLenMap[len].Count == 0) return Solve(idx + 1);

                for (var i = 0; i < wordLenMap[len].Count; i++)
                {
                    var word = wordLenMap[len][i];
                    wordLenMap[len].RemoveAt(i);
                    if (matrixWordMap[len].Count == 0) return false;
                    for (var j = 0; j < matrixWordMap[len].Count; ++j)
                    {
                        var wordRef = matrixWordMap[len][j];
                        matrixWordMap[len].RemoveAt(j);
                        var currWord = GetWord(len, wordRef.R, wordRef.C, wordRef.IsHorizontal);
                        var canPlace = true;
                        for (var k = 0; k < len; ++k)
                        {
                            if (currWord[k] == '#' || currWord[k] == word[k]) continue;

                            canPlace = false;
                            break;
                        }

                        if (canPlace)
                        {
                            SetWord(word, wordRef.R, wordRef.C, wordRef.IsHorizontal);
                            if (Solve(idx)) return true;
                            SetWord(currWord, wordRef.R, wordRef.C, wordRef.IsHorizontal);
                        }
                        
                        matrixWordMap[len].Insert(j, wordRef);
                    }
                    wordLenMap[len].Insert(i, word);
                }

                return false;
            }

            string GetWord(int len, int r, int c, bool isHorizontal)
            {
                var w = new StringBuilder();
                for (var i = 0; i < len; ++i) w.Append(isHorizontal ? matrix[r][c + i] : matrix[r + i][c]);
                return w.ToString();
            }

            void SetWord(string w, int r, int c, bool isHorizontal)
            {
                for (var i = 0; i < w.Length; ++i)
                {
                    if (isHorizontal) matrix[r][c + i] = w[i];
                    else matrix[r + i][c] = w[i];
                }
            }
        }


        class WordRef
        {
            public int Len { get; }
            public int R { get; }
            public int C { get; }
            public bool IsHorizontal { get; }
            public bool IsUsed { get; set; }

            public WordRef(int len, int r, int c, bool isHorizontal)
            {
                Len = len;
                R = r;
                C = c;
                IsHorizontal = isHorizontal;
            }
        }
    }
}
