using AdventOfCode2024.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2019.Days
{
    public class Day2Service(IInputService inputService) : SingleDayService(inputService, 2019, 2)
    {
        private readonly IntcodeComputerService computer = new();

        public async Task<int> Part1()
        {
            var program = (await InputService.GetInputAsIntList(year, day, ',')).Replace(1, 12).Replace(2, 2);
            return await computer.ExecuteProgram(program);
        }

        public async Task<int> Part2()
        {
            var originalProgram = await InputService.GetInputAsIntList(year, day, ',');
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    var program = originalProgram.Replace(1, noun).Replace(2, verb);
                    if (await computer.ExecuteProgram(program) == 19690720) return 100 * noun + verb;
                }
            }
            throw new Exception();
        }
    }
}
