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
    public class Day22ServiceTests : DayServiceTestsBase
    {
        private readonly Day22Service service;

        public Day22ServiceTests()
        {
            service = new(inputServiceMock.Object);
        }

        [Theory]
        [InlineData("1\n10\n100\n2024", 37327623)]
        public async Task Part1Test(string input, long expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsLongList(2024, 22, '\n')).ReturnsAsync(input.Split('\n').Select(long.Parse));
            Assert.Equal(expectedOut, await service.Part1());
        }

        [Theory]
        [InlineData("1\n2\n3\n2024", 23)]
        public async Task Part2Test(string input, long expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsLongList(2024, 22, '\n')).ReturnsAsync(input.Split('\n').Select(long.Parse));
            Assert.Equal(expectedOut, await service.Part2());
        }
    }
}