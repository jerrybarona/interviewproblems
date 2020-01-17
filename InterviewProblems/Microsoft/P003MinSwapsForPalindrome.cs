using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblems.Microsoft
{
    public class P003MinSwapsForPalindrome
    {
        public int MinimumSwaps(string str)
        {
            var list = new LinkedList<char>(str);
            return MinSwaps();

            int MinSwaps()
            {
                if (list.Count == 1) return 0;
                if (list.Count == 2) return list.First.Value == list.Last.Value ? 0 : -1;

                var swaps = 0;
                if (list.First.Value != list.Last.Value)
                {
                    var first = list.First;
                    var last = list.Last;
                    var firstVal = list.First.Value;
                    var lastVal = list.Last.Value;

                    var swapsFirst = 0;
                    var currFirst = first;
                    for ( ; currFirst != null && currFirst.Value != lastVal || currFirst == last; ++swapsFirst, currFirst = currFirst.Next) ;

                    var swapsLast = 0;
                    var currLast = last;
                    for ( ; currLast != null && currLast.Value != firstVal || currLast == first; ++swapsLast, currLast = currLast.Previous) ;

                    if (swapsFirst == swapsLast && swapsFirst == list.Count) return -1;

                    if (swapsFirst <= swapsLast)
                    {
                        list.Remove(currFirst);
                        list.RemoveLast();
                    }
                    else
                    {
                        list.Remove(currLast);
                        list.RemoveFirst();
                    }

                    swaps = Math.Min(swapsFirst, swapsLast);                    
                }
                else
                {
                    list.RemoveFirst();
                    list.RemoveLast();
                }

                var nextResult = MinSwaps();
                return nextResult == -1 ? -1 : swaps + nextResult;
            }
        }
    }
}
