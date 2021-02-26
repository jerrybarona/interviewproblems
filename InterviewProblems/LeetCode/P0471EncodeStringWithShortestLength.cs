using System;
using System.Collections.Generic;

namespace InterviewProblems.LeetCode
{
	public class P0471EncodeStringWithShortestLength
	{
        public void EncodeTest()
		{
            foreach(var input in new [] { "abbbabbbcabbbabbbc", "aabcaabcd", "aaaaa" })
			{
                Console.WriteLine($"Input: {input}\nOutput: {Encode(input)}\n");
            }            
		}

        public string Encode(string s)
        {
            var map = new Dictionary<string, string>();

            return EncodeString(s);

            string EncodeString(string str)
			{
                if (string.IsNullOrEmpty(str)) return string.Empty;
                if (str.Length <= 4) return str;
                if (map.ContainsKey(str)) return map[str];

                var result = str;
                for (var i = str.Length/2; i < str.Length; ++i)
				{
                    var pattern = str[i..];
                    var times = CountRepeat(str, pattern);
                    if (times * pattern.Length != str.Length) continue;
                    var candidate = $"{times}[{EncodeString(pattern)}]";

                    if (candidate.Length < result.Length) result = candidate;
				}

                for (var i = 1; i < str.Length; ++i)
				{
                    var left = EncodeString(str[..i]);
                    var right = EncodeString(str[i..]);
                    var candidate = left + right;
                    
                    if (candidate.Length < result.Length) result = candidate;
				}

                map.Add(str, result);
                return result;
			}

            int CountRepeat(string s, string pattern)
            {
                int times = 0;
                while (s.Length >= pattern.Length)
                {
                    var sub = s[^pattern.Length..];
                    if (sub.Equals(pattern))
                    {
                        times++;
                        s = s[..^pattern.Length];
                    }
                    else return times;
                }
                return times;
            }
        }
    }
}
