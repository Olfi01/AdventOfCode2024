using Xunit;
using AdventOfCode2024.Services._2019;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Services._2019.Tests
{
    public class IntcodeComputerServiceTests
    {
        private IntcodeComputerService service = new();

        [Theory]
        [InlineData("1,9,10,3,2,3,11,0,99,30,40,50", 0, 3500)]
        [InlineData("1,0,0,0,99", 0, 2)]
        [InlineData("2,3,0,3,99", 3, 6)]
        [InlineData("2,4,4,5,99,0", 5, 9801)]
        [InlineData("1,1,1,4,99,5,6,0,99", 0, 30)]
        [InlineData("1002,4,3,4,33", 4, 99)]
        public async Task ExecuteProgramTest(string program, int readAt, int result)
        {
            Assert.Equal(result, await service.ExecuteProgram(program.Split(',').Select(int.Parse), readAt));
        }

        [Theory]
        [InlineData(8, "3,9,8,9,10,9,4,9,99,-1,8", 1)]
        [InlineData(9, "3,9,8,9,10,9,4,9,99,-1,8", 0)]
        [InlineData(4, "3,9,7,9,10,9,4,9,99,-1,8", 1)]
        [InlineData(8, "3,9,7,9,10,9,4,9,99,-1,8", 0)]
        [InlineData(8, "3,3,1108,-1,8,3,4,3,99", 1)]
        [InlineData(7, "3,3,1108,-1,8,3,4,3,99", 0)]
        [InlineData(7, "3,3,1107,-1,8,3,4,3,99", 1)]
        [InlineData(12, "3,3,1107,-1,8,3,4,3,99", 0)]
        [InlineData(1, "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 1)]
        [InlineData(0, "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 0)]
        [InlineData(1, "3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 1)]
        [InlineData(0, "3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 0)]
        [InlineData(7, "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 999)]
        [InlineData(8, "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 1000)]
        [InlineData(12, "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 1001)]
        public async Task ExecuteProgramWithInputTest(int input, string program, int expectedOutput)
        {
            await service.InputStream.Writer.WriteAsync(input);
            await service.ExecuteProgram(program.Split(',').Select(int.Parse));
            Assert.Equal(expectedOutput, await service.OutputStream.Reader.ReadAsync());
        }
    }
}