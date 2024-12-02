using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2019.Days
{
    public class Day3Service(IInputService inputService) : SingleDayService(inputService, 2019, 3)
    {
        private Dictionary<(int x, int y), (int wireA, int wireB)> grid = [];
        private List<(int x, int y)> intersections = [];

        public async Task<int> Part1()
        {
            var wires = await InputService.GetInputAsList(year, day);
            IEnumerable<string> wireA = wires.First().Split(',');
            IEnumerable<string> wireB = wires.Last().Split(',');
            MapWire(wireA, false);
            MapWire(wireB, true);
            return intersections.Select(xy => Math.Abs(xy.x) + Math.Abs(xy.y)).Min();
        }

        public async Task<int> Part2()
        {
            var wires = await InputService.GetInputAsList(year, day);
            IEnumerable<string> wireA = wires.First().Split(',');
            IEnumerable<string> wireB = wires.Last().Split(',');
            MapWire(wireA, false);
            MapWire(wireB, true);
            return intersections.Select(xy => grid[xy].wireA + grid[xy].wireB).Min();
        }

        private void MapWire(IEnumerable<string> wire, bool isWireB)
        {
            (int x, int y, int steps) = (0, 0, 0);
            foreach (var instruction in wire)
            {
                (int dx, int dy) = instruction[0] switch
                {
                    'R' => (1, 0),
                    'U' => (0, 1),
                    'L' => (-1, 0),
                    'D' => (0, -1),
                    _ => throw new Exception(),
                };
                int distance = int.Parse(instruction[1..]);
                for (int i = 0; i < distance; i++)
                {
                    steps++;
                    (x, y) = (x + dx, y + dy);
                    if (isWireB)
                    {
                        if (!grid.ContainsKey((x, y))) grid[(x, y)] = (-1, steps);
                        else if (grid[(x, y)].wireB < 0) grid[(x, y)] = (grid[(x, y)].wireA, steps);
                        if (grid[(x, y)].wireA > 0) intersections.Add((x, y));
                    }
                    else
                    {
                        if (!grid.ContainsKey((x, y))) grid[(x, y)] = (steps, -1);
                    }
                }
            }
        }
    }
}
