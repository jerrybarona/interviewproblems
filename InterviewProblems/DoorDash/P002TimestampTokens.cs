using System;
using System.Collections.Generic;

namespace InterviewProblems.DoorDash
{
    internal class P002TimestampTokens : ITestable
    {
        public void RunTest()
        {
            foreach (var (start,end,interval) in new (string, string, int)[]
            {
                //("Mon 10:12 AM", "Mon 10:00 PM", 5),
                //("Mon 10:12 AM", "Mon 10:00 PM", 59),
                ("Sat 10:12 PM", "Mon 10:00 AM", 59),
            })
            {
                Console.WriteLine(string.Join(", ", Tokens(start,end,interval)));
            }
        }

        public IList<string> Tokens(string start, string end, int interval)
        {
            var s = Parse(start);
            var e = Parse(end);
            if (!IsEndGreaterOrEqual(s, e))
            {
                e = (e.day + 7, e.hour, e.min);
            }

            var result = new List<string>();
            var rday = s.day;
            var rhour = s.hour;
            var rmin = s.min;
            bool done = false;
            for (; rday <= e.day; rday++)
            {
                for (; rhour < 24; rhour++)
                {
                    for (; rmin < 60; rmin += interval)
                    {
                        if (IsEndGreaterOrEqual((rday,rhour,rmin), e))
                        {
                            result.Add(Format(rday,rhour,rmin));
                        }
                        else
                        {
                            done = true;
                            break;
                        }
                    }

                    if (done) break;
                    rmin = rmin % 60;
                }

                if (done) break;
                rhour = rhour % 24;
            }

            return result;

            string Format(int day, int hour, int min)
            {
                return $"{day % 7}{hour:D2}{min:D2}";
            }

            bool IsEndGreaterOrEqual((int day, int hour, int min) s, (int day, int hour, int min) e)
            {
                if (e.day != s.day) return e.day > s.day;
                if (e.hour != s.hour) return e.hour > s.hour;
                return e.min >= s.min;
            }

            (int day,int hour,int min) Parse(string str)
            {
                string[] part = str.Split(new char[] { ' ', ':' });
                int day = 0;
                switch (part[0])
                {
                    case "Mon":
                        day = 1; break;
                    case "Tue":
                        day = 2; break;
                    case "Wed":
                        day = 3; break;
                    case "Thu":
                        day = 4; break;
                    case "Fri":
                        day = 5; break;
                    case "Sat":
                        day = 6; break;
                    case "Sun":
                        day = 7; break;
                    default:
                        throw new NotImplementedException();
                }

                int hour = int.Parse(part[1]);
                if (part[3] == "AM" && hour == 12) hour = 0;
                else if (part[3] == "PM" && hour < 12) hour += 12;

                return (day, hour, int.Parse(part[2]));
            }
        }
    }
}
