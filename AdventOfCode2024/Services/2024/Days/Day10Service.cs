using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public class Day10Service(IInputService inputService) : SingleDayService(inputService, 2024, 10)
    {
        public async Task<long> Part1()
        {
            var input = await InputService.GetInputAsList(year, day);
            short[,] grid = new short[input.First().Length, input.Count()];
            var enumerator = input.GetEnumerator();
            List<(int x, int y)> trailheads = [];
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                enumerator.MoveNext();
                string line = enumerator.Current;
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    grid[x, y] = short.Parse(line[x] + "");
                    if (grid[x, y] == 0) trailheads.Add((x, y));
                }
            }

            return trailheads.Sum(t => ListReachablePeaks(t, grid, []).Count);
        }

        private static readonly (int dx, int dy)[] directions = [(1, 0), (-1, 0), (0, 1), (0, -1)];

        private static HashSet<(int x, int y)> ListReachablePeaks((int x, int y) trailhead, short[,] grid, HashSet<(int x, int y)> reachablePeaks)
        {
            foreach (var (dx, dy) in directions)
            {
                int newX = trailhead.x + dx;
                int newY = trailhead.y + dy;
                if (newX < 0 || newY < 0 || newX >= grid.GetLength(0) || newY >= grid.GetLength(1)
                    || grid[newX, newY] != grid[trailhead.x, trailhead.y] + 1) continue;
                if (grid[newX, newY] == 9) reachablePeaks.Add((newX, newY));
                else ListReachablePeaks((newX, newY), grid, reachablePeaks);
            }
            return reachablePeaks;
        }

        public async Task<long> Part2()
        {
            var input = await InputService.GetInputAsList(year, day);
            short[,] grid = new short[input.First().Length, input.Count()];
            var enumerator = input.GetEnumerator();
            List<(int x, int y)> trailheads = [];
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                enumerator.MoveNext();
                string line = enumerator.Current;
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    grid[x, y] = short.Parse(line[x] + "");
                    if (grid[x, y] == 0) trailheads.Add((x, y));
                }
            }

            return trailheads.Sum(t => GetTrailheadRating(t, grid));
        }

        private static int GetTrailheadRating((int x, int y) trailhead, short[,] grid)
        {
            int sum = 0;
            foreach (var (dx, dy) in directions)
            {
                int newX = trailhead.x + dx;
                int newY = trailhead.y + dy;
                if (newX < 0 || newY < 0 || newX >= grid.GetLength(0) || newY >= grid.GetLength(1)
                    || grid[newX, newY] != grid[trailhead.x, trailhead.y] + 1) continue;
                if (grid[newX, newY] == 9) sum += 1;
                else sum += GetTrailheadRating((newX, newY), grid);
            }
            return sum;
        }
    }
}
