namespace InterviewProblems.Microsoft
{
    public class P019MaxInsertsToObtainStringWithoutThreeConsecutivesAs
    {
        public int MaxInserts(string str)
        {
            var result = 0;

            for (var (i,count) = (0, 0); i < str.Length; ++i)
            {
                if (str[i] != 'a') result += 2;
                else
                {
                    while (i < str.Length && str[i] == 'a')
                    {
                        ++count;
                        ++i;
                    }
                    if (count == 1) ++result;
                    else if (count >= 3) return -1;
                    count = 0;
                }
            }

            if (str[str.Length - 1] != 'a') result += 2;
            return result;
        }
    }
}
