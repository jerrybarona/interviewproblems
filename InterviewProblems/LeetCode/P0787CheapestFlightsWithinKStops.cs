using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace InterviewProblems.LeetCode
{
    internal class P0787CheapestFlightsWithinKStops : ITestable
    {
        public void RunTest()
        {
            foreach (var (n, flights, src, dst, k) in new (int, int[][], int, int, int)[]
            {
                //(3, new[]{ new[] { 0, 1, 100 },new[] { 1, 2, 100 }, new [] {0,2,500 } }, 0, 2, 1),
                (9, new [] { new[] { 0, 1, 1 }, new[] { 1, 2, 1 }, new[] { 2, 3, 1}, new[] { 3, 7, 1}, new[] { 0, 4, 3 }, new[] { 4, 5, 3 }, new[] { 5, 7, 3 }, new[] { 0, 6, 5 }, new[] { 6, 7, 100 }, new [] { 7, 8, 1 } }, 0, 8, 3)
            })
            {
                Console.WriteLine(FindCheapestPrice(n, flights, src, dst, k));
            }
        }

        private int FindCheapestPrice(int n, int[][] flights, int src, int dst, int k)
        {
            var len = 9;
            var graph = flights.Aggregate(new List<(int node, int price)>[len], (arr, flight) =>
            {
                var from = flight[0];
                var to = flight[1];
                var price = flight[2];

                if (arr[from] == null) arr[from] = new List<(int node, int price)>();
                arr[from].Add((to, price));

                return arr;
            });

            //var minPrice = Enumerable.Repeat(int.MaxValue, len).ToArray();
            var minStops = Enumerable.Repeat(int.MaxValue, len).ToArray();

            //minPrice[src] = 0;
            //minStops[src] = 0;
            var pq = new PriorityQueue<(int node, int price, int stops), int>();
            pq.Enqueue((src, 0, 0), 0);

            while (pq.Count > 0)
            {
                var (node, price, stops) = pq.Dequeue();

                if (stops > k + 1 || stops > minStops[node]) continue;
                
                minStops[node] = stops;

                if (node == dst) return price;

                if (graph[node] == null) continue;
                foreach (var (neighbor, neighborPrice) in graph[node])
                {
                    var currPrice = price + neighborPrice;
                    pq.Enqueue((neighbor, currPrice, stops + 1), currPrice);
                }
            }

            return -1;
        }
    }
}
