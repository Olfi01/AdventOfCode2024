using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public class Day18Service(IInputService inputService) : SingleDayService(inputService, 2024, 18)
    {
        private static readonly (int dx, int dy)[] directions = [(1, 0), (0, 1), (-1, 0), (0, -1)];

        public async Task<long> Part1(int dim = 71, int bytesDrop = 1024)
        {
            var input = await InputService.GetInputAsList(year, day);
            Dictionary<(int x, int y), int> bitDroppedAt = [];
            var enumerator = input.GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext())
            {
                var split = enumerator.Current.Split(',');
                var bit = (int.Parse(split[0]), int.Parse(split[1]));
                bitDroppedAt[bit] = i++;
                if (i >= bytesDrop) break;
            }

            return Dijkstra(bitDroppedAt, dim);
        }

        private static long Dijkstra(Dictionary<(int x, int y), int> bitDroppedAt, int dim)
        {
            HashSet<(int x, int y)> unvisited = [];
            Dictionary<(int x, int y), int> distance = [];
            for (int x = 0; x < dim; x++)
            {
                for (int y = 0; y < dim; y++)
                {
                    unvisited.Add((x, y));
                    if (x == 0 && y == 0) distance[(x, y)] = 0;
                    else distance[(x, y)] = int.MaxValue;
                }
            }

            while (unvisited.Count > 0)
            {
                var current = unvisited.MinBy(n => distance[n]);
                var currentDistance = distance[current];
                if (current == (dim - 1, dim - 1)) return currentDistance;
                foreach (var (dx, dy) in directions)
                {
                    var neighbor = (x: current.x + dx, y: current.y + dy);
                    if (neighbor.x < 0 || neighbor.x >= dim || neighbor.y < 0 || neighbor.y >= dim || bitDroppedAt.ContainsKey(neighbor)) continue;
                    var newDistance = currentDistance + 1;
                    if (distance[neighbor] > newDistance) distance[neighbor] = newDistance;
                }
                unvisited.Remove(current);
            }

            throw new Exception();
        }

        public async Task<string> Part2(int dim = 71, int startSim = 1025)
        {
            var input = await InputService.GetInputAsList(year, day);
            Dictionary<(int x, int y), int> bitDroppedAt = [];
            var enumerator = input.GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext())
            {
                var split = enumerator.Current.Split(',');
                var bit = (int.Parse(split[0]), int.Parse(split[1]));
                bitDroppedAt[bit] = i++;
            }

            for (int j = bitDroppedAt.Count - 1; j >= startSim; j--)
            {
                if (DoesPathExist(bitDroppedAt, dim, j))
                {
                    var (x, y) = bitDroppedAt.First(kvp => kvp.Value == j).Key;
                    return $"{x},{y}";
                }
            }

            throw new Exception();
        }

        private static bool DoesPathExist(Dictionary<(int x, int y), int> bitDroppedAt, int dim, int bitsDropped)
        {
            HashSet<(int x, int y)> unvisited = [];
            Dictionary<(int x, int y), int?> distance = [];
            for (int x = 0; x < dim; x++)
            {
                for (int y = 0; y < dim; y++)
                {
                    if (!bitDroppedAt.TryGetValue((x, y), out int value) || value >= bitsDropped) unvisited.Add((x, y));
                    if (x == 0 && y == 0) distance[(x, y)] = 0;
                    else distance[(x, y)] = null;
                }
            }

            while (unvisited.Count > 0)
            {
                var current = unvisited.MinBy(n => distance[n] ?? int.MaxValue);
                var currentDistance = distance[current];
                if (currentDistance == null) return false;
                if (current == (dim - 1, dim - 1)) return true;
                foreach (var (dx, dy) in directions)
                {
                    var neighbor = (x: current.x + dx, y: current.y + dy);
                    if (neighbor.x < 0 || neighbor.x >= dim || neighbor.y < 0 || neighbor.y >= dim || (bitDroppedAt.TryGetValue(neighbor, out int value) && value < bitsDropped)) continue;
                    var newDistance = currentDistance + 1;
                    if (distance[neighbor] == null || distance[neighbor] > newDistance) distance[neighbor] = newDistance;
                }
                unvisited.Remove(current);
            }

            return false;
        }
    }
}
