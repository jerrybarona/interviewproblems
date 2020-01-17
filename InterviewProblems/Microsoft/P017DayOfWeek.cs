using System.Collections.Generic;

namespace InterviewProblems.Microsoft
{
    public class P017DayOfWeek
    {
        private static readonly string[] _dayArr = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

        private static readonly Dictionary<string, int> _dayDict = new Dictionary<string, int>
            { { "Mon", 0 }, { "Tue", 1 }, { "Wed", 2 }, { "Thu", 3 }, { "Fri", 4 }, { "Sat", 5 }, { "Sun", 6 } };

        public string NextDay(string str, int k) => _dayArr[(_dayDict[str] + k) % 7];
    }
}
