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
    public class Day9ServiceTests : DayServiceTestsBase
    {
        private readonly Day9Service service;

        public Day9ServiceTests()
        {
            service = new Day9Service(inputServiceMock.Object);
        }

        [Theory]
        [InlineData("2333133121414131402", 1928)]
        public async Task Part1Test(string input, long expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsString(2024, 9)).ReturnsAsync(input);
            Assert.Equal(expectedOut, await service.Part1());
        }

        [Theory]
        [InlineData("2333133121414131402", 2858)]
        public async Task Part2Test(string input, long expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsString(2024, 9)).ReturnsAsync(input);
            Assert.Equal(expectedOut, await service.Part2());
        }
    }
}