using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        var lines = File.ReadAllLines("INPUT.TXT");
        int N = int.Parse(lines[0]); // Number of villages
        string[] inputs = lines[1].Split();
        int d = int.Parse(inputs[0]); // Start village
        int v = int.Parse(inputs[1]); // Destination village
        int R = int.Parse(lines[2]); // Number of routes

        // Create a graph to hold the bus routes
        var graph = new Dictionary<int, List<(int, int, int)>>();
        for (int i = 3; i < 3 + R; i++)
        {
            var route = lines[i].Split();
            int start = int.Parse(route[0]);
            int departure = int.Parse(route[1]);
            int destination = int.Parse(route[2]);
            int arrival = int.Parse(route[3]);

            if (!graph.ContainsKey(start))
            {
                graph[start] = new List<(int, int, int)>();
            }
            graph[start].Add((departure, destination, arrival));
        }

        // Dijkstra's algorithm to find the shortest time
        var earliestArrival = new Dictionary<int, int>();
        var pq = new PriorityQueue<(int village, int time), int>();

        earliestArrival[d] = 0;
        pq.Enqueue((d, 0), 0);

        while (pq.Count > 0)
        {
            var (currentVillage, currentTime) = pq.Dequeue();

            if (currentVillage == v)
            {
                File.WriteAllText("OUTPUT.TXT", currentTime.ToString());
                return;
            }

            if (!graph.ContainsKey(currentVillage)) continue;

            foreach (var (departure, destination, arrival) in graph[currentVillage])
            {
                if (departure >= currentTime) // Can take this bus
                {
                    if (!earliestArrival.ContainsKey(destination) || arrival < earliestArrival[destination])
                    {
                        earliestArrival[destination] = arrival;
                        pq.Enqueue((destination, arrival), arrival);
                    }
                }
            }
        }

        File.WriteAllText("OUTPUT.TXT", "-1");
    }
}