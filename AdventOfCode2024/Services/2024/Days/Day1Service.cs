using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2024.Days
{
    public class Day1Service(IInputService inputService) : SingleDayService(inputService, 2024, 1)
    {
        public async Task<int> Part1()
        {
            var input = await InputService.GetInputAsList(year, day);
            List<int> leftList = [.. input.Select(x => int.Parse(x.Split("   ")[0])).Order()];
            List<int> rightList = [.. input.Select(x => int.Parse(x.Split("   ")[1])).Order()];
            int sum = 0;
            for (int i = 0; i < leftList.Count; i++)
            {
                sum += Math.Abs(leftList[i] - rightList[i]);
            }
            return sum;
        }

        public async Task<int> Part2()
        {
            var input = await InputService.GetInputAsList(year, day);
            List<int> leftList = [.. input.Select(x => int.Parse(x.Split("   ")[0])).Order()];
            List<int> rightList = [.. input.Select(x => int.Parse(x.Split("   ")[1])).Order()];
            return leftList.Sum(x => rightList.Count(y => y == x) * x);
        }
    }
}
