using System;

namespace InterviewProblems.Utilities
{
    public static class ArrayUtilities
    {
        public static void PrintMatrix(int[][] grid, string separator = "\t")
        {
            foreach (var row in grid)
            {
                Console.WriteLine(string.Join(separator, row));
            }
            Console.Write('\n');
        }
    }
}
