using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewProblems.Others
{
    // https://github.com/jesus-seijas-sp/hand-challenge
    public class P01HandChallenge
    {
        public void HandChallengeTest()
        {
            foreach (var program in
                new[] { "👇🤜👇👇👇👇👇👇👇👉👆👈🤛👉👇👊👇🤜👇👉👆👆👆👆👆👈🤛👉👆👆👊👆👆👆👆👆👆👆👊👊👆👆👆👊",
                    "👉👆👆👆👆👆👆👆👆🤜👇👈👆👆👆👆👆👆👆👆👆👉🤛👈👊👉👉👆👉👇🤜👆🤛👆👆👉👆👆👉👆👆👆🤜👉🤜👇👉👆👆👆👈👈👆👆👆👉🤛👈👈🤛👉👇👇👇👇👇👊👉👇👉👆👆👆👊👊👆👆👆👊👉👇👊👈👈👆🤜👉🤜👆👉👆🤛👉👉🤛👈👇👇👇👇👇👇👇👇👇👇👇👇👇👇👊👉👉👊👆👆👆👊👇👇👇👇👇👇👊👇👇👇👇👇👇👇👇👊👉👆👊👉👆👊" })
            {
                Console.WriteLine(GetMessage(program));
            }
        }

        public string GetMessage(string input)
        {
            var result = new StringBuilder();
            var len = input.Length;
            var cells = new LinkedList<byte>(new byte[] { 0 });
            var curr = cells.First;
            var idx = 0;
            Traverse(0);

            return result.ToString();

            void Traverse(int i)
            {
                for (; idx < len && input[idx..(idx+2)] != "🤛"; idx += 2)
                {
                    var sign = input[idx..(idx + 2)];
                    if (sign == "👆")
                    {
                        curr.Value = (byte)(curr.Value == 255 ? 0 : curr.Value + 1);
                    }
                    else if (sign == "👇")
                    {
                        curr.Value = (byte)(curr.Value == 0 ? 255 : curr.Value - 1);
                    }
                    else if (sign == "👉")
                    {
                        if (curr.Next == null)
                        {
                            cells.AddAfter(curr, 0);
                        }
                        curr = curr.Next;
                    }
                    else if (sign == "👈")
                    {
                        if (curr.Previous == null)
                        {
                            cells.AddBefore(curr, 0);
                        }
                        curr = curr.Previous;
                    }
					else if (sign == "👊")
					{
                        result.Append(Convert.ToChar(curr.Value));
                    }
                    else if (sign == "🤜")
                    {
                        if (curr.Value == 0)
                        {
                            for (var close = 0; idx < len; idx += 2)
                            {
                                if (input[idx..(idx + 2)] == "🤜")
                                {
                                    ++close;
                                    continue;
                                }
                                
                                if (input[idx..(idx + 2)] == "🤛")
                                {
                                    --close;
                                }

                                if (close == 0) break;
                            }
                        }
                        else
                        {
                            var currIdx = idx;
                            idx += 2;
                            Traverse(currIdx);
                        }
                    }
                }
                
                if (idx < len && curr.Value != 0)
                {
                    idx = i-2;
                }
            }
        }
    }
}
