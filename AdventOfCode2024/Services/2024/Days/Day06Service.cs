using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public class Day06Service(IInputService inputService) : SingleDayService(inputService, 2024, 6)
    {
        private static readonly Dictionary<(int, int), (int, int)> TurnRight = new()
        {
            { (0, -1), (1, 0) },
            { (1, 0), (0, 1) },
            { (0, 1), (-1, 0) },
            { (-1, 0), (0, -1) }
        };

        public async Task<int> Part1()
        {
            var input = await InputService.GetInputAsList(year, day);
            var grid = new bool[input.First().Length, input.Count()];
            var walked = new bool[input.First().Length, input.Count()];
            var enumerator = input.GetEnumerator();
            (int x, int y) pos = (0, 0);
            (int dx, int dy) direction = (0, -1);
            for (int y = 0; y < input.Count(); y++)
            {
                enumerator.MoveNext();
                var line = enumerator.Current;
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == '#') grid[x, y] = true;
                    else if (line[x] == '^')
                    {
                        pos = (x, y);
                        walked[x, y] = true;
                    }
                }
            }
            while (pos.x + direction.dx >= 0 && pos.x + direction.dx < grid.GetLength(0) && pos.y + direction.dy >= 0 && pos.y + direction.dy < grid.GetLength(1))
            {
                if (grid[pos.x + direction.dx, pos.y + direction.dy])
                {
                    direction = TurnRight[direction];
                }
                else
                {
                    pos = (pos.x + direction.dx, pos.y + direction.dy);
                    walked[pos.x, pos.y] = true;
                }
            }
            return walked.Cast<bool>().Count(b => b);
        }

        public async Task<int> Part2()
        {
            var input = await InputService.GetInputAsList(year, day);
            var grid = new bool[input.First().Length, input.Count()];
            var walked = new bool[input.First().Length, input.Count()];
            var enumerator = input.GetEnumerator();
            (int x, int y) pos = (0, 0);
            for (int y = 0; y < input.Count(); y++)
            {
                enumerator.MoveNext();
                var line = enumerator.Current;
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == '#') grid[x, y] = true;
                    else if (line[x] == '^')
                    {
                        pos = (x, y);
                        walked[x, y] = true;
                    }
                }
            }

            int sum = 0;
            for (int ox = 0; ox < grid.GetLength(0); ox++)
            {
                for (int oy = 0; oy < grid.GetLength(1); oy++)
                {
                    if (IsLoop(grid, pos, ox, oy)) sum++;
                }
            }
            return sum;
        }

        private static bool IsLoop(bool[,] oldGrid, (int x, int y) pos, int ox, int oy)
        {
            bool[,] grid = new bool[oldGrid.GetLength(0), oldGrid.GetLength(1)];
            Dictionary<(int, int), bool>[,] walked = new Dictionary<(int, int), bool>[grid.GetLength(0), grid.GetLength(1)];
            (int dx, int dy) direction = (0, -1);
            walked[pos.x, pos.y] = new() { { direction, true } };
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0;  j < grid.GetLength(1); j++)
                {
                    if (i == ox && j == oy) grid[i, j] = true;
                    else grid[i, j] = oldGrid[i, j];
                }
            }

            while (pos.x + direction.dx >= 0 && pos.x + direction.dx < grid.GetLength(0) && pos.y + direction.dy >= 0 && pos.y + direction.dy < grid.GetLength(1))
            {
                if (grid[pos.x + direction.dx, pos.y + direction.dy])
                {
                    direction = TurnRight[direction];
                }
                else
                {
                    pos = (pos.x + direction.dx, pos.y + direction.dy);
                    if (walked[pos.x, pos.y] == null) walked[pos.x, pos.y] = [];
                    else if (walked[pos.x, pos.y].ContainsKey(direction))
                    {
                        return true;
                    }
                    walked[pos.x, pos.y][direction] = true;
                }
            }

            return false;
        }
    }
}
