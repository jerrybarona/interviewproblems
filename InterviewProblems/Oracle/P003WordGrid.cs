using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Oracle
{
    public class P003WordGrid
    {
        // https://leetcode.com/discuss/interview-experience/475709/Oracle-OCI-or-SDE-2-or-Pleasanton-or-Jan-2020-Reject

        public void GetWordGridsTest()
        {
            var input = new[] { "ATOM", "ACID", "PARK", "LACK", "MARK", "DARK", "POOL", "KIRK" };
            var result = GetWordGrids(input);

            Console.WriteLine("Input: " + string.Join(',', input));
            Console.WriteLine("Result: ");
            Console.Write("\n");

            foreach (var square in result)
            {
                square.Print();
            }
        }

        public List<Square> GetWordGrids(string[] strs)
        {
            if (strs.Length < 4) return new List<Square>();
            var result = new List<Square>();
            var square = new Square();
            var used = new HashSet<string>();

            var firstMap = strs.Aggregate(new Dictionary<char, List<string>>(), (dict, str) =>
            {
                if (!dict.ContainsKey(str[0])) dict.Add(str[0], new List<string>());
                dict[str[0]].Add(str);
                return dict;
            });

            var firstLastMap = strs.Aggregate(new Dictionary<(char, char), List<string>>(), (dict, str) =>
            {
                if (!dict.ContainsKey((str[0], str[str.Length - 1])))
                    dict.Add((str[0], str[str.Length - 1]), new List<string>());
                dict[(str[0], str[str.Length - 1])].Add(str);
                return dict;
            });

            MakeGrids();
            return result;

            void MakeGrids()
            {
                if (square.IsComplete())
                {
                    result.Add(new Square(square));
                    return;
                }

                if (square.Top == null)
                {
                    foreach (var str in strs)
                    {
                        if (!used.Contains(str))
                        {
                            square.Top = str;
                            used.Add(str);
                            MakeGrids();
                            used.Remove(str);
                        }
                    }
                }
                else if (square.Left == null)
                {
                    if (firstMap[square.Top[0]].Count > 1 && firstMap[square.Top[square.Top.Length - 1]].Count > 0)
                    {
                        foreach (var leftStr in firstMap[square.Top[0]])
                        {
                            if (!used.Contains(leftStr))
                            {
                                square.Left = leftStr;
                                used.Add(leftStr);
                                if (firstMap.ContainsKey(square.Top[square.Top.Length - 1]))
                                {
                                    foreach (var rightStr in firstMap[square.Top[square.Top.Length - 1]])
                                    {
                                        if (!used.Contains(rightStr))
                                        {
                                            square.Right = rightStr;
                                            used.Add(rightStr);
                                            MakeGrids();
                                            used.Remove(rightStr);
                                            square.Right = null;
                                        }
                                    }
                                }
                                used.Remove(leftStr);
                                square.Left = null;
                            }
                        }
                    }
                }
                else
                {
                    if (firstLastMap.ContainsKey((square.Left[square.Left.Length - 1],
                        square.Right[square.Right.Length - 1])))
                    {
                        foreach (var str in firstLastMap[(square.Left[square.Left.Length - 1],
                            square.Right[square.Right.Length - 1])])
                        {
                            if (!used.Contains(str))
                            {
                                square.Bottom = str;
                                MakeGrids();
                                square.Bottom = null;
                            }
                        }
                    }
                }
            }
        }

        public class Square
        {
            public string Top { get; set; }
            public string Left { get; set; }
            public string Right { get; set; }
            public string Bottom { get; set; }

            public Square()
            {
                
            }

            public Square(Square square)
            {
                Top = square.Top;
                Left = square.Left;
                Right = square.Right;
                Bottom = square.Bottom;
            }

            public bool IsComplete() =>
                new[] { Top, Bottom, Left, Right }.All(side => !string.IsNullOrEmpty(side));

            public void Print()
            {
                for (var i = 0; i < Top.Length; ++i)
                {
                    if (i == 0) Console.WriteLine(Top);
                    else if (i == Top.Length - 1) Console.WriteLine(Bottom);
                    else
                    {
                        Console.WriteLine($"{Left[i]}{new string(' ', Top.Length - 2)}{Right[i]}");
                    }
                }
                Console.Write("\n");
            }
        }
    }
}
