using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public class Day20Service(IInputService inputService) : SingleDayService(inputService, 2024, 20)
    {
        private static readonly (int dx, int dy)[] directions = [(1, 0), (0, 1), (-1, 0), (0, -1)];

        public async Task<long> Part1(int savesAtLeast = 100)
        {
            var input = await InputService.GetInputAsList(year, day);
            var enumerator = input.GetEnumerator();
            HashSet<(int x, int y)> nodes = [];
            (int x, int y) start = (-1, -1);
            (int x, int y) goal = (-1, -1);
            (int width, int height) = (input.First().Length, input.Count());
            for (int y = 0; enumerator.MoveNext(); y++)
            {
                var line = enumerator.Current;
                for (int x = 0; x < line.Length; x++)
                {
                    switch (line[x])
                    {
                        case 'S':
                            start = (x, y);
                            nodes.Add(start);
                            break;
                        case 'E':
                            goal = (x, y);
                            nodes.Add(goal);
                            break;
                        case '.':
                            nodes.Add((x, y));
                            break;
                    }
                }
            }

            var minWithoutCheating = Dijkstra(nodes, start, goal);

            List<(int x, int y)> path = [];
            var current = start;
            while (current != goal)
            {
                path.Add(current);
                foreach (var (dx, dy) in directions)
                {
                    var neighbor = (current.x + dx, current.y + dy);
                    if (path.Contains(neighbor)) continue;
                    if (nodes.Contains(neighbor))
                    {
                        current = neighbor;
                        break;
                    }
                }
            }
            path.Add(current);

            int maxToCount = minWithoutCheating - savesAtLeast;
            long cheatingPaths = 0;
            for (int i = 0; i < path.Count - (savesAtLeast + 1); i++)
            {
                var (x, y) = path[i];
                cheatingPaths += path.Skip(i + savesAtLeast + 1).Count(p => Math.Abs(p.x - x) + Math.Abs(p.y - y) == 2);
            }

            return cheatingPaths;
        }

        public async Task<long> Part2(int savesAtLeast = 100)
        {
            var input = await InputService.GetInputAsList(year, day);
            var enumerator = input.GetEnumerator();
            HashSet<(int x, int y)> nodes = [];
            (int x, int y) start = (-1, -1);
            (int x, int y) goal = (-1, -1);
            (int width, int height) = (input.First().Length, input.Count());
            for (int y = 0; enumerator.MoveNext(); y++)
            {
                var line = enumerator.Current;
                for (int x = 0; x < line.Length; x++)
                {
                    switch (line[x])
                    {
                        case 'S':
                            start = (x, y);
                            nodes.Add(start);
                            break;
                        case 'E':
                            goal = (x, y);
                            nodes.Add(goal);
                            break;
                        case '.':
                            nodes.Add((x, y));
                            break;
                    }
                }
            }

            var minWithoutCheating = Dijkstra(nodes, start, goal);

            List<(int x, int y)> path = [];
            var current = start;
            while (current != goal)
            {
                path.Add(current);
                foreach (var (dx, dy) in directions)
                {
                    var neighbor = (current.x + dx, current.y + dy);
                    if (path.Contains(neighbor)) continue;
                    if (nodes.Contains(neighbor))
                    {
                        current = neighbor;
                        break;
                    }
                }
            }
            path.Add(current);

            int maxToCount = minWithoutCheating - savesAtLeast;
            long cheatingPaths = 0;
            for (int i = 0; i < path.Count - (savesAtLeast + 1); i++)
            {
                var (x, y) = path[i];
                for (int skip = 2; skip <= 20; skip++)
                {
                    cheatingPaths += path.Skip(i + savesAtLeast + skip).Count(p => Math.Abs(p.x - x) + Math.Abs(p.y - y) == skip);
                }
            }

            return cheatingPaths;
        }

        private static int Dijkstra(HashSet<(int x, int y)> nodes, (int x, int y) start, (int x, int y) goal)
        {
            HashSet<(int x, int y)> unvisited = [..nodes];
            Dictionary<(int x, int y), int> distance = [];

            foreach (var node in unvisited)
            {
                if (node == start) distance[node] = 0;
                else distance[node] = int.MaxValue;
            }

            while (unvisited.Count > 0)
            {
                var current = unvisited.MinBy(n => distance[n]);
                var currentDistance = distance[current];
                if (current == goal) return currentDistance;
                foreach (var (dx, dy) in directions)
                {
                    var neighbor = (x: current.x + dx, y: current.y + dy);
                    if (!unvisited.Contains(neighbor)) continue;
                    var newDistance = currentDistance + 1;
                    if (distance[neighbor] > newDistance) distance[neighbor] = newDistance;
                }
                unvisited.Remove(current);
            }

            throw new Exception();
        }
    }
}
