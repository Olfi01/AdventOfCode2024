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
    public class Day11ServiceTests : DayServiceTestsBase
    {
        private readonly Day11Service service;

        public Day11ServiceTests()
        {
            service = new Day11Service(inputServiceMock.Object);
        }

        [Theory]
        [InlineData("125 17", 55312)]
        public async Task Part1Test(string input, long expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsLongList(2024, 11, ' ')).ReturnsAsync(input.Split(' ').Select(long.Parse));
            Assert.Equal(expectedOut, await service.Part1());
        }
    }
}