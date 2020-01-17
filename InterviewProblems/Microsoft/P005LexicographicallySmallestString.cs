namespace InterviewProblems.Microsoft
{
    public class P005LexicographicallySmallestString
    {
        public string Smallest(string str)
        {
            var i = 0;
            for ( ; i < str.Length - 1; ++i)
            {
                if (str[i] > str[i+1]) break;
            }

            if (i == str.Length) return str.Substring(0, str.Length - 1);
            return str.Substring(0, i) + str.Substring(i + 1);
        }
    }
}
