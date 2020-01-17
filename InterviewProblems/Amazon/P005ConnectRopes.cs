﻿using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.Amazon
{
    public class P005ConnectRopes
    {
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
