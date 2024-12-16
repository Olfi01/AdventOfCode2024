using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public class Day16Service(IInputService inputService) : SingleDayService(inputService, 2024, 16)
    {
        private static readonly (int dx, int dy)[] directions = [(1, 0), (0, 1), (-1, 0), (0, -1)];
        public async Task<long> Part1()
        {
            var lines = (await InputService.GetInputAsList(year, day)).ToArray();
            List<(int x, int y, int dir)> unvisited = [];
            (int x, int y, int dir) start = (-1, -1, 0);
            (int x, int y) goal = (-1, -1);
            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                for (int x = 0; x < line.Length; x++)
                {
                    char c = line[x];
                    if (c == 'S')
                    {
                        start = (x, y, 0);
                        foreach (var dir in Enumerable.Range(0, directions.Length))
                        {
                            unvisited.Add((x, y, dir));
                        }
                    }
                    else
                    {
                        if (c == 'E') goal = (x, y);
                        if (c != '#')
                        {
                            foreach (var dir in Enumerable.Range(0, directions.Length))
                            {
                                unvisited.Add((x, y, dir));
                            }
                        }
                    }
                }
            }
            return ModifiedDijkstra(unvisited, start, goal);
        }

        private static long ModifiedDijkstra(List<(int x, int y, int dir)> unvisited, (int x, int y, int dir) start, (int x, int y) goal)
        {
            Dictionary<(int x, int y, int dir), long> distances = [];
            distances.Add(start, 0);
            while (unvisited.Count > 0)
            {
                var currentNode = unvisited.MinBy(n => distances.TryGetValue(n, out long value) ? value : long.MaxValue);
                var currentScore = distances[currentNode];
                if (currentNode.x == goal.x && currentNode.y == goal.y) return currentScore;
                foreach (var (node, score) in GetNeighbors(currentNode, unvisited, currentScore))
                {
                    if (!distances.TryGetValue(node, out long value) || value > score)
                    {
                        distances[node] = score;
                    }
                }
                unvisited.Remove(currentNode);
            }
            throw new Exception();
        }

        private static IEnumerable<((int x, int y, int dir) node, long score)> GetNeighbors((int x, int y, int dir) currentNode, IEnumerable<(int x, int y, int dir)> unvisited, long score)
        {
            var (dx, dy) = directions[currentNode.dir];
            var ahead = (currentNode.x + dx, currentNode.y + dy, currentNode.dir);
            if (unvisited.Contains(ahead))
            {
                yield return (ahead, score + 1);
            }
            var left = currentNode.dir - 1;
            if (left < 0) left += directions.Length;
            var nodeLeft = (currentNode.x, currentNode.y, left);
            if (unvisited.Contains(nodeLeft))
            {
                yield return (nodeLeft, score + 1000);
            }
            var right = currentNode.dir + 1;
            if (right >= directions.Length) right -= directions.Length;
            var nodeRight = (currentNode.x, currentNode.y, right);
            if (unvisited.Contains(nodeRight))
            {
                yield return (nodeRight, score + 1000);
            }
        }

        public async Task<long> Part2()
        {
            var lines = (await InputService.GetInputAsList(year, day)).ToArray();
            HashSet<(int x, int y, int dir)> nodes = [];
            (int x, int y, int dir) start = (-1, -1, 0);
            (int x, int y) goal = (-1, -1);
            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                for (int x = 0; x < line.Length; x++)
                {
                    char c = line[x];
                    if (c == 'S')
                    {
                        start = (x, y, 0);
                        foreach (var dir in Enumerable.Range(0, directions.Length))
                        {
                            nodes.Add((x, y, dir));
                        }
                    }
                    else
                    {
                        if (c == 'E') goal = (x, y);
                        if (c != '#')
                        {
                            foreach (var dir in Enumerable.Range(0, directions.Length))
                            {
                                nodes.Add((x, y, dir));
                            }
                        }
                    }
                }
            }
            Dictionary<(int x, int y, int dir), List<(int x, int y, int dir)>> cameFrom = [];
            ModifiedAStar(nodes, start, goal, cameFrom);
            HashSet<(int x, int y)> visited = [goal];
            HashSet<(int x, int y, int dir)> pathHeads = Enumerable.Range(0, directions.Length).Select(dir => (goal.x, goal.y, dir)).ToHashSet();
            HashSet<(int x, int y, int dir)> newPathHeads = [];
            while (pathHeads.Count > 0)
            {
                foreach (var head in pathHeads)
                {
                    if (cameFrom.TryGetValue(head, out var previous))
                    {
                        foreach (var p in previous)
                        {
                            newPathHeads.Add(p);
                            visited.Add((p.x, p.y));
                        }
                    }
                }
                pathHeads = newPathHeads;
                newPathHeads = [];
            }
            return visited.Count;
        }

        private static long ModifiedAStar(HashSet<(int x, int y, int dir)> allNodes, (int x, int y, int dir) start, (int x, int y) goal, Dictionary<(int x, int y, int dir), List<(int x, int y, int dir)>> cameFrom)
        {
            HashSet<(int x, int y, int dir)> openSet = [.. allNodes];
            Dictionary<(int x, int y, int dir), long> gScore = [];
            gScore[start] = 0;
            Dictionary<(int x, int y, int dir), long> fScore = [];
            fScore[start] = HeuristicScore(start, goal);

            while (openSet.Count > 0)
            {
                var current = openSet.MinBy(n => fScore.GetValueOrDefault(n, long.MaxValue));
                var currentScore = gScore[current];
                if (current.x == goal.x && current.y == goal.y) return currentScore;
                openSet.Remove(current);
                foreach (var (neighbor, score) in GetNeighbors(current, allNodes, currentScore))
                {
                    if (!gScore.TryGetValue(neighbor, out long gscore) || score <= gscore)
                    {
                        if (score == gscore)
                        {
                            cameFrom[neighbor].Add(current);
                        }
                        else
                        {
                            cameFrom[neighbor] = [current];
                            gScore[neighbor] = score;
                            fScore[neighbor] = score + HeuristicScore(neighbor, goal);
                            openSet.Add(neighbor);
                        }
                    }
                }
            }
            throw new Exception();
        }

        private static long HeuristicScore((int x, int y, int dir) start, (int x, int y) goal)
        {
            return Math.Abs((start.x - goal.x) * (start.y - goal.y));
        }
    }
}
