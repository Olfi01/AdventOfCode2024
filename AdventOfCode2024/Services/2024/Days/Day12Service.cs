using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public class Day12Service (IInputService inputService) : SingleDayService(inputService, 2024, 12)
    {
        private static readonly (int dx, int dy)[] directions = [(1, 0), (-1, 0), (0, 1), (0, -1)];

        public async Task<long> Part1()
        {
            var input = await InputService.GetInputAsList(year, day);
            char[,] grid = new char[input.First().Length, input.Count()];
            var enumerator = input.GetEnumerator();

            List<List<(int x, int y, char plant)>> regions = [];

            for (int y = 0; y < grid.GetLength(1); y++)
            {
                enumerator.MoveNext();
                string row = enumerator.Current;
                for (int x = 0;  x < grid.GetLength(1); x++)
                {
                    grid[x, y] = row[x];
                }
            }

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (regions.Any(r => r.Any(p => p.x == x && p.y == y))) continue;
                    regions.Add(CollectRegion(grid, x, y, []));
                }
            }

            return regions.Sum(r => CalculatePrice(r, grid));
        }

        private static List<(int x, int y, char plant)> CollectRegion(char[,] grid, int x, int y, List<(int x, int y, char plant)> regions)
        {
            List<(int x, int y, char plant)> result = [..regions, (x, y, grid[x, y])];
            foreach (var (dx, dy) in directions)
            {
                if (x + dx >= 0 && x + dx < grid.GetLength(0) && y + dy >= 0 && y + dy < grid.GetLength(1) && !result.Any(r => r.x == x + dx && r.y == y + dy) && grid[x + dx, y + dy] == grid[x, y])
                {
                    result = CollectRegion(grid, x + dx, y + dy, result);
                }
            }
            return result;
        }

        private static long CalculatePrice(List<(int x, int y, char plant)> region, char[,] grid)
        {
            long area = region.Count;
            long perimeter = 0;
            foreach (var (x, y, plant) in region)
            {
                foreach ((int dx, int dy) in directions)
                {
                    if (x + dx < 0 || x + dx >= grid.GetLength(0) || y + dy < 0 || y + dy >= grid.GetLength(1) || grid[x + dx, y + dy] != plant) perimeter++;
                }
            }
            return area * perimeter;
        }

        public async Task<long> Part2()
        {
            var input = await InputService.GetInputAsList(year, day);
            char[,] grid = new char[input.First().Length, input.Count()];
            var enumerator = input.GetEnumerator();

            List<List<(int x, int y, char plant)>> regions = [];

            for (int y = 0; y < grid.GetLength(1); y++)
            {
                enumerator.MoveNext();
                string row = enumerator.Current;
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    grid[x, y] = row[x];
                }
            }

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (regions.Any(r => r.Any(p => p.x == x && p.y == y))) continue;
                    regions.Add(CollectRegion(grid, x, y, []));
                }
            }

            return regions.Sum(r => CalculateDiscountedPrice(r, grid));
        }

        private long CalculateDiscountedPrice(List<(int x, int y, char plant)> region, char[,] grid)
        {
            long area = region.Count;
            List<(int x, int y)> hasUpperFence = [];
            List<(int x, int y)> hasLowerFence = [];
            List<(int x, int y)> hasRightFence = [];
            List<(int x, int y)> hasLeftFence = [];

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (!region.Any(p => p.x == x && p.y == y)) continue;
                    char plant = grid[x, y];
                    if (y - 1 < 0 || grid[x, y - 1] != plant)
                    {
                        hasUpperFence.Add((x, y));
                    }
                    if (y + 1 >= grid.GetLength(1) || grid[x, y + 1] != plant)
                    {
                        hasLowerFence.Add((x, y));
                    }
                    if (x - 1 < 0 || grid[x - 1, y] != plant)
                    {
                        hasLeftFence.Add((x, y));
                    }
                    if (x + 1 >= grid.GetLength(0) || grid[x + 1, y] != plant)
                    {
                        hasRightFence.Add((x, y));
                    }
                }
            }

            long sides = 0;
            (int x, int y)? lastFence = null;
            foreach (var fence in hasUpperFence.OrderBy(p => p.y).ThenBy(p => p.x))
            {
                if (lastFence == null || lastFence.Value.x != fence.x - 1 || lastFence.Value.y != fence.y)
                {
                    sides++;
                }
                lastFence = fence;
            }

            lastFence = null;
            foreach (var fence in hasLowerFence.OrderBy(p => p.y).ThenBy(p => p.x))
            {
                if (lastFence == null || lastFence.Value.x != fence.x - 1 || lastFence.Value.y != fence.y)
                {
                    sides++;
                }
                lastFence = fence;
            }

            lastFence = null;
            foreach (var fence in hasLeftFence.OrderBy(p => p.x).ThenBy(p => p.y))
            {
                if (lastFence == null || lastFence.Value.y != fence.y - 1 || lastFence.Value.x != fence.x)
                {
                    sides++;
                }
                lastFence = fence;
            }

            lastFence = null;
            foreach (var fence in hasRightFence.OrderBy(p => p.x).ThenBy(p => p.y))
            {
                if (lastFence == null || lastFence.Value.y != fence.y - 1 || lastFence.Value.x != fence.x)
                {
                    sides++;
                }
                lastFence = fence;
            }
            return area * sides;
        }
    }
}
