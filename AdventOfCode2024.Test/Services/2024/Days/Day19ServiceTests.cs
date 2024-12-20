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
    public class Day19ServiceTests : DayServiceTestsBase
    {
        private readonly Day19Service service;

        public Day19ServiceTests()
        {
            service = new Day19Service(inputServiceMock.Object);
        }

        [Theory]
        [InlineData("r, wr, b, g, bwu, rb, gb, br\n\nbrwrr\nbggr\ngbbr\nrrbgbr\nubwu\nbwurrg\nbrgr\nbbrgwb", 6)]
        public async Task Part1Test(string input, long expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 19, "\n\n")).ReturnsAsync(input.Split("\n\n"));
            Assert.Equal(expectedOut, await service.Part1());
        }

        [Theory]
        [InlineData("r, wr, b, g, bwu, rb, gb, br\n\nbrwrr\nbggr\ngbbr\nrrbgbr\nubwu\nbwurrg\nbrgr\nbbrgwb", 16)]
        public async Task Part2Test(string input, long expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 19, "\n\n")).ReturnsAsync(input.Split("\n\n"));
            Assert.Equal(expectedOut, await service.Part2());
        }
    }
}