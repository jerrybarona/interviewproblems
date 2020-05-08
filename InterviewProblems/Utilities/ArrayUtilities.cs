using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.Utilities
{
    public class ArrayUtilities
    {
        public void PrintMatrix(int[][] grid)
        {
            foreach (var row in grid)
            {
                Console.WriteLine(string.Join('\t', row));
            }
            Console.Write('\n');
        }
    }
}
