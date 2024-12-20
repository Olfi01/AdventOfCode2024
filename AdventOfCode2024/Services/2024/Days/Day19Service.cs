using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public class Day19Service(IInputService inputService) : SingleDayService(inputService, 2024, 19)
    {
        public async Task<long> Part1()
        {
            var input = await InputService.GetInputAsList(year, day, "\n\n");
            var towels = input.First().Split(", ");
            var designs = input.Last().Split('\n');

            return designs.Count(d => !string.IsNullOrEmpty(d) && CanDisplayDesign(d, towels));
        }

        private static bool CanDisplayDesign(string design, string[] towels)
        {
            if (design == "") return true;
            foreach (var towel in towels.Where(design.StartsWith))
            {
                if (CanDisplayDesign(design[towel.Length..], towels)) return true;
            }
            return false;
        }

        public async Task<long> Part2()
        {
            var input = await InputService.GetInputAsList(year, day, "\n\n");
            var towels = input.First().Split(", ");
            var designs = input.Last().Split('\n');

            part2cache[""] = 1;
            foreach (var towel in towels.Where(t => t.Length == 1))
            {
                part2cache[towel] = 1;
            }

            return designs.Sum(d => string.IsNullOrEmpty(d) ? 0 : WaysToDisplayDesign(d, towels));
        }

        private static readonly Dictionary<string, long> part2cache = [];

        private static long WaysToDisplayDesign(string design, string[] towels)
        {
            if (design == "") return 1;
            else if (part2cache.TryGetValue(design, out long value)) return value;
            long sum = 0;
            foreach (var towel in towels.Where(design.StartsWith))
            {
                sum += WaysToDisplayDesign(design[towel.Length..], towels);
            }
            part2cache[design] = sum;
            return sum;
        }
    }
}
