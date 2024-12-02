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
        public void ExecuteProgramTest(string program, int readAt, int result)
        {
            Assert.Equal(result, service.ExecuteProgram(program.Split(',').Select(int.Parse), readAt));
        }
    }
}