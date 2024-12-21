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
    public class Day21ServiceTests : DayServiceTestsBase
    {
        private readonly Day21Service service;

        public Day21ServiceTests()
        {
            service = new(inputServiceMock.Object);
        }

        [Theory]
        [InlineData("029A\n980A\n179A\n456A\n379A", 126384)]
        public async Task Part1Test(string input, long expectedOut)
        {
            inputServiceMock.Setup(s => s.GetInputAsList(2024, 21, '\n')).ReturnsAsync(input.Split('\n'));
            Assert.Equal(expectedOut, await service.Part1());
        }

        [Theory]
        [InlineData("029A", 68)]
        [InlineData("980A", 60)]
        [InlineData("179A", 68)]
        [InlineData("456A", 64)]
        [InlineData("379A", 64)]
        public void ShouldGiveBestNumericSequenceLength(string sequence, int length)
        {
            var best = Day21Service.ShortestSequenceForNumeric(sequence);
            Assert.Equal(length, best);
        }

        [Theory]
        [InlineData("<A", 8)]
        public void ShouldGiveBestDirectionalSequenceLength(string sequence, int length)
        {
            var best = Day21Service.ShortestSequencesForDirectional([sequence]);
            Assert.Equal(length, best.First().Length);
        }

        [Theory]
        [InlineData("029A", 2, 68)]
        [InlineData("980A", 2, 60)]
        [InlineData("179A", 2, 68)]
        [InlineData("456A", 2, 64)]
        [InlineData("379A", 2, 64)]
        public void Part2Test(string input, int abstractionLayers, long expectedOut)
        {
            var best = Day21Service.GetBestLength(input, abstractionLayers);
            Assert.Equal(expectedOut, best);
        }
    }
}