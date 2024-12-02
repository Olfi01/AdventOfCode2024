using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public class Day2Service(IInputService inputService) : SingleDayService(inputService, 2024, 2)
    {
        public async Task<int> Part1()
        {
            var reports = (await InputService.GetInputAsList(year, day)).Select(s => s.Split(' ').Select(int.Parse));
            return reports.Count(IsSafe);
        }

        public static bool IsSafe(IEnumerable<int> report)
        {
            return IsAscendingSafely(report) || IsDescendingSafely(report);
        }

        private static bool IsAscendingSafely(IEnumerable<int> report)
        {
            int last = report.First();
            for (int i = 1; i < report.Count(); i++)
            {
                int current = report.ElementAt(i);
                int delta = current - last;
                if ((delta < 1) || (delta > 3)) return false;
                last = current;
            }
            return true;
        }

        private static bool IsDescendingSafely(IEnumerable<int> report)
        {
            int last = report.First();
            for (int i = 1; i < report.Count(); i++)
            {
                int current = report.ElementAt(i);
                int delta = last - current;
                if ((delta < 1) || (delta > 3)) return false;
                last = current;
            }
            return true;
        }

        public async Task<int> Part2()
        {
            var reports = (await InputService.GetInputAsList(year, day)).Select(s => s.Split(' ').Select(int.Parse));
            return reports.Count(IsSafeIncludingDampener);
        }

        public static bool IsSafeIncludingDampener(IEnumerable<int> report)
        {
            if(IsSafe(report))
            {
                return true;
            }
            for (int i = 0; i < report.Count(); i++)
            {
                var dampenedReport = report.Take(i).Concat(report.Skip(i + 1));
                if (IsSafe(dampenedReport))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
