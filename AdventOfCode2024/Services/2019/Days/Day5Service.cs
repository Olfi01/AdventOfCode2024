using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2019.Days
{
    public class Day5Service(IInputService inputService) : SingleDayService(inputService, 2019, 5)
    {
        private readonly IntcodeComputerService computer = new();

        public async Task<int> Part1()
        {
            var program = await InputService.GetInputAsIntList(year, day, ',');
            await computer.InputStream.Writer.WriteAsync(1);
            await computer.ExecuteProgram(program);
            while (computer.OutputStream.Reader.TryRead(out var output))
            {
                if (output != 0)
                {
                    if (computer.OutputStream.Reader.Count > 0) throw new Exception();
                    else return output;
                }
            }
            throw new Exception();
        }

        public async Task<int> Part2()
        {
            var program = await InputService.GetInputAsIntList(year, day, ',');
            await computer.InputStream.Writer.WriteAsync(5);
            await computer.ExecuteProgram(program);
            return await computer.OutputStream.Reader.ReadAsync();
        }
    }
}
