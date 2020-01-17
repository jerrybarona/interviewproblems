namespace InterviewProblems.Microsoft
{
    public class P021OnlineEval
    {
        public int FirstInteger(int[] A)
        {
            for (var i = 0; i < A.Length; ++i)
            {
                while (A[i] >= 1 && A[i] <= A.Length && A[i] != i + 1 && A[i] != A[A[i] - 1])
                {
                    var tmp = A[i];
                    A[i] = A[A[i] - 1];
                    A[tmp - 1] = tmp;
                }
            }

            for (var i = 0; i < A.Length; ++i)
            {
                if (i + 1 != A[i]) return i + 1;
            }

            return A.Length + 1;
        }
    }
}
