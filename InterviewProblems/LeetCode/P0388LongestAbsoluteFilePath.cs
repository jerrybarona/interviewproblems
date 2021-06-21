using System;

namespace InterviewProblems.LeetCode
{
	public class P0388LongestAbsoluteFilePath
	{
        public void LengthLongestPathTest()
		{
            var input = "a\n\taa\n\t\taaa\n\t\t\tfile1.txt\naaaaaaaaaaaaaaaaaaaaa\n\tsth.png";
            Console.WriteLine($"Input: {input}\nANS: {LengthLongestPath(input)}");

            input = "dir\n\t        file.txt\n\tfile2.txt";
            Console.WriteLine($"Input: {input}\nANS: {LengthLongestPath(input)}");
        }

        public int LengthLongestPath(string input)
        {
            var (idx, len, level, isFile) = (0, -2, -2, false);
            return Longest(0, -1);

            int Longest(int prevLen, int prevLevel)
			{
                var result = 0;
                while (idx < input.Length)
				{
                    var currLevel = level != -2 ? level : GetTabCount();
                    var (currLen, isCurrFile) = len != -2 ? (len, isFile) : GetLen();

                    if (currLevel > prevLevel)
					{
                        (idx, len, level, isFile) = (idx + 1, -2, -2, false);
                        if (isCurrFile) result = Math.Max(result, prevLen + currLen);
                        else result = Math.Max(result, Longest(prevLen + currLen + 1, currLevel));
                    }
					else
					{
                        (len, level, isFile) = (currLen, currLevel, isCurrFile);
                        break;
					}
                }

                return result;
			}

            (int, bool) GetLen()
            {
                var c = 0;
                var f = false;
                for (; idx < input.Length && input[idx] != '\n'; ++idx, ++c)
                {
                    if (input[idx] == '.') f = true;
                }

                return (c, f);
            }

            int GetTabCount()
            {
                var t = 0;
                for (; idx < input.Length && input[idx] == '\t'; ++idx)
                {
                    ++t;
                }

                return t;
            }
        }
    }
}
