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
    public class Day20ServiceTests : DayServiceTestsBase
    {
        private readonly Day20Service service;

        public Day20ServiceTests()
        {
            service = new(inputServiceMock.Object);
        }

        [Theory]
        [InlineData("###############\n#...#...#.....#\n#.#.#.#.#.###.#\n#S#...#.#.#...#\n#######.#.#.###\n#######.#.#...#\n#######.#.###.#\n###..E#...#...#\n###.#######.###\n#...###...#...#\n#.#####.#.###.#\n#.#...#.#.#...#\n#.#.#.#.#.#.###\n#...#...#...###\n###############", 1)]
        public async Task Part1Test(string input, long expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 20, '\n')).ReturnsAsync(input.Split('\n'));
            Assert.Equal(expectedOut, await service.Part1(50));
        }

        [Theory]
        [InlineData("###############\n#...#...#.....#\n#.#.#.#.#.###.#\n#S#...#.#.#...#\n#######.#.#.###\n#######.#.#...#\n#######.#.###.#\n###..E#...#...#\n###.#######.###\n#...###...#...#\n#.#####.#.###.#\n#.#...#.#.#...#\n#.#.#.#.#.#.###\n#...#...#...###\n###############", 3)]
        public async Task Part2Test(string input, long expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 20, '\n')).ReturnsAsync(input.Split('\n'));
            Assert.Equal(expectedOut, await service.Part2(75));
        }
    }
}