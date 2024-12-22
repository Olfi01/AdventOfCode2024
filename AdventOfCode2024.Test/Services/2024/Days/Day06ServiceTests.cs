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
    public class Day06ServiceTests : DayServiceTestsBase
    {
        private readonly Day06Service service;

        public Day06ServiceTests()
        {
            service = new Day06Service(inputServiceMock.Object);
        }

        [Theory]
        [InlineData("....#.....\n.........#\n..........\n..#.......\n.......#..\n..........\n.#..^.....\n........#.\n#.........\n......#...", 41)]
        public async Task Part1Test(string input, int expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 6, '\n')).ReturnsAsync(input.Split('\n'));
            Assert.Equal(expectedOut, await service.Part1());
        }

        [Theory]
        [InlineData("....#.....\n.........#\n..........\n..#.......\n.......#..\n..........\n.#..^.....\n........#.\n#.........\n......#...", 6)]
        public async Task Part2Test(string input, int expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 6, '\n')).ReturnsAsync(input.Split('\n'));
            Assert.Equal(expectedOut, await service.Part2());
        }
    }
}