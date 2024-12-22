using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2019.Days
{
    public class Day09Service(IInputService inputService) : SingleDayService(inputService, 2019, 9)
    {
        private IntcodeComputerService computer = new();

        public async Task<long> Part1()
        {
            var program = await InputService.GetInputAsLongList(year, day, ',');
            await computer.InputStream.Writer.WriteAsync(1);
            await computer.ExecuteProgram(program);
            if (computer.OutputStream.Reader.Count > 1) throw new Exception();
            return await computer.OutputStream.Reader.ReadAsync();
        }

        public async Task<long> Part2()
        {
            var program = await InputService.GetInputAsLongList(year, day, ',');
            await computer.InputStream.Writer.WriteAsync(2);
            await computer.ExecuteProgram(program);
            if (computer.OutputStream.Reader.Count > 1) throw new Exception();
            return await computer.OutputStream.Reader.ReadAsync();
        }
    }
}
