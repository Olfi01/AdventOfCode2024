using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public class Day4Service(IInputService inputService) : SingleDayService(inputService, 2024, 4)
    {
        private static readonly (int, int)[] directions = [(0, 1), (0, -1), (1, 0), (1, 1), (1, -1), (-1, 0), (-1, 1), (-1, -1)];

        public async Task<int> Part1()
        {
            var grid = (await InputService.GetInputAsList(year, day)).ToArray();
            int sum = 0;
            for (int r = 0; r < grid.Length; r++)
            {
                string row = grid[r];
                for (int i = 0; i < row.Length; i++)
                {
                    if (row[i] == 'X')
                    {
                        foreach ((int dx, int dy) in directions)
                        {
                            if (i + 3 * dx < row.Length && i + 3 * dx >= 0 && r + 3 * dy < grid.Length && r + 3 * dy >= 0)
                            {
                                if (grid[r + dy][i + dx] == 'M' && grid[r + 2 * dy][i + 2 * dx] == 'A' && grid[r + 3 * dy][i + 3 * dx] == 'S') sum++;
                            }
                        }
                    }
                }
            }
            return sum;
        }

        public async Task<int> Part2()
        {
            var grid = (await InputService.GetInputAsList(year, day)).ToArray();
            int sum = 0;
            for (int r = 1; r + 1 < grid.Length; r++)
            {
                string row = grid[r];
                for (int i = 1; i + 1 < row.Length; i++)
                {
                    if (row[i] == 'A')
                    {
                        char[] xLetters = [grid[r - 1][i - 1], grid[r + 1][i - 1], grid[r - 1][i + 1], grid[r + 1][i + 1]];
                        if (xLetters.Count(c => c == 'M') == 2 && xLetters.Count(c => c == 'S') == 2 && xLetters[0] != xLetters[3]) sum++;
                    }
                }
            }
            return sum;
        }
    }
}
