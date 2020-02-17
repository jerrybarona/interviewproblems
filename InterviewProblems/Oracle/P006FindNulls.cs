using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.Oracle
{
    public class P006FindNulls
    {
        public void FindNullsTest()
        {
            var input = new Dictionary<string, object>
            {
                {"Jon", "Smith" },
                {"Adam", new[] {"Jake", null, "Nancy"} },
                {"Alex", new Dictionary<string,object>
                {
                    {"Muller", new[]{null, "Sam"} },
                    {"Phil", null },
                    {"Xav", new[]{"Mike", "Tom"} }
                } },
                {"Lex", null }
            };

            var result = FindNulls(input);
            Console.WriteLine($"[{string.Join(", ", result)}]");
        }        

        public string[] FindNulls(Dictionary<string,object> input)
        {
            var result = new List<List<string>>();
            var sequence = new List<string>();
            Dfs(input);

            return result.Select(x => string.Join('.', x)).ToArray();

            void Dfs(Dictionary<string, object> dict)
            {
                foreach (var item in dict)
                {
                    switch (item.Value)
                    {
                        case null:
                            sequence.Add(item.Key);
                            result.Add(new List<string>(sequence));
                            sequence.RemoveAt(sequence.Count - 1);
                            break;
                        case string[] arr:
                            {
                                for (int i = 0; i < arr.Length; i++)
                                {
                                    if (arr[i] == null)
                                    {
                                        sequence.Add($"{item.Key}.{i}");
                                        result.Add(new List<string>(sequence));
                                        sequence.RemoveAt(sequence.Count - 1);
                                    }
                                }

                                break;
                            }
                        case Dictionary<string, object> d:
                            sequence.Add(item.Key);
                            Dfs(d);
                            sequence.RemoveAt(sequence.Count - 1);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
