using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public class Day8Service(IInputService inputService) : SingleDayService(inputService, 2024, 8)
    {
        public async Task<long> Part1()
        {
            var input = await InputService.GetInputAsList(year, day);
            Dictionary<(int x, int y), char> antennas = [];
            HashSet<(int x, int y)> antinodes = [];
            var enumerator = input.GetEnumerator();
            for (int i = 0; i < input.Count(); i++)
            {
                enumerator.MoveNext();
                string line = enumerator.Current;
                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j] != '.') antennas[(j, i)] = line[j];
                }
            }
            foreach (var group in antennas.GroupBy(kvp => kvp.Value))
            {
                foreach (var pos1 in group.Select(g => g.Key))
                {
                    foreach (var pos2 in group.Select(g => g.Key).Except([pos1]))
                    {
                        (int dx, int dy) = (pos1.x - pos2.x, pos1.y - pos2.y);
                        antinodes.Add((pos1.x + dx, pos1.y + dy));
                        antinodes.Add((pos2.x - dx, pos2.y - dy));
                    }
                }
            }
            return antinodes.Count(a => a.x >= 0 && a.x < input.First().Length && a.y >= 0 && a.y < input.Count());
        }

        public async Task<long> Part2()
        {
            var input = await InputService.GetInputAsList(year, day);
            Dictionary<(int x, int y), char> antennas = [];
            HashSet<(int x, int y)> antinodes = [];
            var enumerator = input.GetEnumerator();
            for (int i = 0; i < input.Count(); i++)
            {
                enumerator.MoveNext();
                string line = enumerator.Current;
                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j] != '.') antennas[(j, i)] = line[j];
                }
            }
            int width = input.First().Length;
            int height = input.Count();
            foreach (var group in antennas.GroupBy(kvp => kvp.Value))
            {
                foreach (var pos1 in group.Select(g => g.Key))
                {
                    foreach (var pos2 in group.Select(g => g.Key).Except([pos1]))
                    {
                        (int dx, int dy) = (pos1.x - pos2.x, pos1.y - pos2.y);
                        (int x, int y) = pos1;
                        do
                        {
                            antinodes.Add((x, y));
                            (x, y) = (x + dx, y + dy);
                        } while (x >= 0 && x < width && y >= 0 && y < height);
                        (x, y) = pos2;
                        do
                        {
                            antinodes.Add((x, y));
                            (x, y) = (x - dx, y - dy);
                        } while (x >= 0 && x < width && y >= 0 && y < height);
                    }
                }
            }
            return antinodes.Count;
        }
    }
}
