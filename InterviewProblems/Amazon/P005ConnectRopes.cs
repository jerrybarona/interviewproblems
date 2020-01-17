using System;

namespace InterviewProblems.Amazon
{
    public class P005ConnectRopes
    {
        // https://aonecode.com/amazon-online-assessment-questions
        public int Cost(int[] ropes)
        {
            Array.Sort(ropes);
            var result = 0;
            var curr = ropes[0];
            for (var i = 1; i < ropes.Length; ++i)
            {
                curr += ropes[i];
                result += curr;
            }

            return result;
        }
    }
}
