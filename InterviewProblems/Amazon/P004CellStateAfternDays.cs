﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.Amazon
{
    public class P004CellStateAfternDays
    {
        public int[] NextState(int[] input, int k)
        {
            var state = GetIntState();

            for (var i = 0; i < k; ++i)
            {
                var l = state << 1;
                var r = state >> 1;
                state = l ^ r;
            }

            return GetArrState();

            int GetIntState() => input.Aggregate(0, (res, x) => (res << 1) | x);
            int[] GetArrState() =>
                Enumerable.Range(0, 8).Select(x => (state >> (7 - x)) & 1).ToArray();
        }
    }
}
