using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public partial class Day3Service(IInputService inputService) : SingleDayService(inputService, 2024, 3)
    {
        [GeneratedRegex(@"mul\((?<a>\d{1,3}),(?<b>\d{1,3})\)")]
        private static partial Regex MulStatement();

        public async Task<int> Part1()
        {
            var input = await InputService.GetInputAsString(year, day);
            Regex re = MulStatement();
            int sum = 0;
            foreach (Match match in re.Matches(input))
            {
                int a = int.Parse(match.Groups["a"].Value);
                int b = int.Parse(match.Groups["b"].Value);
                sum += a * b;
            }
            return sum;
        }

        public async Task<int> Part2()
        {
            var input = await InputService.GetInputAsString(year, day);
            int sum = 0;
            IEnumerable<string> split = [input.Split("don't()")[0]];
            split = split.Concat(input.Split("don't()").Select(s => s.Contains("do()") ? s[s.IndexOf("do()")..] : ""));
            foreach (var str in split)
            {
                Regex re = MulStatement();
                foreach (Match match in re.Matches(str))
                {
                    int a = int.Parse(match.Groups["a"].Value);
                    int b = int.Parse(match.Groups["b"].Value);
                    sum += a * b;
                }
            }
            return sum;
        }
    }
}
