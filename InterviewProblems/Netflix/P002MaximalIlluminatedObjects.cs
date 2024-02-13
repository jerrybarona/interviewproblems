using System;

namespace InterviewProblems.Netflix
{
    public class P002MaximalIlluminatedObjects : ITestable
    {
        // https://leetcode.com/discuss/interview-question/2454448/OA-SDE-1-or-Netflix-or-max-object-illuminated

        public void RunTest()
        {
            foreach (var (objs, radius) in new (int[], int)[]
            {
                (new[]{-5, 3, 4, 9}, 5),
                (new []{-2,4,5,6,7}, 1)
            })
            {
                Console.WriteLine(GetCoordinate(objs, radius));
            }
        }

        public int GetCoordinate(int[] objs, int radius)
        {
            if (objs.Length == 0 || radius == 0) return 0;

            var diam = radius * 2;
            var maxCount = 1;
            var maxLast = 0;            

            for (var (s,e) = (0,1); e < objs.Length; e++)
            {
                if (objs[e] - objs[s] <= diam)
                {
                    var objCount = e - s + 1;
                    if (objCount > maxCount)
                    {
                        maxCount = objCount;
                        maxLast = objs[e];
                    }
                }
                else
                {
                    s = e;
                }
            }

            return maxLast - radius;            
        }
    }
}
