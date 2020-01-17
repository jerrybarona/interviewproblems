namespace InterviewProblems.Microsoft
{
    public class P001MinMovesToObtainStringWithoutConsecutiveLetters
    {
        public int MinMoves(string str)
        {
            var result = 0;
            for (var (i,count) =(1,1); i <= str.Length; ++i)
            {
                if (i < str.Length && str[i] == str[i - 1]) ++count;
                else
                {
                    if (count >= 3) result += count / 3;
                    count = 1;
                }
            }

            return result;
        }
    }
}
