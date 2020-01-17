using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.Microsoft
{
    public class P008JumpGame
    {
        public bool JumpGame(int[] arr, int start)
        {
            var failed = new HashSet<int>();
            return CanReach0(start);

            bool CanReach0(int idx)
            {
                if (idx < 0 || idx >= arr.Length || failed.Contains(idx)) return false;
                if (arr[idx] == 0) return true;

                failed.Add(idx);
                if (CanReach0(idx - arr[idx]) || CanReach0(idx + arr[idx]))
                {
                    failed.Remove(idx);
                    return true;
                }

                return false;
            }   
        }
        


    }
}
