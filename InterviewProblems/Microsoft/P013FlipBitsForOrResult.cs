namespace InterviewProblems.Microsoft
{
    public class P013FlipBitsForOrResult
    {
        public int FlipBits(uint a, uint b, uint c)
        {
            uint result = 0;
            for (; a > 0 || b > 0 || c > 0; a >>= 1, b >>= 1, c >>= 1)
            {
                if (((a & 1) | (b & 1)) == (c & 1)) continue;
                if ((c & 1) == 1) ++result;
                else result += (a & 1) + (b & 1);
            }

            return (int) result;
        }
    }
}
