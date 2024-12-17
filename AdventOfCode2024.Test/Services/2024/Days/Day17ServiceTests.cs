using Xunit;
using AdventOfCode2024.Services._2024.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2024.Test.Services;
using Moq;

namespace AdventOfCode2024.Services._2024.Days.Tests
{
    public class Day17ServiceTests : DayServiceTestsBase
    {
        private readonly Day17Service service;

        public Day17ServiceTests()
        {
            service = new(inputServiceMock.Object);
        }

        [Theory]
        [InlineData("Register A: 729\nRegister B: 0\nRegister C: 0\n\nProgram: 0,1,5,4,3,0", "4,6,3,5,6,3,5,2,1,0")]
        public async Task Part1Test(string input, string expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsString(2024, 17)).ReturnsAsync(input);
            Assert.Equal(expectedOut, await service.Part1());
        }
    }
}