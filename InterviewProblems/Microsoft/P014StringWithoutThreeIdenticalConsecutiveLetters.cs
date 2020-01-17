using System.Text;

namespace InterviewProblems.Microsoft
{
    public class P014StringWithoutThreeIdenticalConsecutiveLetters
    {
        public string MinStr(string str)
        {
            var result = new StringBuilder($"{str[0]}");

            for (var (i, count) = (1, 1); i < str.Length; ++i)
            {
                if (str[i] == str[i - 1]) ++count;
                else count = 1;

                if (count <= 2) result.Append(str[i]);
                else --count;
            }

            return result.ToString();
        }
    }
}
