using System;

namespace InterviewProblems.LeetCode
{
	public class P0678ValidParenthesisString
	{
        public void CheckValidStringTest()
		{
            Console.WriteLine(CheckValidString("(*(**)))))(*))(**(*)(*)()"));
		}

        public bool CheckValidString(string s)
        {
            var (stars, count) = (0, 0);

            for (var i = 0; i < s.Length; ++i)
            {
                if (s[i] == '(') ++count;
                else if (s[i] == ')')
                {
                    if (count > 0) --count;
                    else if (stars > 0) --stars;
                    else return false;
                }
                else ++stars;
            }
            Console.WriteLine(count);
            if (count == 0) return true;
            if (count > 0 && stars < count) return false;

            //(stars, count) = (0, 0);
            stars = 0;
            for (var i = s.Length - 1; i >= 0 && count > 0; --i)
            {
                if (s[i] == '*') ++stars;
                else if (s[i] == '(')
                {
                    if (stars > 0)
                    {
                        --stars;
                        --count;
                    }
                    else return false;
                }
            }

            return true;
        }
    }
}
