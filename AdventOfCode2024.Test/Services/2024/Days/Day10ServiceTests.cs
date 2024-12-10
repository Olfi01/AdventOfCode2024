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
    public class Day10ServiceTests : DayServiceTestsBase
    {
        private readonly Day10Service service;

        public Day10ServiceTests()
        {
            service = new Day10Service(inputServiceMock.Object);
        }

        [Theory]
        [InlineData("0123\n1234\n8765\n9876", 1)]
        [InlineData("89010123\n78121874\n87430965\n96549874\n45678903\n32019012\n01329801\n10456732", 36)]
        public async Task Part1Test(string input, long expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 10, '\n')).ReturnsAsync(input.Split('\n'));
            Assert.Equal(expectedOut, await service.Part1());
        }

        [Theory]
        [InlineData("89010123\n78121874\n87430965\n96549874\n45678903\n32019012\n01329801\n10456732", 81)]
        public async Task Part2Test(string input, long expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 10, '\n')).ReturnsAsync(input.Split('\n'));
            Assert.Equal(expectedOut, await service.Part2());
        }
    }
}