using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public class Day05Service(IInputService inputService) : SingleDayService(inputService, 2024, 5)
    {
        public async Task<int> Part1()
        {
            var inputs = await InputService.GetInputAsList(year, day, "\n\n");
            var rules = inputs.First().Split('\n').Select(l => l.Split('|').Select(int.Parse).ToArray());
            var manuals = inputs.Last().Split('\n').Where(l => !string.IsNullOrEmpty(l)).Select(l => l.Split(',').Select(int.Parse).ToList());

            int sum = 0;

            foreach (var manual in manuals)
            {
                foreach (var rule in rules.Where(r => manual.Contains(r[0]) && manual.Contains(r[1])))
                {
                    if (manual.IndexOf(rule[0]) > manual.IndexOf(rule[1]))
                    {
                        goto CONTINUE;
                    }
                }
                sum += manual[manual.Count / 2];
                CONTINUE:;
            }
            return sum;
        }

        public async Task<int> Part2()
        {
            var inputs = await InputService.GetInputAsList(year, day, "\n\n");
            var rules = inputs.First().Split('\n').Select(l => l.Split('|').Select(int.Parse).ToArray());
            var manuals = inputs.Last().Split('\n').Where(l => !string.IsNullOrEmpty(l)).Select(l => l.Split(',').Select(int.Parse).ToList());

            int sum = 0;

            foreach (var manual in manuals)
            {
                foreach (var rule in rules.Where(r => manual.Contains(r[0]) && manual.Contains(r[1])))
                {
                    if (manual.IndexOf(rule[0]) > manual.IndexOf(rule[1]))
                    {
                        goto FIX;
                    }
                }
                goto CONTINUE;
            FIX:;
                var comparer = new RuleComparer(rules);
                var sortedManual = manual.OrderBy(e => e, comparer);
                sum += sortedManual.ElementAt(manual.Count / 2);
                CONTINUE:;
            }
            return sum;
        }

        private class RuleComparer(IEnumerable<int[]> rules) : IComparer<int>
        {
            private readonly IEnumerable<int[]> rules = rules;

            public int Compare(int x, int y)
            {
                int[]? rule = rules.FirstOrDefault(r => r![0] == x && r[1] == y || r[1] == x && r[0] == y, null);
                if (rule == null) return 0;
                else if (rule[0] == x) return -1;
                else return 1;
            }
        }
    }
}
