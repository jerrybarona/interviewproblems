using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblems.TikTok
{
    internal class P012MoveColoredCarsToColoredParkingSpots : ITestable
    {
        // https://leetcode.com/discuss/interview-question/3860327/TikTok-Senior-Interview-Quesion

        public void RunTest()
        {
            var cars1 = new Color[4];
            cars1[(int)Color.NoColor] = Color.NoColor;
            cars1[(int)Color.Green] = Color.Yellow;
            cars1[(int)Color.Yellow] = Color.Red;
            cars1[(int)Color.Red] = Color.Green;

            var parkingSpots1 = new Dictionary<Color, List<Color>>();
            parkingSpots1[Color.NoColor] = new List<Color> { Color.Red, Color.Green };
            parkingSpots1[Color.Green] = new List<Color> { Color.NoColor, Color.Yellow };
            parkingSpots1[Color.Yellow] = new List<Color> { Color.Green, Color.Red };
            parkingSpots1[Color.Red] = new List<Color> { Color.Yellow, Color.NoColor };

            var cars2 = new Color[4];
            cars2[(int)Color.NoColor] = Color.NoColor;
            cars2[(int)Color.Green] = Color.Green;
            cars2[(int)Color.Yellow] = Color.Yellow;
            cars2[(int)Color.Red] = Color.Red;

            var cars3 = new Color[4];
            /*
                                                  (Yellow Spot [Green Car])---(Green Spot [Red Car])
												 /                                               \
								                /                                                  \
											(Red Spot [Yellow Car])------------(No color Spot [No Car])
             
             */
            cars3[(int)Color.NoColor] = Color.NoColor;
            cars3[(int)Color.Green] = Color.Red;
            cars3[(int)Color.Yellow] = Color.Green;
            cars3[(int)Color.Red] = Color.Yellow;

            foreach (var (cars, parkingSpots) in new(Color[], Dictionary<Color, List<Color>>)[]
            {
                (cars1, parkingSpots1), // Green car to NoColor spot, Red car to Red spot, Yellow car to Yellow spot, Green car to Green spot
                (cars2, parkingSpots1), // In place
                (cars3, parkingSpots1), // Red car to NoColor spot, Green car to Green spot, Yellow car to Yellow spot, Red car to Red spot
            })
            {
                Console.WriteLine(GetParkingInstructions(cars, parkingSpots));
            }
        }

        private enum Color
        {
            NoColor = 0,
            Green = 1,
            Yellow = 2,
            Red = 3,
        }

        private string GetParkingInstructions(Color[] cars, Dictionary<Color, List<Color>> parkingSpots)
        {
            var init = GetString(cars);
            var target = GetString(Enumerable.Range(0, cars.Length).Select(i => (Color)i));
            if (init == target) return "In order";
            
            var seen = new Dictionary<string, (string, string)>();
            seen.Add(init, (string.Empty, string.Empty));

            var queue = new Queue<string>(new[] { init });
            while (queue.Count > 0)
            {
                var str = queue.Dequeue();
                var arr = GetArray(str);

                var emptySpot = Enumerable.Range(0, cars.Length).First(i => arr[i] == Color.NoColor);

                foreach (int spot in parkingSpots[(Color)emptySpot])
                {
                    Color car = arr[spot];
                    (arr[emptySpot], arr[spot]) = (arr[spot], arr[emptySpot]);
                    var spotStr = GetString(arr);

                    if (!seen.ContainsKey(spotStr))
                    {
                        seen.Add(spotStr, (str, $"{car} car to {(Color)emptySpot} spot"));
                        queue.Enqueue(spotStr);
                    }

                    if (spotStr == target) return Retrace();

                    (arr[emptySpot], arr[spot]) = (arr[spot], arr[emptySpot]);
                }
            }

            return "No way";

            string Retrace()
            {
                var stack = new Stack<string>();
                var str = target;
                while (!string.IsNullOrEmpty(str))
                {
                    var (nextStr, step) = seen[str];
                    if (!string.IsNullOrEmpty(step)) stack.Push(step);
                    str = nextStr;
                }

                return string.Join(", ", stack);
            }


            string GetString(IEnumerable<Color> arr)
            {
                return string.Join(",", arr.Select(x => (int)x));
            }

            Color[] GetArray(string str)
            {
                return str.Split(',').Select(x => (Color)int.Parse(x)).ToArray();
            }
        }
    }
}
